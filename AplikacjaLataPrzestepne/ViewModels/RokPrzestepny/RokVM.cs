using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AplikacjaLataPrzestepne.ViewModels.RokPrzestepny
{
    public class RokVM
    {
        public int Id { get; set; }
        public int Rok { get; set; }

        public string? Imie { get; set; }
        public DateTime Data { get; set; }
        public string? user_id { get; set; }
        public string? login { get; set; }
        public string czy_przestepny { get; set; }
    }
}
