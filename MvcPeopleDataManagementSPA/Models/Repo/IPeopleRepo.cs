using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.Repo
{
    public interface IPeopleRepo
    {
        Person Create(Person person);

        Person Read(int id);

        Person Read_2(int id);  //Added for json data manipulation

        List<Person> Read();

        public List<Person> Read_2();  //Added for json data manipulation

        Person Update(Person person);

        bool Delete(int id);
    }
}
