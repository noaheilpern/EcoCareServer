
Use master


Create Database EcoCareDB
Go

Use EcoCareDB
Go


CREATE TABLE GraphItem(
	ValueFootPrint float not null, 
	DateGraph Date, 
	); 

CREATE TABLE Product(
    Title nvarchar(255) not null,
    Price INT not null,
    Description nvarchar(4000) not null,
    ImageSource nvarchar(255) not null,
    Active bit not null,
    SellersUsername nvarchar(255) not null,
    ProductId int identity(1,1)  primary key not null
);
CREATE TABLE Countries(
	CountryName nvarchar(255) primary key not null, 
	EF float not null, 
	);


CREATE TABLE Users(
    UserName nvarchar(255) primary key not null,
    Email nvarchar(255) not null,
    Pass nvarchar(255) not null,
    FirstName nvarchar(255) not null,
    LastName nvarchar(255) not null,
    IsAdmin bit not null,
    CONSTRAINT UC_Email UNIQUE(Email),
	Country nvarchar(255) foreign key references Countries(CountryName) not null,


);

CREATE TABLE RegularUser(
    UserName nvarchar(255) primary key foreign key references Users(UserName) not null,
    Birthday date not null,
    InitialMeatsMeals int not null,
    VeganRareMeat bit not null,
    Vegetarian bit not null,
    Transportation nvarchar(255) not null,
    DistanceToWork float not null,
    LastElectricityBill float not null,
    PeopleAtTheHousehold int not null,
	Stars int,
	UserCarbonFootPrint float not null, 

);

CREATE TABLE Seller(
    UserName nvarchar(255) primary key foreign key references Users(UserName) not null,
    PhoneNum nvarchar(255) not null,    
);

CREATE TABLE Sales(
    BuyerUserName nvarchar(255) references RegularUser not null,
	SellerUserName nvarchar(255) foreign key references Seller not null, 
    ProductId int foreign key references Product not null,
    DateBought Date not null,
    PriceBought int not null,
    SaleId int primary key not null,

);

CREATE TABLE DatasCategories(
	CategoryName nvarchar(255) not null, 
	CategoryId int identity(1,1) primary key not null,
	);

CREATE TABLE UsersData(
	CategoryId int not null references DatasCategories,
	CategoryValue float not null,
	CarbonFootprint float, 
    DateT Date not null,
    UserName nvarchar(255) references RegularUser not null,
	primary key (DateT, CategoryId, UserName),
);

CREATE TABLE Goals(
    DateT date primary key not null,
    Goal int not null,
    UserName nvarchar(255) foreign key references RegularUser not null,
);

USE [EcoCareDB]
GO

		   


SELECT * FROM RegularUser
Go

			insert into Countries values ('China','7.38')
			insert into Countries values ('United States','15.52')
			insert into Countries values ('India','1.91')
			insert into Countries values ('Russia','11.44')
			insert into Countries values ('Japan','9.7')
			insert into Countries values ('Germany','9.44')
			insert into Countries values ('Canada','18.58')
			insert into Countries values ('Iran','8.08')
			insert into Countries values ('South Korea','11.85')
			insert into Countries values ('Indonesia','2.03')
			insert into Countries values ('Saudi Arabia','15.94')
			insert into Countries values ('Brazil','2.25')
			insert into Countries values ('Mexico','3.58')
			insert into Countries values ('Australia','17.1')
			insert into Countries values ('South Africa','6.95')
			insert into Countries values ('Turkey','4.61')
			insert into Countries values ('United Kingdom','5.55')
			insert into Countries values ('Italy','5.9')
			insert into Countries values ('France','5.13')
			insert into Countries values ('Poland','7.81')
			insert into Countries values ('Taiwan','11.72')
			insert into Countries values ('Thailand','3.93')
			insert into Countries values ('Malaysia','8.68')
			insert into Countries values ('Spain','5.4')
			insert into Countries values ('Ukraine','5.22')
			insert into Countries values ('Kazakhstan','13.01')
			insert into Countries values ('Egypt','2.32')
			insert into Countries values ('United Arab Emirates','23.37')
			insert into Countries values ('Vietnam','2.2')
			insert into Countries values ('Argentina','4.61')
			insert into Countries values ('Pakistan','0.87')
			insert into Countries values ('Venezuela','5.89')
			insert into Countries values ('Netherlands','9.62')
			insert into Countries values ('Iraq','4.44')
			insert into Countries values ('Algeria','3.85')
			insert into Countries values ('Philippines','1.22')
			insert into Countries values ('Czech Republic (Czechia)','10.53')
			insert into Countries values ('Uzbekistan','3.48')
			insert into Countries values ('Kuwait','25.65')
			insert into Countries values ('Qatar','37.29')
			insert into Countries values ('Belgium','8.34')
			insert into Countries values ('Oman','19.61')
			insert into Countries values ('Nigeria','0.44')
			insert into Countries values ('Chile','4.46')
			insert into Countries values ('Turkmenistan','14')
			insert into Countries values ('Romania','3.98')
			insert into Countries values ('Colombia','1.61')
			insert into Countries values ('Bangladesh','0.47')
			insert into Countries values ('Austria','8.43')
			insert into Countries values ('Greece','6.39')
			insert into Countries values ('Israel','8.04')
			insert into Countries values ('Belarus','6.63')
			insert into Countries values ('North Korea','2.32')
			insert into Countries values ('Morocco','1.64')
			insert into Countries values ('Peru','1.87')
			insert into Countries values ('Libya','8.12')
			insert into Countries values ('Finland','9.31')
			insert into Countries values ('Hungary','5.23')
			insert into Countries values ('Bulgaria','7.11')
			insert into Countries values ('Portugal','4.86')
			insert into Countries values ('Singapore','8.56')
			insert into Countries values ('Hong Kong','6.5')
			insert into Countries values ('Sweden','4.54')
			insert into Countries values ('Norway','8.28')
			insert into Countries values ('Serbia','4.65')
			insert into Countries values ('Ecuador','2.43')
			insert into Countries values ('Switzerland','4.73')
			insert into Countries values ('Ireland','8.32')
			insert into Countries values ('Syria','2.18')
			insert into Countries values ('Denmark','6.65')
			insert into Countries values ('Slovakia','6.77')
			insert into Countries values ('Trinidad and Tobago','25.39')
			insert into Countries values ('Azerbaijan','3.45')
			insert into Countries values ('New Zealand','7.14')
			insert into Countries values ('Angola','1.06')
			insert into Countries values ('Cuba','2.68')
			insert into Countries values ('Tunisia','2.6')
			insert into Countries values ('Bosnia and Herzegovina','7.58')
			insert into Countries values ('Yemen','0.94')
			insert into Countries values ('Bahrain','17.15')
			insert into Countries values ('Dominican Republic','2.26')
			insert into Countries values ('Jordan','2.38')
			insert into Countries values ('Estonia','17.02')
			insert into Countries values ('Lebanon','3.26')
			insert into Countries values ('Bolivia','1.76')
			insert into Countries values ('Croatia','4.61')
			insert into Countries values ('Mongolia','6.08')
			insert into Countries values ('Guatemala','1.12')
			insert into Countries values ('Sri Lanka','0.88')
			insert into Countries values ('Myanmar','0.31')
			insert into Countries values ('Kenya','0.33')
			insert into Countries values ('Montenegro','25.9')
			insert into Countries values ('Slovenia','7.1')
			insert into Countries values ('Ghana','0.51')
			insert into Countries values ('Lithuania','4.74')
			insert into Countries values ('Sudan','0.33')
			insert into Countries values ('Panama','2.87')
			insert into Countries values ('Ethiopia','0.1')
			insert into Countries values ('Luxembourg','17.51')
			insert into Countries values ('Zimbabwe','0.72')
			insert into Countries values ('Côte dIvoire','0.42')
			insert into Countries values ('Afghanistan','0.28')
			insert into Countries values ('Tanzania','0.18')
			insert into Countries values ('Cameroon','0.4')
			insert into Countries values ('Honduras','1.01')
			insert into Countries values ('Papua New Guinea','1.1')
			insert into Countries values ('Jamaica','3.08')
			insert into Countries values ('North Macedonia','4.28')
			insert into Countries values ('Georgia','2.14')
			insert into Countries values ('Costa Rica','1.7')
			insert into Countries values ('Moldova','2.03')
			insert into Countries values ('Senegal','0.55')
			insert into Countries values ('Latvia','4.13')
			insert into Countries values ('Nepal','0.29')
			insert into Countries values ('Brunei','18.28')
			insert into Countries values ('Kyrgyzstan','1.14')
			insert into Countries values ('Cyprus','5.87')
			insert into Countries values ('El Salvador','1.08')
			insert into Countries values ('DR Congo','0.08')
			insert into Countries values ('Benin','0.6')
			insert into Countries values ('Uruguay','1.9')
			insert into Countries values ('Cambodia','0.41')
			insert into Countries values ('Botswana','2.98')
			insert into Countries values ('Tajikistan','0.7')
			insert into Countries values ('Paraguay','0.89')
			insert into Countries values ('Mozambique','0.21')
			insert into Countries values ('Gabon','2.84')
			insert into Countries values ('Nicaragua','0.84')
			insert into Countries values ('Congo','1.05')
			insert into Countries values ('Albania','1.8')
			insert into Countries values ('Uganda','0.13')
			insert into Countries values ('Armenia','1.57')
			insert into Countries values ('Laos','0.66')
			insert into Countries values ('Bahamas','11.65')
			insert into Countries values ('Zambia','0.26')
			insert into Countries values ('South Sudan','0.37')
			insert into Countries values ('Iceland','11.81')
			insert into Countries values ('Namibia','1.65')
			insert into Countries values ('Guyana','4.22')
			insert into Countries values ('Mauritius','2.53')
			insert into Countries values ('Macao','5.07')
			insert into Countries values ('Haiti','0.28')
			insert into Countries values ('Madagascar','0.12')
			insert into Countries values ('Martinique','7.21')
			insert into Countries values ('Mauritania','0.62')
			insert into Countries values ('Guadeloupe','6.2')
			insert into Countries values ('Burkina Faso','0.13')
			insert into Countries values ('New Caledonia','8.53')
			insert into Countries values ('Togo','0.31')
			insert into Countries values ('Malta','5.18')
			insert into Countries values ('Equatorial Guinea','1.77')
			insert into Countries values ('Suriname','3.81')
			insert into Countries values ('Niger','0.1')
			insert into Countries values ('Guinea','0.18')
			insert into Countries values ('Malawi','0.11')
			insert into Countries values ('Fiji','1.95')
			insert into Countries values ('Bhutan','2.28')
			insert into Countries values ('Chad','0.11')
			insert into Countries values ('Mali','0.09')
			insert into Countries values ('Barbados','5.39')
			insert into Countries values ('Djibouti','1.62')
			insert into Countries values ('French Guiana','5.5')
			insert into Countries values ('Rwanda','0.12')
			insert into Countries values ('Sierra Leone','0.17')
			insert into Countries values ('Somalia','0.09')
			insert into Countries values ('Maldives','2.59')
			insert into Countries values ('Réunion','1.3')
			insert into Countries values ('Belize','3.02')
			insert into Countries values ('Burundi','0.1')
			insert into Countries values ('French Polynesia','3.65')
			insert into Countries values ('Liberia','0.18')
			insert into Countries values ('Puerto Rico','0.22')
			insert into Countries values ('Eritrea','0.2')
			insert into Countries values ('Eswatini','0.59')
			insert into Countries values ('Bermuda','10.09')
			insert into Countries values ('Saint Lucia','3.38')
			insert into Countries values ('Gibraltar','16.98')
			insert into Countries values ('Grenada','5.03')
			insert into Countries values ('Central African Republic','0.12')
			insert into Countries values ('Seychelles','5.43')
			insert into Countries values ('Timor-Leste','0.41')
			insert into Countries values ('Antigua and Barbuda','4.64')
			insert into Countries values ('Cayman Islands','6.49')
			insert into Countries values ('St. Vincent & Grenadines','3.31')
			insert into Countries values ('Solomon Islands','0.55')
			insert into Countries values ('Guinea-Bissau','0.18')
			insert into Countries values ('Lesotho','0.15')
			insert into Countries values ('Aruba','2.74')
			insert into Countries values ('Gambia','0.12')
			insert into Countries values ('Tonga','2.49')
			insert into Countries values ('Western Sahara','0.39')
			insert into Countries values ('Saint Kitts & Nevis','3.93')
			insert into Countries values ('Dominica','2.61')
			insert into Countries values ('Samoa','0.87')
			insert into Countries values ('Vanuatu','0.49')
			insert into Countries values ('Comoros','0.14')
			insert into Countries values ('British Virgin Islands','3.4')
			insert into Countries values ('Cabo Verde','0.19')
			insert into Countries values ('Turks and Caicos','1.79')
			insert into Countries values ('Sao Tome & Principe','0.28')
			insert into Countries values ('Kiribati','0.47')
			insert into Countries values ('Falkland Islands','16.59')
			insert into Countries values ('Palau','2.35')
			insert into Countries values ('Cook Islands','2.13')
			insert into Countries values ('Anguilla','2.1')
			insert into Countries values ('Saint Helena','2.2')
			insert into Countries values ('Saint Pierre & Miquelon','1.49')
			insert into Countries values ('Faeroe Islands','0.04')
			insert into Countries values ('Greenland','0.03')




INSERT INTO DatasCategories Values('Meat_Meals')
GO
INSERT INTO DatasCategories Values('Distance')
GO
INSERT INTO DatasCategories Values('Electricity_Usage')
GO


 


INSERT INTO Users VALUES('noa', 'noa@gmail.com', '123456' , 'Noa', 'Heilpern', '0', 'Israel')
INSERT into RegularUser Values('noa', '2016-08-27','7'
			,'0'
           ,'0'
           ,'walking'
           ,'0.5'
           ,'234'
           ,'6', '0', 
		   '30000')

INSERT INTO Product values('Ice Cream', 300, 'A very good ice cream from Golda', 'https://getgolda.co.il/wp-content/uploads/sites/56/2020/03/png-02.png',
1, 'Golda')
Go


INSERT INTO Product values('Medium Cup Of ReBar', 500, 'A medium cup of selected rebar tastes', 'https://cdn.groo.co.il/_media/media/20728/215232.png
', 1, 'ReBar')
Go

INSERT INTO Product values('JBL Filp Essential', 1200, 'A Black JBL Speaker, water proof, has bluetooth a connection', 'https://www.payngo.co.il/media/catalog/product/cache/fe04646a7504602017a124bbc269ed24/0/1/012efeff60111.png
', 1, 'JBL')
Go

INSERT INTO Product values('Zoi Gift Card 50$', 2000, 'A great greek restaurnt in Kfar Saba', 'https://zoi-kitchen.co.il/wp-content/uploads/2021/05/unnamed-file.png',
1, 'Zoi')
Go


INSERT INTO UsersData values(1,7,null, '2022-5-6', 'noa') 
INSERT INTO UsersData values(2, 100, null, '2022-5-4', 'noa') 
INSERT INTO UsersData VALUES(3, 200, null, '2022-5-7', 'noa')

INSERT INTO UsersData VALUES(1, 5, null, '2022-5-12', 'noa')
INSERT INTO UsersData VALUES(2, 70, null, '2022-5-13', 'noa')
INSERT INTO UsersData VALUES(3,250,null,'2022-5-13', 'noa')


 

 SELECT * FROM RegularUser
Go
select * from Users
