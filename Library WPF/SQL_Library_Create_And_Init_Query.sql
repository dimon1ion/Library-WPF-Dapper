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

CREATE TRIGGER BookSales_Insert
ON BookSales
AFTER INSERT
AS
if(0 <= ALL (SELECT Quantity - 1 FROM Books, inserted WHERE inserted.BookId = Books.Id))
BEGIN
	UPDATE Books
	Set Quantity = (SELECT TOP 1 Books.Quantity FROM Books, inserted WHERE inserted.BookId = Books.Id) - 1
	WHERE (SELECT BookId FROM inserted) = Books.Id
END
else
BEGIN
	DELETE FROM BookSales
	WHERE BookSales.Id = ANY (SELECT inserted.Id FROM inserted)
END

CREATE TRIGGER ShelvedBooks_Delete
ON ShelvedBooks
AFTER DELETE
AS
UPDATE Books
SET Quantity = (SELECT TOP 1 Books.Quantity FROM Books, deleted WHERE deleted.BookId = Books.Id) + 1
WHERE Books.Id = (SELECT deleted.BookId FROM deleted)

