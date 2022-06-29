using Microsoft.EntityFrameworkCore;
using WebsiteCinema.Data.Base;
using WebsiteCinema.Models;

namespace WebsiteCinema.Data.Services
{
    public class ProducerService : EntityBaseRepository<Producer>, IEntityBaseRepository<Producer>
    {
        public ProducerService(AppDbContext context) : base(context) { }
    }
}
