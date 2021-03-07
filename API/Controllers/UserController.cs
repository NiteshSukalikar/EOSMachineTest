using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Porabay.Models;
using PorabayData.ViewModels;

namespace Porabay.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("Policy1")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        //[Authorize]
        [EnableCors("Policy1")]
        [Route("getuser")]
        [HttpPost]
        public IActionResult GetUser([FromBody] FilterVM dataTablesParameters)
        { 
            var userData = this.userRepository.GetUsers(dataTablesParameters);
           
            return Ok(userData);
        }

        // [Authorize]
        [EnableCors("Policy1")]
        [Route("createuser")]
        [HttpPost]
        public IActionResult Create([FromBody] UserVM model)
        {
            var userData = this.userRepository.addUser(model);
            return Ok(userData);
        }


        // [Authorize]
        [EnableCors("Policy1")]
        [Route("{Id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            this.userRepository.deleteUser(id);
            return Ok(true);
        }


        //[Authorize]
        [EnableCors("Policy1")]
        [Route("{Id}")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody] UserVM model)
        {
            var userList = this.userRepository.EditUser(id, model);
            return Ok(true);
        }

     


    }
}
