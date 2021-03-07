using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Porabay.Models;
using PorabayData.Models;
using PorabayData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porabay.Controllers
{
    
    [Route("api/[controller]")]
    public class DomainController : Controller
    {

        private readonly IMapper mapper;
        private readonly IDomainRepository domainRepository;

        public DomainController(IMapper mapper, IDomainRepository domainRepository)
        {
            this.mapper = mapper;
            this.domainRepository = domainRepository;
        }

        [Authorize]
        [EnableCors("Policy1")]
        [HttpGet]
        public IActionResult Get()
        {
            //var user = mapper.Map<List<Domain>>(dbContext.tblDomainData.ToList());
            // var user = dbContext.tblLogin.ToList();
            //List<DomainVM> domainDeatils = mapper.Map<List<DomainVM>>(domainData); working solution 
            var domainData = this.domainRepository.GetDomains();
            return Ok(domainData);
        }

        [Authorize]
        [Route("{Id?}")]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var domainData = this.domainRepository.GetDomain(id.Value);
            return Ok(domainData);
        }

        [Authorize]
        [EnableCors("Policy1")]
        [HttpPost]
        public IActionResult Create([FromBody] DomainVM model)
        {
            var domainData = this.domainRepository.addDomain(model);
            return Ok(domainData);
        }

        [Authorize]
        [EnableCors("Policy1")]
        [Route("{Id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            this.domainRepository.deleteDomain(id);
            return Ok(true);
        }

        [Authorize]
        [EnableCors("Policy1")]
        [Route("{Id}")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody] DomainVM model)
        {
            var domainlist = this.domainRepository.EditDomain(id, model);
            return Ok(true);
        }


    }
}
