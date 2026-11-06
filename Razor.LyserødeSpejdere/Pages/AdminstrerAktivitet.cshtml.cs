using LyserødeSpejdere.Data;
using Microsoft.AspNetCore.Mvc;
using LyserødeSpejdere.Lib;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Razor.LyserødeSpejdere.Pages
{
    public class AdminsterAktivitetModel : PageModel
    {
    
        private readonly IAktivitetRepo _aktivitetRepo;
        private readonly IAktivitetTypeRepo _aktivitetTypeRepo;
        public List<Aktivitet> Aktiviteter { get; set; } = new();

        public AdminsterAktivitetModel(IAktivitetRepo aktivitetRepo, IAktivitetTypeRepo aktivitetTypeRepo)
        {

            _aktivitetRepo = aktivitetRepo;
            _aktivitetTypeRepo = aktivitetTypeRepo;
        }


        [BindProperty]
        [Required(ErrorMessage = "Aktivitetstype er påkrævet")]
        public int AktivitetTypeId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Starttidspunkt er påkrævet")]
        public DateTime StartTid { get; set; } = DateTime.Now;

        [BindProperty]
        [Required(ErrorMessage = "Sluttidspunkt er påkrævet")]
        public DateTime SlutTid { get; set; } = DateTime.Now.AddHours(1);

        [BindProperty]
        [Required(ErrorMessage = "Sted er påkrævet")]
        public string Sted { get; set; } = string.Empty;


        public List<AktivitetType> AktivitetTyper { get; set; } = new();


        // Hjælpemetode — bruges af alle handlers 
        private bool ErLeder()
        {
            return HttpContext.Session.GetString("ErLoggetInd") == "true"
                && HttpContext.Session.GetString("ErLeder") == "True";
        }

        // Ændr fra "void OnGet()" til "IActionResult OnGet()" 
        public IActionResult OnGet()
        {
            if (!ErLeder())
            {
                TempData["Fejl"] = "Kun ledere har adgang til at administrere aktiviteter.";
                return RedirectToPage("/Aktivitet");
            }
            

            AktivitetTypeId = 0;
            StartTid = DateTime.Now;
            SlutTid = DateTime.Now.AddHours(1);
            Sted = "";

            Aktiviteter = _aktivitetRepo.GetAllaktivitet();
            AktivitetTyper = _aktivitetTypeRepo.GetAll();

            return Page();
        }

        public IActionResult OnPostOpret()
        {
            
            if (!ModelState.IsValid)
            {
                AktivitetTyper = _aktivitetTypeRepo.GetAll();
                return Page();
            }


            _aktivitetRepo.CreateAktivitet(AktivitetTypeId, StartTid, SlutTid, Sted);

            return RedirectToPage("/AdminstrerAktivitet");
        }
        
        public IActionResult OnPostRediger()
        {
            if (!ModelState.IsValid)
            {
                AktivitetTyper = _aktivitetTypeRepo.GetAll();
                return Page();
            }
            _aktivitetRepo.Update(AktivitetTypeId, StartTid, SlutTid, Sted);

            return RedirectToPage("/AdminstrerAktivitet");
        }

        public IActionResult OnPostSlet(int aktivitetId)
        {
           _aktivitetRepo.Delete(aktivitetId);

            return RedirectToPage("/AdminstrerAktivitet");
        }
    }
}
