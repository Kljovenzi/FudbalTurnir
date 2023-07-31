using FudbalskiTurnir_FilipNikolic.Models.ViewModel;

namespace FudbalskiTurnir_FilipNikolic.Models.Repository
{
    public interface IFudbalskiTurnir
    {
        /// <summary>
        /// CRUD metode za rad sa turnirima
        /// </summary>
        TurnirModel CreateTurnir(string ime);
        void DeleteTurnir(int id);
        void UpdateTurnir(TurnirModel turnir);
        TurnirModel ReadTurnir(int id);

        /// <summary>
        /// CRUD Metode koje su potrebne za rad sa  - igracima -
        /// </summary>
        IgracModel CreateIgrac(string ime, string prezime);
        void  DeleteIgrac(int id);
        IgracModel UpdateIgrac (IgracModel updateIgrac);
        IgracModel ReadIgrac(int id);

        /// <summary>
        /// CRUD Metode koje su potrebne za rad sa  - timovima -
        /// </summary>
        void CreateTim(string ime, List<IgracModel> igraci);
        void CreateTim(string ime);
        void  DeleteTim(int id);
        TimModel ReadTim(int id);
        TimModel UpdateTim (TimModel updateTim);

        /// <summary>
        /// CRUD Metode koje su potrebne za rad sa  - rezultatima -
        /// </summary>
        void CreateRezultati (List<RezultatModel> rezultati);
        void DeleteRezultate(int turnirId);
        RezultatModel UpdateRezultat (IzmeniRezultatViewModel rezultat );
        RezultatModel ReadRezultat(int id);

        /// <summary>
        /// Čita i vraća sve rezultate , sve timove, sve igrače
        /// </summary>
        /// <returns></returns>
        IEnumerable<RezultatModel> ReadAlLRezultat(int turnirId);
        IEnumerable<TimModel> ReadAllTim();
        IEnumerable<IgracModel> ReadAllIgrac();
        IEnumerable<TurnirModel> ReadAllTurnir();

        /// <summary>
        /// Dodavanje Igraca u Tim, Tim u Turnir
        /// </summary>
        /// <param name="idIgraca"></param>
        /// <param name="idTima"></param>
        void DodajIgracaUTim(int idIgraca, int idTima);
        void IzbaciIgracaIzTima(int idIgraca);
        void DodajTimUTurnir(int idTima, int idTurnira);
        void IzbaciTimIzTurnira(int idTima);

        /// <summary>
        /// Sprecavanje duplog unosa
        /// </summary>
        /// <param name="ime"></param>
        /// <returns></returns>
        bool IsTimUnique(string ime);

        /// <summary>
        /// Logika Turnira
        /// </summary>
        /// <param name="timovi"></param>
        /// <returns></returns>
        List<RezultatModel> NapraviRezultate(TimModel[] timovi, int turnirId);
        void DodajIshode(List<RezultatModel> rezultat);
        List<TimModel> PoredjajTimovePoPoenima(List<TimModel> timovi);
        void TurnirOdigran(int turnirId);

    }
}
