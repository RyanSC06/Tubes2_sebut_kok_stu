using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool bfsStatus = false;
        bool dfsStatus = false;
        string filepath = "";
        int width = 15, height = 15;
        double duration = 100;
        double execTime;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text files (*.txt) | *.txt";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filepath= dlg.FileName;
                btnOpenFile.Content = System.IO.Path.GetFileName(filepath);
            }
        }

        private void rbBfs_Checked(object sender, RoutedEventArgs e)
        {
            bfsStatus = (bool)rbBfs.IsChecked;
            dfsStatus = (bool)rbDfs.IsChecked;
        }

        private void rbDfs_Checked(object sender, RoutedEventArgs e)
        {
            bfsStatus = (bool)rbBfs.IsChecked;
            dfsStatus = (bool)rbDfs.IsChecked;
        }

        private void slDuration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            duration = slDuration.Value;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (filepath == "") {
                MessageBox.Show("You haven't choose any file", "Error Message");
            }
            else if ( !bfsStatus && !dfsStatus) {
                MessageBox.Show("You haven't choose any file. Pick one, BFS or DFS", "Error Message");
            } else {
                
                for (int i = 0; i < width; i++) {
                    // Create a new column definition
                    ColumnDefinition newColumn = new ColumnDefinition();
                    newColumn.Width = new GridLength(1, GridUnitType.Star);

                    // Add the column to the grid
                    map.ColumnDefinitions.Add(newColumn);
                }
                
                for (int j = 0; j < height; j++) {
                    // Create a new row definition
                    RowDefinition newRow = new RowDefinition();
                    newRow.Height = new GridLength(1, GridUnitType.Star);

                    // Add the row to the grid
                    map.RowDefinitions.Add(newRow);                    
                }

                for (int i = 0; i < width; i++) {
                    for (int j = 0; j < height; j++) {
                        StackPanel panel= new StackPanel();
                        panel.Background = new SolidColorBrush(Colors.Black);
                        panel.HorizontalAlignment = HorizontalAlignment.Stretch;
                        panel.VerticalAlignment = VerticalAlignment.Stretch;
                        panel.Margin = new Thickness(0.5);
                        map.Children.Add(panel);
                        Grid.SetRow(panel, j);
                        Grid.SetColumn(panel, i);
                    }
                }
            }
        }
    }
}
