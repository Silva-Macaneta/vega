using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using vega.Controllers.Resources;

namespace vega.Controllers.Resources
{
    public class SaveVehicleResource{
        public int Id { get; set; }
        public bool isRegistered { get; set; }

        [Required]
        public MakeResource Make { get; set; }
        public KeyValuePairResource Model { get; set; }
        public int ModelId { get; set; }        
        public ContactResource Contact { get; set; }
        public ICollection<int> Features { get; set; }
        public DateTime LastUpdate { get; set; }        
        public SaveVehicleResource()
        {
            Features = new Collection<int>();
        }
    }
}