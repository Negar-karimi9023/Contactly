using Contactly.Data;
using Contactly.Models;
using Contactly.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contactly.Controllers
{
    [Route("api/Contacts")]
    //[Route("api/Contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactlydbContext dbContext;

        public ContactsController(ContactlydbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts = dbContext.Contacts.ToList();
            return Ok(contacts);
        }

        [HttpPost]
        public IActionResult AddContact(AddContactRequestDTO request)
        {
            var domainModelContact = new Contact()
            {
                Id = new Guid(),
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                Favorite = request.Favorite
            };
            dbContext.Contacts.Add(domainModelContact);
            dbContext.SaveChanges();
            return Ok(domainModelContact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult deleteContact(Guid id)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact is not null)
            {
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(Guid id, EditContactRequestDTO request)
        {
            if (id != request.Id)
            {
                return BadRequest("Id is mistach");
            }
            var contact=dbContext.Contacts.Find(id);
            if (contact is not null)
            {
                contact.Name=request.Name;
                contact.Phone=request.Phone;
                contact.Email=request.Email;
                contact.Favorite=request.Favorite;

                dbContext.Contacts.Update(contact);
                dbContext.SaveChanges();
            }
                return Ok();
        }
    }
}
