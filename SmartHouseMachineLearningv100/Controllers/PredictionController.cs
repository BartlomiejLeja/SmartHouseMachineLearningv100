using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartHouseMachineLearningv100.DTO;
using SmartHouseMachineLearningv100.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHouseMachineLearningv100.Controllers
{
    [Route("api/[controller]")]
    public class PredictionController : ControllerBase
    {

        private readonly IMachineLearningService _machineLearningService;
        private readonly ILogger _logger;

        public PredictionController(IMachineLearningService machineLearningService,
            ILogger<PredictionController> logger)
        {
            _machineLearningService = machineLearningService;
            _logger = logger;
        }

        // GET: api/prediction/train
        [HttpGet("train")]
        public async Task<IActionResult> Train()
        {
            _logger.LogInformation("Train fire up");
            await _machineLearningService.Train();
            return new ObjectResult("Trained");
        }

        // GET: api/prediction/5/5/5/5/5
        [HttpGet("{lightBulbNumber}/{month}/{day}/{from}/{to}")]
        public async Task<string> GetOneHourSchedule(int lightBulbNumber,int month,
            int day,int from, int to)
        {
            var minuteScheduleList = new List<OneMinuteScheduleDTO>();
            for (var minute = from; minute < to; minute++)
            {
                var usageOfLightBulbModel = new UsageOfLightBulbModel
                {
                    LightBulbID = lightBulbNumber,
                    Month = month,
                    Day = day,
                    Time = minute,
                    IsOn = 0,
                };
                var status = await _machineLearningService.Predicate(usageOfLightBulbModel);

                minuteScheduleList.Add(new OneMinuteScheduleDTO
                {
                    ID= lightBulbNumber,
                    Minutes= minute,
                    Status= status
                });
            }
            var scheduleForOneHour = JsonConvert.SerializeObject(minuteScheduleList);
            return scheduleForOneHour;
        }   

      

    }
}
