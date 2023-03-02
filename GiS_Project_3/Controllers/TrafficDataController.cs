using Microsoft.AspNetCore.Mvc;
using GiS_Project_3.Models;
using GiS_Project_3.Repository;
using System.Device.Location;
using NetTopologySuite.Utilities;
using Npgsql;

namespace GiS_Project_3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficDataController : ControllerBase
    {
        private string connString = "Server=localhost;Port=5432;Database=nis;User Id=postgres;Password=ADMIN;";
        private readonly ILogger<TrafficDataController> _logger;

        public TrafficDataController(ILogger<TrafficDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTrafficData")]
        public IEnumerable<string> GetTrafficData(int speed, int timestep)
        {
            using var con = new NpgsqlConnection(connString);
            con.Open();

            var jointSpatialSql = @$"SELECT road.name
                FROM nis_road_line AS road
                JOIN nis_fcd AS car
                ON ST_DWithin(road.way, ST_Transform(ST_SetSRID(ST_MakePoint(car.vehicle_x, car.vehicle_y), 4326),3857),100)
                WHERE vehicle_speed > {speed} AND road.name IS NOT NULL AND car.vehicle_type IN ('bike_bicycle','bus_bus') AND timestep_time = {timestep}
                ORDER BY vehicle_speed DESC, road.name ASC
                limit 50";

            using var cmd = new NpgsqlCommand(jointSpatialSql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            IList<string> result = new List<string>();
            while (rdr.Read())
            {
                result.Add(rdr.GetValue(0).ToString());
            }
            return result;

        }
    }
}