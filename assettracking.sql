/***************************************************************************
    Database Script for HR
    CPRG 102 .NET Development with MVC 
    SAIT Polytechnic
    Tables:
	Department - parent table
        Employee -  child table
    To run as script:
    1. Open sqlcmd
    2. type: sqlcmd -S .\sqlexpress -E -i filename
       (The dot is alias for machine name and sqlexpress is instance name of sql server. Might just be . or localhost if default name used)
    To run as query:
    1. Open SQL Server Management Studio Express
    2. Open Databases
    3. Right click server and select new query
    4. Copy and paste file contents into query window and execute
***************************************************************************/

use master
go

if exists(select * from sysdatabases where name = 'HR')
  ALTER DATABASE HR SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE HR;
go


CREATE DATABASE HR
go

USE HR
go

CREATE TABLE Department(
	Id int IDENTITY(1, 1) NOT NULL,
	Name varchar(50) NOT NULL,
        Location varchar(50) NOT NULL,	
	CONSTRAINT PK_Department PRIMARY KEY  CLUSTERED 
	(
		Id
	)
)
go


CREATE TABLE Employee(
	Id int IDENTITY(1, 1) NOT NULL,
	EmployeeNumber varchar(50) NOT NULL,
	FirstName varchar(50) NOT NULL,
        LastName varchar(50) NOT NULL,
	Position varchar(50) NOT NULL,
	Phone varchar(14) NOT NULL,
	DepartmentId int NOT NUll ,	
	CONSTRAINT PK_Employee PRIMARY KEY  CLUSTERED 
	(
		Id
	),
	CONSTRAINT FK_Employee_Department FOREIGN KEY 
	(
		DepartmentId
	) REFERENCES Department
	(
		Id
	)
)
go

SET IDENTITY_INSERT [Department] ON
INSERT INTO Department(Id, Name, Location) VALUES (1, 'Administration', 'Calgary')
INSERT INTO Department(Id, Name, Location) VALUES (2, 'IT', 'Calgary')
INSERT INTO Department(Id, Name, Location) VALUES (3, 'Engineering', 'Edmonton')
INSERT INTO Department(Id, Name, Location) VALUES (4, 'Sales', 'Vancouver')
SET IDENTITY_INSERT [HR] OFF
go

INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('DO1001', 'John', 'Doe', 'Manager', '403-555-8501', 1)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('CA1002', 'Sam', 'Carr', 'CIO', '403-555-8510', 2)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('SM1003', 'Jane', 'Smith', 'Reception', '403-555-8500', 1)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('BE1004', 'Colin', 'Benson', 'Desktop Support', '403-555-8520', 2)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('CO1005', 'Sarah', 'Collins', 'Developer', '403-555-8530', 2)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('WI1006', 'Glen', 'Wilson', 'GIS Engineer', '780-555-1010', 3)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('UN1007', 'Paul', 'Unger', 'Engineering Technician', '780-555-1020', 3)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('WA1008', 'Mary', 'Watson', 'Sales Rep', '250-555-6550', 4)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('KI1009', 'Ben', 'Kim', 'Sales Rep', '250-555-6560', 4)
INSERT INTO Employee(EmployeeNumber, FirstName, LastName, Position, Phone, DepartmentId) VALUES ('MO1010', 'Dan', 'Moore', 'Sales Rep', '250-555-6570', 4)
go

SELECT e.FirstName, e.LastName, d.Name 
FROM   Employee e INNER JOIN Department d ON e.DepartmentId = d.Id
go
