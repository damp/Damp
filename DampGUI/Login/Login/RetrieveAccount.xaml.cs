﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Login
{
    /// <summary>
    /// Interaction logic for RetrieveAccount.xaml
    /// </summary>
    public partial class RetrieveAccount : Window
    {
        private bool _cancelButtonClicked = false;
        public RetrieveAccount()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Owner.Left = this.Left;
            Owner.Top = this.Top;
            Owner.Show();
            _cancelButtonClicked = true;
            this.Close();
        }

        private void Logo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void RetrieveText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SubmitButton.IsEnabled = !RetrieveText.Text.Equals("");
        }

        private void RetrieveAccount_OnClosing(object sender, CancelEventArgs e)
        {
            if(_cancelButtonClicked ==false)
                Owner.Close();
        }
    }
}
