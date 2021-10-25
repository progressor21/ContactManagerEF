using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactManagerEF.CMData;
using ContactManagerEF.Models;
using static ContactManagerEF.HelperUtlity;

namespace ContactManagerEF.Controllers
{
    public class ContactsController : Controller
    {
        //private readonly ContactManagerContext _context;
        //private readonly MockContactManagerRepository _repository = new MockContactManagerRepository();
        private readonly IContactManagerRepository _repository;

        public ContactsController(IContactManagerRepository repository)
        {
            _repository = repository;
        }

        // GET: Contacts - Replaced with the Index action below - with parameters
        //public async Task<IActionResult> Index()
        //{
        //    var contactItems = _repository.GetAllContactsAsync();
        //    return View(await contactItems);
        //}

        // GET: Contacts
        // GET: Contacts/Index/?Searchstring (with a search/filter parameter)
        public async Task<IActionResult> Index(string searchstring)
        {
            //ViewData["CurrentFilter"] = searchstring;
            if (!String.IsNullOrEmpty(searchstring))
            {
                var filterContacts = await _repository.SearchContactsAsync(searchstring);
                return Json(new { isValid = true, html = HelperUtlity.RenderRazorViewToString(this, "_ViewAllContacts", filterContacts.ToList()) });
            }
            else
            {
                //var allContacts = await _repository.GetAllContactsAsync();
                //return Json(new { isValid = true, html = HelperUtlity.RenderRazorViewToString(this, "_ViewAllContacts", allContacts.ToList()) });
                return View(await _repository.GetAllContactsAsync());
            }

            //return View(await _context.Contacts.ToListAsync());
        }

        // GET: Contacts/Details/5
        [NoDirectAccess]
        public async Task<IActionResult> Details(int id)
        {
            var contactFound = await _repository.GetContactByIdAsync(id);

            if (contactFound == null)
            {
                return NotFound();
            }

            return View(contactFound);
        }

        // GET: Contacts/Create
        [NoDirectAccess]
        public IActionResult Create(int id = 0)
        {
            Contact newContact = new Contact();
            newContact.ContactEmailAddresses.Add(new ContactEmailAddress());

            List<ContactEmailAddress.EmailTypes> emailTypes = Enum.GetValues(typeof(ContactEmailAddress.EmailTypes)).Cast<ContactEmailAddress.EmailTypes>().ToList();
            ////This is for use with the partial view "_EmailPartial"
            //ViewBag.RequiredEmailType = new SelectList(emailTypes); 
            ////******
            ViewBag.RequiredEmailType = emailTypes;
            return View(newContact);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,FirstName,LastName,item.EmailAddress")] Contact contact)
        {
            var emailIds = Request.Form["item.Id"].ToList();
            var emailType = Request.Form["item.EmailType"].ToList();
            var emailAddresses = Request.Form["item.EmailAddress"].ToList();

            for (int i = 0; i < emailIds.Count; i++)
            {
                if (emailAddresses[i].ToString() != string.Empty)
                {
                    ContactEmailAddress emItem = new ContactEmailAddress()
                    {
                        Id = Int32.Parse(emailIds[i]),
                        EmailType = (ContactEmailAddress.EmailTypes)Enum.Parse(typeof(ContactEmailAddress.EmailTypes), emailType[i]),
                        EmailAddress = emailAddresses[i].ToString(),
                        ContactId = contact.ContactId,
                        Contact = contact
                    };
                    contact.ContactEmailAddresses.Add(emItem);
                }
            }

            if (ModelState.IsValid)
            {
                await _repository.CreateContactAsync(contact);
                //return RedirectToAction(nameof(Index));
                return Json(new { isValid = true, html = HelperUtlity.RenderRazorViewToString(this, "_ViewAllContacts", _repository.GetAllContacts()) });
            }

            List<ContactEmailAddress.EmailTypes> emailTypes = Enum.GetValues(typeof(ContactEmailAddress.EmailTypes)).Cast<ContactEmailAddress.EmailTypes>().ToList();
            ////This is for use with the partial view "_EmailPartial"
            //ViewBag.RequiredEmailType = new SelectList(emailTypes); 
            ////******
            ViewBag.RequiredEmailType = emailTypes;
            contact.ContactEmailAddresses.Add(new ContactEmailAddress());
            //return View(contact);
            return Json(new { isValid = false, html = HelperUtlity.RenderRazorViewToString(this, "Create", contact) });
        }

        // GET: Contacts/Edit/5
        [NoDirectAccess]
        public async Task<IActionResult> Edit(int id)
        {
            List<ContactEmailAddress.EmailTypes> emailTypes = Enum.GetValues(typeof(ContactEmailAddress.EmailTypes)).Cast<ContactEmailAddress.EmailTypes>().ToList();

            var contact = await _repository.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ////This is for use with the partial view "_EmailPartial"
            //ViewBag.RequiredEmailType = new SelectList(emailTypes); 
            ////******
            ViewBag.RequiredEmailType = emailTypes;
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,FirstName,LastName,,item.EmailAddress")] Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            var emailIds = Request.Form["item.Id"].ToList();
            var emailType = Request.Form["item.EmailType"].ToList();
            var emailAddresses = Request.Form["item.EmailAddress"].ToList();

            for (int i = 0; i < emailIds.Count; i++)
            {
                if (emailAddresses[i].ToString() != string.Empty)
                {
                    ContactEmailAddress emItem = new ContactEmailAddress()
                    {
                        Id = Int32.Parse(emailIds[i]),
                        EmailType = (ContactEmailAddress.EmailTypes)Enum.Parse(typeof(ContactEmailAddress.EmailTypes), emailType[i]),
                        EmailAddress = emailAddresses[i].ToString(),
                        ContactId = contact.ContactId,
                        Contact = contact
                    };
                    contact.ContactEmailAddresses.Add(emItem);
                }
            }

            List<ContactEmailAddress.EmailTypes> emailTypes = Enum.GetValues(typeof(ContactEmailAddress.EmailTypes)).Cast<ContactEmailAddress.EmailTypes>().ToList();
            ////This is for use with the partial view "_EmailPartial"
            //ViewBag.RequiredEmailType = new SelectList(emailTypes); 
            ////******
            ViewBag.RequiredEmailType = emailTypes;

            if (ModelState.IsValid)
            {
                try
                {
                    //ContactEmailAddress cEmailAddresses = TempData["cEmails"] as ContactEmailAddress;
                    await _repository.UpdateContactAsync(contact);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return Json(new { isValid = true, html = HelperUtlity.RenderRazorViewToString(this, "_ViewAllContacts", _repository.GetAllContacts()) });

            }
            //return View(contact);
            return Json(new { isValid = false, html = HelperUtlity.RenderRazorViewToString(this, "Create", contact) });
        }

        public IActionResult AddEmail()
        {
            //List<ContactEmailAddress.EmailTypes> emailTypes = Enum.GetValues(typeof(ContactEmailAddress.EmailTypes)).Cast<ContactEmailAddress.EmailTypes>().ToList();
            //ViewBag.RequiredEmailType = new SelectList(emailTypes);
            ContactEmailAddress emItem = new ContactEmailAddress()
            {
                Id = 0,
                EmailType = ContactEmailAddress.EmailTypes.Business,
                EmailAddress = "",
                //ContactId = 1,
                //Contact = new Contact()
            };

            return PartialView("_EmailsPartial.cshtml", emItem);
        }

        // GET: Contacts/Delete/5
        [NoDirectAccess]
        public async Task<IActionResult> Delete(int id)
        {
            //var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.ContactId == id);
            var contact = await _repository.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteContactAsync(id);
            //return RedirectToAction(nameof(Index));
            return Json(new { isValid = true, html = HelperUtlity.RenderRazorViewToString(this, "_ViewAllContacts", _repository.GetAllContacts()) });
        }

        private bool ContactExists(int id)
        {
            return _repository.ContactExist(id);
        }
    }
}
