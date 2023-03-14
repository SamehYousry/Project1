using Azure.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Migrations;
using Project1.Models;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> Register(RegisterCustomer model)
        {
            if(_context.Customer.Any(u => u.Email == model.Email))
            {
                return BadRequest("User already exists.");

            }
            CreatePasswordHash(model.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);


           var customer = new Customer
            {
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                District = model.District,
               PasswordHash = passwordHash,
               PasswordSalt = passwordSalt,

           };

            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();

            return Ok("User successfully created!");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(Login request)
        {
            var user = await _context.Customer.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect.");
            }

               return Ok($"Welcome back, {user.Email}! :)");
        }
       
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string PhoneNumber)
        {
            var user = await _context.Customer.FirstOrDefaultAsync(u => u.PhoneNumber == PhoneNumber);
            if (user == null)
            {
                return BadRequest("Phone not found.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResettPassword(ResetPasswordRequest request)
        {
            var user = await _context.Customer.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("Password successfully reset.");
        }



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateRandomToken()
        {

            byte[] bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);
            int value = BitConverter.ToInt32(bytes, 0);
            return value.ToString("X");
        }

        [HttpPost("admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdmin adminDto)
        {
            var admin = new Models.Admin
            {
                Name = adminDto.Name,
                Email = adminDto.Email,
                Password = adminDto.Password,
                Providers = new List<Provider>()
            };

            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("RegisterProvider")]
        public async Task<IActionResult> Register([FromForm] RegisterProvider model)
        {
            if (_context.Providers.Any(u => u.Email == model.Email))
            {
                return BadRequest("User already exists.");

            }

            var admin = await _context.Admin.FindAsync(model.AdminId);

            if (admin == null)
            {
                return NotFound();
            }


            CreatePasswordHash(model.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);
            using var dataStream = new MemoryStream();
            await model.ImageFront.CopyToAsync(dataStream);
            await model.ImageBack.CopyToAsync(dataStream);
            await model.ImageProvider.CopyToAsync(dataStream);

            var provider = new Provider
            {
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                District = model.District,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Service= model.Service,
                ImageFront = dataStream.ToArray(),
                ImageBack = dataStream.ToArray(),
                ImageProvider = dataStream.ToArray(),
                Admin= admin

            };
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();
            return Ok("User successfully created!");
        }

        [HttpPost("loginProvider")]
        public async Task<IActionResult> LoginProvider(Login request)

        {
           

            var user = await _context.Providers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect.");
            }

            return Ok($"Welcome back, {user.Email}! :)");
        }

        [HttpPost("forgot-password-Provider")]
        public async Task<IActionResult> ForgotPasswordProvider(string PhoneNumber)
        {
            var user = await _context.Providers.FirstOrDefaultAsync(u => u.PhoneNumber == PhoneNumber);
            if (user == null)
            {
                return BadRequest("Phone not found.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password-Provider")]
        public async Task<IActionResult> ResettPasswordProvider(ResetPasswordRequest request)
        {
            var user = await _context.Providers.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("Password successfully reset.");
        }

      
    }
}

    
    

