using AplikacjaLataPrzestepne.Forms;
using AplikacjaLataPrzestepne.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AplikacjaLataPrzestepne.Interfaces;
using AplikacjaLataPrzestepne.ViewModels.RokPrzestepny;


namespace AplikacjaLataPrzestepne.Services
{
    public class RokPrzestepnyService : IRokPrzestepnyService
    {
        private readonly Wyszukiwania _context;
        private readonly IRokPrzestepnyRepository _rokRepository;

        public RokPrzestepnyService(Wyszukiwania context,
            IRokPrzestepnyRepository personRepository)
        {
            _context = context;
            _rokRepository = personRepository;
        }

        public async Task<List<RokPrzestepny>> GetAllRokAsync()
        {
            return await _context.LeapData.ToListAsync();
        }

        public async Task<RokPrzestepny> GetRokByIdAsync(int id)
        {
            return await _context.LeapData.FindAsync(id);
        }

        public async Task CreateRokAsync(RokPrzestepny rok)
        {
            _context.LeapData.Add(rok);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRokAsync(RokPrzestepny rok)
        {
            _context.LeapData.Update(rok);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRokAsync(int id)
        {
            var rok = await _context.LeapData.FindAsync(id);
            if (rok != null)
            {
                _context.LeapData.Remove(rok);
                await _context.SaveChangesAsync();
            }
        }

        public ListRokVM GetYearsForList()
        {
            var lata = _rokRepository.GetActiveLeapYears();
            ListRokVM result = new ListRokVM();
            result.Years = new List<RokVM>();
            foreach (var rok in lata)
            {
                // mapowanie obiektów
                var rVM = new RokVM()
                {
                    Id = rok.Id,
                    Imie = rok.Imie,
                    Rok = rok.Rok,
                    czy_przestepny = rok.czy_przestepny,
                    Data = rok.Data,
                    user_id = rok.user_id,
                    login = rok.login
                };
                result.Years.Add(rVM);
            }
            return result;
        }
    }
}