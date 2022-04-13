#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using System.Net.Mail;
using System.Net;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIContactform : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIContactform(CatalogDBContext context)
        {
            _context = context;
        }


        // POST: api/APIContactform
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contactform>> PostContactform(Contactform contactform)
        {

            MailMessage msg = new MailMessage();
            // Set mail parameters
            msg.From = new MailAddress("bashpoddenxjob@gmail.com");
            msg.To.Add("bashpoddenxjob@gmail.com");
            msg.Subject = "Kontaktformulär Bashpodden" + contactform.Email;
            msg.Body = "Avsändare: " + contactform.Name + " - Epost: " + contactform.Email + " - Meddelande: " + contactform.Message;
            //Allow html and css design in mail
            msg.IsBodyHtml = true;


            // Set client settings
            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("bashpoddenxjob@gmail.com", "HejHej123");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            }


            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactform", new { id = contactform.Id }, contactform);
        }



        private bool ContactformExists(int id)
        {
            return _context.Contactform.Any(e => e.Id == id);
        }
    }
}
