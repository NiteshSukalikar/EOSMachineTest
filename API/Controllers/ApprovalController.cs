using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Porabay.Models;
using PorabayData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porabay.Controllers
{
    [Route("api/[controller]")]
    public class ApprovalController : Controller
    {
        private readonly IMapper mapper;
        private readonly IApprovalRepository approvalRepository;

        public ApprovalController(IMapper mapper, IApprovalRepository approvalRepository)
        {
            this.mapper = mapper;
            this.approvalRepository = approvalRepository;
        }


        //[Authorize]
        [EnableCors("Policy1")]
        [HttpGet]
        public IActionResult Get()
        {
            //var user = mapper.Map<List<Domain>>(dbContext.tblDomainData.ToList());
            // var user = dbContext.tblLogin.ToList();
            //List<DomainVM> domainDeatils = mapper.Map<List<DomainVM>>(domainData); working solution 
            var approvalData = this.approvalRepository.GetApprovals();
            return Ok(approvalData);
        }

       // [Authorize]
        [Route("{Id?}")]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var approvalData = this.approvalRepository.GetApproval(id.Value);
            return Ok(approvalData);
        }

       
        [EnableCors("Policy1")]
        [HttpPost]
        public IActionResult Create([FromBody] ApprovalVM model)
        {
            var approvalData = this.approvalRepository.addApprovalDetails(model);
            return Ok(approvalData);
        }

       
        [EnableCors("Policy1")]
        [Route("{Id}")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody] ApprovalVM model)
        {
            var approvallist = this.approvalRepository.EditApprovalDetails(id, model);
            return Ok(true);
        }

      
        [EnableCors("Policy1")]
        [Route("{Id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            this.approvalRepository.DeleteApprovalDetails(id);
            return Ok(true);
        }

    }
}
