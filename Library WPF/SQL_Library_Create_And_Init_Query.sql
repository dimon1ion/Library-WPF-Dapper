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

CREATE TABLE Genres(
Id INT PRIMARY KEY IDENTITY(1,1),
Name nvarchar(100) NOT NULL
)

CREATE TABLE Books(
Id INT PRIMARY KEY IDENTITY(1,1),
Name nvarchar(100) NOT NULL,
AuthorId int NOT NULL FOREIGN KEY REFERENCES Authors(Id) ON DELETE CASCADE,
GenreId int NOT NULL FOREIGN KEY REFERENCES Genres(Id) ON DELETE CASCADE,
PublisherId int NOT NULL FOREIGN KEY REFERENCES Publishers(Id) ON DELETE CASCADE,
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
BookId int NOT NULL FOREIGN KEY REFERENCES Books(Id) ON DELETE CASCADE,
--SalesmanId int NOT NULL FOREIGN KEY REFERENCES Salesmans(Id) ON DELETE CASCADE,
CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(Id) ON DELETE CASCADE,
Purchase_date datetime NOT NULL,
Selling_price money NOT NULL
)

CREATE TABLE ShelvedBooks(
Id INT PRIMARY KEY IDENTITY(1,1),
BookId int NOT NULL FOREIGN KEY REFERENCES Books(Id) ON DELETE CASCADE,
CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(Id) ON DELETE CASCADE
)

CREATE TABLE Stocks(
Id INT PRIMARY KEY IDENTITY(1,1),
Name nvarchar(100) NOT NULL
)

CREATE TABLE StocksBooks(
Id INT PRIMARY KEY IDENTITY(1,1),
StockId int NOT NULL FOREIGN KEY REFERENCES Stocks(Id) ON DELETE CASCADE,
BookId int NOT NULL FOREIGN KEY REFERENCES Books(Id) ON DELETE CASCADE,
StockPercent int NOT NULL CONSTRAINT PercentCheck CHECK (0 < StockPercent AND StockPercent <= 100)
)

--DROP TABLE StocksBooks
--DROP TABLE Stocks
--DROP TABLE ShelvedBooks
--DROP TABLE BookSales
--DROP TABLE Customers
--DROP TABLE Salesmen
--DROP TABLE Admins
--DROP TABLE Books
--DROP TABLE Publishers
--DROP TABLE Authors

--INSERT INTO Genres(Name)
--VALUES('Drama')

--INSERT INTO Genres(Name)
--VALUES('Roman')

--INSERT INTO Authors(FirstName, LastName)
--VALUES ('Keisy', 'Uest')

--INSERT INTO Publishers(Name)
--VALUES ('Idk')

--INSERT INTO Books(GenreId, AuthorId, PublisherId, Name, Number_of_pages, Continuation, Cost_price, Quantity, Year_of_publishing, Selling_price)
--VALUES(3, 1, 1, 'true', 204, 0, 10, 100, 2018, 0)

--INSERT INTO BookSales(BookId, CustomerId, Purchase_date, Selling_price)
--VALUES (5, 1, GETDATE(), 0)

--INSERT INTO ShelvedBooks(BookId, CustomerId)
--VALUES (5, 1)

--INSERT INTO Stocks(Name)
--VALUES ('Happy new Year')

--INSERT INTO StocksBooks(BookId, StockId, StockPercent)
--VALUES (5, 1, 20)

SELECT * FROM Books

DELETE FROM Genres
WHERE Genres.Name = 'Drama'


SELECT * FROM Customers

--INSERT INTO Customers(FirstName, LastName, Login, Password)
--VALUES ('Di1', 'Ma', 'kaka', '12345678')

SELECT COUNT(Id) FROM Customers WHERE Customers.Login = 's'


--INSERT INTO Admins(Login, Password)
--VALUES ('admin', 'admin')

--INSERT INTO Salesmen(FirstName, LastName, Login, Password)
--VALUES ('sales', 'girl', 'sexy', 'sex')


SELECT Books.Id, Books.Name[Name], Authors.FirstName + ' ' + Authors.LastName[Author],
	Genres.Name[Genre], Publishers.Name[Publisher], Books.Number_of_pages,
	Books.Year_of_publishing, Books.Cost_price,
	Books.Selling_price, Books.Continuation, Books.Quantity
	FROM Books, Authors, Publishers, Genres
WHERE Books.AuthorId = Authors.Id AND Books.PublisherId = Publishers.Id
AND Books.GenreId = Genres.Id

SELECT BookSales.Id, '"' + Books.Name + '" ' + Authors.FirstName + ' ' + Authors.LastName[Book], Customers.FirstName + ' ' + Customers.LastName[Customer],  
	BookSales.Purchase_date[PurchaseDate], BookSales.Selling_price
FROM BookSales, Books, Customers, Authors
WHERE BookSales.BookId = Books.Id AND BookSales.CustomerId = Customers.Id
AND Books.AuthorId = Authors.Id

SELECT ShelvedBooks.Id, '"' + Books.Name + '" ' + Authors.FirstName + ' ' + Authors.LastName, Customers.FirstName + ' ' + Customers.LastName
FROM ShelvedBooks, Books, Customers, Authors
WHERE ShelvedBooks.BookId = Books.Id AND ShelvedBooks.CustomerId = Customers.Id
AND Books.AuthorId = Authors.Id

SELECT StocksBooks.Id, Stocks.Name, Books.Name, Authors.FirstName + ' ' + Authors.LastName, StocksBooks.StockPercent
FROM StocksBooks, Stocks, Books, Authors
WHERE StocksBooks.StockId = Stocks.Id AND StocksBooks.BookId = Books.Id 
AND Books.AuthorId = Authors.Id

SELECT StocksBooks.Id, Stocks.Name[StockName], '\"' + Books.Name + '\" ' + Authors.FirstName + ' ' + Authors.LastName[BookName], StocksBooks.StockPercent 
FROM StocksBooks, Stocks, Books, Authors
WHERE StocksBooks.StockId = Stocks.Id AND StocksBooks.BookId = Books.Id 
AND Books.AuthorId = Authors.Id

UPDATE StocksBooks SET StockPercent