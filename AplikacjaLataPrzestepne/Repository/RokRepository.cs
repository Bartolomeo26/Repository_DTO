using AplikacjaLataPrzestepne.Data;
using AplikacjaLataPrzestepne.Forms;
using AplikacjaLataPrzestepne.Interfaces;
using System;

namespace AplikacjaLataPrzestepne.Repository
{
    public class RokRepository : IRokPrzestepnyRepository
    {
        private readonly Wyszukiwania _context;
        public RokRepository(Wyszukiwania context)
        {
            _context = context;
        }
        public IQueryable<RokPrzestepny> GetActiveLeapYears()
        {
            return _context.LeapData;
        }
    }
}
