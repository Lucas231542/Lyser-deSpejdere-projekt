using LyserødeSpejdere.Data;
using LyserødeSpejdere.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Razor.LyserødeSpejdere.Pages
{
    public class AktivitetModel : PageModel
    {
        //Tilføjede af Sofyane Amrhar:
        //Kode starter Herfra
        //Henter aktiviteterne fra databasen i stedet for fast HTML
        private readonly IAktivitetRepo _aktivitetRepo;
        private readonly ITilmeldingRepo _tilmeldingRepo;
        public List<TilmeldingRepo> Tilmeldinger { get; set; } = new();

        //Listen, som siden viser (Kap 6) 
        public List<Aktivitet> Aktiviteter { get; set; } = new(); // --- Tilføjet af sofyane

        // Dependency injection via konstruktøren (kap 10) 
        public AktivitetModel(IAktivitetRepo aktivitetRepo, ITilmeldingRepo tilmeldingRepo) //---Tilføjet af Sofyane ---
        {
            _aktivitetRepo = aktivitetRepo;
            _tilmeldingRepo = tilmeldingRepo;
        }

        // Køres når siden åbnes: Hent alle aktiviteter 
        public void OnGet()     // --- Tilføjelse af sofyane ----
        {
            Aktiviteter = _aktivitetRepo.GetAllaktivitet();
        }
        // ----- Slut på Sofyane's kode -----


        //public IActionResult OnPostTilfoej(string aktivitetNavn)
        //{
        //    // Kontrollerer, om medlemmet er logget ind
        //    if (HttpContext.Session.GetString("ErLoggetInd") != "true")
        //    {
        //        return RedirectToPage("/LogIn");
        //    }

        //    // Henter medlemmets eksisterende aktiviteter
        //    var json = HttpContext.Session.GetString("MineAktiviteter");

        //    List<string> aktiviteter = string.IsNullOrEmpty(json)
        //        ? new List<string>()
        //        : JsonSerializer.Deserialize<List<string>>(json)
        //          ?? new List<string>();

        //    // Kontrollerer, at aktiviteten er gyldig
        //    var tilladteAktiviteter = new List<string>
        //    {
        //        "Ridning",
        //        "Mad over bål",
        //        "Fang dyr ved vandhullet"
        //    };

        //    if (!tilladteAktiviteter.Contains(aktivitetNavn))
        //    {
        //        return RedirectToPage("/Aktivitet");
        //    }

        //    // Tilføjer kun aktiviteten, hvis den ikke allerede findes
        //    if (!aktiviteter.Contains(aktivitetNavn))
        //    {
        //        aktiviteter.Add(aktivitetNavn);
        //    }

        //    // Gemmer listen i Session
        //    HttpContext.Session.SetString(
        //        "MineAktiviteter",
        //        JsonSerializer.Serialize(aktiviteter)
        //    );

        //    // Går videre til MineAktiviteter
        //    return RedirectToPage("/MineAktiviteter");
        //}

       
        public IActionResult OnPostTilfoej(int aktivitetId)
        {
            // tjekker om man er logget ind
            int? brugerId = HttpContext.Session.GetInt32("BrugerId");
            if (brugerId == null)
            {
                return RedirectToPage("/LogIn");
            }

            string? fejl = _tilmeldingRepo.Tilmeld(brugerId.Value, aktivitetId);

            if (fejl != null)
            {
                TempData["Fejl"] = fejl;
                return RedirectToPage();
            }

            return RedirectToPage("/MineAktiviteter");
        }
    }
}