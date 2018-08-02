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
                //Creattion du pipeline
                var pipeline = new LearningPipeline(); 

                //Chargement du fichier d'entrainement
                pipeline.Add(new TextLoader(Files.LoadTrainDataSet()).CreateFrom<Sell>(useHeader: true, separator: ',')); //Load data
                
                //Selection de la colonne à prédire (Y)
                pipeline.Add(new ColumnCopier(("Amount", "Label"))); 

                //Selection des colonnes aidant à prédire (X)
                pipeline.Add(new ColumnConcatenator("Features", "Age", "CityCode", "Temperature", "TreatedProbability")); //select features (X)
                
                //Selection de l'algorithme utilisé pour la prédiction
                pipeline.Add(new FastTreeRegressor()); 

                //Lancement du processus d'entrainement
                return pipeline.Train<Sell, SellPrediction>();
            });
        }

    }
}