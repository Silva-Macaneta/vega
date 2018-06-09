using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace vega.Models
{
    public class Feature
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public ICollection<VehicleFeature> VehicleFeature { get; set; }
        public Feature()
        {
            this.VehicleFeature = new Collection<VehicleFeature>();
        }
    }
}