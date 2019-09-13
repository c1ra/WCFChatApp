﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using System.Windows.Threading;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {

        TheController theController = TheController.Instance();
        public MainWindow()
        {
            InitializeComponent();
            AdminLogIn adminLogIn = new AdminLogIn();
            this.Navigate(adminLogIn);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            theController.Disconnect();
        }
    }
}
