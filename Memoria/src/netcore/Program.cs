using System;
using System.Threading;
using System.Threading.Tasks;
using blogs.memoria.helpers;
using blogs.memoria.ml;
using blogs.memoria.ml.model;
using blogs.memoria.ml.model.core;
using blogs.memoria.ml.model.ext;
using Microsoft.ML;

namespace netcore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var source = new CancellationTokenSource();
            var model = await new MemoriaAgent().Build(source.Token);
            var evaluation = await model.Evaluate();

            Console.WriteLine($" Rms : {evaluation.Rms}");
            Console.WriteLine($" Coef of determination : {evaluation.RSquared}");
            Console.WriteLine("Testing mode with single prediction:");

            var test = new Sell
            {

                Temperature = 7,
                Age = 72,
                TreatedProbability = 0.7741935483870968f,
                CityCode = 1
            };

            var prediction = model.Predict(test);
            Console.WriteLine($"Prediction is {prediction.Amount}");

            Console.WriteLine($"Saving the model...");
            await model.SaveAsync();

            Console.WriteLine($"Reloading the model...");
            var loadedModel = await PredictionModel.ReadAsync<Sell, SellPrediction>(Files.LoadStorePath());
            prediction = loadedModel.Predict(test);
            Console.WriteLine($"Prediction using previous test data is {prediction.Amount}");
        }
    }
}
