﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System;
using System.Windows.Media;

using LoanManagementSystem.View.WpfPage;
using MahApps.Metro.Controls.Dialogs;
using LoanManagementSystem.Util;
using LoanManagementSystem.Properties;


namespace LoanManagementSystem.View.WpfWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static MainWindow instance;

        private MainWindow()
        {
            InitializeComponent();
            this.ShowCloseButton = false;

            ContentFrame.Content = DashBoardPage.Instance;
            setLoginDeatails();            
            instance = this;
        }

        public static MainWindow Instance
        {
            get
            {
                if (instance == null || !instance.IsActive)
                {
                    instance = new MainWindow();
                }

                return instance; 
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OnMinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OnMaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
        }

        private void OnCloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DoctorHomePageButton_Click(object sender, RoutedEventArgs e)
        {
            TransferToDoctorHomePage();
        }

        public void TransferToDoctorHomePage()
        {
            //ContentFrame.Content = null;
            //ContentFrame.Content = DoctorHomePage.Instance;
        }

        private void LogoutPageButton_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.Instance.FileMenu.Visibility = Visibility.Hidden;
            //ContentFrame.Content = DashBoardPage.Instance;
        }

        private void PatientsPageButton_Click(object sender, RoutedEventArgs e)
        {
           // ContentFrame.Content = PatientsPage.Instance;
        }

        private void DirectoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Content = EmployeePage.Instance;
        }

        private void DashBoardButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Content = DashBoardPage.Instance;
        }

        private async void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
                MessageDialogResult result = await this.ShowMessageAsync("Log off", "Do You really want to Log off?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    LoginWindow.Instance.Show();
                    this.Close();
                }

        }

        private void CompanyButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Content = CompanyPage.Instance;
            //CompanyPage.Instance.ContentFrame.Content = ViewPage.Instance;
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Content = ReportPage.Instance;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = DashBoardPage.Instance;
        }
        public void setLoginDeatails()
        {
            this.EmpUserNameLable.Content = Util.LetterHandller.UppercaseFirst(Session.LoggedEmployee.USERNAME);
            Util.ImageHandller.setProfImage(Session.LoggedEmployee.PROFPIC, this.ProfPicBox);

            Settings.Default["RecentLoginName"] = Session.LoggedEmployee.USERNAME;
            Settings.Default["RecentLoginUserProfPic"] = System.Convert.ToBase64String(Session.LoggedEmployee.PROFPIC);
            Settings.Default.Save();
        }
    }
}
