using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
using System.Runtime.Serialization;

namespace GiS_Project_3.Models
{
    [Table("nis_fcd")]
    [Keyless]
    public class TrafficData
    {
        [Column("timestep_time")]
        public double Timestep { get; set; }

        [Column("vehicle_angle")]
        public double VehicleAngle { get; set; }

        [Column("vehicle_id")]
        public string VehicleId { get; set; }

        [Column("vehicle_lane")]
        public string VehicleLane { get; set; }

        [Column("vehicle_pos")]
        public double VehiclePos { get; set; }

        [Column("vehicle_slope")]
        public double VehicleSlope { get; set; }

        [Column("vehicle_speed")]
        public double VehicleSpeed { get; set; }

        [Column("vehicle_type")]
        public string VehicleType { get; set; }

        [Column("vehicle_x")]
        public double Longitude { get; set; }

        [Column("vehicle_y")]
        public double Latitude { get; set;}

        //[NotMapped]
        //[JsonIgnore]
        //public GeoCoordinate? geoCoordinate => new GeoCoordinate(Latitude, Longitude);

    }
}
