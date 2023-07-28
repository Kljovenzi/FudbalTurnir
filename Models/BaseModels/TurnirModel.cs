using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudbalskiTurnir_FilipNikolic
{
    public class TurnirModel
    {
        /// <summary>
        /// Model Turnira kako bi se napravio novi turnir, pratili prethodni turniri
        /// </summary>
        public int Id { get; set; }
        public string Ime { get; set; }
        public List<TimModel>? Timovi { get; set; } = new List<TimModel>();
    }
}
