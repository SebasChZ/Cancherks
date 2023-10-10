using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CancherksWebApp.Pages
{
    public class IndexModel : PageModel
    {

        public IEnumerable<Installation> Installations { get; set; }
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db= db;
        }


        public void OnGet()
        {
            Installations = _db.Installation;

        }
    }
}