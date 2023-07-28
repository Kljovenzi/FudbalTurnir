using FudbalskiTurnir_FilipNikolic.Models.Repository;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Security.Cryptography;

namespace FudbalskiTurnir_FilipNikolic.Controllers
{
	public class ZapocniController : Controller
	{
        private readonly IFudbalskiTurnir fudbalskiTurnir;

        public ZapocniController(IFudbalskiTurnir fudbalskiTurnir)
        {
            this.fudbalskiTurnir = fudbalskiTurnir;
        }
        [HttpGet]
        public IActionResult OdaberiTurnir()
		{
            var turniri = fudbalskiTurnir.ReadAllTurnir().ToList();
            ViewBag.Turniri = turniri;
;			return View(turniri);
		}
        [HttpPost]
        public IActionResult OdaberiTurnir(int selectedOption)
        {
            return RedirectToAction("Rezultati" , "Zapocni" , new
            {
                SelectedOption = selectedOption
            });
        }
        [HttpGet]
        public IActionResult Rezultati(int selectedOption)
        {
            var turnir = fudbalskiTurnir.ReadTurnir(selectedOption);
            var timovi = fudbalskiTurnir.ReadAllTim().Where(tim => tim.TurnirId == turnir.Id).ToArray();
            //Ako nema bar tri tima u turniru, vratiti ga na dodaj timove u turnir sa returnUrl
            List<RezultatModel> rezultati = fudbalskiTurnir.NapraviRezultate(timovi, turnir.Id);
            RezultatiViewModel rezultatiVM = new RezultatiViewModel()
            {
                TimoviVM = timovi,
                RezultatiVM = rezultati
            };
            rezultatiVM.TimoviVM = fudbalskiTurnir.PoredjajTimovePoPoenima(timovi);
            return View(rezultatiVM);
        }
    }
}
