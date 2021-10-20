using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CMDataBLL.Models
{
    public class ContactEmailAddress
    {
        [Key]
        public int Id { get; set; }

        public ContactEmailAddress()
        {
            this.Id = 0;
            this.EmailType = EmailTypes.Business;
            this.EmailAddress = "";
            this.ContactId = 0;
        }
        public ContactEmailAddress(int cID)
        {
            this.Id = 0;
            this.EmailType = EmailTypes.Business;
            this.EmailAddress = "";
            this.ContactId = cID;
        }

        public enum EmailTypes
        { 
            [Description("Business")]
            Business,
            [Description("Personal")]
            Personal
        }

        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public EmailTypes EmailType { get; set; }

        [Required]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "nvarchar(150)")]
        public string EmailAddress { get; set; }

        [Required]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

    }
}
