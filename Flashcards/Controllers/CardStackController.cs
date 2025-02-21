using System.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace Flashcards.Controllers;

internal class CardStackController
{
    private string stackName;
    private string connectionString;

    internal CardStackController()
    {
        connectionString = ConfigurationManager.ConnectionStrings["dbString2"].ConnectionString;
    }
    internal void AddStack()
    {
        stackName = UserInput.getStringInput("Enter name of the new stack: ");
        var sql = "INSERT INTO Stacks (Name) VALUES (@Name)";
        object[] parameters = { new { Name = stackName } };

        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Execute(sql, parameters);
                AnsiConsole.MarkupLine($"[green]Stack {stackName} added successfully![/]");
            }
            catch (SqlException e)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {e.Message}");
                Console.ReadKey();
            }
            
        }
    }
}