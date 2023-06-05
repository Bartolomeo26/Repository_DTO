using AplikacjaLataPrzestepne.Data;
using AplikacjaLataPrzestepne.Forms;
using ListLeapYears;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AplikacjaLataPrzestepne.Services;
using System.Configuration;
using AplikacjaLataPrzestepne.Interfaces;
using AplikacjaLataPrzestepne.ViewModels.RokPrzestepny;


namespace AplikacjaLataPrzestepne.Pages
{
    public class ListModel : PageModel
    {
        public IEnumerable<RokVM> LeapYearList;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        private readonly IRokPrzestepnyService _rokService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ListRokVM ListaLat { get; set;}
        public ListModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpContextAccessor contextAccessor, Wyszukiwania context, IRokPrzestepnyService rokService)
        {
            _logger = logger;
            Configuration = configuration;
            _contextAccessor = contextAccessor;
            _rokService = rokService;
        }
        public RokVM obiekt_doSzukania { get; set; } = new RokVM();
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<RokVM> LataPrzestepne { get; set; }
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                obiekt_doSzukania.user_id = user_id.Value;
                
            }
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            ListaLat = _rokService.GetYearsForList();
            switch (sortOrder)
            {
                case "Date":
                    ListaLat.Years = ListaLat.Years.OrderBy(s => s.Data).ToList();
                    break;
                case "date_desc":
                    ListaLat.Years = ListaLat.Years.OrderByDescending(s => s.Data).ToList();
                    break;
                default:
                    ListaLat.Years = ListaLat.Years.OrderByDescending(s => s.Data).ToList();
                    break;
            }
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            
            var lataQueryable = ListaLat.Years.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 4);
            LataPrzestepne = await PaginatedList<RokVM>.CreateAsync(ListaLat.Years, pageIndex ?? 1, pageSize);

        }
       
        public IActionResult OnPost(int id_User)
        {   

            _rokService.DeleteYears(id_User);
           
            return RedirectToAction("Async");
        }
       
    }
}