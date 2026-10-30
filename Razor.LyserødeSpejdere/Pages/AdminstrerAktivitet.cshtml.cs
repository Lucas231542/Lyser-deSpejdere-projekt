using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.LyserødeSpejdere.Pages
{
    public class AdminsterAktivitetModel : PageModel
    {
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

            return Page();
        }

        public IActionResult OnPostOpret()
        {
            if (!ErLeder())
            {
                return RedirectToPage("/Aktivitet");
            }      
            
            return RedirectToPage("/AdministrerAktivitet");
        }
        
        public IActionResult OnPostRediger()
        {
            if (!ErLeder())
            {
                return RedirectToPage("/Aktivitet");
            }
            
            return RedirectToPage("/AdministrerAktivitet");
        }

        public IActionResult OnPostSlet()
        {
            if (!ErLeder())
            {
                return RedirectToPage("/Aktivitet");
            }

            return RedirectToPage("/AdministrerAktivitet");
        }
    }
}
