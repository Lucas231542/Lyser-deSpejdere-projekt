using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.LyserødeSpejdere.Pages
{ 
public class LogInModel : PageModel
{
    [BindProperty]
    public string Brugernavn { get; set; } = "";

    [BindProperty]
    public string Adgangskode { get; set; } = "";

    public string Fejl { get; set; } = "";

    public IActionResult OnPost()
    {
            // Eksempel på loginoplysninger til test
            if ((Brugernavn == "Anto3061" && Adgangskode == "bxh57eme") ||
         (Brugernavn == "Aleks1902" && Adgangskode == "Løven") ||
         (Brugernavn == "Lucas2005" && Adgangskode == "Lucas2004") ||
         (Brugernavn == "Khadija" && Adgangskode == "KhadijaAhmad.") ||
         (Brugernavn == "Ahmad" && Adgangskode == "kode456") ||
         (Brugernavn == "Sofyane" && Adgangskode == "kode456")) 
            {
                    HttpContext.Session.SetString("ErLoggetInd", "true");

                    return RedirectToPage("/Medlemmer");
                }

        Fejl = "Forkert brugernavn eller adgangskode.";
        return Page();
    }
}
}
