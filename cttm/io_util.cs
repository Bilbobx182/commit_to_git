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

        public io_util()
        {
            web_client = new WebClient();

            var task = print_projects("https://github.com/bilbobx182");
            task.Wait(); // Blocks current thread until GetFooAsync task completes
            // For pedagogical use only: in general, don't do this!
            var result = task.Result;
        }
    
        private async Task<IReadOnlyList<Repository>> print_projects(string url)
        {
            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
            var ii = await github.User.Get("bilbobx182");
            var repos = await github.Repository.GetAllForUser("bilbobx182");
            foreach (var repo in repos)
            {
                var lastpush = repo.UpdatedAt.ToString();
                var reponame = repo.FullName;
                Console.WriteLine(lastpush);
                Console.WriteLine(lastpush + " " + reponame);
            }
            return repos;
        }
    }
}