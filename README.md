# WeRecruit

A simple recruitment portal using .NET 7, C#, ASP.NET MVC, SQL Server and Entity Framework

## Routes

``/``

- Public.
- contains form for submitting application.
- the form is client-side validated using jQuery.

``/login``

- Public.
- Log In for the Admin.
- ``Identifier``: admin
- ``Password``: pass
- Credentials are customizable through ``appsettings.json``.

``/logout``

- Log Out

``/home``

- Requires authentication.
- Contains table that shows all received applications.
- The table can be filtered based on multiple fields using a text input.

``/resume/{directory}``

- Requires authentication.
- Retrieves a Resume file from the filesystem.

``/submissions/{submissionId}/delete``

- Requires authentication.
- Deletes a submission from the database along with the associated Resume file.

## Run locally

### Pre-requisites

1. .NET 7
2. dotnet-ef (CLI tool)
3. Docker

### Steps

1. `cd` into the 'Infrastructure' folder: `cd Infrastructure`.
2. Run the 'docker-compose.yml' file (contains MSSQL and SMTP servers): ``docker-compose up``.
3. Run the EF migration ``dotnet ef database update``.
4. `cd` into the project folder: ``cd ../WeRecruit``.
5. Change `FileBucket` value in `appsettings.json`, put the path of the directory where you want to put the uploaded
   files.
6. Run the application : `dotnet run`.
7. open `localhost:5079` on the browser to access the application.
8. open `localhost:1080` on the browser to access the SMTP server UI.

### Settings

through appsettings.json, you can:

- Change the MSSQL connection string, if you want to use another Server.
- Change the path of the directory where you want to put uploaded files.
- Change the SMTP Server.
- Add/Change admin credentials.
- Modify the template of the mail that gets sent as a confirmation after applying.