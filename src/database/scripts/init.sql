USE [master]
GO

IF NOT EXISTS (SELECT *
FROM sys.databases
WHERE name = 'CAO_HUB')
BEGIN
    CREATE DATABASE [CAO_HUB];
END;
GO