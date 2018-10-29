﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHouseMachineLearningv100.Model;
using SmartHouseMachineLearningv100.Repository;

namespace SmartHouseMachineLearningv100.Controllers
{
    [Route("api/[controller]")]
    public class LightBulbController : ControllerBase
    {
        private readonly ILightBulbRepository _lightBulbRepository;

        public LightBulbController(ILightBulbRepository lightBulbRepository)
        {
            _lightBulbRepository = lightBulbRepository;
        }

        // GET: api/LightBulb
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _lightBulbRepository.GetAllLightBulbs());
        }

        // POST: api/LightBulb
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PredictionUsageLightBulbModel lightBulbDbModel)
        {
            await _lightBulbRepository.Create(lightBulbDbModel);
            return new OkObjectResult(lightBulbDbModel);
        }
    }
}