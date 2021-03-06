using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
    [Table("vehicles_v")]
    public class Vehicle{
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }

        [StringLength(255)]
        public string ContactEmail { get; set; }
        public bool isRegistered { get; set; }
       
        [Required]
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
        public DateTime LastUpdate { get; set; }        
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
        }
    }
}
