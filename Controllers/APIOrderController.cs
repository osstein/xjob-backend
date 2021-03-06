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
    public class APIOrderController : ControllerBase
    {
        private readonly CatalogDBContext _context;

        public APIOrderController(CatalogDBContext context)
        {
            _context = context;
        }

       


        // POST: api/APIOrder (OrderPostRequest? ?)
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderPostRequest>> PostOrder(OrderPostRequest OrderPostRequest)
        {

            //Behöver hämta in discount från discount codes och kontrollera datum, tid och kod
            //Discountdata 
            var discountData = from DiscountCodes in _context.DiscountCodes select DiscountCodes;
            // Variables to store data
            decimal discountFactor = 0;
            decimal totalDiscountedFactor = 1;
            decimal PriceTotal = 0;
            decimal VatTotal = 0;
            decimal DiscountTotal = 0;
            // Variable for mail construction
            var mailProducts = "";
            //Now variable
            DateTime dateNow = DateTime.Now;
            //Check discount available 
            
            foreach (var item in discountData)
            {
                if (item.Code == OrderPostRequest.Order.DiscountCode && item.CampaignStart < dateNow && item.CampaignEnd > dateNow)
                {
                    discountFactor = item.Discount / 100;
                    totalDiscountedFactor = 1 - discountFactor;
                }
            }
            //Save instance of order
            OrderPostRequest.Order.OrderNumber = "OW-" + DateTime.Now.ToString("yyyyMMddssfff");
            OrderPostRequest.Order.ReceiptNumber = "RN-" + new Random().Next(11) + "-" + DateTime.Now.ToString("yyyyMMddssfff");
            _context.Order.Add(OrderPostRequest.Order);
            await _context.SaveChangesAsync();
           

            // Add items with id for order
            foreach (var item in OrderPostRequest.OrderProducts)
            {
                //Connectiing the id
                item.OrderId = OrderPostRequest.Order.Id;
                // Get product for price data etc. in database
                var product = await _context.Product.FindAsync(item.ProductId);


                //Add VAT to total
                var newVat = (product.Vat / 100) * product.Price * item.Amount;
                if (product.Discount != 0)
                {
                    var ProductDiscountFactor = (1 - (product.Discount / 100));
                    newVat = product.Price * item.Amount * ProductDiscountFactor * (product.Vat / 100);
                }

                VatTotal += newVat;
                // Price from database

                item.Price = product.Price;

                //Add discount
                if (product.Discount != 0)
                {
                    var ProductDiscountFactor = (100 - product.Discount) / 100;
                    item.Price = product.Price * ProductDiscountFactor;
                }
                // Add price to price total with the amount
                PriceTotal += item.Price * item.Amount;
                //Set the product article number
                item.ProductNumber = product.ProductNumber;

                //Update the amount in the database
                var type = await _context.ProductType.FindAsync(item.TypeId);
                type.Amount -= item.Amount;
                if (type.Amount <= 0)
                {
                    type.Amount = 0;
                }
                //Add color and size
                var Color = await _context.ProductColor.FindAsync(type.ProductColorId);
                var Size = await _context.ProductSize.FindAsync(type.ProductSizeId);
                item.ProductColor = Color.Color;
                item.ProductSize = Size.Size;
                //Add ProductNumber from DB
                var Product = await _context.Product.FindAsync(item.ProductId);
                item.ProductNumber = Product.ProductNumber;

                mailProducts += "<tr><td>" + item.Amount + "</td><td>" + product.Name + "</td><td>" + product.ProductNumber + "</td><td>" + item.ProductColor + "</td><td>" + item.ProductSize + "</td><td>" + (item.Price * item.Amount) + "</td></tr>";

                _context.OrderProducts.Add(item);
                await _context.SaveChangesAsync();
            }
            //Total discount
            DiscountTotal = PriceTotal * discountFactor;
            // Set price values
            OrderPostRequest.Order.PriceTotal = PriceTotal;
            OrderPostRequest.Order.VatTotal = VatTotal;
            OrderPostRequest.Order.DiscountTotal = DiscountTotal;
            OrderPostRequest.Order.Status = "New";

            // Update and save
            _context.Order.Update(OrderPostRequest.Order);
            await _context.SaveChangesAsync();

            //Mail the order to customer
            MailMessage msg = new MailMessage();
            // Set mail parameters
            msg.From = new MailAddress("bashpoddenxjob@gmail.com");
            msg.To.Add(OrderPostRequest.Order.CustomerMail);
            msg.Subject = "BASHPODDEN - Din Beställning:" + OrderPostRequest.Order.ReceiptNumber + " - " + OrderPostRequest.Order.Timestamp;
            msg.Body = "<h1>Hej " + OrderPostRequest.Order.CustomerFirstName + " " + OrderPostRequest.Order.CustomerLastName + "!</h1><br><p>Här kommer en bekräftelse på din beställning med kvittonummer:" + OrderPostRequest.Order.ReceiptNumber + "</p><table style='width:100%; text-align:left;'><thead><tr><th>Antal</th><th>Produkt</th><th>Artikel Nummer</th><th>Färg</th><th>Storlek</th><th>Pris</th></tr></thead><tbody>" + mailProducts + "<tr><td></td><td></td><td></td><td></td><td style='text-align:right;' >Summa:</td><td>" + OrderPostRequest.Order.PriceTotal + "</td></tr><tr><td></td><td></td><td></td><td></td><td style='text-align:right;'>Varav Moms:</td><td>" + OrderPostRequest.Order.VatTotal + "</td></tr><tr><td></td><td></td><td style='text-align:right;'>Rabattkod:</td><td>" + OrderPostRequest.Order.DiscountCode + "</td><td style='text-align:right;'>Total Rabatt:</td><td>" + OrderPostRequest.Order.DiscountTotal + "</td></tr><tr><td></td><td></td><td></td><td></td><td style='text-align:right;' >Summa efter rabatt:</td><td>" + (OrderPostRequest.Order.PriceTotal - OrderPostRequest.Order.DiscountTotal) + "</td></tr></tbody></table><p>Observera att detta mail också gäller som kvitto på din beställning</p><ul><li>Beställare: " + OrderPostRequest.Order.CustomerFirstName + " " + OrderPostRequest.Order.CustomerLastName + "</li><li>Telefonnummer: " + OrderPostRequest.Order.CustomerPhone + "</li><li>E-mail: " + OrderPostRequest.Order.CustomerMail + "</li><li>Kvittonummer: " + OrderPostRequest.Order.ReceiptNumber + "</li><li>Ordernummer: " + OrderPostRequest.Order.OrderNumber + "</li></ul><ul><li>Bashpodden</li><li>Gatan 3, 137 19, Långtbortistan</li><li>Bankgiro: 9845-789</li><li>" + OrderPostRequest.Order.Timestamp + "</li></ul><h2>Hälsningar Bashpodden</h2>";
            //Allow html and css design in mail
            msg.IsBodyHtml = true;
            //msg.Priority = MailPriority.High;

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



            return CreatedAtAction("GetOrder", new { id = OrderPostRequest.Order.Id }, _context.Order);
        }


        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}