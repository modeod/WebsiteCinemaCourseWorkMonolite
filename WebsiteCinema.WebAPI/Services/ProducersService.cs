using Microsoft.EntityFrameworkCore;
using WebsiteCinema.WebAPI.Base;
using WebsiteCinema.WebAPI.Models;

namespace WebsiteCinema.WebAPI.Services
{
    public class ProducerService : EntityBaseRepository<Producer>, IEntityBaseRepository<Producer>
    {
        public ProducerService(AppDbContext context) : base(context) { }
    }
}
