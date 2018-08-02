using System.Threading;
using System.Threading.Tasks;
using blogs.memoria.helpers;
using blogs.memoria.ml.model.core;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;

namespace blogs.memoria.ml.model.ext
{

    public static class AgentExtentions
    {
        public static Task<RegressionMetrics> Evaluate(this PredictionModel<Sell, SellPrediction> model)
        {
            return Task.Factory.StartNew(() =>
            {
                var dataLoader = new TextLoader(Files.LoadTestDataSet()).CreateFrom<Sell>(useHeader: true, separator: ',');
                var evaluator = new RegressionEvaluator();
                return evaluator.Evaluate(model, dataLoader);
            });
        }

        public static Task SaveAsync(this PredictionModel<Sell, SellPrediction> model,CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return model.WriteAsync(Files.LoadStorePath());
        }
    }
}