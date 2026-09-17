using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using BlogAPI.Models;

namespace BlogAPI.Controllers
{
    [Route("blogger")]
    [ApiController]
    public class BloggerController : ControllerBase
    {
        

        public string ConnectionString = "server=localhost;database=blog;uid=root;password=";

        [HttpGet]
        public object GetAllBlogger()
        {
            List<Blogger> bloggers = new List<Blogger>();
            using var connector = new MySqlConnection(ConnectionString);

            connector.Open();
            string sql = @"SELECT * FROM `blogger`";
            var cmd = new MySqlCommand(sql, connector);
            var dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var blogger = new Blogger
                { 
                    Name = dataReader.GetString(1),
                    Email = dataReader.GetString(2),
                    Age = dataReader.GetInt32(3),
                    Password = dataReader.GetString(4),
                    RegistrationTime = dataReader.GetDateTime(5)
                };
                bloggers.Add(blogger);

            }
            connector.Close();

            return new { message = "Sikeres lekérdezés" , result = bloggers };
        }

        [HttpGet]
        public object getbloggerbyid(int id)
        {
            var connector = new MySqlConnection(ConnectionString);

            connector.Open();

            string sql = $"SELECT * FROM `blogger` WHERE `id` = @id;";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);

            var dataReader = cmd.ExecuteReader();

            dataReader.Read();
            var blogger = new Blogger
            {
                Name = dataReader.GetString(1),
                Email = dataReader.GetString(2),
                Age = dataReader.GetInt32(3),
                Password = dataReader.GetString(4),
                RegistrationTime = dataReader.GetDateTime(5)
            };

            connector.Close();

            return new { message = "Sikeres találat" };
        }
    }
}