using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ML.Legacy;
using Microsoft.ML.Legacy.Data;
using Microsoft.ML.Legacy.Trainers;
using Microsoft.ML.Legacy.Transforms;
using SmartHouseMachineLearningv100.Repository;

namespace SmartHouseMachineLearningv100.Services
{
    public class MachineLearningService : IMachineLearningService
    {
        private readonly ILightBulbRepository _lightBulbRepository;

        public MachineLearningService(ILightBulbRepository lightBulbRepository)
        {
            _lightBulbRepository = lightBulbRepository;
        }
        static string currentDirectory =
            "E:\\SmartHouse\\SmartHouseMachineLearningv100\\SmartHouseMachineLearningv100";
        static readonly string _dataPath = Path.Combine(currentDirectory, "MachineLearningData", "bulbLightTrain.csv");
        static readonly string _modelpath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");
        PredictionModel<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel> model;

        public async Task<bool> Predicate(UsageOfLightBulbModel usageOfLightBulbModel)
        {
          // PredictionModel<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel> model = await Train();
            UsageOfLightBulbPredictionModel prediction = model.Predict(usageOfLightBulbModel);
            Console.WriteLine("Predicted fare: {0}, actual fare: 29.5", prediction.IsOn);
            var result = (prediction.IsOn > 0.5) ? true : false;
            return result;
        }


        public async Task<PredictionModel<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel>> Train()
        {
            var dataFromDb = await _lightBulbRepository.GetAllLightBulbs();
         
            var dataToTrain = new List<UsageOfLightBulbModel>(dataFromDb);
       
            var collection = CollectionDataSource.Create(dataToTrain);
        
            var pipeline = new LearningPipeline
            {
                collection,
                new ColumnCopier(("IsOn", "Label")),
                new ColumnConcatenator(
                    "Features",
                    "LightBulbID",
                    "Month",
                    "Day",
                    "Time"),
                new FastTreeRegressor(),
            
            };
            model = pipeline.Train<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel>();

            //  await model.WriteAsync(_modelpath);
            return model;
        }
    }
}
