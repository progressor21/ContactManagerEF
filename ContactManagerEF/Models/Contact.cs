using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ContactManagerEF.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactEmailAddresses = new HashSet<ContactEmailAddress>();
            //ContactEmailAddresses.Add(new ContactEmailAddress(0));
        }

        [Key]
        public int ContactId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "First Name must contain letters only and start with a capital letter")]
        [Column(TypeName = "nvarchar(60)")]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        [DisplayName("First Name:")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Last Name must contain letters only and start with a capital letter")]
        [Column(TypeName = "nvarchar(100)")]
        [MaxLength(100)]
        [Display(Name = "Last Name")]
        [DisplayName("Last Name:")]
        public string LastName { get; set; }

        public virtual ICollection<ContactEmailAddress> ContactEmailAddresses { get; set; }

        //public List<ContactEmailAddress> GetContactEmailAddress(int ContactId)
        //{

        //}

    }
}
