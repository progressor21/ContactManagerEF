using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CMDataBLL.Models
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
        [Column(TypeName = "nvarchar(60)")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [MaxLength(100)]
        public string LastName { get; set; }

        public virtual ICollection<ContactEmailAddress> ContactEmailAddresses { get; set; }

    }
}
