-- SQL Notes
-- Comments in SQL are noted with a "--"
/* multi line 

   Comments 

*/

-- DDL - Data Definition Language - Creatiiing and modifying the structure of data/table/database
-- DQL - Data Query - Retrieving the data
-- DML - Data Manipulation - Creating and modifying the data inside established structure
-- DCL - Data Control - Access control . and server admin

-- DQL -
-- SELECT - sorting, filtering , and gathering data from tables within the database -- SELECT always returns tables

USE MyDatabase
GO
SELECT 2; 

SELECT * FROM [MyDatabase].[dbo].[Artist];
-- use select to specify that we want a response , and using from to specify where we want the data gathered from.

SELECT * FROM [MyDatabase].[dbo].[Customer];
-- gather customer table

SELECT FirstName, LastName FROM [MyDatabase].[dbo].[Customer];
-- gather table using firsname and lastname

SELECT FirstName, LastName FROM [MyDatabase].[dbo].[Customer] WHERE LastName ='Smith';
-- garther table wich lastname is smith


-- Using Where keyword to filter the data
SELECT FirstName, LastName FROM [MyDatabase].[dbo].[Customer] WHERE Country ='Canada' OR Country = 'France';
-- gather the table filter countries

SELECT COUNT(*) FROM [MyDatabase].[dbo].[Customer];
-- gather how many data

SELECT * FROM [MyDatabase].[dbo].[Invoice];
--gather all data with the invoice coulmn

SELECT SUM(Total) FROM [MyDatabase].[dbo].[Invoice];
--gather tolal sum of invoices

SELECT CustomerId, SUM(Total) FROM [MyDatabase].[dbo].[Invoice] WHERE BillingCountry = 'USA' AND SUM(Total) > 40 ;