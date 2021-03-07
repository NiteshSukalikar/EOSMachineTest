using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Porabay.Models;
using PorabayData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porabay.Controllers
{
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        private readonly PorabayContext dbContext;
        private readonly IMapper mapper;

        public RegistrationController(PorabayContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = mapper.Map<List<Registration>>(dbContext.tblRegistration.ToList());

            // var user = dbContext.tblLogin.ToList();

            return Ok(user);
        }
    }
}
