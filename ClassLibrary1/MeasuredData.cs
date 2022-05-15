using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ClassLibrary1
{
    public class MeasuredData : IDataErrorInfo, INotifyPropertyChanged
    {
        private int cnt;
        public int Cnt
        {
            get { return cnt; }
            set
            {
                error_cnt = false;
                cnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cnt)));
            }

        }
        private double left;
        public double Left
        {
            get { return left; }
            set
            {
                error_limits = false;
                left = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Left)));
            }
        }
        private double right;
        public double Right
        {
            get { return right; }
            set
            {
                error_limits = false;
                right = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Right)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> clct { get; set; }
        public SPf func { get; set; }
        public double[] grid { get; set; }
        public double[] measures { get; set; }
        public bool error_cnt = false;
        public bool error_limits = false;

        public MeasuredData(int Cnt, double Left, double Right, SPf func)
        {
            this.Cnt = Cnt;
            this.Left = Left;
            this.Right = Right;
            this.func = func;
            clct = new();
        }

        public void NewGrid()
        {
            grid = new double[Cnt];
            grid[0] = Left;
            grid[Cnt - 1] = Right;
            var rand = new Random();
            for (int i = 1; i < Cnt - 1; i++)
            {
                double random_val = Left;
                while (random_val <= Left)
                    random_val = Right * rand.NextDouble();
                grid[i] = random_val;
            }
            Array.Sort(grid);
        }

        public void Measure()
        {
            measures = new double[Cnt];

            if (func == SPf.NET_func)
            {
                for (int i = 0; i < Cnt; i++)
                    measures[i] = Math.Sin(grid[i]);
            }
            else if (func == SPf.Poly)
            {
                for (int i = 0; i < Cnt; i++)
                    measures[i] = Math.Pow(grid[i], 3);
            }
            else if (func == SPf.Random)
            {
                var rand = new Random();
                for (int i = 0; i < Cnt; i++)
                    measures[i] = i * rand.NextDouble();
            }

            clct.Clear();
            for (int i = 0; i < Cnt; i++)
                clct.Add($"Point: {grid[i]}\nFunction value: {measures[i]}\n");
        }

        public string this[string column]
        {
            get
            {
                string error = null;
                switch (column)
                {
                    case "Cnt":
                        if (Cnt < 3)
                        {
                            error_cnt = true;
                            error = "Cnt should be greater than 2";
                        }
                        else
                            error_cnt = false;
                        break;
                    case "Right":
                        if (Right < Left)
                        {
                            error_limits = true;
                            error = "Right is less than left";
                        }
                        else
                            error_limits = false;
                        break;
                    case "Left":
                        if (Right < Left)
                        {
                            error_limits = true;
                            error = "Left is greater than right";
                        }
                        else
                            error_limits = false;
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
