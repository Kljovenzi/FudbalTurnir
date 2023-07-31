using FudbalskiTurnir_FilipNikolic.Models.DBContext;
using FudbalskiTurnir_FilipNikolic.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Transactions;
using System.Xml;

namespace FudbalskiTurnir_FilipNikolic.Models.Repository
{
    public class FudbalskiTurnir : IFudbalskiTurnir
    {

        private readonly DBContext_FudbalskiTurnir _dbContext;

        public FudbalskiTurnir(DBContext_FudbalskiTurnir dbContext)
        {
            _dbContext = dbContext;
        }

        public IgracModel CreateIgrac(string ime, string prezime)
        {
            var noviIgrac = new IgracModel { Ime = ime, Prezime = prezime };
            _dbContext.Igraci.Add(noviIgrac);
            _dbContext.SaveChanges();
            return noviIgrac;
        }

        public void CreateRezultati(List<RezultatModel> rezultati)
        {
            if (rezultati != null)
            {
                foreach (RezultatModel rezultat in rezultati)
                {
                    _dbContext.Rezultati.Add(rezultat);
                }
            }
        }

        public void CreateTim(string ime, List<IgracModel> igraci)
        {
            var newTim = new TimModel { Ime = ime };
            if (igraci != null)
            {
                foreach (var igrac in igraci)
                {
                    newTim.IgraciUTimu.Add(igrac);
                }
            }
            _dbContext.Timovi.Add(newTim);
            _dbContext.SaveChanges();
        }
        public void CreateTim(string ime)
        {
            var newTim = new TimModel { Ime = ime };
            _dbContext.Timovi.Add(newTim);
            _dbContext.SaveChanges();
        }

        public TurnirModel CreateTurnir(string ime)
        {
            var newTurnir = new TurnirModel { Ime = ime };
            _dbContext.Turniri.Add(newTurnir);
            _dbContext.SaveChanges();
            return newTurnir;
        }

        public void DeleteIgrac(int id)
        {
            var result = _dbContext.Igraci.FirstOrDefault(igrac => igrac.Id == id);
            if (result != null)
            {
                _dbContext.Igraci.Remove(result);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteRezultate(int turnirId)
        {
            if (_dbContext.Rezultati.Any(x => x.TurnirId == turnirId))
            {
                var resultsToDelete = _dbContext.Rezultati.Where(x => x.TurnirId == turnirId);
                _dbContext.RemoveRange(resultsToDelete);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteTim(int id)
        {
            var result = _dbContext.Timovi.FirstOrDefault(t => t.Id == id);
            if (result != null)
            {
                _dbContext.Timovi.Remove(result);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteTurnir(int id)
        {
            var result = _dbContext.Turniri.FirstOrDefault(t => t.Id == id);
            if (result != null)
            {
                _dbContext.Turniri.Remove(result);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<IgracModel> ReadAllIgrac()
        {
            var igraci = _dbContext.Igraci.ToList();
            return igraci;
        }

        public void TurnirOdigran(int turnirId)
        {
            var turnir = ReadTurnir(turnirId);
            turnir.TurnirOdigran = true;
            _dbContext.SaveChanges();
        }
        public IEnumerable<RezultatModel> ReadAlLRezultat(int turnirId)
        {
            return _dbContext.Rezultati.Where(x=>x.TurnirId==turnirId);
        }

        public IEnumerable<TimModel> ReadAllTim()
        {
            var timovi = _dbContext.Timovi.ToList();
            return timovi;
        }

        public IgracModel ReadIgrac(int id)
        {
            var igrac = _dbContext.Igraci.FirstOrDefault(p => p.Id == id);
            if (igrac != null)
            {
                return igrac;
            }
            else
            {
                return null;
            };
        }

        public RezultatModel ReadRezultat(int id)
        {
            var result = _dbContext.Rezultati.FirstOrDefault(x => x.Id == id);
            if(result==null)
            {
                result = new RezultatModel();
            }
            return result;
        }

        public TimModel ReadTim(int id)
        {
            var tim = _dbContext.Timovi.FirstOrDefault(p => p.Id == id);
            if (tim != null)
            {
                return tim;
            }
            else
            {
                return null;
            }
        }
        public TurnirModel ReadTurnir(int id)
        {
            var turnir = _dbContext.Turniri.FirstOrDefault(p => p.Id == id);
            if (turnir != null)
            {
                return turnir;
            }
            else
            {
                return null;
            }
        }

        public IgracModel UpdateIgrac(IgracModel updateIgrac)
        {
            var igrac = _dbContext.Igraci.FirstOrDefault(x => x.Id == updateIgrac.Id);
            igrac.Ime = updateIgrac.Ime;
            igrac.Prezime = updateIgrac.Prezime;
            _dbContext.SaveChanges();
            return igrac;
        }

        public RezultatModel UpdateRezultat(IzmeniRezultatViewModel rezultat)
        {
            var rezultatToUpdate = _dbContext.Rezultati.FirstOrDefault(x => x.Id == rezultat.RezultatId);
            rezultatToUpdate.Tim_1_BrojGolova = rezultat.Tim1BrojGolova;
            rezultatToUpdate.Tim_2_BrojGolova = rezultat.Tim2BrojGolova;
            _dbContext.SaveChanges();
            return rezultatToUpdate;
        }
        

        public TimModel UpdateTim(TimModel updateTim)
        {
            var tim = _dbContext.Timovi.FirstOrDefault(x => x.Id == updateTim.Id);
            tim.Ime = updateTim.Ime;
            _dbContext.SaveChanges();
            return tim;
        }

        public void UpdateTurnir(TurnirModel turnir)
        {
            var turnirUpdate = _dbContext.Turniri.FirstOrDefault(x => x.Id == turnir.Id);
            turnirUpdate.Ime = turnir.Ime;
            _dbContext.SaveChanges();
        }
        public void DodajIgracaUTim(int idIgraca, int idTima)
        {
            IgracModel updateIgrac = _dbContext.Igraci.FirstOrDefault(x => x.Id == idIgraca);
            if (updateIgrac != null)
            {
                updateIgrac.TimId = idTima;
            }
            _dbContext.SaveChanges();
        }
        public void IzbaciIgracaIzTima(int idIgraca)
        {
            IgracModel updateIgrac = _dbContext.Igraci.FirstOrDefault(x => x.Id == idIgraca);
            if (updateIgrac != null)
            {
                updateIgrac.TimId = null;
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<TurnirModel> ReadAllTurnir()
        {
            var turniri = _dbContext.Turniri.ToList();
            return turniri;
        }

        public void DodajTimUTurnir(int idTima, int idTurnira)
        {
            TimModel updateTim = _dbContext.Timovi.FirstOrDefault(x => x.Id == idTima);
            if (updateTim != null)
            {
                updateTim.TurnirId = idTurnira;
            }
            _dbContext.SaveChanges();
        }
        public void IzbaciTimIzTurnira(int idTima)
        {
            TimModel updateTim = _dbContext.Timovi.FirstOrDefault(x => x.Id == idTima);
            if (updateTim != null)
            {
                updateTim.TurnirId = null;
            }
            _dbContext.SaveChanges();
        }

        public bool IsTimUnique(string ime)
        {
            return !_dbContext.Timovi.Any(t => t.Ime == ime);
        }

        public List<RezultatModel> NapraviRezultate(TimModel[] timovi, int turnirId)
        {
            //T0 Do, napraviti posebnu metodu za brisanje rezultata
            DeleteRezultate(turnirId);
            List<RezultatModel> rezultati = new List<RezultatModel>();

            for (int i = 0; i < timovi.Length - 1; i++)
            {
                for (int j = i + 1; j < timovi.Length; j++)
                {
                    int brojGolovaTim1 = new Random().Next(6);
                    int brojGolovaTim2 = new Random().Next(6 - brojGolovaTim1);
                    RezultatModel rezultat = new RezultatModel
                    {
                        Tim_1_Id = timovi[i].Id,
                        Tim_2_Id = timovi[j].Id,
                        Tim_1_BrojGolova = brojGolovaTim1,
                        Tim_2_BrojGolova = brojGolovaTim2,
                        Tim_1_Ime = timovi[i].Ime,
                        Tim_2_Ime = timovi[j].Ime,
                        TurnirId = turnirId
                    };
                    _dbContext.Rezultati.Add(rezultat);
                    rezultati.Add(rezultat);
                }
            }
            _dbContext.Rezultati.AddRange(rezultati);
            _dbContext.SaveChanges();
            return rezultati;
        }

        public void DodajIshode(List<RezultatModel> rezultati)
        {
            if (_dbContext.Timovi.Any(x => x.BrojPobeda != 0 || x.BrojIzgubljenih != 0 || x.BrojRemija != 0))
            {
                var timoviZaPodesavanjeParametara = _dbContext.Timovi.Where(x => x.TurnirId == rezultati.First().TurnirId);
                foreach (var tim in timoviZaPodesavanjeParametara)
                {
                    tim.BrojIzgubljenih = 0;
                    tim.BrojRemija = 0;
                    tim.BrojPobeda = 0;
                }
            }
            foreach (var rezultat in rezultati)
            {
                if (rezultat.Tim_1_BrojGolova > rezultat.Tim_2_BrojGolova)
                {
                    _dbContext.Timovi.FirstOrDefault(t => t.Id == rezultat.Tim_1_Id).BrojPobeda += 1;
                    _dbContext.Timovi.FirstOrDefault(t => t.Id == rezultat.Tim_2_Id).BrojIzgubljenih += 1;
                }
                else if (rezultat.Tim_1_BrojGolova < rezultat.Tim_2_BrojGolova)
                {
                    _dbContext.Timovi.FirstOrDefault(t => t.Id == rezultat.Tim_2_Id).BrojPobeda += 1;
                    _dbContext.Timovi.FirstOrDefault(t => t.Id == rezultat.Tim_1_Id).BrojIzgubljenih += 1;
                }
                else
                {
                    _dbContext.Timovi.FirstOrDefault(t => t.Id == rezultat.Tim_1_Id).BrojRemija += 1;
                    _dbContext.Timovi.FirstOrDefault(t => t.Id == rezultat.Tim_2_Id).BrojRemija += 1;
                }
            }
            _dbContext.SaveChanges();
        }

        public List<TimModel> PoredjajTimovePoPoenima(List<TimModel> timovi)
        {
            foreach (var t in timovi)
            {
                t.BrojOsvojenihBodovaNaTurniru = t.BrojPobeda * 3 + t.BrojRemija * 1;
            }
            var poredjani = timovi.OrderByDescending(x => x.BrojOsvojenihBodovaNaTurniru);
            return poredjani.ToList();
        }
    }
}
