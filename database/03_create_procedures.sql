USE InventoryDB;
GO

CREATE PROCEDURE AddProduct
    @ProductName VARCHAR(100),
    @Quantity INT,
    @Price DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Products (ProductName, Quantity, Price)
    VALUES (@ProductName, @Quantity, @Price);
END;