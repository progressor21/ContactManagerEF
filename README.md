Simple Contact Manager application

Project Description:
--------------------
Applicaion is developed/built using C# and Microsoft ASP.NET Core 5.0 with MVC and Entity Framework (EF Core), jQuery/AJAX, Javacript in MS Visual Studio 2019 with a SQL Server database (SQLExpress) running locally.

Requirements:
-------------
1. Use MS Entity Framework (EF Core) to interface with the DB
	* Code First approach
	* Use migration to create the database and seed the demo/test data
2. Use jQuery/Ajax posts only
3. Separate Data/Business logic into a different project (CMDataBL) from the UI project (ContactManagerEF)
4. On New (Add New) and Edit Contact, treat the EmailAddresses as a collection
	* Allow adding/removing contact's Email Addresses from the contact object
	* Do not persist the EmaiAddresses collection until the "Save" button is clicked
	
Schema:
-------
* Contact (table Cotacts)
	* First Name
	* Last Name
	* EmailAddresses (table ContactEmailAddresses)
		* EmaiType (enum: Personal, Business)
		* EmailAddress (string, data constrainted to "email" format)

Main Appication Features:
-------------------------
1. Contacts' List presented in GridView 
	* used simple table-based Grid design
2. Fuzzy Search/Filter
	* Searching in one "Search" textbox should search all fields using "contains" search approach
	* Filter the grid as the user types in the search box
3. "Add New Contact" button
	* Opens new contact form in a popup window (div)
	* Enforce unobtrusive validation on at least one element
	* Use Javacript, jQuery/Ajax to post data
	* On Save Success to close the window
	* Gracefully alert user to save failures
4. Double-click on a grid row to edit (or click on an "Edit" buton)
	* Opens edit contact form in a popup window (div not iframe)
	* Enforce unobtrusive validation on at least one element
	* Use Javacript, jQuery/Ajax to post data
	* On Save Success close the window
	* Gracefully alert user to save failures
5. Delete/Remove contact button 
	* Use Javacript, jQuery/Ajax
6. Click on a "Details" button in a contacts row
	* Opens contact's details form with all email addresses in a popup window (div not iframe)
	* Click on a email address to send an email to the contact


The current version of the ContactManagerEF application imlements all Main Appication Features above.
There are  a few known bugs that will be fixed in the next version of this app.
And there are some improvments (To Do List) that can be made/implemented in the next iteration/version of this app:

To Do List:
-----------
1. Implement GridView with pagination
2. Fix a bug in the Fuzzy Search/Filter Grid feature:
	* when you deleting text and delete the last remaing letter in the search input box it does not reset the grid. 
	You have to either push the "Enter" button on your keyboard or click the "Reset" button
3. Handle Concurrency conflicts
	* Implement Optimistic Concurrency (Entity Framework Core provides no built-in support for Pessimistic concurrency (locking))
4. Implement logging
	* SQL logging
	* Application logging (events log)
5. Implement/fix unobtrusive validation on all New/Edit form's elements

Also, not implemented in the first version of the app and on To Do List for the next Release:
Optional Appication Features and Functionality:
-----------------------------------------------
1. Data Import
	* Allow uploading CSV file to the website
	* Use a dropzone (drag/drop file to webpage)
	* Import contact data from uploaded CSV file
	* Indicate success/failure
2. Implement Hangfire (hangfire.io)
	* Create a new column Contact.DisplayName (as a migration)
	* After a contact is saved, asynchronously update DisplayName from First and Last name
3. Implement SignalR
	* When a record is edited, update the grid in all browsers open to the "search contacts" grid


=======================
Running the Application
=======================
The application requires a recent version of .NET Core Framework (5.0.9 or later), local copy of the SQL Server (SQL Express) to run.
And MS Visual Studio 2019 for a code review.

If you clone the ContactManagerEF repo locally on your machine you can open the projects' solution in MS Visual Studio 2019.
To build ContactManagerEF database and seed the test data on local SQL server instance: Server=(local)\\SQLEXPRESS open a Package Manager Console in VS and run the following command:
Update-Database
To run the app from the Visual Studio press Ctrl+F5


To run the application locally on your machine please follow these instructions:
--------------------------------------------------------------------------------
1. Download publish.zip file from https://github.com/progressor21/ContactManagerEF/blob/master/publish.zip to your local machine.
2. Extract all files to a local "publish" folder.
3. On your local machine, go into this "publish" folder (like "C:\publish"). 
4. Run the ContactManagerEF application by executing the ContactManagerEF.exe file in this folder.
5. Go to a browser and navigate to this URL: http://localhost:5000/

The application will create the ContactManagerEF database and seed the demo/test data for three contact records on the first run.

Once you finished working/testing the app and close the web app in your browser, find a CommandPrompt window on your desktop running ContactManagerEF.exe and press Ctrl+C to shut down the application and close the hosting window.

