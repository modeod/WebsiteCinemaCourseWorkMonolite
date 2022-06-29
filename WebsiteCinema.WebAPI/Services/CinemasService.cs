using Microsoft.EntityFrameworkCore;
using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.Models;

namespace WebsiteCinema.WebAPI.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, IEntityBaseRepository<Cinema>
    {
        public CinemasService(AppDbContext context) : base(context) { }
    }
}
