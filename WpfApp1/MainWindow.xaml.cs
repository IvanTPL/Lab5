using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewData data { get; set; }
        public bool show_check = false;
        public bool isMeasured { get; set; }
        public MainWindow()
        {
            try
            {
                data = new();
                DataContext = this;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InitializeComponent();
            Func.ItemsSource = Enum.GetValues(typeof(ClassLibrary1.SPf));
        }
        private void MeasuredData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(data.spData.md.error_limits | data.spData.md.error_cnt | data.spData.sp.error_cnt);
        }
        private void MeasuredData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                given_dv_1_a.Text = "";
                given_dv_1_b.Text = "";
                dv_1_a.Text = "";
                dv_1_b.Text = "";
                dv_1_a_h.Text = "";
                dv_1_b_h.Text = "";
                given_dv_2_a.Text = "";
                given_dv_2_b.Text = "";
                dv_2_a.Text = "";
                dv_2_b.Text = "";
                dv_2_a_h.Text = "";
                dv_2_b_h.Text = "";
                val_a.Text = "";
                val_b.Text = "";
                val_a_h.Text = "";
                val_b_h.Text = "";
                data.spData.md.NewGrid();
                data.spData.md.Measure();
                isMeasured = true;
                data.Graphics.ClearCollection();
                data.Graphics.AddSeries(data.spData.md.grid, data.spData.md.measures, "Measured", 0);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Splines_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(data.spData.md.error_limits | data.spData.md.error_cnt | data.spData.sp.error_cnt) && isMeasured;
        }

        private void Splines_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                data.Graphics.ClearCollection();
                data.Graphics.AddSeries(data.spData.md.grid, data.spData.md.measures, "Measured", 0);
                double status = data.spData.Create_spl_1();

                if (status == 0)
                {
                    val_a.Text = $"Spline value at point 'a': {data.spData.dvs_1[0]}";
                    val_a_h.Text = $"Spline value at point 'a + h': {data.spData.dvs_1[2]}";
                    val_b.Text = $"Spline value at point 'b': {data.spData.dvs_1[4]}";
                    val_b_h.Text = $"Spline value at point 'b - h': {data.spData.dvs_1[6]}";
                    given_dv_1_a.Text = $"Given value at point 'a': {data.spData.sp.spl_dvs_1[0]:0.00}";
                    given_dv_1_b.Text = $"Given value at point 'b': {data.spData.sp.spl_dvs_1[1]:0.00}";
                    dv_1_a.Text = $"Value at point 'a': {data.spData.dvs_1[1]:0.00}";
                    dv_1_b.Text = $"Value at point 'b': {data.spData.dvs_1[5]:0.00}";
                    dv_1_a_h.Text = $"Value at point 'a + h': {data.spData.dvs_1[3]:0.00}";
                    dv_1_b_h.Text = $"Value at point 'b - h': {data.spData.dvs_1[7]:0.00}";
                    data.Graphics.AddSeries(data.spData.grid_uniform, data.spData.poly_spl_1, "Spline1", 1);
                }
                else
                {
                    MessageBox.Show($"Error in Interpolate()1: {status}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                status = data.spData.Create_spl_2();

                if (status == 0)
                {
                    given_dv_2_a.Text = $"Given value at point 'a': {data.spData.sp.spl_dvs_2[0]:0.00}";
                    given_dv_2_b.Text = $"Given value at point 'b': {data.spData.sp.spl_dvs_2[1]:0.00}";
                    dv_2_a.Text = $"Value at point 'a': {data.spData.dvs_2[1]:0.00}";
                    dv_2_b.Text = $"Value at point 'b': {data.spData.dvs_2[5]:0.00}";
                    dv_2_a_h.Text = $"Value at point 'a + h': {data.spData.dvs_2[3]:0.00}";
                    dv_2_b_h.Text = $"Value at point 'b - h': {data.spData.dvs_2[7]:0.00}";
                    data.Graphics.AddSeries(data.spData.grid_uniform, data.spData.poly_spl_2, "Spline2", 2);
                }
                else
                {
                    MessageBox.Show($"Error in Interpolate()2: {status}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand MeasuredData = new
            (
                "MeasuredData",
                "MeasuredData",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D1, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Splines = new
            (
                "Splines",
                "Splines",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D2, ModifierKeys.Control)
                }
            );
    }
}
