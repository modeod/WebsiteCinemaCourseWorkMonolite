using Microsoft.EntityFrameworkCore;
using WebsiteCinema.Data.Base;
using WebsiteCinema.Models;

namespace WebsiteCinema.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, IEntityBaseRepository<Cinema>
    {
        public CinemasService(AppDbContext context) : base(context) { }
    }
}
