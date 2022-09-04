using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyAPI3.DBContext;
using MyAPI3.Models;
using MyAPI3.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI3.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var list = _repository.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var user = _repository.GetUserById(id);
            if(user == null)
                return NotFound("This is is not exist");
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (_repository.AddUser(user))
                return Ok("Add success");
            return BadRequest("Add fail");
        }

        [HttpPut]
        public IActionResult EditUser(User user)
        {
            if(_repository.EditUser(user))
                return Ok("Update success");
            return BadRequest("Update fail");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            if (_repository.DeleteUser(id))
                return Ok("Delete success");
            return BadRequest("Delete fail");
        }

    }
}
