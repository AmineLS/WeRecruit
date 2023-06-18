# WeRecruit

WeRecruit is a simple recruitment portal built using .NET 7, C#, ASP.NET MVC, SQL Server, and Entity Framework.

## Routes

- **Public Home Page `/`:**
   - Accessible to the public.
   - Contains a form for submitting job applications.
   - The form is client-side validated using jQuery.

- **Admin Login `/login`:**
   - Public page for admin login.
   - Default credentials:
      - **Identifier**: admin
      - **Password**: pass
   - Admin credentials can be customized through the `appsettings.json` file.

- **Dashboard `/home`:**
   - Requires authentication.
   - Displays a table showing all received job applications.
   - Supports filtering based on multiple fields.

- **Resume Access `/resume/{directory}`:**
   - Requires authentication.
   - Retrieves a resume file from the filesystem.

- **Submission Deletion `/submissions/{submissionId}/delete`:**
   - Requires authentication.
   - Deletes a job application from the database along with the associated resume file.

## Getting Started

### Prerequisites

Make sure you have the following installed:

1. .NET 7
2. dotnet-ef (CLI tool)
3. Docker

### Setup Steps

1. Open your terminal and navigate to the 'Infrastructure' folder: `cd Infrastructure`.
2. Start the Docker containers (MSSQL and SMTP servers) by running the 'docker-compose.yml' file: `docker-compose up`.
3. Run the EF migration to create the database: `dotnet ef database update`.
4. Navigate to the project folder: `cd ../WeRecruit`.
5. In the `appsettings.json` file, update the `FileBucket` value with the path to the directory where you want to store uploaded files.
6. Start the application: `dotnet run`.
7. Access the application in your browser by visiting `localhost:5079`.
8. Access the SMTP server UI in your browser by visiting `localhost:1080`.

### Customization

You can customize various settings in the `appsettings.json` file, including:

- Change the MSSQL connection string to use a different server if needed.
- Update the path of the directory where uploaded files will be stored.
- Configure the SMTP server settings.
- Add or modify admin credentials.
- Modify the template of the email sent as a confirmation after submitting an application.

Feel free to explore and enhance the application based on your requirements.

Thank you for!

---