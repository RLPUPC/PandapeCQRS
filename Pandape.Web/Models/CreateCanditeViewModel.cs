using System.ComponentModel.DataAnnotations;

namespace Pandape.Web.Models
{
    public class CreateCanditeViewModel
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthdate { get; set; }
        public string Email { get; set; } = default!;
    }
}
