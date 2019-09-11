using Marketplace.Data;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class NuGetPackageService
    {
        public async Task<IEnumerable<ModuleInfo>> GetUniversalDashboardPackages()
        {
            var packages = await GetDashboards();
            return packages.Concat(await GetControls());
        }

        public async Task<IEnumerable<ModuleInfo>> GetDashboards()
        {
            var packages = await GetUniversalDashboardPackages("ud-dashboard", ItemType.Dashboard);

            foreach(var package in packages)
            {
                if (package.Tags.Contains("ud-component"))
                {
                    package.Type = ItemType.Control;
                }
            }

            return packages;
        }

        public async Task<IEnumerable<ModuleInfo>> GetControls()
        {
            return await GetUniversalDashboardPackages("ud-control", ItemType.Control);
        }

        public async Task<IEnumerable<ModuleInfo>> GetUniversalDashboardPackages(string tag, ItemType itemType)
        {
            var logger = new Log();
            List<Lazy<INuGetResourceProvider>> providers = new List<Lazy<INuGetResourceProvider>>();
             providers.AddRange(Repository.Provider.GetCoreV3());  // Add v3 API support
            //providers.AddRange(Repository.Provider.GetCoreV2());  // Add v2 API support
            PackageSource packageSource = new PackageSource("https://www.powershellgallery.com/api/v2/");
            SourceRepository sourceRepository = new SourceRepository(packageSource, providers);
            PackageSearchResource searchResource = sourceRepository.GetResource<PackageSearchResource>();

            var filter = new SearchFilter(true);

            int skip = 0;
            IEnumerable<IPackageSearchMetadata> searchMetadata;
            List<ModuleInfo> packageInfos = new List<ModuleInfo>();
            do
            {
                searchMetadata = await searchResource.SearchAsync(tag, filter, skip, 10, logger, CancellationToken.None);

                if (!searchMetadata.Any()) break;

                foreach(var metadata in searchMetadata)
                {
                    var packageInfo = new ModuleInfo
                    {
                        NuGetId = metadata.Identity.Id,
                        Version = metadata.Identity.Version?.ToFullString(),
                        ProjectUrl = metadata.ProjectUrl?.ToString(),
                        Title = metadata.Title,
                        Tags = metadata.Tags,
                        Summary = metadata.Summary,
                        RequireLicenseAcceptance = metadata.RequireLicenseAcceptance,
                        Owners = metadata.Owners,
                        Published = metadata.Published,
                        ReportAbuseUrl = metadata.ReportAbuseUrl?.ToString(),
                        LicenseUrl = metadata.LicenseUrl?.ToString(),
                        IconUrl = metadata.IconUrl?.ToString(),
                        DownloadCount = metadata.DownloadCount,
                        Description = metadata.Description,
                        Authors = metadata.Authors,
                        Type = itemType
                    };

                    packageInfos.Add(packageInfo);
                }

                skip += 10;

            } while (searchMetadata.Any());
            
            return packageInfos.OrderByDescending(m => m.Published);
        }
    }

    public class Log : NuGet.Common.ILogger
    {
        public async Task LogAsync(NuGet.Common.LogLevel level, string data)
        {
            
        }

        public async Task LogAsync(ILogMessage message)
        {
            
        }

        public void LogDebug(string data)
        {
            
        }

        public void LogError(string data)
        {
            
        }

        public void LogInformation(string data)
        {
            
        }

        public void LogInformationSummary(string data)
        {
            
        }

        public void LogMinimal(string data)
        {
            
        }

        public void LogVerbose(string data)
        {

        }

        public void LogWarning(string data)
        {
            
        }

        void NuGet.Common.ILogger.Log(NuGet.Common.LogLevel level, string data)
        {
            
        }

        void NuGet.Common.ILogger.Log(ILogMessage message)
        {
            
        }
    }
}
