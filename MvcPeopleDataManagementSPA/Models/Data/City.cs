using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPeopleDataManagementSPA.Models.Data
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public Country State { get; set; }

        public int StateId { get; set; }

        public List<Person> PersonList { get; set; }

        public City()
        {
            PersonList = new List<Person>();
        }
    }
}
