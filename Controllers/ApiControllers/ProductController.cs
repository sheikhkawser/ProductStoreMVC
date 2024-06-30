using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStore.DBContext;
using ProductStore.Models;

namespace ProductStore.Controllers.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductStoreDBContext _context;

        public ProductController(ProductStoreDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryModel query)
        {
            int pageSize = Convert.ToInt32(query.Length);
            int skip = Convert.ToInt32(query.Start);

            var data = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchValue))
            {
                data = data.Where(m => m.Name.Contains(query.SearchValue)
                                       || m.Category.Contains(query.SearchValue)
                                       || m.Description.Contains(query.SearchValue)
                                       || m.Height.ToString().Contains(query.SearchValue)
                                       || m.Width.ToString().Contains(query.SearchValue)
                                       || m.Price.ToString().Contains(query.SearchValue)
                                       || m.Rating.ToString().Contains(query.SearchValue));
            }

            if (!string.IsNullOrEmpty(query.SortColumn) && !string.IsNullOrEmpty(query.SortColumnDirection))
            {
                data = data.OrderBy($"{query.SortColumn} {query.SortColumnDirection}");
            }

            int recordsTotal = data.Count();
            var dataToReturn = await data.Skip(skip).Take(pageSize).ToListAsync();

            var jsonData = new
            {
                draw = query.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = dataToReturn
            };
            return Ok(jsonData);
        }
    }
}
