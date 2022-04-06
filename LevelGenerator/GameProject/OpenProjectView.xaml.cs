using LevelGenerator.GameProject.Common.Models.Templates;
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

namespace LevelGenerator.GameProject
{
    /// <summary>
    /// Interaction logic for OpenProjectView.xaml
    /// </summary>
    public partial class OpenProjectView : UserControl
    {
        public OpenProjectView()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                var item = projectListBox.ItemContainerGenerator.ContainerFromIndex(projectListBox.SelectedIndex) as ListBoxItem;
                item?.Focus();
            };
        }


        public void OnClick_Open_Button(object sender, RoutedEventArgs e)
        {
            OnClick_Open();
        }

        public void OnClick_Open_DoubleClick(object sender, RoutedEventArgs e)
        {
            OnClick_Open();
        }

        public void OnClick_Open()
        {
            var project = OpenProject.Open(projectListBox.SelectedItem as ProjectData);
            bool dialogResult = false;
            var win = Window.GetWindow(this);
            if (project != null)
            {
                dialogResult = true;
            }
            win.DialogResult = dialogResult;
            win.DataContext = project;
            win.Close();
        }

    }
}
