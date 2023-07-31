using FudbalskiTurnir_FilipNikolic.Models.Repository;
using FudbalskiTurnir_FilipNikolic.Models.Validation.TurnirValidators;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FudbalskiTurnir_FilipNikolic.Controllers
{

    [Authorize]
    public class TurnirController : Controller
    {
        private readonly IFudbalskiTurnir fudbalskiTurnir;

        public TurnirController(IFudbalskiTurnir fudbalskiTurnir)
        {
            this.fudbalskiTurnir = fudbalskiTurnir;
        }

        public IActionResult TurnirOverview()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateTim()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTim(CreateTimViewModel model)
        {
            var validator = new CreateTimViewModelValidator();
            var result = validator.Validate(model);
            if (result.IsValid)
            {
                fudbalskiTurnir.CreateTim(model.Ime);
                return RedirectToAction("PregledTimova", "Turnir");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateTurnir()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTurnir(CreateTurnirViewModel model)
        {
            var validator = new CreateTurnirViewModelValidator();
            var result = validator.Validate(model);
            // TO DO, napraviti dva izlaza, jedan za kad vec postoji ime, drugi koji proverava valid
            if (result.IsValid && fudbalskiTurnir.IsTimUnique(model.Ime))
            {
                fudbalskiTurnir.CreateTurnir(model.Ime);
                return RedirectToAction("PregledTurnira", "Turnir");
            }
            return View(model);
        }
        public IActionResult PregledTimova()
        {
            var timovi = fudbalskiTurnir.ReadAllTim();
            var igraci = fudbalskiTurnir.ReadAllIgrac();
            if (timovi == null)
            {
                timovi = new List<TimModel>();
            }
            PregledTimovaViewModel model = new PregledTimovaViewModel()
            {
                Igraci = igraci.ToList(),
                Timovi = timovi.ToList()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult DodajIgraceUTim(int timId)
        {
            ViewBag.TimId = timId;
            var tim = fudbalskiTurnir.ReadTim(timId);
            if (tim == null)
            {
                ViewBag.ErrorMessage = $"Tim sa Id {timId} ne moze biti pronadjen.";
                return View("Not Found");
            }
            List<IgracModel> igraci = fudbalskiTurnir.ReadAllIgrac().Where(p => p.TimId == timId || p.TimId == null).ToList();
            var model = new List<IgracTimViewModel>();

            foreach (var igrac in igraci)
            {
                var igracTimViewModel = new IgracTimViewModel
                {
                    IgracId = igrac.Id,
                    IgracName = igrac.Ime
                };
                if (igrac.TimId == timId)
                {
                    igracTimViewModel.IsSelected = true;
                }
                else
                {
                    igracTimViewModel.IsSelected = false;
                }
                model.Add(igracTimViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult DodajIgraceUTim(List<IgracTimViewModel> model, int timId)
        {
            foreach (var igrac in model)
            {
                if (igrac.IsSelected)
                {
                    fudbalskiTurnir.DodajIgracaUTim(igrac.IgracId, timId);
                }
                else
                {
                    fudbalskiTurnir.IzbaciIgracaIzTima(igrac.IgracId);
                }
            }
            return RedirectToAction("PregledTimova", "Turnir");
        }
        [HttpGet]
        public IActionResult DodajTimoveUTurnir(int turnirId)
        {
            ViewBag.TurnirId = turnirId;
            var turnir = fudbalskiTurnir.ReadTurnir(turnirId);
            if (turnir == null)
            {
                ViewBag.ErrorMessage = $"Tim sa Id {turnirId} ne moze biti pronadjen.";
                return View("Not Found");
            }
            List<TimModel> timovi = fudbalskiTurnir.ReadAllTim().Where(p => p.TurnirId == turnirId || p.TurnirId == null).ToList();
            //Ovde filtrirati da prosledimo timove u view koji imaju preko 5 igraca, da bi bilo legitimno
            var model = new List<TimTurnirViewModel>();

            foreach (var tim in timovi)
            {
                var timTurnirViewModel = new TimTurnirViewModel
                {
                    TimId = tim.Id,
                    TimName = tim.Ime
                };
                if (tim.TurnirId == turnirId)
                {
                    timTurnirViewModel.IsSelected = true;
                }
                else
                {
                    timTurnirViewModel.IsSelected = false;
                }
                model.Add(timTurnirViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult DodajTimoveUTurnir(List<TimTurnirViewModel> model, int turnirId)
        {
            foreach (var tim in model)
            {
                if (tim.IsSelected)
                {
                    fudbalskiTurnir.DodajTimUTurnir(tim.TimId, turnirId);
                }
                else
                {
                    fudbalskiTurnir.IzbaciTimIzTurnira(tim.TimId);
                }
            }
            return RedirectToAction("PregledTurnira", "Turnir");
        }
        [HttpGet]
        public IActionResult CreateIgrac()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateIgrac(CreateIgracViewModel model)
        {
            var validator = new CreateIgracViewModelValidator();
            var result = validator.Validate(model);
            if (result.IsValid)
            {
                fudbalskiTurnir.CreateIgrac(model.Ime, model.Prezime);
                return RedirectToAction("PregledIgraca", "Turnir");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult PregledIgraca()
        {
            var igraci = fudbalskiTurnir.ReadAllIgrac();
            if (igraci == null)
            {
                igraci = new List<IgracModel>();
            }
            return View(igraci);
        }
        [HttpPost]
        public IActionResult DeleteTim(int timId)
        {
            fudbalskiTurnir.DeleteTim(timId);
            return RedirectToAction("PregledTimova", "Turnir");
        }
        [HttpPost]
        public IActionResult DeleteIgrac(int igracId)
        {
            fudbalskiTurnir.DeleteIgrac(igracId);
            return RedirectToAction("PregledIgraca", "Turnir");
        }
        [HttpPost]
        public IActionResult DeleteTurnir(int turnirId)
        {
            fudbalskiTurnir.DeleteTurnir(turnirId);
            return RedirectToAction("PregledTurnira", "Turnir");
        }
        [HttpGet]
        public IActionResult UpdateTim(int timId)
        {
            ViewBag.TimId = timId;
            var tim = fudbalskiTurnir.ReadTim(timId);
            CreateTimViewModel timUpdate = new CreateTimViewModel { Ime = tim.Ime };
            return View(timUpdate);
        }
        [HttpPost]
        public IActionResult UpdateTim(CreateTimViewModel model, int timId)
        {
            CreateTimViewModelValidator validator = new CreateTimViewModelValidator();
            var result = validator.Validate(model);
            if (result.IsValid)
            {
                TimModel tim = fudbalskiTurnir.ReadTim(timId);
                tim.Ime = model.Ime;
                fudbalskiTurnir.UpdateTim(tim);
                return RedirectToAction("PregledTimova", "Turnir");
            }
            ViewBag.ErrorMessage = result.Errors.ToString();
            return View(timId);
        }
        [HttpGet]
        public IActionResult UpdateTurnir(int turnirId)
        {
            ViewBag.TurnirId = turnirId;
            var turnir = fudbalskiTurnir.ReadTurnir(turnirId);
            CreateTurnirViewModel turnirUpdate = new CreateTurnirViewModel { Ime = turnir.Ime };
            return View(turnirUpdate);
        }
        [HttpPost]
        public IActionResult UpdateTurnir(CreateTurnirViewModel model, int turnirId)
        {
            CreateTurnirViewModelValidator validator = new CreateTurnirViewModelValidator();
            var result = validator.Validate(model);
            if (result.IsValid)
            {
                TurnirModel turnir = fudbalskiTurnir.ReadTurnir(turnirId);
                turnir.Ime = model.Ime;
                fudbalskiTurnir.UpdateTurnir(turnir);
                return RedirectToAction("PregledTurnira", "Turnir");
            }
            ModelState.AddModelError("", "Molim unesite pravilno ime.");
            return View(turnirId);
        }
        [HttpGet]
        public IActionResult UpdateIgrac(int igracId)
        {
            ViewBag.IgracId = igracId;
            var igrac = fudbalskiTurnir.ReadIgrac(igracId);
            CreateIgracViewModel igracUpdate = new CreateIgracViewModel { Ime = igrac.Ime, Prezime = igrac.Prezime };
            return View(igracUpdate);
        }
        [HttpPost]
        public IActionResult UpdateIgrac(CreateIgracViewModel model, int igracId)
        {
            CreateIgracViewModelValidator validator = new CreateIgracViewModelValidator();
            var result = validator.Validate(model);
            if (result.IsValid)
            {
                IgracModel igrac = fudbalskiTurnir.ReadIgrac(igracId);
                igrac.Ime = model.Ime;
                igrac.Prezime = model.Prezime;
                fudbalskiTurnir.UpdateIgrac(igrac);
                return RedirectToAction("PregledIgraca", "Turnir");
            }
            ViewBag.ErrorMessage = result.Errors.ToString();
            return View(igracId);
        }
        [HttpGet]
        public IActionResult PregledTurnira()
        {
            var timovi = fudbalskiTurnir.ReadAllTim();
            var turniri = fudbalskiTurnir.ReadAllTurnir();
            if (turniri == null)
            {
                turniri = new List<TurnirModel>();
            }
            PregledTurniraViewModel model = new PregledTurniraViewModel()
            {
                Timovi = timovi.ToList(),
                Turniri = turniri.ToList()
            };
            return View(model);
        }
        // NEDOVRSENO, potrebna dorada
        [HttpGet]
        public IActionResult PregledRezultata(int turnirId)
        {
            var rezultati = fudbalskiTurnir.ReadAlLRezultat(turnirId);
            return View(rezultati);
        }
        // NEDOVRSENO, potrebna dorada
        [HttpPost]
        public IActionResult PregledRezultata(IEnumerable<RezultatModel> rezultati)
        {
            return View();
        }
        [HttpGet]
        public IActionResult UpdateRezultat(int rezultatId)
        {
            var rezultat = fudbalskiTurnir.ReadRezultat(rezultatId);
            IzmeniRezultatViewModel izmeni = new IzmeniRezultatViewModel
            {
                Tim1BrojGolova = rezultat.Tim_1_BrojGolova,
                Tim2BrojGolova = rezultat.Tim_2_BrojGolova,
                Tim1Ime = rezultat.Tim_1_Ime,
                Tim2Ime = rezultat.Tim_2_Ime,
                RezultatId = rezultat.Id
            };
            ViewBag.RezultatId = rezultatId;
            return View(izmeni);
        }
        [HttpPost] 
        public IActionResult UpdateRezultat(IzmeniRezultatViewModel izmeni)
        {
            var rezultat = fudbalskiTurnir.ReadRezultat(izmeni.RezultatId);

            var result = fudbalskiTurnir.UpdateRezultat(izmeni);

            return RedirectToAction("Rezultati", "Zapocni", new
            {
                selectedOption = result.TurnirId
            }) ;
        }
    }
}
