using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class EditCity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CreateCity CreateCity { get; set; }
    }
}
