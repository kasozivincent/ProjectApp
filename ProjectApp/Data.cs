using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectApp
{
    public class Data : IComparable<Data>
    {
        public string Label { get; set; } //stores the string of the disease
        public string Percentage { get; set; } // stores the percentage of the predicted disease
        public float Prediction { get; set; } // stores the output prediction from the model. it is from 0 to 1, so we scale it up in percentage.

        public int CompareTo(Data other)
        {
            if (this.Prediction * 100 < other.Prediction * 100)
                return 1;
            else if (this.Prediction * 100 > other.Prediction * 100)
                return -1;
            else
                return 0;
        }
    }
}
