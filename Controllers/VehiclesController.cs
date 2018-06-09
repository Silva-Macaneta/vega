using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;


namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController: Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public IVehicleRepository Repository { get; }

        public VehiclesController(VegaDbContext context, IMapper mapper,IVehicleRepository Repository, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.mapper = mapper;
            this.Repository = Repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleResource>> GetVehicles()
        {
            var Vehicle = await context.Vehicle.ToListAsync();

            return mapper.Map<List<Vehicle>,List<VehicleResource>>(Vehicle);        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await Repository.GetVehicleAsync(id);

            if(vehicle == null)
                return NotFound();

            var result = mapper.Map<Vehicle,VehicleResource>(vehicle);            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveVehicleResource VehicleResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var vehicle = mapper.Map<SaveVehicleResource,Vehicle>(VehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            Repository.Add(vehicle);
            await unitOfWork.CompleteAsync();
                
            vehicle = await Repository.GetVehicleAsync(vehicle.id);

            var result = mapper.Map<Vehicle,VehicleResource>(vehicle);            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveVehicleResource VehicleResource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var vehicle = await Repository.GetVehicleAsync(id);

            if(vehicle == null) 
                return NotFound();
            
            mapper.Map<SaveVehicleResource,Vehicle>(VehicleResource,vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            var result = mapper.Map<Vehicle,VehicleResource>(vehicle);            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await Repository.GetVehicleAsync(id,false);
            
            if(vehicle == null) 
                return NotFound();

            Repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();
            
            return Ok(id);
        }
    }
}