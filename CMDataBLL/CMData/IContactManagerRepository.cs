using CMDataBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDataBLL.CMData
{
    public interface IContactManagerRepository
    {
        public List<Contact> GetAllContacts();
        public List<Contact> GetAllContacts(int? pageNumber);
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<IEnumerable<Contact>> SearchContactsAsync(string searchString);
        Task<Contact> GetContactByIdAsync(int id);
        Task<Contact> CreateContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
        bool ContactExist(int id);
        //Task<IEnumerable<ContactEmailAddress>> GetContactEmailsAsync(int Id);

    }
}
