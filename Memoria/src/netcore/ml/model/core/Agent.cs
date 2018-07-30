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

namespace blogs.memoria.ml.model.core
{
    public class MemoriaAgent
    {
        public static Task<PredictionModel<Sell, SellPrediction>> Build(CancellationToken Token)
        {
            return Task.Factory.StartNew(() =>
            {
                var pipeline = new LearningPipeline();
                pipeline.Add(new TextLoader(Files.LoadTrainDataSet()).CreateFrom<Sell>(useHeader: true, separator: ',')); //Load data
                pipeline.Add(new ColumnCopier(("Amount", "Label"))); //Sect Column of interest (Y)
                pipeline.Add(new ColumnConcatenator("Features", "Age", "CityCode", "Temperature", "TreatedProbability")); //select features (X)
                pipeline.Add(new FastTreeRegressor()); // Select Learning algorithm
                return pipeline.Train<Sell, SellPrediction>();
            });
        }

    }
}