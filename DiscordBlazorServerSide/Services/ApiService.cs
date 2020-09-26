using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using DiscordBlazorServerSide.Models;

namespace DiscordBlazorServerSide.Services
{
    public class ApiService
    {
        HttpClient Http;
        Home home = new Home();
        FirstLoad first = new FirstLoad();
        string token = string.Empty;
        string userId;
        public async Task<FirstLoad> FirstLoad(string _token,string _userId)
        {
            token = _token;
            Http = new HttpClient();
            Http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var response = await Http.GetAsync("https://localhost:44336/api/load/firstload");
            var responseBody = await response.Content.ReadAsStringAsync();
            home = JsonSerializer.Deserialize<Home>(responseBody);

            //token = _token;
            //userId = _userId;
            //Http = new HttpClient();
            //List<Task> tasks = new List<Task>()
            //{
            //    GetFriendlist(),
            //    GetFullUser(),
            //    GetRequests(),
            //    GetServerlist()
            //};

            //await Task.WhenAll(tasks);
            first.home = home;
            return first;
        }

        private async Task GetFriendlist()
        {
            
            Http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
            var response = await Http.GetAsync("https://localhost:44336/api/relation/getmyfriends");
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            home.friends = JsonSerializer.Deserialize<List<FriendList>>(responseBody);
        }

        private async Task GetFullUser()
        {
            Http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var response = await Http.GetAsync("https://localhost:44336/api/user/getuser");
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            home.user = JsonSerializer.Deserialize<FullUser>(responseBody);
        }

        private async Task GetRequests()
        {
            Http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var response = await Http.GetAsync("https://localhost:44336/api/relation/getrequests");
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            List<FriendRequestVm> allReqs = JsonSerializer.Deserialize<List<FriendRequestVm>>(responseBody);
            foreach(FriendRequestVm vm in allReqs)
            {
                if(vm.SenderId == userId)
                {
                    //home.myrequests.Add(vm);
                }
                else
                {
                    //home.othersrequests.Add(vm);
                }
            }          
        }

        private async Task GetServerlist()
        {
            //Get friendlist
        }

        public async Task<ConversationList> GetConversationFromServer(string token)
        {
            Http.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
            var response = await Http.GetAsync("https://localhost:44314/api/chat/getconversation?friendid=");
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ConversationList>(responseBody);
        }
    }
}
