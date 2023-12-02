using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O Usuário deve ser informado")]
        [Display(Name = "Usuário")]
        public string User { get; set; }

        [Required(ErrorMessage = "A Senha deve ser informada")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public int Id { get; set; }
    }

    public class SigninRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome deve ser informado")]
        [Display(Name = "Nome")]
        //[StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Usuário deve ser informado")]
        [Display(Name = "Usuário")]
        public string User { get; set; }

        [Required(ErrorMessage = "A Senha deve ser informada")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
