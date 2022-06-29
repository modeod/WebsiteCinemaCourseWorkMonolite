using WebsiteCinema.Models;

namespace WebsiteCinema.Services
{
    public interface ICharacherGeneratorApi
    {
        public Task<ActorApiModel> Generate();
    }
}
