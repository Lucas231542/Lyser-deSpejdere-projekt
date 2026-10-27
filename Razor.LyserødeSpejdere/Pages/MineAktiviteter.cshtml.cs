
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Razor.LyserødeSpejdere.Pages;

public class MineAktiviteterModel : PageModel
{
    public List<string> Aktiviteter { get; set; } = new();

    public IActionResult OnGet()
    {
        // Kontrollerer, om brugeren er logget ind
        if (HttpContext.Session.GetString("ErLoggetInd") != "true")
        {
            return RedirectToPage("/LogIn");
        }

        // Henter brugerens tilmeldte aktiviteter
        var json = HttpContext.Session.GetString("MineAktiviteter");

        if (!string.IsNullOrEmpty(json))
        {
            Aktiviteter =
                JsonSerializer.Deserialize<List<string>>(json)
                ?? new List<string>();
        }

        return Page();
    }
}
