using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ContactManagerEF.Models
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
        [Display(Name = "Email Type")]
        [DisplayName("Email Type:")]
        [Column(TypeName = "nvarchar(15)")]
        public EmailTypes EmailType { get; set; }

        [Required]
        [MaxLength(150)]
        [StringLength(150, ErrorMessage = "Email address cannot be longer than 150 characters.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        [Display(Name = "Email Address")]
        [DisplayName("Email Address:")]
        [Column(TypeName = "nvarchar(150)")]
        public string EmailAddress { get; set; }

        [Required]
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

    }
}
