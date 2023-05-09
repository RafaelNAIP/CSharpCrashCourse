// See https://aka.ms/new-console-template for more information
using Dapper;
using estudos_intermediarios.Data;
using estudos_intermediarios.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace estudos_intermediarios
{


    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF entityFramework = new DataContextEF(config);

            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            Console.WriteLine(rightNow);
    
            Computer computer = new Computer()
            {
                Motherboard = "Z690",
                hasWifi = true,
                hasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

            entityFramework.Add(computer);
            entityFramework.SaveChanges();

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + computer.Motherboard 
                    + "','" + computer.hasWifi
                    + "','" + computer.hasLTE
                    + "','" + computer.ReleaseDate
                    + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + computer.VideoCard
            + "')";

            Console.WriteLine(sql);

            // int result = dapper.ExecuteSqlWithRowCount(sql);
            bool result = dapper.ExecuteSql(sql);

            Console.WriteLine(result);

            string sqlSelect = @"SELECT 
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                VideoCard FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            foreach(Computer singleComputer in computers)
            {
                Console.WriteLine("'" + singleComputer.ComputerId 
                    + "','" + singleComputer.Motherboard
                    + "','" + singleComputer.hasWifi
                    + "','" + singleComputer.hasLTE
                    + "','" + singleComputer.ReleaseDate
                    + "','" + singleComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + singleComputer.VideoCard
            + "'");
            }
            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();


            if(computersEf != null)
            {
            foreach (Computer singleComputer in computersEf)
            {
                Console.WriteLine("'" + computer.ComputerId
                    + "','" + singleComputer.Motherboard
                    + "','" + singleComputer.hasWifi
                    + "','" + singleComputer.hasLTE
                    + "','" + singleComputer.ReleaseDate
                    + "','" + singleComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + singleComputer.VideoCard
            + "'");
            }

            }

        }
    }
}
