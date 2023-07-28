namespace FudbalskiTurnir_FilipNikolic
{
    public class IgracModel
    {
        /// <summary>
        /// Predstavlja Igrača u timu ( TimModel )
        /// </summary>
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public TimModel? Tim { get; set; } 
        public int? TimId { get; set; }
    }
}