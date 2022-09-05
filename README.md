**UserManagement** 

# What is UserManagement? 
Initially it was a user tracking system but i changed my mind and created an asp.net quiz

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:
*  [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* .Net Core 5.0 or later
* EF Core 5.0 or later

### Installing
Follow these steps to get your development environment set up:
1. Clone the repository
2. At the root directory, restore required packages by running:
dotnet restore
3. Next, build the solution by running:
dotnet build
4. Next, launch the back end by running:
dotnet run
5. Launch http://localhost:1492/ in your browser to view the Web UI.
If you have Visual Studio after cloning Open solution with your IDE, AspnetRun.Web should be the start-up project. Directly run this project on Visual Studio with F5 or Ctrl+F5. 

##Usage
After cloning or downloading the sample you should be able to run it using an In Memory database immediately. The default configuration of Entity Framework Database is "InMemoryDatabase". If you wish to use the project with a persistent database, you will need to run its Entity Framework Core migrations before you will be able to run the app, and update the ConfigureDatabases method in Startup.cs (see below).
public void ConfigureServices(IServiceCollection services)
{
           
            // add real database dependecy
            services.AddDbContext<UsersDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));  
}
1. Ensure your connection strings in appsettings.json point to a local SQL Server instance.
2. Open a command prompt in the Web folder and execute the following commands:
dotnet restore
dotnet ef database update -c AspnetRunContext
Or you can direct call ef commands from Visual Studio Package Manager Console. Open Package Manager Console, set default project to AspnetRun.Infrastructure and run below command;
update-database
These commands will create A usermanagement database which include Users and Questions table. You can see from **AspnetRunContext.cs**.
3.I've included a scriptt named "Query" that you can run on MSSQL to insert data in the questions table
