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

        [HttpPost]
        public async Task<IActionResult> GetData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var data = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(m => m.Name.Contains(searchValue)
                                       || m.Category.Contains(searchValue)
                                       || m.Description.Contains(searchValue)
                                       || m.Height.ToString().Contains(searchValue)
                                       || m.Width.ToString().Contains(searchValue)
                                       || m.Price.ToString().Contains(searchValue)
                                       || m.Rating.ToString().Contains(searchValue));
            }

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                data = data.OrderBy($"{sortColumn} {sortColumnDirection}");
            }

            int recordsTotal = data.Count();
            var dataToReturn = await data.Skip(skip).Take(pageSize).ToListAsync();

            var jsonData = new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = dataToReturn
            };
            return Ok(jsonData);
        }
    }
}
