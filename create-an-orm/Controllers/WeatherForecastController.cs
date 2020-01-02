using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace create_an_orm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public const string ConnectionString = "Write your db connection string here!";

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task Get()
        {
            List<Person> people = await GetAll<Person>();

            var person = new Person
            {
                Id = 12,
                Birthdate = new DateTime(1984, 1, 1),
                Height = 174,
                Name = "Aram"
            };

            await Insert(person);

            var updatedPerson = people[0];
            updatedPerson.Height += 2;

            await Update(nameof(Person.Id), updatedPerson);
        }

        public async Task<List<T>> GetAll<T>() where T : new()
        {
            // Note: the new() constraint makes sure the T has a public constructor that takes no parameters.
            // So that we can write var model = new T();
            var list = new List<T>();

            var properties = typeof(T).GetProperties();

            var columnNames = properties.Select(p => p.Name);
            var columns = string.Join(", ", columnNames);

            var sql = $"SELECT {columns} FROM {typeof(T).Name}";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var model = new T();

                        foreach (var property in properties)
                        {
                            property.SetValue(model, reader[property.Name]);
                        }

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public async Task Insert<T>(T item)
        {
            var properties = typeof(T).GetProperties();

            var columns = string.Join(", ", properties.Select(p => p.Name));
            var columnParameters = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var sql = $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({columnParameters})";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();

                foreach (var property in properties)
                {
                    command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item));
                }

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update<T>(string idPropertyName, T item)
        {
            var properties = typeof(T).GetProperties();

            var columnUpdates = properties.Where(p => p.Name != idPropertyName)
                                          .Select(p => $"{p.Name} = @{p.Name}");
            var columns = string.Join(", ", columnUpdates);

            var sql = $"UPDATE {typeof(T).Name} SET {columns} WHERE {idPropertyName} = @{idPropertyName}";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();

                foreach (var property in properties)
                {
                    command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item));
                }

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Height { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
