//using gred.Data;
//using gred.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace gred.Repository
//{
//    public class AllIndiaPincodeRepository : IAllIndiaPincodeRepository
//    {
//        private readonly GredDbContext _context;

//        public AllIndiaPincodeRepository(GredDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<Allindiapincode>> SearchByNameAsync(string name)
//        {
//            return await _context.Allindiapincodes
//                                 .Where(x => x.Officename!.Contains(name))
//                                 .ToListAsync();
//        }
//    }
//}
