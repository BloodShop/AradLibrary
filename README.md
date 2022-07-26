# AradLibrary
Our application is a simple way to view what kind of Library Items we have in our stock and make a quick orders. Everyone can access this application without any commitment you can browse the application as a guest without logging in, or you could just simply register as a customer and you will remain our lovely user. 

# Main Purposes of Arad City Library Application:
1.	Modify any new Item into stock as administrator.
2.	Search and Explore certain Library Items.
3.	Purchase or Borrow and kind of Items you like.
4.	Easy using as possible for the customer and the administrator.
5.	Make things clarify and manage easily.

-	Arad City Library made for windows Application users

Architecture and technological requirements
1. Infrastructures
	a. DataBase Architecture
		I. DataMock
		II. JsonTextFile
	b. Web Application Client Side Platform
		I. UWP C#
2. Internationalization
	a. Languages
		I. English
		II. Hebrew
3. Unit Tests
	a. Person.Model
	b. Library.Model
	c. Library.DAL

# Use cases and functionality
User View Application:
As user intro page will open up after loading an excisting Json file or DataMock. In the log In page the user can create new registration or log in with existing username and password. If the user doesn’t enter the correct username / password he won’t be able to log In and an error message will pop. Also an opportunity to browse as a Guest. 

The SearchPage will open up and the user will be able to search for any kind of item by his preferences. Also the user is able to change his password.
 
As the user selects the preferred item he will continue to the next pages where he can see more information, reviews, specific data about the item and etc. User also can add a review to the selected item and also read reviews from previous reading experiences.

At the logIn page the user can create a new username with his unique data, and it will be saved forever. 
Required Fields: Name, password, City, Street and House number.
Optional: House Entry, Postal code.
At the final stage the user is able to purchase / borrow any kind of item he will select. The final step shows the user his Address that the item is being sent to and a message the day he has to retrieve the item (if he borrowed the item) as a guest the last stage requires him to fill the address he want the shipping to arrive and can not borrow an item only purchase, In contrast to the  customer which can also purchase and borrow any item.
 
As any user the is an Navigation Panel which can be accessed at any page to navigate to certain pages or changes like: 
1.	Returning to the MainPage
2.	Changing Password
3.	Exiting the application
4.	Registration

# Management panel
The management panel will be used by both the management members and the users of the application and therefore it will contain within it
A permission-managed information mechanism, that is, information that is displayed or can be edited only by those with suitable permission.
The management panel will be based on the template attached at the beginning of this document (on the architecture page).
# Permissions –
A.	Manager -  There is access to all the information in the system:
    1.	Adding new items to stock.
    2.	Delete / Update any item.
    3.	Set / End Sale on all items.
    4.	Return any item to stock that was loaned.
    5.	Search / Delete users
B.	Employee – 
    1.	Get certain information for the delivery.
    2.	Adding new items to stock.
    3.	Return any item to stock that was loaned.
C.	Customer –
    1.	Search any Item and purchase / borrow
    2.	React / Review to any item
    3.	Change password
D.	Guest – 
    1.Search any Item and purchase only
    2. React / Review to any item
    3. Doesn’t have sale on any item
    
# The workers panel pages
The administrator ‘main page’ more likely to be administration page is easy to used and to set new things. 
This section is shown to site Managers and Employees only.
This section allows administrators to do the following:
    1.	Add new Item to stock.
    2.	Find / Search for any existing item.
    3.	View all loaned items.
    4.	Set / End sale on all items.
    5.	Editing an existing item with certain fields.
    6.	Exploring new items at the browsing explorer.
 
At the Navigation Bar can change Password, navigate to the MainPage, Exit and etc.
Add item / Edit Exciting item, the manager must fill required fields and there are some optional fields as well.
Required Fields: 
    1.	Title name
    2.	Item’s type (Book, Journal, Magazine and etc..)
    3.	Price
    4.	Amount of pages
    5.	Publish Date
As any selected item there are different optional fields
Common Optional Fields: Sale, Genres.

Book Optional Fields: Authors, Publisher, Revision, Serial Number, Synopsis.

Journal Optional Fields: Contributors, Editors, Frequency. 

# Manage / View Loaned Library Items:
The manager and Employee can see what items are being loaned and who is the owner at this moment and can also press on any of them to retrieve the item to the stock.

As the manager only Can press at the navigation view panel and open the Users info so he can Delete User’s account or retrieve all items to stock.
As the administrator while searching any item the options Delete and Update are being enabled for any kind of item.
The Employee also has access to the loaned Items page but doesn’t have permission to access the user list and delete accounts.
For example selecting an item immediately pops up these buttons and can be pressed and seen by the manager and employee.



