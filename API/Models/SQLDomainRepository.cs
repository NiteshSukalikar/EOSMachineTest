using AutoMapper;
using PorabayData.Models;
using PorabayData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porabay.Models
{
    public class SQLDomainRepository : IDomainRepository
    {
        private readonly PorabayContext dbContext;
        private readonly IMapper mapper;

        public SQLDomainRepository(PorabayContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public DomainVM addDomain(DomainVM domainVM)
        {
            if (domainVM != null)
            {
                var domaindata = mapper.Map<Domain>(domainVM);
                dbContext.tblDomainData.Add(domaindata);
                dbContext.SaveChanges();
            }

            return mapper.Map<DomainVM>(dbContext.tblDomainData.Where(a => a.Title == domainVM.Title).Single());
        }

        public DomainVM deleteDomain(int id)
        {
            var user = dbContext.tblDomainData.Find(id);

            if (user != null)
            {
                dbContext.tblDomainData.Remove(user);
                dbContext.SaveChanges();
            }


            return mapper.Map<DomainVM>(user);
        }

        public DomainVM EditDomain(int id, DomainVM domainVM)
        {

            var domain = mapper.Map<Domain>(domainVM);
            domain.DomianID = id;

            dbContext.Entry(domain).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();

            return mapper.Map<DomainVM>(dbContext.tblDomainData.FirstOrDefault(x => x.DomianID == id));
        }

        public DomainVM GetDomain(int id)
        {
            return mapper.Map<DomainVM>(dbContext.tblDomainData.FirstOrDefault(x => x.DomianID == id));
        }

        public IEnumerable<DomainVM> GetDomains()
        {
            return mapper.Map<List<DomainVM>>(dbContext.tblDomainData);
        }
    }
}
