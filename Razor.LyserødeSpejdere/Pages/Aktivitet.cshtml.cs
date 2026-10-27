using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Razor.LyserødeSpejdere.Pages
{
    public class AktivitetModel : PageModel


    {
        public IActionResult OnPostTilfoej(string aktivitetNavn)
        {
            // Kontrollerer, om medlemmet er logget ind
            if (HttpContext.Session.GetString("ErLoggetInd") != "true")
            {
                return RedirectToPage("/LogIn");
            }

            // Henter medlemmets eksisterende aktiviteter
            var json = HttpContext.Session.GetString("MineAktiviteter");

            List<string> aktiviteter = string.IsNullOrEmpty(json)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(json)
                    ?? new List<string>();

            // Kontrollerer, at aktiviteten er gyldig
            var tilladteAktiviteter = new List<string>
        {
            "Ridning",
            "Mad over bål",
            "Fang dyr ved vandhullet"
        };

            if (!tilladteAktiviteter.Contains(aktivitetNavn))
            {
                return RedirectToPage("/Aktivitet");
            }

            // Tilføjer kun aktiviteten, hvis den ikke allerede findes
            if (!aktiviteter.Contains(aktivitetNavn))
            {
                aktiviteter.Add(aktivitetNavn);
            }

            // Gemmer listen i Session
            HttpContext.Session.SetString(
                "MineAktiviteter",
                JsonSerializer.Serialize(aktiviteter)
            );

            // Går videre til MineAktiviteter
            return RedirectToPage("/MineAktiviteter");
        }
    }
}
