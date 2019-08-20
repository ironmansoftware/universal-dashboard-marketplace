# Universal Dashboard Marketplace

[Universal Dashboard Marketplace](https://marketplace.universaldashboard.io)

## About

The Universal Dashboard Marketplace is a website for listing dashboards and controls built on [Universal Dashboard](https://poshud.com). The website has a background process that synchronizes with the PowerShell Gallery every hour. The background job is currently looking for items tagged `ud-dashboard` or `ud-control`. It then will read the metadata about the module, such as the Project URL. If the Project URL is a GitHub project, it will synchronize with GitHub to produce the description page for the module within the Marketplace. 

## Contributing

This website is built on ASP.NET Core using Entity Framework Core and Razor pages. You will need the 2.1 .NET Core SDK, Visual Studio and a local SQL Server instance to contribute. You should simply be able to open the `.\src\Marketplace.sln` file in Visual Studio. Next, create the database by running the following command in the `Package Manager Console`.

```
Update-Database 
```

Finally, you should be able to press `F5` to run the project. By default, it listens on port `5000` for HTTP and `5001` for HTTPS. 

## Additional Links

- [Universal Dashboard](https://poshud.com)
- [Universal Dashboard Purchasing](https://ironmansoftware.com/powershell-universal-dashboard)
- [Universal Dashboard GitHub](https://github.com/ironmansoftware/universal-dashboard)