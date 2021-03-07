using PorabayData.Models;
using PorabayData.ViewModels;
using System.Collections.Generic;

namespace Porabay.Models
{
    public interface IDomainRepository
    {
        DomainVM GetDomain(int id);

        IEnumerable<DomainVM> GetDomains();

        DomainVM addDomain(DomainVM domainVM);

        DomainVM deleteDomain(int id);

        DomainVM EditDomain(int id, DomainVM domainVM);


    }
}
