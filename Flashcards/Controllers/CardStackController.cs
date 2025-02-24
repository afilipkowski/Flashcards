using System.Configuration;
using Dapper;
using Flashcards.Models;
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

    internal void AddStack(string stackName)
    {
        var sql = "INSERT INTO Stacks (Name) VALUES (@Name)";

        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Execute(sql, new { Name = stackName });
                AnsiConsole.MarkupLine($"[green]Stack {stackName} added successfully![/]");
            }
            catch (SqlException e)
            {
                AnsiConsole.MarkupLine($"[red]Error adding a stack:[/] {e.Message}");
                Console.ReadKey();
            }
        }
    }

    internal void DisplayStacks()
    {
        var sql = "SELECT * FROM Stacks ORDER BY Id";
        using (var connection = new SqlConnection(connectionString))
        {
            var stacks = connection.Query<CardStack>(sql).ToList();
            if (stacks.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No stacks found![/]");
            }
            else
            {
                foreach (var stack in stacks)
                {
                    AnsiConsole.MarkupLine($"[green]{stack.Id}[/]: {stack.Name}");
                }
            }
        }
    }

    internal void DeleteStack(int id)
    {
        var sql = "DELETE FROM Stacks WHERE Id = @Id";

        if (StackExists(id))
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, new { Id = id });
            }
        }
        else
        {
            Console.WriteLine("Stack with this ID does not exist!");
        }
    }

    internal void EditStack(int id, string name)
    {
        var sql = "UPDATE Stacks SET Name = @Name WHERE Id = @Id";
        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Execute(sql, new { Name = name, Id = id });
            }
            catch (SqlException e)
            {
                AnsiConsole.MarkupLine($"[red]Error editing a stack:[/] {e.Message}");
                Console.ReadKey();
            }
        }
    }

    internal bool StackExists(int id)
    {
        var sql = "SELECT COUNT(*) FROM Stacks WHERE Id = @Id";
        using (var connection = new SqlConnection(connectionString))
        {
            return connection.ExecuteScalar<int>(sql, new { Id = id }) > 0;
        }
    }
}