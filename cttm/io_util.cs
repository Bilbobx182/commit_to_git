using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Octokit;

namespace cttm

{
    public class io_util
    {
        public static WebClient web_client;
        private static GitHubClient github_client;

        public io_util()
        {
            web_client = new WebClient();
            github_client = new GitHubClient(new ProductHeaderValue("commit_to_git"));

            var task = print_projects("bilbobx182");
            task.Wait();
            var result = task.Result;
        }
    
        private async Task<Dictionary<string, string>> print_projects(string username)
        {
            var repos = await github_client.Repository.GetAllForUser(username);
            Dictionary<string, string> repo_info = new Dictionary<string, string>();
            
            foreach (var repo in repos) {
                repo_info.Add(repo.FullName,repo.UpdatedAt.ToString());
            }
            return repo_info;
        }
    }
}