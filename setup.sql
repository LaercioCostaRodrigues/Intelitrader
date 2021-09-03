CREATE DATABASE PersonRegistryApi
go

USE PersonRegistryApi
go

CREATE TABLE [User](
Id NVARCHAR(450) not null constraint [PK_User_Id] Primary key clustered,
FirstName NVARCHAR(80) not null,
Surname NVARCHAR(80) null,
Age INT not null,
CreationDate DATETIME2 not null
)


CREATE DATABASE UserLogs
go

USE UserLogs
go

CREATE SCHEMA EventLogging
go


CREATE TABLE [EventLogging].[Logs](
Id INT not null constraint [PK_Logs_Id] Primary key clustered,
Message NVARCHAR(MAX) null,
MessageTemplate NVARCHAR(MAX) null,
[Level] NVARCHAR(128) null,
TimeStamp datetime null,
Exception NVARCHAR(MAX) null,
Properties NVARCHAR(MAX) null
)