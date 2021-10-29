using ContactManagerEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerEF.CMData
{
    public class SqlContactManagerRepository : IContactManagerRepository, IDisposable
    {
        private readonly ContactManagerContext _context;

        public SqlContactManagerRepository(ContactManagerContext context)
        {
            this._context = context;
        }

        public List<Contact> GetAllContacts()
        {
            return _context.Contacts.ToList();
            //throw new NotImplementedException();
        }

        //TO DO: Implement pagination for the GridView:
        //Fetch records in a part instead of complete records list using Linq's Skip and Take methods
        //Skip() method skip record, for the first page it passes 0 and for the rest of the pages (pagesize *(pagenumber -1)).
        //Take() is similar to top from SQL query, it is used to take exact number of records. 
        public List<Contact> GetAllContacts(int? _pageNumber)
        {
            //Contact cModel = new Contact();
            //cModel.PageNumber = (_pageNumber == null ? 1 : Convert.ToInt32(_pageNumber));
            //cModel.PageSize = 10; 
            
            //List<Contact> _contacts = _context.Contacts.ToList();

            //cModel.Products = _contacts.OrderBy(x => x.ContactId)
            //                  .Skip(cModel.PageSize * (cModel.PageNumber - 1))
            //                  .Take(cModel.PageSize).ToList();

            //cModel.TotalCount = _contacts.Count();
            //var page = (cModel.TotalCount / cModel.PageSize) -
            //           (cModel.TotalCount % cModel.PageSize == 0 ? 1 : 0);
            //cModel.PagerCount = page + 1;
            
            return _context.Contacts.ToList();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchString)
        {
            //List<Contact> _contacts = _context.Contacts.ToList();
            //_contacts = _context.Contacts.Where(c => c.LastName.Contains(searchString) || c.FirstName.Contains(searchString)).ToListAsync();
            return await _context.Contacts.Where(c => c.LastName.ToUpper().Contains(searchString.ToUpper()) || 
                                                    c.FirstName.ToUpper().Contains(searchString.ToUpper()) || 
                                                    c.ContactEmailAddresses.Any(ec => ec.EmailAddress.Contains(searchString.ToUpper()))).ToListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            //Contact c = await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);
            //var cEmailItems = _context.ContactEmailAddresses.Where(ce => ce.ContactId == id);
            //foreach (ContactEmailAddress ceItem in cEmailItems)
            //{
            //    c.ContactEmailAddresses.Add(ceItem);
            //}

            Contact c = await _context.Contacts
                        .Include(ce => ce.ContactEmailAddresses)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.ContactId == id);

            return c;
            //return await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            foreach (var emItem in contact.ContactEmailAddresses)
            {
                _context.ContactEmailAddresses.Add(emItem);
            }

            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            //ContactEmailAddress uCEA = new ContactEmailAddress;

            List<int> updContactEmailIds = new List<int>();

             _context.Entry(contact).State = EntityState.Modified;
            foreach (var emItem in contact.ContactEmailAddresses)
            {
                updContactEmailIds.Add(emItem.Id);
                _context.Entry(emItem).State = emItem.Id == 0 ? EntityState.Added : EntityState.Modified;
                //_context.Entry(emItem).State = EntityState.Modified;
            }

            List<ContactEmailAddress> eContactEmailAdresses = _context.ContactEmailAddresses.Where(ce => ce.ContactId == contact.ContactId).ToList();

            foreach (var eItem in eContactEmailAdresses)
            {
                if (!updContactEmailIds.Contains(eItem.Id))
                {
                    _context.Entry(eItem).State = EntityState.Deleted;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            //var contactToRemove = await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);
            var contactToRemove = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contactToRemove);
            await _context.SaveChangesAsync();
        }

        public bool ContactExist(int id)
        {
            return _context.Contacts.Any(c => c.ContactId == id);
        }

        public void Dispose()
        {
            //GC.SuppressFinalize(true);
        }

        //Class Destructor
        ~SqlContactManagerRepository()
        {
            Console.WriteLine("Unloaded");
        }
    }
}
