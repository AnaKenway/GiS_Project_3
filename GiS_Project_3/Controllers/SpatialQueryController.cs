using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Reflection.PortableExecutable;
using NpgsqlTypes;
using Microsoft.AspNetCore.Cors;

namespace GiS_Project_3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpatialQueryController : Controller
    {
        private string connString = "Server=localhost;Port=5432;Database=nis;User Id=postgres;Password=ADMIN;";
        [HttpGet(Name = "GetEducationNearRoad")]
        public IEnumerable<string> GetEducationNearRoad(int radius, string road)
        {

            using var con = new NpgsqlConnection(connString);
            con.Open();

            var spatialSql = "SELECT sum(ST_Length(way))/1000 AS km_roads FROM nis_road_line;";
            var jointSpatialSql = @$"SELECT education.osm_id as Id
                        FROM  nis_education_polygon AS education 
                        JOIN nis_road_line AS roads ON ST_Dwithin(roads.way, education.way,{radius}) 
                        WHERE roads.name = '{road}'";

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
