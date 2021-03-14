﻿using Google.Maps;
using Google.Maps.Geocoding;
using Google.Maps.StaticMaps;
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
using TaskGrab.Navigation;

namespace TaskGrab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        private bool menu_open = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOpenModal_Click(object sender, RoutedEventArgs e)
        {
            
        }



        private void MenuToggle(object sender, RoutedEventArgs e)
        {
            Button menuButton = (Button)sender;
            if (menu_open)
            {
                menuButton.LayoutTransform = new RotateTransform(0);
                this.MenuActions.Height = 0;
                menu_open = false;

            }
            else
            {
                menuButton.LayoutTransform = new RotateTransform(90);
                this.MenuActions.Height = double.NaN;
                menu_open = true;
            }
        }
    }
}
