using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductStore.DBContext;
using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductStoreDBContext _context;

        public HomeController(ProductStoreDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData([FromQuery] ProductQueryModel query)
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
