
--------------------------------------------------------DB Creation------------------------------------------------------

--GO
--USE master

--GO
--CREATE DATABASE StartUpDemoDb

--GO
--USE StartUpDemoDb


--------------------------------------------------------TABLES------------------------------------------------------


--GO 
--CREATE TABLE Sizes
--(
--[Id] INT PRIMARY KEY IDENTITY,
--[Description] NVARCHAR(10) NOT NULL UNIQUE
--)


----Bulk insertion of the most popular sizes


--GO
--INSERT INTO Sizes VALUES (N'XS')
--INSERT INTO Sizes VALUES (N'S')
--INSERT INTO Sizes VALUES (N'M')
--INSERT INTO Sizes VALUES (N'L')
--INSERT INTO Sizes VALUES (N'XL')

--GO
--CREATE TABLE Colors
--(
--[Id] INT IDENTITY PRIMARY KEY,
--[Name] NVARCHAR(100) NOT NULL UNIQUE
--)


----Bulk insertion of the most popular colors


--GO
--INSERT INTO Colors VALUES (N'Black')
--INSERT INTO Colors VALUES (N'White')
--INSERT INTO Colors VALUES (N'Gray')
--INSERT INTO Colors VALUES (N'Blue')
--INSERT INTO Colors VALUES (N'Red')
--INSERT INTO Colors VALUES (N'Green')


--GO
--CREATE TABLE Targets
--(
--[Id] INT IDENTITY PRIMARY KEY,
--[Name] NVARCHAR(100) UNIQUE NOT NULL
--)


----Bulk insertion of the target audience (for men, women or kids)


--GO
--INSERT INTO Targets VALUES (N'Men')
--INSERT INTO Targets VALUES (N'Women')
--INSERT INTO Targets VALUES (N'Kids')
--INSERT INTO Targets VALUES (N'Unisex')

--GO
--CREATE TABLE Products
--(
--[Id] INT IDENTITY PRIMARY KEY,
--[Title] NVARCHAR(255) NOT NULL UNIQUE,
--[Price] DECIMAL (10,2) NOT NULL,
--[Poster] NVARCHAR (MAX) NOT NULL,
--[Description] NVARCHAR (MAX) NOT NULL,
--[TargetId] INT FOREIGN KEY REFERENCES Targets(Id) NOT NULL,

-- CONSTRAINT CHK_Products_PriceIsGreaterThanZero CHECK (Price > 0)
--)


----Bulk insertions of some products from the popular E-Commerce websites


--GO
--INSERT INTO Products VALUES
--(
--	N'SLIM FIT SUIT TROUSERS',
--	89.00,
--	N'https://static.zara.net/photos///2023/V/0/1/p/1564/711/800/2/w/750/1564711800_1_1_1.jpg?ts=1680599059254',
--	N'Slim fit trousers with front pockets, rear welt pockets and zip fly and top button fastening.',
--	1
--)
--INSERT INTO Products VALUES
--(
--	N'RIPPED SLIM FIT JEANS',
--	119.00,
--	N'https://static.zara.net/photos///2023/V/0/2/p/5585/415/400/2/w/750/5585415400_1_1_1.jpg?ts=1682066572606',
--	N'Whitewashed slim fit jeans featuring a five-pocket design and rips with inner patches on the legs. Zip fly and top button fastening.',
--	1
--)
--INSERT INTO Products VALUES
--(
--	N'PRINTED BLOUSE WITH POCKET',
--	119.00,
--	N'https://static.zara.net/photos///2023/V/0/1/p/3106/161/400/2/w/750/3106161400_1_1_1.jpg?ts=1682065662068',
--	N'Collared printed blouse featuring long sleeves with cuffs. Patch pockets on the chest. Button-up front.',
--	2
--)
--INSERT INTO Products VALUES
--(
--	N'BLOUSE WITH SLIT',
--	89.00,
--	N'https://static.zara.net/photos///2023/V/0/1/p/3666/122/250/2/w/750/3666122250_1_1_1.jpg?ts=1681988797458',
--	N'Round neck blouse with long sleeves, slit at the chest on the sleeves and a buttoned opening at the back.',
--	2
--)
--INSERT INTO Products VALUES
--(
--	N'Nike Air Force 1 07 FlyEase',
--	110,
--	N'https://static.nike.com/a/images/t_PDP_864_v1/f_auto,b_rgb:f5f5f5/13721f24-2774-443e-a27d-998d2c6be068/air-force-1-07-flyease-mens-shoes-vpmlK3.png',
--	N'Quicker than 1, 2, 3�the original hoops shoe lets you step in and get going. Its easy-entry FlyEase system gives you a hands-free experience',
--	4
--)


--GO
--CREATE TABLE ProductsToSale 
--(
--[Id] INT IDENTITY PRIMARY KEY,
--[ProductId] INT FOREIGN KEY REFERENCES Products(Id) NOT NULL,
--[ColorId] INT FOREIGN KEY REFERENCES Colors(Id) NOT NULL,
--[SizeId] INT FOREIGN KEY REFERENCES Sizes(Id) NOT NULL,
--[Amount] INT NOT NULL,

--CONSTRAINT CHK_ProductsToSale_AmountIsPositive CHECK (Amount >= 0)
--)



----Bulk insert some fake data

--GO
--INSERT INTO ProductsToSale VALUES (1,1,2,10) --Black | S | 10 units
--INSERT INTO ProductsToSale VALUES (1,1,3,10) --Black | M | 10 units
--INSERT INTO ProductsToSale VALUES (1,2,3,15) --White | M | 15 units

--GO
--INSERT INTO ProductsToSale VALUES (2,3,4,10) --Gray | L | 10 units
--INSERT INTO ProductsToSale VALUES (2,4,5,3) --Blue | XL | 3 units

--GO
--INSERT INTO ProductsToSale VALUES(3,5,1,20) --Red | XS | 20 units
--INSERT INTO ProductsToSale VALUES(3,6,2,20) --Green | S | 20 units

--GO
--INSERT INTO ProductsToSale VALUES(4,1,1,17) --Black | XS | 17 units
--INSERT INTO ProductsToSale VALUES(4,2,2,3) --White | S | 3 units

--GO
--INSERT INTO ProductsToSale VALUES(5,2,2,30) --White | S | 30 units
--INSERT INTO ProductsToSale VALUES(5,2,3,20) --White | M | 20 units
--INSERT INTO ProductsToSale VALUES(5,2,4,25) --White | L | 25 units

--GO
--CREATE TABLE Roles
--(
--[Id] INT PRIMARY KEY IDENTITY,
--[Name] NVARCHAR(100) UNIQUE NOT NULL,
--)

----Initital Roles
--GO
--INSERT INTO Roles VALUES (N'Admin');
--INSERT INTO Roles VALUES (N'User');


--GO
--CREATE TABLE Users
--(
--[Id] INT PRIMARY KEY IDENTITY,
--[Login] NVARCHAR(100) UNIQUE NOT NULL,
--[Password] NVARCHAR(100) NOT NULL,
--[RoleId] INT FOREIGN KEY REFERENCES Roles(ID) NOT NULL,
--[Salt] NVARCHAR(255) UNIQUE NOT NULL,
--)

--------------------------------------------------------STORED PROCEDURES------------------------------------------------------

--GO
--CREATE PROC GetAllProducts
--AS
--BEGIN
--SELECT * FROM Products
--END

--GO
--CREATE PROC GetProductById @Id INT
--AS
--BEGIN
--SELECT * FROM Products 
--WHERE Products.Id = @Id
--END

--GO
--CREATE PROC GetProductOptionsById @Id INT
--AS
--BEGIN
--SELECT clr.Name AS [Color], pts.Amount as [Amount], sz.Description as [SizeDescription],trg.Name AS [Target] FROM  ProductsToSale as pts
--JOIN Products as pr ON pr.Id = pts.ProductId
--JOIN Colors as clr ON clr.Id = pts.ColorId
--JOIN Sizes as sz ON pts.SizeId = sz.Id
--JOIN Targets as trg ON trg.Id = pr.TargetId
--WHERE pr.Id = @Id
--END


--GO
--CREATE PROC UpdateProduct @Id INT, @Title NVARCHAR(255), @Price DECIMAL(10,2), @Poster NVARCHAR(MAX), @Description NVARCHAR(MAX), @TargetId INT
--AS
--BEGIN
--UPDATE Products SET Title = @Title, Price = @Price, Poster = @Poster, Description = @Description, TargetId = @TargetId WHERE Id = @Id
--END

--Go
--CREATE PROC DeleteProduct @Id INT
--AS
--BEGIN 
--DELETE FROM ProductsToSale WHERE ProductsToSale.ProductId= @Id
--DELETE FROM Products WHERE Products.Id= @Id
--END


----Insert a new entity into Users and retrieve Id of the inserted entity
--GO
--CREATE PROC AddUser @Login NVARCHAR(100), @Password NVARCHAR(100),@RoleId INT, @Salt NVARCHAR(255)
--AS 
--BEGIN
--INSERT INTO Users (Login,Password,RoleId,Salt)
--OUTPUT inserted.Id
--VALUES (@Login,@Password,@RoleId,@Salt)
--END

----Retrieve each entity from the Users, joined with its role from the Roles
--GO
--CREATE PROC GetAllUsers
--AS
--BEGIN
--SELECT u.*, r.* FROM Users u
--JOIN Roles r on u.RoleId = r.Id
--END


----Retrieve a specific entity by its id from the Users, joined with its role from the Roles
--GO
--CREATE PROC GetSingleUser @Id INT
--AS
--BEGIN
--SELECT u.*, r.* FROM Users u
--JOIN Roles r on u.RoleId = r.Id
--WHERE u.Id = @Id
--END


----Retrieve a specific entity by its login from the Users, joined with its role from the Roles
--GO
--CREATE PROC  GetUserByLogin @Login NVARCHAR(100)
--AS
--BEGIN
--select u.*, r.* FROM Users as u
--JOIN Roles as r ON r.Id = u.RoleId 
--where u.Login = @Login COLLATE Latin1_General_CS_AS
--END



