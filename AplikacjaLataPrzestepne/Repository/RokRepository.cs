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
        public void DeleteYear(int id)
        {
            var _post = _context.LeapData.Find(id);
            if (_post != null)
            {
                _post.Id = id; // Set a permanent value for SearchId
                _context.LeapData.Remove(_post);
                _context.SaveChanges();
            }
        }
    }
}
