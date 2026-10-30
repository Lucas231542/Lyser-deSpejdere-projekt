using LyserødeSpejdere.Data;
using LyserødeSpejdere.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.LyserødeSpejdere.Pages
{
    public class LogInModel : PageModel
    {

        private readonly IBrugerRepo _brugerRepo;
        
        // Dependency injection: repo'et gives til siden via konstruktøren (kap 10)
        public LogInModel(IBrugerRepo brugerRepo)
        {
            _brugerRepo = brugerRepo;
        }

        [BindProperty] public string Brugernavn { get; set; } = "";

        [BindProperty] public string Adgangskode { get; set; } = "";

        [BindProperty] public bool HuskMig { get; set; } = false;
        public string Fejl { get; set; } = "";

        public void OnGet()
        {
            var gemtBrugernavn = Request.Cookies["BrugerNavn"];
            if (!string.IsNullOrEmpty(gemtBrugernavn))
            {
                Brugernavn = gemtBrugernavn;
            }
        }

        public IActionResult OnPost()
        {
            // Spørger databasen om brugernavn og adgangskode passer 
            
            Bruger? bruger = _brugerRepo.Login(Brugernavn, Adgangskode);

            if (bruger == null)
            {
                Fejl = "Forkert brugernavn eller adgangskode.";
                return Page();
            }
            
            HttpContext.Session.SetString("ErLoggetInd", "true");

            HttpContext.Session.SetInt32("BrugerId", bruger.BrugerId);

            HttpContext.Session.SetString("Brugernavn", bruger.Brugernavn);

            if (HuskMig)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30),
                    HttpOnly = true // Sikre at cookies ikke kan stjæles.
                };
                Response.Cookies.Append("BrugerNavn", Brugernavn, cookieOptions);
            }
            else
            {
                Response.Cookies.Delete("BrugerNavn");
            }

            return RedirectToPage("/Medlemmer");
        }
    }
}