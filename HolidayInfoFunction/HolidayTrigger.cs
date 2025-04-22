using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.VisualBasic;

public class HolidayTrigger
{
    // Client object to access external APIs
    private readonly HttpClient _client;

    // Constructor to initialize the HttpClient
    public HolidayTrigger(HttpClient client)
    {
        _client = client;
    }
    
    
    // Trigger function to access DB and external holiday data site
    [FunctionName("MyHttpTrigger")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Triggered MyHttpTrigger function.");

        string year = req.Query["year"];
        string countryCode = req.Query["countryCode"];

        if (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(countryCode))
        {
            log.LogError("Missing query parameters: year or countryCode.");
            return new BadRequestObjectResult("Please provide 'year' and 'countryCode'.");
        }

        try
        {
            // Fetch holidays from HolidayInfoAPI if available
            var apiUrl = $"https://localhost:7283/api/holiday/holidays?year={year}&countryCode={countryCode}";
            var response = await _client.GetAsync(apiUrl);

            log.LogInformation($"HolidayInfoAPI response: {response.StatusCode}.");
            if (response.IsSuccessStatusCode)
            {
                var holidays = await response.Content.ReadAsStringAsync();

                // Return if data exists in the database
                if (!string.IsNullOrWhiteSpace(holidays) && holidays != "[]")
                {
                    log.LogInformation("Holidays retrieved from database.");
                    return new OkObjectResult(holidays);
                }
                log.LogWarning("Database is empty. Fetching data from external API...");
            }

            // Otherwise, fetch holidays from Nager.Date API
            var externalApiUrl = $"https://date.nager.at/api/v3/publicholidays/{year}/{countryCode}";
            var externalResponse = await _client.GetStringAsync(externalApiUrl);

            var holidaysFromExternalApi = JsonConvert.DeserializeObject<List<Holiday>>(externalResponse);

            if (holidaysFromExternalApi == null || holidaysFromExternalApi.Count == 0)
            {
                log.LogError("No holidays found from external API.");
                return new NotFoundObjectResult("No holidays found.");
            }

            // Finally, save holidays to database via HolidayInfoAPI
            var saveUrl = $"https://localhost:7283/api/holiday/holidays";
            var saveResponse = await _client.PostAsJsonAsync(saveUrl, holidaysFromExternalApi);

            if (saveResponse.IsSuccessStatusCode)
            {
                log.LogInformation("Holidays successfully saved to database.");
                return new OkObjectResult(holidaysFromExternalApi);
            }
            log.LogError($"Failed to save holidays. Status code: {saveResponse.StatusCode}.");
            return new StatusCodeResult((int)saveResponse.StatusCode);
        }
        catch (HttpRequestException ex)
        {
            log.LogError($"Error during API calls: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
        catch (JsonException ex)
        {
            log.LogError($"Error deserializing JSON: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    // Holiday object class for holding holiday data (local)
    public class Holiday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public DateTime Date { get; set; }
    }
}