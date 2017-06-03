using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using System.Windows.Media;
using System.Drawing;


using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace ImageQualityCheck
{
    internal static class NotifyDataText
    {
        public static readonly string Capt_Err = "Error";
        public static readonly string Capt_Confirm = "Confirmation";
        public static readonly string Capt_Success = "Success";

        public static readonly string Err_Input_Img = "Some field is empty ! Please check again...";
        public static readonly string Clear_Input_Img = "All field was Cleared ! Process Successfully...";
        public static readonly string Confirm_Exec_Process_Img = "Confirm to Execute ? Click 'OK' to continue...";
    }

    class DataProcess
    {

        public PointCollection Exec_Histo(int[] pointHisto)
        {
            var values = SmoothHistogram(pointHisto);

            int max = values.Max();

            PointCollection dots = new PointCollection();
            // first point (lower-left corner)
            dots.Add(new System.Windows.Point(0, max));
            // middle points
            for (int i = 0; i < values.Length; i++)
            {
                dots.Add(new System.Windows.Point(i, max - values[i]));
            }
            // last point (lower-right corner)
            dots.Add(new System.Windows.Point(values.Length - 1, max));

            return dots;
        }

        private int[] SmoothHistogram(int[] originalValues)
        {
            int[] smoothedValues = new int[originalValues.Length];

            double[] mask = new double[] { 0.25, 0.5, 0.25 };

            for (int bin = 1; bin < originalValues.Length - 1; bin++)
            {
                double smoothedValue = 0;
                for (int i = 0; i < mask.Length; i++)
                {
                    smoothedValue += originalValues[bin - 1 + i] * mask[i];
                }
                smoothedValues[bin] = (int)smoothedValue;
            }

            return smoothedValues;
        }

    }
}
