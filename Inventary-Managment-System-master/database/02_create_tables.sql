USE InventoryDB;
GO

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName VARCHAR(100),
    Quantity INT,
    Price DECIMAL(10,2)
);