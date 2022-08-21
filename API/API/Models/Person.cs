using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Person
    {
        [Key()]
        public int Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "O Nome informado não pode conter mais de 200 caracteres.")]
        public string Name { get; set; }
    }
}
