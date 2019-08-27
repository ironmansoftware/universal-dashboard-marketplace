using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Data;
using Marketplace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class PublishController : Controller
    {
        private readonly GitHubService _gitHubService;
        private readonly ApplicationDbContext _dbContext;

        public PublishController(GitHubService githubService, ApplicationDbContext applicationDbContext)
        {
            _gitHubService = githubService;
            _dbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(string gitHubRespository)
        {
            return Ok();
            
            if (!_gitHubService.ValidateGitHubUrl(gitHubRespository))
            {
                return BadRequest();
            }

            var accountName = _gitHubService.GetAccountName(gitHubRespository);
            var projectName = _gitHubService.GetProjectName(gitHubRespository);
            var readme = _gitHubService.GetReadme(gitHubRespository);

            var moduleInfo = new ModuleInfo();

            moduleInfo.Authors = accountName;
            moduleInfo.ProjectUrl = gitHubRespository;
            moduleInfo.Readme = readme;
            moduleInfo.Title = projectName;
            moduleInfo.Type = ItemType.GitHubRepo;
            //moduleInfo.SubmittedBy = User

            var existingModule = _dbContext.ModuleInfo.FirstOrDefault(m => m.Title == moduleInfo.Title && m.Authors == moduleInfo.Authors);
            var existingGitHubRepo = _dbContext.ModuleInfo.FirstOrDefault(m => m.ProjectUrl == gitHubRespository);

            if (existingModule != null || existingGitHubRepo != null)
            {
                return BadRequest();
            }

            _dbContext.Add(moduleInfo);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}