using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.IdentityService.Components
{
    public class SelectApiResourceViewComponent : ViewComponent
    {
        private readonly ConfigDbContext _context;
        public SelectApiResourceViewComponent(ConfigDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _context.ApiResources.AsQueryable().ToListAsync();
            return View(list);
        }
    }
}
