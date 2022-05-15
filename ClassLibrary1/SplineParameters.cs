using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ClassLibrary1
{
    public class SplineParameters : IDataErrorInfo, INotifyPropertyChanged
    {
        private int Cnt;
        public int cnt
        {
            get { return Cnt; }
            set
            {
                Cnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cnt)));
            }

        }
        public double[] spl_dvs_1 { get; set; }
        public double[] spl_dvs_2 { get; set; }
        public bool error_cnt = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public SplineParameters(int cnt)
        {
            this.cnt = cnt;
            spl_dvs_1 = new double[2];
            spl_dvs_2 = new double[2];
            spl_dvs_1[0] = -5.0;
            spl_dvs_1[1] = 1.5;
            spl_dvs_2[0] = 5.0;
            spl_dvs_2[1] = 7.5;
        }

        public string this[string column]
        {
            get
            {
                string error = null;
                switch (column)
                {
                    case "cnt":
                        if (cnt < 3)
                        {
                            error = "Cnt should be greater than 2";
                            error_cnt = true;
                        }
                        else
                        {
                            error_cnt = false;
                        }
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
