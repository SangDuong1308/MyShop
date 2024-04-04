using LiveCharts.Wpf;
using LiveCharts;
using MyShop.BUS;
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

namespace MyShop.UI.Pages
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        private ReportBUS _reportBUS;
        private CartesianChart _chart;
        private int _currentYear;
        private Frame _pageNavigation;
        private ProgressBar _loadingProgressBar;
        public Report(Frame pageNavigation, ProgressBar loadingProgressBar)
        {
            _reportBUS = new ReportBUS();
            _pageNavigation = pageNavigation;
            _loadingProgressBar = loadingProgressBar;
            InitializeComponent();

            _chart = chart;

            _chart.AxisY.Add(new Axis
            {
                Foreground = Brushes.Black,
                Title = "Revenue/ Profit",
                MinValue = 0
            });
            Title.Text = "Revenue by year";
        }

        private async void displayYearMode()
        {
            _loadingProgressBar.IsIndeterminate = true;
            var pricesByYear = await _reportBUS.groupPriceTotalByYear();
            var profitsByYear = await _reportBUS.groupProfitTotalByYear();
            _loadingProgressBar.IsIndeterminate = false;
            var valuesLineChart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByYear)
            {
                valuesColChart.Add((double)item);
            }

            foreach (var item in profitsByYear)
            {
                valuesLineChart.Add((double)item);
            }

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new LineSeries()
            {
                Stroke = Brushes.DeepSkyBlue,
                Title = "Profit by Year",
                Values = valuesLineChart
            });

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Revenue by Year",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Year",
                    Labels = new string[] {
                        "2023",
                        "2024",
                    }
                });
            Title.Text = "Revenue by year ";
        }

        private async void displayMonthMode(int year)
        {
            _loadingProgressBar.IsIndeterminate = true;
            var pricesByMonth = await _reportBUS.groupPriceTotalByMonth(year);
            var profitsByMonth = await _reportBUS.groupProfitTotalByMonth(year);
            _loadingProgressBar.IsIndeterminate = false;
            var valuesLineChart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByMonth)
            {
                valuesColChart.Add((double)item);
            }

            foreach (var item in profitsByMonth)
            {
                valuesLineChart.Add((double)item);
            }

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new LineSeries()
            {
                Stroke = Brushes.DeepSkyBlue,
                Title = "Profit by Month",
                Values = valuesLineChart
            });

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Revenue by Month",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Month",
                    Labels = new string[] {
                        "January",
                        "February",
                        "March",
                        "April",
                        "May",
                        "June",
                        "July",
                        "August",
                        "September",
                        "October",
                        "November",
                        "December",
                    }
                });
            Title.Text = "Month View Mode";
            MonthCombobox.IsEnabled = true;
            MonthCombobox.SelectedIndex = 0;
        }

        private async void displayWeekMode(int month, int year)
        {
            _loadingProgressBar.IsIndeterminate = true;
            var pricesByWeek = await _reportBUS.groupPriceTotalByWeek(month, year);
            var profitsByWeek = await _reportBUS.groupProfitTotalByWeek(month, year);
            _loadingProgressBar.IsIndeterminate = false;
            var valuesLineChart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByWeek)
            {
                valuesColChart.Add((double)item);
            }

            foreach (var item in profitsByWeek)
            {
                valuesLineChart.Add((double)item);
            }

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new LineSeries()
            {
                Stroke = Brushes.DeepSkyBlue,
                Title = "Profit by Week",
                Values = valuesLineChart
            });

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Revenue by Week",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Week",
                    Labels = new string[] {
                        "Week 1",
                        "Week 2",
                        "Week 3",
                        "Week 4",
                        "Week 5",
                    }
                });
            Title.Text = "Week View Mode";
        }

        private async void displayDateMode(DateTime startDate, DateTime endDate)
        {
            _loadingProgressBar.IsIndeterminate = true;
            var pricesByDate = await _reportBUS.groupPriceTotalByDate(startDate, endDate);
            var profitsByDate = await _reportBUS.groupProfitTotalByDate(startDate, endDate);
            _loadingProgressBar.IsIndeterminate = false;
            var valuesLineChart = new ChartValues<double>();
            var valuesColChart = new ChartValues<double>();

            foreach (var item in pricesByDate)
            {
                valuesColChart.Add((double)item);
            }

            foreach (var item in profitsByDate)
            {
                valuesLineChart.Add((double)item);
            }

            _chart.Series = new SeriesCollection();
            _chart.AxisX = new AxesCollection();

            _chart.Series.Add(new LineSeries()
            {
                Stroke = Brushes.DeepSkyBlue,
                Title = "Profit by day",
                Values = valuesLineChart
            });

            _chart.Series.Add(new ColumnSeries()
            {
                Fill = Brushes.Chocolate,
                Title = "Revenue by Day",
                Values = valuesColChart
            });

            chart.AxisX.Add(
                new Axis()
                {
                    Foreground = Brushes.Black,
                    Title = "Date",
                    Labels = _reportBUS.EachDayConverter(startDate, endDate)
                });
            Title.Text = "Day View Mode";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            displayYearMode();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDate.SelectedDate;
            var endDate = EndDate.SelectedDate;

            displayDateMode((DateTime)startDate, (DateTime)endDate);
        }

        private void MonthCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = MonthCombobox.SelectedIndex;
            if (i == -1 || i == 0)
            {
                return;
            }
            else
            {
                displayWeekMode(i, _currentYear);
            }
        }

        private void YearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = YearCombobox.SelectedIndex;
            if (i == -1)
            {
                return;
            }
            else
            {
                if (i == 1)
                {
                    displayMonthMode(2024);
                    _currentYear = 2024;
                }
            }
        }
    }
}
