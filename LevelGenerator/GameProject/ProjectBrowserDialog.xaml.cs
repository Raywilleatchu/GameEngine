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
using System.Windows.Shapes;

namespace LevelGenerator.GameProject
{
    /// <summary>
    /// Interaction logic for ProjectBrowserDialog.xaml
    /// </summary>
    public partial class ProjectBrowserDialog : Window
    {
        public ProjectBrowserDialog()
        {
            InitializeComponent();
        }

        private void OnToggleButton_Click(Object sender, RoutedEventArgs e)
        {
            if(sender == openProjectButton)
            {
                if(openProjectButton.IsChecked == true)
                {
                    newProjectButton.IsChecked = false;
                    displayContent.Margin = new Thickness(0, 40, 0, -350);
                }
                openProjectButton.IsChecked = true;
            }
            else
            {
                if(newProjectButton.IsChecked == true)
                {
                    openProjectButton.IsChecked = false;
                    displayContent.Margin = new Thickness(-800, 40, 0, -350);
                }
                newProjectButton.IsChecked = true;
            }
        }
    }
}
