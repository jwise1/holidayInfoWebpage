USE HolidayInfo;
DROP TABLE Holidays;

CREATE TABLE Holidays (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255),
	CountryCode NVARCHAR(10),
    Date DATETIME2
);