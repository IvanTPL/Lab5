using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;

namespace ClassLibrary1
{
    public class SplinesData
    {
        public MeasuredData md { get; set; }
        public SplineParameters sp { get; set; }
        public double[] poly_spl_1 { get; set; }
        public double[] poly_spl_2 { get; set; }
        public double[] dvs_1 { get; set; } = new double[9];
        public double[] dvs_2 { get; set; } = new double[9];

        public double[] grid_uniform { get; set; }

        public SplinesData(MeasuredData md, SplineParameters sp)
        {
            this.md = md;
            this.sp = sp;
        }

        [DllImport("..\\..\\..\\..\\x64\\Debug\\DLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double Create_spline(int cnt, int cnt_uniform, double[] points, double[] limits, double[] func, double[] dvs, double[] res);
        public double Create_spl_1()
        {
            grid_uniform = new double[sp.cnt];
            for (int i = 0; i < sp.cnt; i++)
                grid_uniform[i] = md.Left + (Math.Abs(md.Right - md.Left) / (sp.cnt - 1)) * i;    
            poly_spl_1 = new double[sp.cnt];
            poly_spl_2 = new double[sp.cnt];
            double[] res = new double[3 * sp.cnt];
            double[] limits = new double[2];
            limits[0] = md.Left;
            limits[1] = md.Right;
            double status = Create_spline(md.Cnt, sp.cnt, md.grid, limits, md.measures, sp.spl_dvs_1, res);

            if (status == 0)
            {
                for (int i = 0; i < sp.cnt; i++)
                    poly_spl_1[i] = res[0 + (3 * i)];
                dvs_1[0] = res[0];                  // Spline value at 'a'
                dvs_1[1] = res[1];                  // First derivative at 'a'
                dvs_1[2] = res[3];                  // Spline value at 'a + h'
                dvs_1[3] = res[4];                  // First derivative at 'a + h'
                dvs_1[4] = res[(3 * sp.cnt) - 3];   // Spline value at 'b'
                dvs_1[5] = res[(3 * sp.cnt) - 2];   // ЗFirst derivative at 'b'
                dvs_1[6] = res[(3 * sp.cnt) - 6];   // Spline value at 'b - h'
                dvs_1[7] = res[(3 * sp.cnt) - 5];   // First derivative at 'b - h'
                return 0;
            }
            else
                return status;
           
        }
        public double Create_spl_2()
        {
            grid_uniform = new double[sp.cnt];
            for (int i = 0; i < sp.cnt; i++)
                grid_uniform[i] = md.Left + (Math.Abs(md.Right - md.Left) / (sp.cnt - 1)) * i;
            double[] res = new double[3 * sp.cnt];
            double[] limits = new double[2];
            limits[0] = md.Left;
            limits[1] = md.Right;
            double status = Create_spline(md.Cnt, sp.cnt, md.grid, limits, md.measures, sp.spl_dvs_2, res);

            if (status == 0)
            {
                for (int i = 0; i < sp.cnt; i++)
                    poly_spl_2[i] = res[0 + (3 * i)];
                dvs_2[0] = res[0];
                dvs_2[1] = res[1];
                dvs_2[2] = res[3];
                dvs_2[3] = res[4];
                dvs_2[4] = res[(3 * sp.cnt) - 3];
                dvs_2[5] = res[(3 * sp.cnt) - 2];
                dvs_2[6] = res[(3 * sp.cnt) - 6];
                dvs_2[7] = res[(3 * sp.cnt) - 5];
                return 0;
            }
            else
                return status;
        }
    }
}
