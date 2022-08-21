using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Contact
    {
        [Key()]
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int IdPerson { get; set; }
        public virtual Person Person { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O Tipo do contato informado não pode conter mais de 100 caracteres.")]
        public string TypeContact { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "O Contato informado não pode conter mais de 100 caracteres.")]
        public string DescriptionContact { get; set; }
    }
}
