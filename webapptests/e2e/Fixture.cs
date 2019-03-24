using System.Data.Common;
using System.IO;
using Npgsql;
using NUnit.Framework;
using webapp;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace webapptests.e2e
{
    [SetUpFixture]
    public class Fixture
    {
        [OneTimeSetUp]
        public void RunBeforeTests()
        {
            string configFilePath = "config.yml";
            Options options = ReadOptionsFile(configFilePath);
            NpgsqlConnection connection = new NpgsqlConnection(options.PostgresConnectionString);
            connection.Open();
            ExecuteFile(connection, options.DbFixtureFolder + "/drop.sql");
            ExecuteFile(connection, options.DbFixtureFolder + "/create.sql");
            ExecuteFile(connection, options.DbFixtureFolder + "/test_data.sql");
        }
        
        private static Options ReadOptionsFile(string configFilePath)
        {
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            string optionsYamlFile = File.ReadAllText(configFilePath);
            Options options = deserializer.Deserialize<Options>(optionsYamlFile);
            return options;
        }
        
        private static void ExecuteFile(NpgsqlConnection conn, string filePath)
        {
            string text = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
            using (DbCommand command = conn.CreateCommand())
            {
                command.CommandText = text;
                command.ExecuteNonQuery();
            }
        }
    }
}