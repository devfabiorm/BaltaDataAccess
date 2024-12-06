﻿using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";
        // var connection = new SqlConnection(connectionString);
        // connection.Open();
        // //Code...
        // connection.Close();
        using (var connection = new SqlConnection(connectionString))
        {
            //UpdateCategory(connection);
            //ListCategories(connection);
            //CreateCategory(connection);
            GetCategory(connection);
        }
    }

    static void CreateCategory(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS";
        category.Url = "amazon";
        category.Description = "Categoria destinada a serviços do AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        // Created query using sql parameters
        var insertSql = @"INSERT INTO
                [Category]
            VALUES (
                @Id,
                @Title, 
                @Url, 
                @Summary, 
                @Order, 
                @Description, 
                @Featured)";

        //Instead of doing string interpolation, use Sql Parameters to provider values to the query being executed
        var rows = connection.Execute(insertSql, new
        {
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });

        Console.WriteLine($"{rows} linhas inseridas.");
    }

    static void CreateManyCategories(SqlConnection connection)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Title = "Amazon AWS";
        category.Url = "amazon";
        category.Description = "Categoria destinada a serviços do AWS";
        category.Order = 8;
        category.Summary = "AWS Cloud";
        category.Featured = false;

        var category2 = new Category();
        category2.Id = Guid.NewGuid();
        category2.Title = "Categoria Nova";
        category2.Url = "categoria-nova";
        category2.Description = "Categoria nova";
        category2.Order = 9;
        category2.Summary = "Categoria";
        category2.Featured = true;

        // Created query using sql parameters
        var insertSql = @"INSERT INTO
                [Category]
            VALUES (
                @Id,
                @Title, 
                @Url, 
                @Summary, 
                @Order, 
                @Description, 
                @Featured)";

        //Instead of doing string interpolation, use Sql Parameters to provider values to the query being executed
        var rows = connection.Execute(insertSql, new[]
        {
            new {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured
            },
            new {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured
            }
        });

        Console.WriteLine($"{rows} linhas inseridas.");
    }

    static void GetCategory(SqlConnection connection)
    {
        var category = connection
            .QueryFirstOrDefault<Category>(
                "SELECT TOP 1 [Id], [Title] FROM [Category] WHERE Id=@id",
                new
                {
                    id = "239c46e1-3d3e-46a3-bf5a-40c0b579777e"
                });

        Console.WriteLine($"{category.Id} - {category.Title}");
    }
    static void ListCategories(SqlConnection connection)
    {
        var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

        foreach (var item in categories)
        {
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    static void UpdateCategory(SqlConnection connection)
    {
        var updateQuery = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @Id";

        var rows = connection.Execute(updateQuery, new
        {
            id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
            title = "Frontend 2024"
        });

        Console.WriteLine($"{rows} registros atualizados.");
    }
    private static void Module1(SqlConnection connection)
    {
        connection.Open();

        using (var command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT [Id], [Title] FROM [Category]";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
            }
        }
    }
}