Use Library;

CREATE TABLE Authors(
Id INT PRIMARY KEY IDENTITY(1,1),
FirstName nvarchar(100) NOT NULL,
LastName nvarchar(100) NOT NULL
)

CREATE TABLE Publishers(
Id INT PRIMARY KEY IDENTITY(1,1),
Name nvarchar(100) NOT NULL
)

CREATE TABLE Books(
Id INT PRIMARY KEY IDENTITY(1,1),
AuthorId int NOT NULL FOREIGN KEY REFERENCES Authors(Id),
Genre int NOT NULL,
PublisherId int NOT NULL FOREIGN KEY REFERENCES Publishers(Id),
Number_of_pages int NOT NULL,
Year_of_publishing int NOT NULL,
Cost_price money NOT NULL,
Selling_price money NOT NULL,
Continuation bit Default(0),
Quantity int Default(0)
)

CREATE TABLE Admins(
Id INT PRIMARY KEY IDENTITY(1,1),
Login nvarchar(100) NOT NULL,
Password nvarchar(100) NOT NULL
)

CREATE TABLE Salesmen(
Id INT PRIMARY KEY IDENTITY(1,1),
FirstName nvarchar(100) NOT NULL,
LastName nvarchar(100) NOT NULL,
Login nvarchar(100) NOT NULL,
Password nvarchar(100) NOT NULL
)

CREATE TABLE Customers(
Id INT PRIMARY KEY IDENTITY(1,1),
FirstName nvarchar(100) NOT NULL,
LastName nvarchar(100) NOT NULL,
Login nvarchar(100) NOT NULL,
Password nvarchar(100) NOT NULL
)

CREATE TABLE BookSales(
Id INT PRIMARY KEY IDENTITY(1,1),
BookId int NOT NULL FOREIGN KEY REFERENCES Books(Id),
SalesmanId int NOT NULL FOREIGN KEY REFERENCES Salesmans(Id),
CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(Id),
Selling_price money NOT NULL
)

CREATE TABLE ShelvedBooks(
Id INT PRIMARY KEY IDENTITY(1,1),
BookId int NOT NULL FOREIGN KEY REFERENCES Books(Id),
CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(Id)
)

CREATE TABLE Stocks(
Id INT PRIMARY KEY IDENTITY(1,1),
Name nvarchar(100) NOT NULL
)

CREATE TABLE StocksBooks(
Id INT PRIMARY KEY IDENTITY(1,1),
StockId int NOT NULL FOREIGN KEY REFERENCES Stocks(Id),
BookId int NOT NULL FOREIGN KEY REFERENCES Books(Id),
StockPercent int NOT NULL CONSTRAINT PercentCheck CHECK (0 < StockPercent AND StockPercent <= 100)
)

SELECT * FROM Customers

--INSERT INTO Customers(FirstName, LastName, Login, Password)
--VALUES ('Di1', 'Ma', 'kaka', '1234567')

SELECT COUNT(Id) FROM Customers WHERE Customers.Login = 's'


--INSERT INTO Admins(Login, Password)
--VALUES ('admin', 'admin')

--INSERT INTO Salesmen(FirstName, LastName, Login, Password)
--VALUES ('sales', 'girl', 'sexy', 'sex')
