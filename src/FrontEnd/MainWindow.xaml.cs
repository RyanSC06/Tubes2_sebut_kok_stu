using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        string[] fileMap;
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
                if (filepath != dlg.FileName)
                {
                    filepath = dlg.FileName;
                    btnOpenFile.Content = System.IO.Path.GetFileName(filepath);
                    map.Background = new SolidColorBrush(Colors.Black);
                    map.ColumnDefinitions.Clear();
                    map.RowDefinitions.Clear();
                    map.Children.Clear();

                    fileMap = InputFile.makeMap(filepath);

                    if (fileMap[0] == "") 
                    {
                        MessageBox.Show("Map is not valid!", "Error Message");
                    } else
                    {
                        height = fileMap.Length;
                        width= fileMap[0].Length;

                        /*map.Width = width;
                        map.Height = height;*/

                        for (int i = 0; i < width; i++)
                        {
                            // Create a new column definition
                            ColumnDefinition newColumn = new ColumnDefinition();
                            newColumn.Width = new GridLength(1, GridUnitType.Star);

                            // Add the column to the grid
                            map.ColumnDefinitions.Add(newColumn);
                        }

                        for (int j = 0; j < height; j++)
                        {
                            // Create a new row definition
                            RowDefinition newRow = new RowDefinition();
                            newRow.Height = new GridLength(1, GridUnitType.Star);

                            // Add the row to the grid
                            map.RowDefinitions.Add(newRow);
                        }

                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                StackPanel panel = new StackPanel();
                                panel.HorizontalAlignment = HorizontalAlignment.Stretch;
                                panel.VerticalAlignment = VerticalAlignment.Stretch;
                                panel.Margin = new Thickness(0.5);

                                if (fileMap[i][j] == 'K')
                                {
                                    Image myImage = new Image();
                                    panel.Background = new SolidColorBrush(Colors.Red);
                                    myImage.Source = new BitmapImage(new Uri("asset/start.png", UriKind.Relative));
                                    myImage.Stretch = Stretch.Uniform;
                                    /*myImage.HorizontalAlignment = HorizontalAlignment.Center;
                                    myImage.VerticalAlignment = VerticalAlignment.Center;*/
                                    panel.Children.Add(myImage);
                                }
                                else if (fileMap[i][j] == 'R')
                                {
                                    panel.Background = new SolidColorBrush(Colors.White);
                                }
                                else if (fileMap[i][j] == 'X')
                                {
                                    panel.Background = new SolidColorBrush(Colors.Black);
                                }
                                else
                                {
                                    Image myImage = new Image();
                                    panel.Background = new SolidColorBrush(Colors.Gold);
                                    myImage.Source = new BitmapImage(new Uri("asset/treasure.png", UriKind.Relative));
                                    myImage.Stretch = Stretch.Uniform;
                                    /*myImage.HorizontalAlignment = HorizontalAlignment.Center;
                                    myImage.VerticalAlignment = VerticalAlignment.Center;*/
                                    panel.Children.Add(myImage);
                                }

                                map.Children.Add(panel);
                                Grid.SetRow(panel, i);
                                Grid.SetColumn(panel, j);

                            }
                        }
                    }
                }
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

            }
        }
    }
}
