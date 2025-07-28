using System.ComponentModel.DataAnnotations;

namespace FinanzasTaxista_Api.DTO_s
{
    public class UserLogin
    {
        public string Identificador { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }

        


    }
}
