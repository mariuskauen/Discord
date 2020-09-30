using Discord.Api.Components.Auth;
using Discord.Api.Components.User;
using System.Threading.Tasks;


namespace Discord.Api.Components.Mediator
{
    public class MediatorService
    {
        private readonly UserService user;

        public MediatorService(UserService user)
        {
            this.user = user;
        }
        public async Task InitializeUser(string id, string username)
        {
            await user.InitializeUser(id, username);
        }
    }
}
