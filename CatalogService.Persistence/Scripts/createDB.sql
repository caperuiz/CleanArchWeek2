-- Create the Category table
CREATE TABLE Category
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    ImageUrl NVARCHAR(MAX),
    ParentCategoryId INT
);

-- Create the Item table
CREATE TABLE Item
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX),
    ImageUrl NVARCHAR(MAX),
    CategoryId INT NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Amount INT NOT NULL
);

-- Create a foreign key constraint to link items to categories
ALTER TABLE Item
ADD CONSTRAINT FK_Item_Category FOREIGN KEY (CategoryId)
REFERENCES Category(Id);

-- Optionally, you can create an index for the Category table for better performance
-- CREATE INDEX IX_Category_ParentCategoryId ON Category(ParentCategoryId);
