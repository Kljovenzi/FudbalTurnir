namespace FudbalskiTurnir_FilipNikolic
{
    public class RezultatModel
    {
        /// <summary>
        /// Rezultat svakog meča
        /// </summary>
        public int Id { get; set; }
        public int Tim_1_Id { get; set; }
        public int Tim_2_Id { get; set; }
        public int Tim_1_BrojGolova { get; set; } = 0;
        public int Tim_2_BrojGolova { get; set; } = 0;
        public TurnirModel  Turnir { get; set; }
        public TimModel Tim1 { get; set; }
        public TimModel Tim2 { get; set; }
        public int? TurnirId { get; set; }
        public string? Tim_1_Ime { get; set; }
        public string? Tim_2_Ime { get; set; }



        /*
        public void PobednikRezultata()
        {
            if (Tim_1_BrojGolova > Tim_2_BrojGolova)
            {
                 this.Tim_1.BrojPobeda +=1;
                 this.Tim_2.BrojIzgubljenih += 1;
            }
            else if( Tim_1_BrojGolova < Tim_2_BrojGolova )
            {
                this.Tim_2.BrojPobeda += 1;
                this.Tim_1.BrojIzgubljenih += 1;
            }
            else
            {
                this.Tim_1.BrojIzgubljenih -= 1;
            }
        }
        */
    }
}