using System;
using System.Linq;
using System.Threading;
using Marketplace.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Marketplace.Services
{
    internal class ModuleSyncJob : TimedHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ModuleSyncJob(IServiceScopeFactory serviceScopeFactory, ILogger<ModuleSyncJob> logger) : base(logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override TimerCallback TimerCallback => new TimerCallback(Callback);

        protected override TimeSpan Delay => TimeSpan.FromMinutes(60);

        private string StripTags(string tags)
        {
            if (!tags.Contains(" ")) return tags;

            return tags.Split(" ").Where(m => !m.StartsWith("PSCmdlet") && !m.StartsWith("PSModule") && !m.StartsWith("PowerShellGet") && !m.StartsWith("PSCommand") && !m.StartsWith("PSIncludes") && !m.StartsWith("PSFunction")).Aggregate((x,y) => x + " " + y);
        }

        private void Callback(object state)
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var nuGetPackageService = scope.ServiceProvider.GetRequiredService<NuGetPackageService>();
                var gitHubService = scope.ServiceProvider.GetRequiredService<GitHubService>();
                var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var packages = nuGetPackageService.GetUniversalDashboardPackages().Result;

                foreach (var package in packages)
                {
                    var existingPackage = applicationDbContext.ModuleInfo.FirstOrDefault(m => m.NuGetId == package.NuGetId);
                    if (existingPackage == null)
                    {
                        package.Readme = gitHubService.GetReadme(package.ProjectUrl);
                        applicationDbContext.ModuleInfo.Add(package);
                    }
                    else
                    {
                        var metadata = package;

                        existingPackage.NuGetId = metadata.NuGetId;
                        existingPackage.Version = metadata.Version;
                        existingPackage.ProjectUrl = metadata.ProjectUrl;
                        existingPackage.Title = metadata.Title;
                        existingPackage.Tags = StripTags(metadata.Tags);
                        existingPackage.Summary = metadata.Summary;
                        existingPackage.RequireLicenseAcceptance = metadata.RequireLicenseAcceptance;
                        existingPackage.Owners = metadata.Owners;
                        existingPackage.Published = metadata.Published;
                        existingPackage.ReportAbuseUrl = metadata.ReportAbuseUrl;
                        existingPackage.LicenseUrl = metadata.LicenseUrl;
                        existingPackage.IconUrl = metadata.IconUrl;
                        existingPackage.DownloadCount = metadata.DownloadCount;
                        existingPackage.Description = metadata.Description;
                        existingPackage.Authors = metadata.Authors;
                        existingPackage.Readme = gitHubService.GetReadme(package.ProjectUrl);

                        applicationDbContext.ModuleInfo.Update(existingPackage);
                    }
                }

                applicationDbContext.SaveChanges();
            }


        }
    }
}
