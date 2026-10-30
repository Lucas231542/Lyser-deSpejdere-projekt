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
        private readonly ITilmeldingRepo _tilmeldingRepo;
        
        // Det, siden viser 
        public Aktivitet Aktivitet { get; set; } = new();
        public List<Faerdighed> Krav { get; set; } = new();
        public int AntalTilmeldte { get; set; }
        public List<Bruger> Deltagere { get; set; } = new();
        public bool ErLoggetInd { get; set; }
        public AktivitetType Type {get; set;} = new();
        
        // Dependency injection via konstruktøren (Kap - 10)
        public AktivitetDetaljerModel(IAktivitetRepo aktivitetRepo, IAktivitetTypeRepo typeRepo, ITilmeldingRepo tilmeldingRepo)
        {
            _aktivitetRepo = aktivitetRepo;
            _typeRepo = typeRepo;
            _tilmeldingRepo = tilmeldingRepo;
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
            Krav = _typeRepo.HentKrav(aktivitet.AktivitetTypeId);
            AntalTilmeldte = _tilmeldingRepo.HentDeltagerAntal(id);     
            Deltagere = _tilmeldingRepo.HentDeltagere(id);
            ErLoggetInd = HttpContext.Session.GetString("ErLoggetInd") == "true";
            return Page();
        } 
    }
}

