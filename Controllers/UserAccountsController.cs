using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly DemoProjectContext _context;

        public UserAccountsController(DemoProjectContext context)
        {
            _context = context;
        }

        //xem tat ca tai khoan mat khau trong database
        // GET: api/UserAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccount>>> GetUserAccounts()
        {
            return await _context.UserAccounts.ToListAsync();
        }

        // dang nhap     
        [HttpPost("login")]
        public async Task<ActionResult<UserAccount>> GetUserAccount([FromForm]string acc,[FromForm] string pass)
        {
            var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserName.Equals(acc));

            if (userAccount == null)
            {
                return NotFound("Khong tim thay tai khoan");
            }
            if (userAccount.Password != pass)
            {
                return Problem("Sai mat khau, vui long nhap lai");
            }
            return Ok(userAccount);
        }

        // doi mat khau
        // PUT: api/UserAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUserAccount(string user, string firstPass, string secondPass)
        {
            var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserName.Equals(user));

            if (userAccount == null)
            {
                return NotFound("Khong tim thay tai khoan nay, vui long nhap lai");
            }

            _context.Entry(userAccount).State = EntityState.Modified;

            try
            {
                if(firstPass != secondPass)
                {
                    return NotFound("2 mat khau khong giong nhau, vui long nhap lai");
                }
                userAccount.Password = firstPass;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound("Loi khong cap nhat duoc mat khau, vui long thu lai");              
            }

            return Ok("Cap nhat thanh cong");
        }

        //them 1 tai khoan
        // POST: api/UserAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAccount>> PostUserAccount(List<Object> list)
        {
            int code = Int32.Parse(list.ElementAt(0).ToString());       
            RegisterAcc ra = (RegisterAcc) list.ElementAt(1);
            if (ra.Password1 != ra.Password2)
            {
                return Problem("2 mat khau khong giong nhau, vui long nhap lai");
            }
            if (code > 5)
            {
                return Problem("Code > 5, khong the them tai khoan");
            }
            UserAccount user = new UserAccount() { UserName = ra.UserName, Password = ra.Password1 };
            _context.UserAccounts.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Them tai khoan thanh cong");
        }

        // xoa tai khoan
        // DELETE: api/UserAccounts/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAccount(int id)
        {
            var userAccount = await _context.UserAccounts.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound("Khong tim thay tai khoan can xoa");
            }

            _context.UserAccounts.Remove(userAccount);
            await _context.SaveChangesAsync();

            return Ok("Xoa thanh cong");
        }

        //private bool UserAccountExists(int id)
        //{
        //    return _context.UserAccounts. Any(e => e.UserId == id);
        //}
    }
}
