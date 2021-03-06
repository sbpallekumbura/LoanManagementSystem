﻿using System;
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

using LoanManagementSystem.DBModel;
using LoanManagementSystem.Util;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace LoanManagementSystem.View.WpfPage.Staff.Content
{
    /// <summary>
    /// Interaction logic for CashBorrow.xaml
    /// </summary>
    public partial class CashBorrow : Page
    {
        private static CashBorrow _instance;
        private static string type="borrow";

        private CashBorrow()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this,AmountTextBox);
            setTodayTransactionList();
        }

        public static CashBorrow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CashBorrow();
                }
                return _instance;
            }

        }

        public decimal Amount
        {
            get
            {
                return Convert.ToDecimal(AmountTextBox.Text);
            }
            set
            {
                if (value == 0)
                {
                    AmountTextBox.Text = "";
                }
                else
                AmountTextBox.Text = Convert.ToString(value);
                FocusManager.SetFocusedElement(this, AmountTextBox);
            }
        }

        public DateTime BorrowDateTime
        {
            get
            {
                return Convert.ToDateTime(CashBorrowDayPicker.Text+" "+System.DateTime.Now.TimeOfDay);
            }
            set
            {
                CashBorrowDayPicker.Text = Convert.ToString(value);
            }
        }

        public string Remark
        {
            get
            {
                return Convert.ToString(CommentTextBox.Text);
            }
            set
            {
                CommentTextBox.Text = Convert.ToString(value);
            }
        }

        public void clearBorrowForm()
        {
            Amount = Convert.ToDecimal("0");
            Remark="";
        }

        private void Amount5000Button_Click(object sender, RoutedEventArgs e)
        {
            Amount = Convert.ToDecimal("5000");
        }

        private void Amount10000Button_Click(object sender, RoutedEventArgs e)
        {
            Amount = Convert.ToDecimal("10000");
        }

        private void Amount20000Button_Click(object sender, RoutedEventArgs e)
        {
            Amount = Convert.ToDecimal("20000");
        }

        private employee_cash getEmployee_Cash()
        {
            employee_cash emp_cash = new employee_cash();
            emp_cash.ID = IDHandller.generateID("employee_cash");
            emp_cash.TYPE = type;
            emp_cash.AMOUNT = Amount;
            emp_cash.TRANSACTION_DATE_TIME = BorrowDateTime;
            emp_cash.REMARK = Remark;

            emp_cash.STATUS = true;
            emp_cash.INSERT_DATETIME = DateTime.Now;
            emp_cash.INSERT_USER_ID = Session.LoggedEmployee.ID;
            emp_cash.UPDATE_DATETIME = DateTime.Now;
            emp_cash.UPDATE_USER_ID = Session.LoggedEmployee.ID;

            emp_cash.FK_EMPLOYEE_ID = Session.SelectedEmployee.ID;

            return emp_cash;
        }
                
        private async void CashBorrowSaveButton_Click(object sender, RoutedEventArgs e)
        {
            employee_cash emp_cash = getEmployee_Cash();
            if (Employee_CashService.InsertEmployee_cash(emp_cash) == 1)
            {
                await MainWindow.Instance.ShowMessageAsync("Employe_cash Borrow Success", "Transaction Added Success!");
                clearBorrowForm();
                setTodayTransactionList();
                //await controller.CloseAsync();
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Employe_cash Insert Error", "Please check Deatails", MessageDialogStyle.Affirmative);
            }
        }

        public void setTodayTransactionList()
        {
            TodayTransactionList.ItemsSource = Session.SelectedEmployee.TRANSACTION_LIST;
        }

        private async void DeleteTodayTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Content;
            Label lbl = sp.Children.OfType<Label>().FirstOrDefault();

            if (lbl.Content.ToString() != "")
            {
                if (Employee_CashService.DeleteEmployee_cash_ByID(lbl.Content.ToString()) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync("Transaction", "Transaction Deleted..", MessageDialogStyle.Affirmative);
                    setTodayTransactionList();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync("Transaction", "Transaction Delete Fail..Retry..", MessageDialogStyle.Affirmative);
                }
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Transaction","Transaction Selecting Error..", MessageDialogStyle.Affirmative);

            }
        }


    }
}
