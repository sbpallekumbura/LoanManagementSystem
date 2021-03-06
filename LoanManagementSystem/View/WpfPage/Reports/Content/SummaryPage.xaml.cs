﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Reports.CrystalReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoanManagementSystem.View.WpfPage.Reports.Content
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class SummaryPage : Page, INotifyPropertyChanged
    {
        private static SummaryPage _instance;
        public event PropertyChangedEventHandler PropertyChanged;

        private List<area> _areaList;
        private area _selectedArea;
        private DateTime _dateFrom = System.DateTime.Now;
        private DateTime _dateTo = System.DateTime.Now;

        private SummaryPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static SummaryPage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SummaryPage();
                }
                return _instance;
            }

        }

        public List<area> AreaList
        {
            get { return _areaList; }
            set
            {
                _areaList = value;
                OnPropertyChanged("AreaList");
            }
        }

        public area SelectedArea
        {
            get
            {
                if (_selectedArea == null)
                {
                    _selectedArea = new area();
                    _selectedArea.AREA_NAME = "ALL";
                }
                return _selectedArea;
            }
            set
            {
                _selectedArea = value;
                OnPropertyChanged("SelectedArea");
            }
        }

        public DateTime DateFrom
        {
            get
            {
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }

        public DateTime DateTo
        {
            get
            {
                return _dateTo;
            }
            set
            {
                _dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AreaList = (List<area>)AreaService.getAreaCodes();
            SelectedArea = this.SelectedArea;

            if (AreaComboBox.SelectedIndex == -1)
            {
                AreaComboBox.SelectedIndex = 0;
            }
        }

        private void GenerateReport(string area,DateTime from,DateTime to)
        {
           // ReportDocument report = new ReportDocument();
            //report.Load(@"D:\Projects\Visual Studio 2012\LoanManagementSystem\LoanManagementSystem\Reports\CrystalReports\LoanCrystalReport.rpt");

            LoanCrystalReport loan_report = new LoanCrystalReport();
            PaymentCrystalReport payment_report = new PaymentCrystalReport();

            var loans = LoanService.GetSearchedLoanList(area,from,to);
            var payments = PaymentService.GetSearchedPaymentList(area,from,to);

            DataSet ds1 = Util.ListToDataSet.ToDataSet(loans);
            loan_report.SetDataSource(ds1.Tables[0]);

            DataSet ds2 = Util.ListToDataSet.ToDataSet(payments);
            payment_report.SetDataSource(ds2.Tables[0]);

            loan_report.Subreports[0].SetDataSource(ds2.Tables[0]);

            ParameterField pfGrantedDate = loan_report.ParameterFields["GrantedDate"];
            ParameterDiscreteValue pd1;
            pd1 = new ParameterDiscreteValue();
            if (from.Date == to.Date)
            {
                pd1.Value = "on " + from.Date.ToString("d MMM yyyy");
            }
            else
            {
                pd1.Value = "from " + from.Date.ToString("d MMM yyyy") + " to " + to.Date.ToString("d MMM yyyy");
            }           
            pfGrantedDate.CurrentValues.Add(pd1);

            ParameterField pfGrantedDate1 = payment_report.ParameterFields["GrantedDate"];
            ParameterDiscreteValue pd2;
            pd2 = new ParameterDiscreteValue();
            pd2 = pd1;
            pfGrantedDate1.CurrentValues.Add(pd2);

            crystalReportsViewer1.ViewerCore.ReportSource = loan_report;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var h = ((System.Windows.Controls.Panel)System.Windows.Application.Current.MainWindow.Content).ActualHeight;
            ScrollBar.Height = h - 150;
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void GenrateReportButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport(SelectedArea.AREA_NAME, DateFrom, DateTo);
        }
    }
}
