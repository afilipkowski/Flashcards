using System.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Flashcards
{
    internal class Database
    {
        private string connectionString;
        private string databaseName;

        public Database()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dbString"].ConnectionString;
            databaseName = ConfigurationManager.AppSettings.Get("dbName");
        }

        public void CreateDatabase()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var command = @$" IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}')
                CREATE DATABASE {databaseName}";
                conn.Execute(command);
            }
            connectionString += ";Database=afilipkowski_flashcards";
        }

        public void CreateTables()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var command = $@"IF OBJECT_ID(N'dbo.Stacks', N'U') IS NULL BEGIN
                                 CREATE TABLE dbo.Stacks (
                                 Id INT PRIMARY KEY IDENTITY(1,1),
                                 Name NVARCHAR(50) NOT NULL); END;

                                 IF OBJECT_ID(N'dbo.Cards', N'U') IS NULL BEGIN
                                 CREATE TABLE dbo.Cards (
                                 Id int NOT NULL,
                                 StackId int NOT NULL,
                                 Term NVARCHAR(100) NOT NULL,
                                 Definition NVARCHAR(100) NOT NULL,
                                 PRIMARY KEY (Id, StackId),
                                 FOREIGN KEY (StackId) REFERENCES dbo.Stacks(Id) ON DELETE CASCADE
                                 ); END;";
                conn.Execute(command);
            }
        }
    }
}