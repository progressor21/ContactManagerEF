using ContactManagerEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerEF.CMData
{
    public class MockContactManagerRepository : IContactManagerRepository, IDisposable
    {
        private readonly ContactManagerContext _context;
        public MockContactManagerRepository(ContactManagerContext context)
        {
            this._context = context;
        }

        public Task<Contact> CreateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetAllContacts()
        {
            var contacts = new List<Contact>
            {
                //new Contact{ContactId=1, FirstName="Andrey", LastName="Danilin", ContactEmailAddresses={new ContactEmailAddress{Id = 1, EmailType = "Personal", EmailAddress = "adanilin@danlin.us", ContactId = 1 }, new ContactEmailAddress { Id = 2, EmailType = "Business", EmailAddress = "Andrey.Danilin@saberin.com", ContactId = 1 }} },
                //new Contact{ContactId=2, FirstName="John", LastName="Doe", ContactEmailAddresses={new ContactEmailAddress{Id = 3, EmailType = "Personal", EmailAddress = "JohnDoe@gmail.com", ContactId = 2 }, new ContactEmailAddress { Id = 4, EmailType = "Business", EmailAddress = "John.Doe@abc.com", ContactId = 2 }} },
                //new Contact{ContactId=3, FirstName="Mark", LastName="Waits", ContactEmailAddresses={new ContactEmailAddress{Id = 5, EmailType = "Personal", EmailAddress = "Mark.Waits123@hotmail.com", ContactId = 3 }} }
            };

            foreach (Contact c in contacts)
            {
                _context.Contacts.Add(c);
            }
            return _context.Contacts.ToList();
            //throw new NotImplementedException();
        }

        public List<Contact> GetAllContacts(int? pageNumber)
        {
            //var contacts = new List<Contact>;
            return _context.Contacts.ToList();
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            var contacts = new List<Contact>
            {
                //new Contact{ContactId=1, FirstName="Andrey", LastName="Danilin", ContactEmailAddresses={new ContactEmailAddress{Id = 1, EmailType = "Personal", EmailAddress = "adanilin@danlin.us", ContactId = 1 }, new ContactEmailAddress { Id = 2, EmailType = "Business", EmailAddress = "Andrey.Danilin@saberin.com", ContactId = 1 }} },
                //new Contact{ContactId=2, FirstName="John", LastName="Doe", ContactEmailAddresses={new ContactEmailAddress{Id = 3, EmailType = "Personal", EmailAddress = "JohnDoe@gmail.com", ContactId = 2 }, new ContactEmailAddress { Id = 4, EmailType = "Business", EmailAddress = "John.Doe@abc.com", ContactId = 2 }} },
                //new Contact{ContactId=3, FirstName="Mark", LastName="Waits", ContactEmailAddresses={new ContactEmailAddress{Id = 5, EmailType = "Personal", EmailAddress = "Mark.Waits123@hotmail.com", ContactId = 3 }} }
            };

            foreach (Contact c in contacts)
            {
                await _context.Contacts.AddAsync(c);
            }

            return  await _context.Contacts.ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchString)
        {
            //List<Contact> _contacts = _context.Contacts.ToList();
            //_contacts = _context.Contacts.Where(c => c.LastName.Contains(searchString) || c.FirstName.Contains(searchString)).ToListAsync();
            return await _context.Contacts.Where(c => c.LastName.Contains(searchString) || c.FirstName.Contains(searchString)).ToListAsync();
        }

        public Task<Contact> GetContactByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
        public bool ContactExist(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<ContactEmailAddress>> GetContactEmailsAsync(int cId)
        //{
        //    throw new NotImplementedException();
        //}

        public void Dispose()
        {
            //GC.SuppressFinalize(true);
        }

        //Class Destructor
        ~MockContactManagerRepository()
        {
            Console.WriteLine("Unloaded");
        }

        //public void CreateContact(Contact contact)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public IEnumerable<Contact> GetAllContacts()
        //{
        //    var contacts = new List<Contact>
        //{
        //    new Contact{ContactId=1, FirstName="Andrey", LastName="Danilin", ContactEmailAddresses={new ContactEmailAddress{Id = 1, EmailType = "Personal", EmailAddress = "adanilin@danlin.us", ContactId = 1 }, new ContactEmailAddress { Id = 2, EmailType = "Business", EmailAddress = "Andrey.Danilin@saberin.com", ContactId = 1 }} },
        //    new Contact{ContactId=2, FirstName="John", LastName="Doe", ContactEmailAddresses={new ContactEmailAddress{Id = 3, EmailType = "Personal", EmailAddress = "JohnDoe@gmail.com", ContactId = 2 }, new ContactEmailAddress { Id = 4, EmailType = "Business", EmailAddress = "John.Doe@abc.com", ContactId = 2 }} },
        //    new Contact{ContactId=3, FirstName="Mark", LastName="Waits", ContactEmailAddresses={new ContactEmailAddress{Id = 5, EmailType = "Personal", EmailAddress = "Mark.Waits123@hotmail.com", ContactId = 3 }}}
        //};

        //    return contacts;
        //}

        //public Contact GetContactById(int id)
        //{
        //    return new Contact { ContactId = 1, FirstName = "Andrey", LastName = "Danilin", ContactEmailAddresses = { new ContactEmailAddress { Id = 1, EmailType = "Personal", EmailAddress = "adanilin@danlin.us", ContactId = 1 }, new ContactEmailAddress { Id = 2, EmailType = "Business", EmailAddress = "Andrey.Danilin@saberin.com", ContactId = 1 } } };
        //}

        //public bool SaveChanges()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void UpdateContact(Contact contact)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public void DeleteContact(Contact contact)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
