using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ML.Legacy;
using Microsoft.ML.Legacy.Data;
using Microsoft.ML.Legacy.Trainers;
using Microsoft.ML.Legacy.Transforms;

namespace SmartHouseMachineLearningv100.Services
{
    public class MachineLearningService
    {
        static string currentDirectory =
            "E:\\SmartHouse\\SmartHouseMachineLearningv100\\SmartHouseMachineLearningv100";
        static readonly string _dataPath = Path.Combine(currentDirectory, "MachineLearningData", "bulbLightTrain.csv");
        static readonly string _modelpath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");

        public async Task<string> Predicate(UsageOfLightBulbModel UOFLBMTest1)
        {
            PredictionModel<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel> model = await Train();
            UsageOfLightBulbPredictionModel prediction = model.Predict(UOFLBMTest1);
            Console.WriteLine("Predicted fare: {0}, actual fare: 29.5", prediction.IsOn);
            return $"Predicted fare: {prediction.IsOn}";
        }
        public static async Task<PredictionModel<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel>> Train()
        {
            var pipeline = new LearningPipeline
            {
                new TextLoader(_dataPath).CreateFrom<UsageOfLightBulbModel>(useHeader: true, separator: ','),
                new ColumnCopier(("IsOn", "Label")),
                new ColumnConcatenator(
                    "Features",
                    "LightBulbID",
                    "Month",
                    "Day",
                    "Time"),
                new FastTreeRegressor()
            };

            PredictionModel<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel> model = pipeline.Train<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel>();

            //  await model.WriteAsync(_modelpath);
            return model;
        }
    }
}
