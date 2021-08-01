using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPeopleDataManagementSPA.Models.Data
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public Country State { get; set; }

        [Required]
        public City CurrentCity { get; set; }

        public string CellphoneNumber { get; set; }

        public List<PersonLanguage> PersonLanguages { get; set; } // Many to many join table

        public Person()
        {
            PersonLanguages = new List<PersonLanguage>();
        }
    }
}
