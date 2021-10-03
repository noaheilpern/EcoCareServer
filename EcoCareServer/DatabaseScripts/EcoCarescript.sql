Use master
Create Database EcoCareDB
Go

Use EcoCareDB
Go


Create Table Users (
UserName nvarchar primary key,
Email nvarchar(100) not null,
FirstName nvarchar(30) not null,
LastName nvarchar(30) not null,
UserPswd nvarchar(30) not null,
CONSTRAINT UC_Email UNIQUE(Email)
)

Go
Create Table RegularUser (
UserName nvarchar primary key,
DateOfBirth date not null, 
Country nvarchar(30) not null, 
City nvarchar(30) not null, 
Street nvarchar(30) not null, 
InitialMeatMeals int, 
VeganRareMeat bit,
Vegetarian bit, 
ModeOfTransportation nvarchar(30) not null, 
KilometersFromHomeToWork double not null, 
LastPaymentForElectricity double(10) not null, 
PeopleAtTheSameHousehold int not null, 
Stars int not null,
)

Go 
Create Table DateAndData (
DateWeekStart date primary key, 
UserName nvarchar primary key,
KilometerWalked double not null, 
ElectricityUsed double not null, 
MeatsMeals int not null, 
) 
Go 

Create Table Seller (
UserName nvarchar primary key,
PhoneNumber int not null, 
Country nvarchar(30) not null, 
City nvarchar(30) not null, 
Street nvarchar(30) not null, 
)
Go 

Create Table Sales (
BuyerUserName nvarchar,
ProductTitle nvarchar,
Quantity int not null, 
primary key (BuyerUserName, ProductTitle),
)
INSERT INTO Users VALUES ('kuku@kuku.com','kuku','kaka','1234');
GO
