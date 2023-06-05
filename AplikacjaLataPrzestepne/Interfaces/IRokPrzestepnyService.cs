using AplikacjaLataPrzestepne.ViewModels.RokPrzestepny;
namespace AplikacjaLataPrzestepne.Interfaces
{
    public interface IRokPrzestepnyService
    {
        ListRokVM GetYearsForList();
        void DeleteYears(int id_user);
    }
}
