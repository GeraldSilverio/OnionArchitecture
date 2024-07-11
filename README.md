OnionApiTemplate is a project solution in Onion architecture created by Gerald Silverio (https://www.linkedin.com/in/geraldsilverio/)

with the following layers:

Domain
Application
Infrastructure (Persistence, Identity)
Presentation (WebApi)
This template comes with authentication and registration endpoints, as well as a sample CRUD for a to-do list.

This template includes the following technologies and patterns:

Entity Framework SQL SERVER Identity Automapper CQRS Mediator Generic repository pattern JWT security

Prerequisites --.NET 8 SDK or higher.

Installation

 To install this template, use the following command in the CMD console:  
 dotnet new -i OnionApiTemplate

 To create a new project using this template, use the following command:  
 dotnet new onionapi -n MyNewProject
Configuration

Configuring ConnectionStrings


You need to configure the appsettings.json for the connectionStrings. Make sure to set your server and database name:

"ConnectionStrings": {
"DefaultConnection": "Server=.;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
"IdentityConnection": "Server=.;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
Configuring JWT

You can configure the JWT options in appsettings.json according to your preferences:

"JWTSettings": {
"Key": "eae0fb2a538c89535cbb06a0522817d7cc6831d6ab8c1f7d741bba31918ede08",
"Issuer": "CodeIdentity",
"Audience": "NameThatYouPrefer",
"DurationInMinutes": 60 }
If you want to change the key of your JWT, make sure to configure it properly using the sha256 algorithm.

Run your Project

To run the project, first you need to execute the following commands in the NuGet Package Manager console:

Ensure that your startup project is set to the Presentation layer (WebApi) and run these commands:

1-Update-Database -context IdentityContext

2-Update-Database -context ApplicationContext

Support

For additional help, you can contact es.geraldsilverio@gmail.com
