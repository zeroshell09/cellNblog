using System;
using Microsoft.ML.Runtime.Api;

namespace blogs.memoria.ml.model {

    public class Sell {

        [Column("1")]
        public float Age;
        
        [Column("2")]
        public float CityCode;

        [Column("3")]
        public float Amount;

        [Column("4")]
        public float Temperature;

        [Column("5")]
        public float TreatedProbability;
    }


    public class SellPrediction{

        [ColumnName("Score")] // Regression task output field
        public float Amount;
    }
}