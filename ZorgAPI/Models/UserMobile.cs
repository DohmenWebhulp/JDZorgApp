using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZorgAPI.Models
{
    public class UserMobile
    {
        [Key]
        public Guid Id { get; set; }
        public string Gebruikersnaam { get; set; }
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
    }
}
