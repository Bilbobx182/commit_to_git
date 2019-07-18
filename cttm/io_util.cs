using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var task = get_projects_as_dict("bilbobx182");
            task.Wait();
            write__dict_to_file("testing.txt", task.Result);
        }


        public void write_dictionary_to_file(string path,Dictionary<string, string> data)
        {
            write__dict_to_file(path, data);
        }

        public Dictionary<string, string> get_user_projects(string user)
        {
            var task = get_projects_as_dict(user);
            task.Wait();

            return task.Result;
        }
        private void write__dict_to_file(string path, Dictionary<string, string> dict)
        {
            using (StreamWriter file = new StreamWriter(path))
                foreach (var item in dict) {
                    file.WriteLine("{0},{1}", item.Key, item.Value);
                }
        }
        private async Task<Dictionary<string, string>> get_projects_as_dict(string username)
        {
            var repos = await github_client.Repository.GetAllForUser(username);
            Dictionary<string, string> repo_info = new Dictionary<string, string>();
            
            foreach (var repo in repos)
            {
                string[] date_info = repo.UpdatedAt.ToString().Split(" ").Take(2).ToArray();
                repo_info.Add(repo.FullName,date_info[0] + "_" + date_info[1]);
            }
            return repo_info;
        }
    }
}