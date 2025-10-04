# Disaster Alleviation Foundation (Prototype)

ASP.NET Core MVC prototype with Identity and SQLite to support:

- User registration and login
- Incident reporting
- Resource donation management
- Volunteer tasks and sign-ups

## Structure

- `src/DisasterAlleviation.sln` solution
- `src/DisasterAlleviation.Web` main web app
- `tests/DisasterAlleviation.Web.Tests` xUnit tests
- `azure-pipelines.yml` Azure DevOps pipeline

## Run locally

1. Install .NET 8 SDK.
2. `dotnet restore src/DisasterAlleviation.sln`
3. `dotnet build src/DisasterAlleviation.sln`
4. Create and apply EF migrations:
   - `dotnet tool install --global dotnet-ef`
   - `dotnet ef migrations add InitialCreate --project src/DisasterAlleviation.Web --startup-project src/DisasterAlleviation.Web`
   - `dotnet ef database update --project src/DisasterAlleviation.Web --startup-project src/DisasterAlleviation.Web`
5. `dotnet run --project src/DisasterAlleviation.Web`

Login and registration are under `/Identity/Account/*`.

## Azure DevOps

Import repo into Azure Repos and create a pipeline from `azure-pipelines.yml`.
