using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ClassLibrary1;

namespace WpfApp1
{
    public class ViewData
    {
        public SplinesData spData { get; set; }
        public SplineParameters spParams { get; set; }
        public ChartData Graphics { get; set; }
        public bool InputError { get; set; }

        public ViewData()
        {
            InputError = false;
            MeasuredData mdData = new(20, 10, 1000, SPf.Poly);
            spParams = new(100);
            spParams.PropertyChanged += InputChanged;
            mdData.PropertyChanged += InputChanged;
            spData = new(mdData, spParams);
            spData.md.NewGrid();                                   
            Graphics = new(spData.md.grid);
        }

        private void InputChanged(object sender, PropertyChangedEventArgs e)
        {
            InputError = false;
        }
    }
}
