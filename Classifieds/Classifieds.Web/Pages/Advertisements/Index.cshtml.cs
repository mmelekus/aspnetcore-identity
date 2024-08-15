using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Classifieds.Data;
using Classifieds.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Classifieds.Web.Pages.Advertisements
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Advertisement> Advertisement { get;set; } = null!;

        public async Task OnGetAsync()
        {
            Advertisement = await _context.Advertisements
                .Include(a => a.Category).ToListAsync();
        }
    }
}
