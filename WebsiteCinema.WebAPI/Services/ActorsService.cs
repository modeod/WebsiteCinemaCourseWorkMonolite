using Microsoft.EntityFrameworkCore;
using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.Models;

namespace WebsiteCinema.WebAPI.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IEntityBaseRepository<Actor>
    {
        public ActorsService(AppDbContext context) : base(context) { }
    }
}
