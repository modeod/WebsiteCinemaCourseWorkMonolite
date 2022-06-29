using Microsoft.EntityFrameworkCore;
using WebsiteCinema.Data.Base;
using WebsiteCinema.Models;

namespace WebsiteCinema.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IEntityBaseRepository<Actor>
    {
        public ActorsService(AppDbContext context) : base(context) { }
    }
}
