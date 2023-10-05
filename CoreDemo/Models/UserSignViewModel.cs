using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class UserSignViewModel
    {
        [Required(ErrorMessage ="Lütfen Kullanıcı Adını Girin")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen  Şifrenizi Girin")]
        public string Password { get; set; }

    }
}
