
Use master


Create Database EcoCareDB
Go

Use EcoCareDB
Go



CREATE TABLE Product(
    Title nvarchar not null,
    Price INT not null,
    Description nvarchar not null,
    ImageSource nvarchar not null,
    Active bit not null,
    SellersUsername nvarchar not null,
    ProductId int primary key not null
);

CREATE TABLE Users(
    UserName nvarchar primary key not null,
    Email nvarchar not null,
    Pass nvarchar not null,
    FirstName nvarchar not null,
    LastName nvarchar not null,
    IsAdmin bit not null,
    CONSTRAINT UC_Email UNIQUE(Email)

);

CREATE TABLE RegularUser(
    UserName nvarchar primary key foreign key references Users(UserName) not null,
    Birthday date not null,
    Country nvarchar not null,
    InitialMeatsMeals int not null,
    VeganRareMeat bit not null,
    Vegetarian bit not null,
    Transportation nvarchar not null,
    DistanceToWork float not null,
    LastElectricityBill float not null,
    PeopleAtTheHousehold int not null,

);

CREATE TABLE Seller(
    UserName nvarchar primary key not null,
    PhoneNum nvarchar not null,
    Country nvarchar not null,
    City nvarchar not null,
    Street nvarchar not null
);

CREATE TABLE Sales(
    BuyerUserName nvarchar foreign key references RegularUser not null,
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
    UserName nvarchar foreign key references RegularUser not null
);

CREATE TABLE Goals(
    DateT date primary key not null,
    Goal int not null,
    UserName nvarchar foreign key references RegularUser not null,
);
