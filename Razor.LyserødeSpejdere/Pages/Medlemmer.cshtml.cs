using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.LyserødeSpejdere.Pages
{ 

    public class MedlemmerModel : PageModel
    {
        public List<string> Medlemmer { get; set; } = new()
    {
        "Anto3061",
        "Aleksandar",
        "Lucas2005",
        "Khadija",
        "Sofyane",
        "Ahmad",
    };

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("ErLoggetInd") != "true")
            {
                return RedirectToPage("/LogIn");
            }

            return Page();
        }

        public IActionResult OnPostLogUd()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/LogIn");
        }
    }

}
