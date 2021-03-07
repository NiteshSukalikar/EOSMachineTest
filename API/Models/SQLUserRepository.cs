using AutoMapper;
using PorabayData.Models;
using PorabayData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porabay.Models
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly PorabayContext dbContext;
        private readonly IMapper mapper;

        public SQLUserRepository(PorabayContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public UserVM addUser(UserVM userVM)
        {
            if (userVM != null)
            {
                var userdata = mapper.Map<User>(userVM);
                dbContext.tblUser.Add(userdata);
                dbContext.SaveChanges();
            }

            return mapper.Map<UserVM>(userVM);
        }

        public UserVM deleteUser(int id)
        {
            var user = dbContext.tblUser.Find(id);

            if (user != null)
            {
                dbContext.tblUser.Remove(user);
                dbContext.SaveChanges();
            }


            return mapper.Map<UserVM>(user);
        }

        public UserVM EditUser(int id, UserVM userVM)
        {
            var domain = mapper.Map<User>(userVM);
            domain.Id = id;

            dbContext.Entry(domain).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();

            return mapper.Map<UserVM>(dbContext.tblUser.FirstOrDefault(x => x.Id == id));
        }

        public UserVM GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResultVM GetUsers(FilterVM dataTablesParameters)
        {
            ActionResultVM obj = new ActionResultVM();
            var length = (int)dataTablesParameters.Length;

            var recordsTotal = dbContext.tblUser.Count();

            var sortedData = mapper.Map<List<UserVM>>(dbContext.tblUser)
                .Where(x => (x.FirstName).ToUpper().Contains((dataTablesParameters.search.Value).ToUpper())
                || (x.LastName).ToUpper().Contains((dataTablesParameters.search.Value).ToUpper()));

            sortedData = GetOrderBy(sortedData, dataTablesParameters.Order[0]).Skip((int)dataTablesParameters.Start)
                .Take(length);

            var columnFilteredData = GetSorted(dbContext.tblUser, dataTablesParameters.Columns);

            if (columnFilteredData != null && dataTablesParameters.search.Value == "")
            {
                obj.Data = mapper.Map<List<UserVM>>((columnFilteredData).Skip((int)dataTablesParameters.Start)
                .Take(length).ToList());
                obj.TotalData = recordsTotal;
                return obj;
            }
            obj.Data = sortedData.ToList();
            obj.TotalData = recordsTotal;
            return obj;

        }

        private IEnumerable<UserVM> GetOrderBy(IEnumerable<UserVM> vm, Order order)
        {
            switch (order.Column)
            {
                case 0:
                    return order.Dir == "asc" ? vm.OrderBy(c => c.Id) : vm.OrderByDescending(c => c.Id);

                case 1:
                    return order.Dir == "asc" ? vm.OrderBy(c => c.FirstName) : vm.OrderByDescending(c => c.FirstName);

                case 2:
                    return order.Dir == "asc" ? vm.OrderBy(c => c.LastName) : vm.OrderByDescending(c => c.LastName);

                default:
                    return vm;
            }
        }


        private IEnumerable<User> GetSorted(IEnumerable<User> vm, IEnumerable<Columns> columns)
        {
            //var container = new List<User>();
            IEnumerable<User> container = vm;

            foreach (var item in columns)
            {
                if (item.Data == "id" && item.Search.Value != "")
                {
                    container = container.Where(x => (x.Id).ToString().Contains((item.Search.Value).ToUpper())).ToList();
                }
                else if (item.Data == "FirstName" && item.Search.Value != "")
                {
                    container = container.Where(x => (x.FirstName).ToUpper().Contains((item.Search.Value).ToUpper())).ToList();
                }
                else if (item.Data == "LastName" && item.Search.Value != "")
                {
                    container = container.Where(x => (x.LastName).ToUpper().Contains((item.Search.Value).ToUpper())).ToList();
                }
            }


            if (container.Count() > 0)
            {
                return container;
            }
            else
            {
                return null;

            }

        }
    }
}
