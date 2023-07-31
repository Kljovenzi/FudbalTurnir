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
            return View(turniri);
        }
        [HttpPost]
        public IActionResult OdaberiTurnir(int selectedOption)
        {
            if(fudbalskiTurnir.ReadAllTim().Where(tim => tim.TurnirId == selectedOption).Count() < 3 )
            {
                var turniri = fudbalskiTurnir.ReadAllTurnir().ToList();
                ViewBag.Turniri = turniri;
                ModelState.AddModelError("", "Potrebno je dodati minimum 3 tima kako bi zapoceo turnir.");
                return View(turniri);
            };
            var turnir = fudbalskiTurnir.ReadTurnir(selectedOption);
            var timovi = fudbalskiTurnir.ReadAllTim().Where(tim => tim.TurnirId == turnir.Id).ToArray();
            //Ako nema bar tri tima u turniru, vratiti ga na dodaj timove u turnir sa returnUrl
            List<RezultatModel> rezultati = fudbalskiTurnir.NapraviRezultate(timovi, turnir.Id);
            return RedirectToAction("Rezultati", "Zapocni", new
            {
                selectedOption = selectedOption
            });
        }
        [HttpGet]
        public IActionResult Rezultati(int selectedOption)
        {
            var turnir = fudbalskiTurnir.ReadTurnir(selectedOption);
            var rezultati = fudbalskiTurnir.ReadAlLRezultat(turnir.Id).ToList();
            
            /*RezultatiViewModel rezultatiVM = new RezultatiViewModel()
            {
                TimoviVM = timovi,
                RezultatiVM = rezultati
            };
            */
            //rezultatiVM.TimoviVM = fudbalskiTurnir.PoredjajTimovePoPoenima(timovi);
            ViewBag.RezultatiVM = rezultati;
            return View(rezultati);
        }
        [HttpGet]
        public IActionResult SacuvajTurnir(int turnirId)
        {
            var timovi = fudbalskiTurnir.ReadAllTim().Where(x => x.TurnirId == turnirId).ToList();
            var model = fudbalskiTurnir.PoredjajTimovePoPoenima(timovi);
            fudbalskiTurnir.TurnirOdigran(turnirId);
            return View(model);
        }
        /*
        [HttpGet]
        public IActionResult IzmeniRezultate(int turnirId)
        {
            List<IzmeniRezultateViewModel> rezultatiZaIzmenu = new List<IzmeniRezultateViewModel>();
            var rezultati = fudbalskiTurnir.ReadAlLRezultat().Where(rez => rez.TurnirId == turnirId);
            foreach( var rezultat in rezultati)
            {
                IzmeniRezultateViewModel rez = new IzmeniRezultateViewModel
                {
                    Tim_1_BrojGolova = rezultat.Tim_1_BrojGolova,
                    Tim_2_BrojGolova = rezultat.Tim_2_BrojGolova,
                    Tim_1_Id = rezultat.Tim_1_Id,
                    Tim_2_Id = rezultat.Tim_2_Id,
                    RezultatId = rezultat.Id,
                    Tim_1_Ime = rezultat.Tim_1_Ime,
                    Tim_2_Ime = rezultat.Tim_2_Ime

                };
                rezultatiZaIzmenu.Add(rez);
            }
            ViewBag.TurnirId = turnirId;
            return View(rezultatiZaIzmenu);
        }
        [HttpPost]
        public IActionResult IzmeniRezultate(List<IzmeniRezultateViewModel> rezultati, int turnirId)
        {
            List<RezultatModel> rezultatModels = new List<RezultatModel>();
            foreach (var rezultat in rezultati)
            {
                RezultatModel rezultatModel = new RezultatModel
                {
                    Tim_1_BrojGolova = rezultat.Tim_1_BrojGolova,
                    Tim_2_BrojGolova = rezultat.Tim_2_BrojGolova,
                    Tim_1_Id = rezultat.Tim_1_Id,
                    Tim_2_Id = rezultat.Tim_2_Id,
                    Tim_1_Ime = fudbalskiTurnir.ReadRezultat(rezultat.RezultatId).Tim_1_Ime,
                    Tim_2_Ime = fudbalskiTurnir.ReadRezultat(rezultat.RezultatId).Tim_2_Ime,
                    Id = rezultat.RezultatId
                };
                fudbalskiTurnir.UpdateRezultat(rezultatModel, rezultat.RezultatId);   
            }
            return RedirectToAction("Rezultati", "Zapocni", rezultatModels.First().TurnirId) ;
        }

        */
    }
}
