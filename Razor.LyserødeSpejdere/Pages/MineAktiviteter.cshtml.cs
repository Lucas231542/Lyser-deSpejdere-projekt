
using LyserødeSpejdere.Data;
using LyserødeSpejdere.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Razor.LyserødeSpejdere.Pages
{
    public class MineAktiviteterModel : PageModel
    {
    private readonly TilmeldingRepo _tilmeldingRepo = new();
    public List<Aktivitet> Aktiviteter { get; set; } = new();
    
    public IActionResult OnGet()
    {
            
            int? brugerId = HttpContext.Session.GetInt32("BrugerId");
          
            if (brugerId == null) // tjekker om brugerId eksistere/ om man er logget ind
            {
                return RedirectToPage("/LogIn"); // brugeren bliver sendt til login page
            }
            Aktiviteter = _tilmeldingRepo.HentMineAktiviteter(brugerId.Value); // Sætter brugerens brugerId ind i metoden(HentMineAktiviteter), så den ved hvilken brugers aktiviteter der skal sættes ind på siden

            return Page(); // returnerer siden MineAktiviteter der nu er opdateret med aktiviteterne der er koblet til brugerIdet
    }

    public IActionResult OnPostFrameld(int aktivitetId) 
    {
        int? brugerId = HttpContext.Session.GetInt32("BrugerId"); // sætter brugerId til at være det brugerId som bliver kaldt fra session 
            if (brugerId != null)
            {
                _tilmeldingRepo.Frameld(brugerId.Value, aktivitetId); // Sætter brugerens brugerId ind i metoden(Frameld), så den ved hvilken brugers aktivitet der skal fjernes fra siden
            }

        return RedirectToPage();
    }
}
}