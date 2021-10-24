
Use master


Create Database EcoCareDB
Go

Use EcoCareDB
Go



CREATE TABLE Product(
    Title nvarchar(255) not null,
    Price INT not null,
    Description nvarchar(255) not null,
    ImageSource nvarchar(255) not null,
    Active bit not null,
    SellersUsername nvarchar(255) not null,
    ProductId int primary key not null
);

CREATE TABLE Users(
    UserName nvarchar(255) primary key not null,
    Email nvarchar(255) not null,
    Pass nvarchar(255) not null,
    FirstName nvarchar(255) not null,
    LastName nvarchar(255) not null,
    IsAdmin bit not null,
    CONSTRAINT UC_Email UNIQUE(Email)

);

CREATE TABLE RegularUser(
    UserName nvarchar(255) primary key foreign key references Users(UserName) not null,
    Birthday date not null,
    Country nvarchar(255) not null,
    InitialMeatsMeals int not null,
    VeganRareMeat bit not null,
    Vegetarian bit not null,
    Transportation nvarchar(255) not null,
    DistanceToWork float not null,
    LastElectricityBill float not null,
    PeopleAtTheHousehold int not null,

);

CREATE TABLE Seller(
    UserName nvarchar(255) primary key not null,
    PhoneNum nvarchar(255) not null,
    Country nvarchar(255) not null,
    City nvarchar(255) not null,
    Street nvarchar(255) not null
);

CREATE TABLE Sales(
    BuyerUserName nvarchar(255) foreign key references RegularUser not null,
    ProductId int foreign key references Product not null,
    DateBought int not null,
    PriceBought int not null,
    SaleId int primary key not null
);

CREATE TABLE UsersData(
    DistanceToWork float not null,
    ElecticityUsagePerWeek float not null,
    MeatsMeals int not null,
    DateT int primary key not null,
    UserName nvarchar(255) foreign key references RegularUser not null
);

CREATE TABLE Goals(
    DateT date primary key not null,
    Goal int not null,
    UserName nvarchar(255) foreign key references RegularUser not null,
);


USE [EcoCareDB]
GO
SELECT * FROM RegularUser
INSERT INTO [dbo].[RegularUser]
           ([UserName]
           ,[Birthday]
           ,[Country]
           ,[InitialMeatsMeals]
           ,[VeganRareMeat]
           ,[Vegetarian]
           ,[Transportation]
           ,[DistanceToWork]
           ,[LastElectricityBill]
           ,[PeopleAtTheHousehold])
     VALUES
           ('l'
           ,'2016-08-27'
           ,'Israel'
           ,'7'
           ,'0'
           ,'0'
           ,'walking'
           ,'0.5'
           ,'234'
           ,'6' )
GO


