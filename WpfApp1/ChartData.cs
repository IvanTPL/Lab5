using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace WpfApp1
{
    public class ChartData
    {
        public SeriesCollection series_clct { get; set; }
        public Func<double, string> formatter { get; set; }

        public ChartData(double[] inLabels)
        {
            series_clct = new SeriesCollection();
            formatter = value => value.ToString("F3");
        }

        public void AddSeries(double[] points, double[] values, string title, int mode)
        {
            ChartValues<ObservablePoint> Values = new ChartValues<ObservablePoint>();
            for (int i = 0; i < values.Length; i++)
                Values.Add(new(points[i], values[i]));
            if (mode == 0)
            {
                series_clct.Add(new ScatterSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Black,
                    MinPointShapeDiameter = 6,
                    MaxPointShapeDiameter = 6
                });
            }
            else if (mode == 1)
            {
                series_clct.Add(new LineSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.Red,
                    PointGeometry = null
                });
            }
            else if (mode == 2)
            {
                series_clct.Add(new LineSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.Blue,
                    PointGeometry = null 
                });
            }
        }
        public void ClearCollection()
        {
            series_clct.Clear();
        }
    }
}
