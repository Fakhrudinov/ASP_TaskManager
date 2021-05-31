﻿using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows.Controls;

namespace WpfApp_MetricsVisualisation.ChartControls
{
    public partial class ChartNetWork : UserControl, INotifyPropertyChanged
    {
        public string[] Labels { get; set; }
        public int valuesCount = Properties.Settings.Default.dotsOnChart; // maximum dots on chart
        public SeriesCollection LineSeriesValues { get; set; }

        public ChartNetWork()
        {
            InitializeComponent();

            LineSeriesValues = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "NetWork metric",
                    Values = new ChartValues<int> { }
                }
            };

            Labels = new string[valuesCount];

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}