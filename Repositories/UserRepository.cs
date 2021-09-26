using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VisitsTaskWebAPI_MohammedElmorsy.Helpers;
using VisitsTaskWebAPI_MohammedElmorsy.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace VisitsTaskWebAPI_MohammedElmorsy.Repositories
{
    public class UserRepository : Repository<User>
    {
        private CompoundDBContext db;
        private DbSet<User> Users;
        private readonly AppSettings _appSettings;
        private IWebHostEnvironment webHostEnvironment;


        public UserRepository(CompoundDBContext db, IOptions<AppSettings> appSettings, IWebHostEnvironment hostEnvironment) : base(db)
        {
            this.db = db;
            Users = this.db.Set<User>();
            _appSettings = appSettings.Value;
            webHostEnvironment = hostEnvironment;
        }

        public User Authenticate(string username, string password)
        {
            var user = Users.SingleOrDefault(U => U.UserName == username && U.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("name", $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            db.SaveChanges();

            // remove password before returning
            user.Password = null;

            return user;
        }

        public bool UserIsExist(User _user)
        {
            User user = Users.SingleOrDefault(U => U.UserName == _user.UserName || U.Email == _user.Email);
            if (user == null)
                return false;
            return true;
        }

        public User DeleteToken(int userId)
        {
            User user = Users.Find(userId);
            if (user != null)
            {
                user.Token = null;
                return user;
            }
            return null;
        }

        public bool SendEmailToVisitor(Visitor visitor)
        {
            try
            {
                Execute(visitor).Wait();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        async Task Execute(Visitor visitor)
        {
            
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mohammed.elsayed.elmorsy@gmail.com", "Mohammed Elmorsy");
            var subject = "Visit Invitation";
            var to = new EmailAddress(visitor.Email, "Visitor");
            var plainTextContent = "ddddddddddd";
            string QRCodeImagePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", "qrcode.png");
            var htmlContent = $"<p>Please scan this QR Code</p> <img src= https://developer.enonic.com/docs/qr-code-library/master/_/image/75d05996-a667-4548-b2fe-5cb824853a92:65ded3ad0cf7d4baa6bbde8a2d1d2bd66f29cb4c/width-768/qrcode.png />";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }

}
