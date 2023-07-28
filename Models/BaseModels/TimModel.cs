using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudbalskiTurnir_FilipNikolic
{
    public class TimModel
    {
        /// <summary>
        /// Predstavlja Tim u turniru, sadrži listu igrača.
        /// Broj pobeda, izgubljenih, remija, koristiti za Rang Listu
        /// </summary>
        public int Id { get; set; }
        public List<IgracModel>? IgraciUTimu{ get; set; } = new List<IgracModel>();
        public string Ime { get; set; }
        public int BrojPobeda { get; set; } = 0;
        public int BrojIzgubljenih { get; set; } = 0;
        public int BrojRemija { get; set; } = 0;
        public int BrojOsvojenihBodovaNaTurniru { get; set; } = 0;
        public TurnirModel? Turnir { get; set; }
        public int? TurnirId { get; set;}
    }
}
