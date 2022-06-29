using Newtonsoft.Json;
using WebsiteCinema.Models;

namespace WebsiteCinema.Services
{
    public class CharacherGeneratorApi : ICharacherGeneratorApi
    {
        public string _characterInfoApiURL = @"https://animechan.vercel.app/api/random";
        public string _characterPosterApiURL = @"https://api.waifu.pics/sfw/neko";

        public async Task<ActorApiModel> Generate()
        {
            ActorApiModel actor = new ActorApiModel();

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var characterInfo = httpClient.GetStringAsync(_characterInfoApiURL).Result;
                    actor = JsonConvert.DeserializeObject<ActorApiModel>(characterInfo);

                    var posterUrl = httpClient.GetStringAsync(_characterPosterApiURL).Result;
                    var actorWithUrl = JsonConvert.DeserializeObject<ActorApiModel>(posterUrl);

                    actor.url = actorWithUrl.url;

                }
                catch { }
            }

            return actor;
        }
    }
}
