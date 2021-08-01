using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public interface ICityService
    {
        public City Add(CreateCity createCity);

        public CityViewModel All();

        public CityViewModel All_2(); //Added for json data manipulation

        public City FindById(int id);

        public City Edit(int id, CreateCity createCity);

        public CreateCity CityToCreateCity(City city);

        public bool Remove(int id);
    }
}
