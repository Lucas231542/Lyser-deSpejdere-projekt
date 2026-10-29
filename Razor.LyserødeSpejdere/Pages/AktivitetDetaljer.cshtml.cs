using LyserødeSpejdere.Data;
using LyserødeSpejdere.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.LyserødeSpejdere.Pages
{
    // ----- Lavet af Sofyane Amrhar: Detaljer om en aktivitwt (Kap - 9 ) -----
    public class AktivitetDetaljerModel : PageModel
    {
        private readonly IAktivitetRepo _aktivitetRepo;
        private readonly IAktivitetTypeRepo _typeRepo;
        
        // Det, siden viser 
        public Aktivitet Aktivitet { get; set; } = new();
        public AktivitetType Type {get; set;} = new();
        
        // Dependency injection via konstruktøren (Kap - 10)
        public AktivitetDetaljerModel(IAktivitetRepo aktivitetRepo, IAktivitetTypeRepo typeRepo)
        {
            _aktivitetRepo = aktivitetRepo;
            _typeRepo = typeRepo;
        }
        
        // id kommer fra adressen, fx //AktivitetDetaljer/3 (route-parameter, kap - 9)
        public IActionResult OnGet(int id)
        {
            Aktivitet? aktivitet = _aktivitetRepo.GetAktivitetById(id);
            
            // Findes aktiviteten ikke, går vi tilbage til listen
            if (aktivitet == null)
            {
                return RedirectToPage("/Aktivitet");
            }
            
            Aktivitet = aktivitet;
            Type = _typeRepo.GetById(aktivitet.AktivitetTypeId) ?? new AktivitetType();
            return Page();
        } 
    }
}

