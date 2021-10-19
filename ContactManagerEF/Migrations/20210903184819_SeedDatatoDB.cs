using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManagerEF.Migrations
{
    public partial class SeedDatatoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //new Contact { ContactId = 1, FirstName = "Andrey", LastName = "Danilin", ContactEmailAddresses = { new ContactEmailAddress { Id = 1, EmailType = "Personal", EmailAddress = "adanilin@danlin.us", ContactId = 1 }, new ContactEmailAddress { Id = 2, EmailType = "Business", EmailAddress = "Andrey.Danilin@saberin.com", ContactId = 1 } } },
            //new Contact { ContactId = 2, FirstName = "John", LastName = "Doe", ContactEmailAddresses = { new ContactEmailAddress { Id = 3, EmailType = "Personal", EmailAddress = "JohnDoe@gmail.com", ContactId = 2 }, new ContactEmailAddress { Id = 4, EmailType = "Business", EmailAddress = "John.Doe@abc.com", ContactId = 2 } } },
            //new Contact { ContactId = 3, FirstName = "Mark", LastName = "Waits", ContactEmailAddresses = { new ContactEmailAddress { Id = 5, EmailType = "Personal", EmailAddress = "Mark.Waits123@hotmail.com", ContactId = 3 } } }
            //SEEDING
            migrationBuilder.Sql("INSERT INTO [ContactManagerEF].[dbo].[Contacts] (FirstName, LastName) VALUES('Andrey', 'Danilin');");
            migrationBuilder.Sql("INSERT INTO [ContactManagerEF].[dbo].[ContactEmailAddresses] (EmailType, EmailAddress, ContactId) VALUES('Business', 'Andrey.Danilin@saberin.com', @@IDENTITY);");
            migrationBuilder.Sql("INSERT INTO [ContactManagerEF].[dbo].[ContactEmailAddresses] (EmailType, EmailAddress, ContactId) VALUES('Personal', 'adanilin@danilin.us', @@IDENTITY);");

            migrationBuilder.Sql("INSERT INTO [ContactManagerEF].[dbo].[Contacts] (FirstName, LastName) VALUES('John', 'Doe');");
            migrationBuilder.Sql("INSERT INTO [ContactManagerEF].[dbo].[ContactEmailAddresses] (EmailType, EmailAddress, ContactId) VALUES('Personal', 'JohnDoe@gmail.com', @@IDENTITY);");

            migrationBuilder.Sql("INSERT INTO [ContactManagerEF].[dbo].[Contacts] (FirstName, LastName) VALUES('Mark', 'Waits');");
            migrationBuilder.Sql("INSERT INTO [ContactManagerEF].[dbo].[ContactEmailAddresses] (EmailType, EmailAddress, ContactId) VALUES('Personal', 'Mark.Waits123@hotmail.com', @@IDENTITY);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [ContactManagerEF].[dbo].[Contacts];");
            //migrationBuilder.Sql("TRUNCATE Table Contacts;");

        }
    }
}
