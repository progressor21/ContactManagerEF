using ContactManagerEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerEF.CMData
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

        //bool SaveChanges();
        //IEnumerable<Contact> GetAllContacts();
        //Contact GetContactById(int id);
        //void CreateContact(Contact contact);
        //void UpdateContact(Contact contact);
        //void DeleteContact(Contact contact);

    }
}
