using AplikacjaLataPrzestepne.Forms;
using System;

namespace AplikacjaLataPrzestepne.Interfaces
{
    public interface IRokPrzestepnyRepository
    {
        IQueryable<RokPrzestepny> GetActiveLeapYears();
    }
}
