using System;
using System.Threading;
using System.Threading.Tasks;
using blogs.memoria.helpers;
using blogs.memoria.ml;
using blogs.memoria.ml.model;

namespace netcore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var source = new CancellationTokenSource(); 
            var model = await new MemoriaAgent().Build(source.Token);
            var prediction = model.Predict(new Sell{

                Temperature= 7,
                Age = 72,
                TreatedProbability = 0.7741935483870968f,
                CityCode = 1
            });
        
            Console.WriteLine($"Prediction is {prediction.Amount}");
        }
    }
}
