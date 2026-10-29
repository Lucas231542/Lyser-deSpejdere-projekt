using LyserødeSpejdere.Data;
using LyserødeSpejdere.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.LyserødeSpejdere.Pages
{ 

    public class MedlemmerModel : PageModel
    {
        private readonly IBrugerRepo _brugerRepo;

        public MedlemmerModel(IBrugerRepo brugerRepo)
        {
            _brugerRepo = brugerRepo;
        }

        public List<Bruger> Medlemmer { get; set; } = new();

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("ErLoggetInd") != "true")
            {
                return RedirectToPage("/LogIn");
            }

            Medlemmer = _brugerRepo.GetAll();
            return Page();
        }

        public IActionResult OnPostLogUd()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/LogIn");
        }
    }

}
