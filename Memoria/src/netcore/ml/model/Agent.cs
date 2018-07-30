using System;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using Microsoft.ML.Models;
using blogs.memoria.ml.model;
using blogs.memoria.helpers;
using System.Threading;

namespace blogs.memoria.ml
{
    public class MemoriaAgent
    {
        public Task<PredictionModel<Sell, SellPrediction>> Build(CancellationToken Token)
        {
            var pipeline = new LearningPipeline();
            var dataLoader = new TextLoader(Files.LoadInputDataSet()).CreateFrom<Sell>(useHeader: true, separator: ',');

            pipeline.Add(dataLoader); //Load data
            pipeline.Add(new ColumnCopier(("Amount", "Label"))); //Sect Column of interest (Y)
            pipeline.Add(new ColumnConcatenator("Features", "Age", "CityCode", "Temperature", "TreatedProbability")); //select features (X)
            pipeline.Add(new FastTreeRegressor()); // Select Learning algorithm

            return Task.FromResult(pipeline.Train<Sell, SellPrediction>());
        }

    }

    public static class AgentExtentions{

        public static Task<RegressionMetrics> Evaluate(this PredictionModel<Sell, SellPrediction> model)
        {
            throw new NotImplementedException();
        }
    }
}