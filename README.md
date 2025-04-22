Summary of Process:
1. Install SQL Server, Azure Tools, C# dependencies in VS Code.
2. Create new project/organize files/directories needed.
3. Configure SQL DB and write schema for the table to be used in webpage.
4. Develop backend function and connect DB to backend for accessing DB to query/populate data. Includes modeling of controllers, objects, etc. for handling DB interaction.
5. Test functionality.
6. Write frontend function to access backend/DB for user.
7. Test functionality with backend.
8. Develop Azure function to access holiday info webpage to display/save to DB (when unpopulated).
9. Test/debug functionality with backend/frontend.
10. Make optimization changes (only adding unique holiday values to database, ensure async functionality design)
11. Test/Deploy

To pull and deploy locally:
1. Clone repo using http or ssh by copying the address in git repo.
2. Verify node and dotnet dependencies are installed (npm and dotnet installs).
3. Connect a simple SQL server and run schema.sql file.
4. Use terminal command 'dotnet build' within the backend directory(API) to ensure backend is correctly configured.
5. Use 'dotnet run' within the backend directory(API) to start the backend server.
6. Make note of the port being used after running above command; changes to code may be needed in order to specify correct port.
7. Use 'dotnet build' within the function directory to configure the Azure function locally.
8. Use 'func start' to deploy the Azure function from terminal.
9. Make note of the port number given for the function and ensure it is the same within project files.
10. Finally use 'npm start' within the frontend directory to deploy the react webpage to access both the backend and function.
11. Again, make note of the port number given for the frontend application. This will ensure the pipeline is validly configured.
12. Visit the localhost address given in a browser and test the functionality of the application. Entering in a year and a country code should provide the holidays of that year.

TODOS: 
Make the webpage visually appealing. 
Deploy function, webpage, backend.

ISSUES: 
Hopefully all resolved.

DEBUGS:
Dates not being populated correctly in database; fixed with adjustments to datetime objects
Connection to database faulty; verified correct address by accessing in browser
CORS issues with frontend; changed parameters in program.cs
Others fixed with log statements, copilot and google :)

OTHER:
Good experience building and deploying simple webpage and C# practice. Object oriented approach seems similar to Java even in syntax. Querying and accessing database in this way is useful and good to experience. Hardest part was configuring each piece together--i.e. frontend to backend, frontend to function, backend to function--but was good to work through. 