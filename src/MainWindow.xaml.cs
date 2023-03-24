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
using System.Windows.Threading;
using System.Diagnostics;
using System.Data;

namespace TreasureHuntSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool bfsStatus = false;
        private bool dfsStatus = false;
        private string filepath = "";
        private int width = 15, height = 15;
        private string[] fileMap;
        private Graph graph;
        private double duration = 500;
        private double execTime;
        private List<Point> fullpath;
        private List<Point> path;
        private DispatcherTimer _timer;
        private int idx = 0;
        private int lenFullPath = 0;
        private int lenPath = 0;
        private StackPanel changedPanel = null;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(duration);
            _timer.Tick += Timer_Tick;
        }

        private void makeMap()
        {
            btnOpenFile.Content = System.IO.Path.GetFileName(filepath);
            map.Background = new SolidColorBrush(Colors.Black);
            map.ColumnDefinitions.Clear();
            map.RowDefinitions.Clear();
            map.Children.Clear();
            height = fileMap.Length;
            width = fileMap[0].Length;

            route.Text = "Route: ";
            stepLabel.Content = "Steps: ";
            nodeLabel.Content = "Nodes: ";
            execTimeLabel.Content = "Execution Time: ";

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

                    string newPanelName = "panel" + i.ToString() + j.ToString();
                    panel.SetValue(FrameworkElement.NameProperty, newPanelName);

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
                        var newColor = System.Windows.Media.Color.FromRgb(255, 223, 0);
                        panel.Background = new SolidColorBrush(newColor);
                        myImage.Source = new BitmapImage(new Uri("asset/treasure.png", UriKind.Relative));
                        myImage.Stretch = Stretch.Uniform;
                        myImage.HorizontalAlignment = HorizontalAlignment.Center;
                        myImage.VerticalAlignment = VerticalAlignment.Center;
                        panel.Children.Add(myImage);
                    }

                    map.Children.Add(panel);
                    Grid.SetRow(panel, i);
                    Grid.SetColumn(panel, j);

                }
            }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            idx = 0;
            changedPanel = null;
            _timer.Stop();
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
                    fileMap = InputFile.makeMap(dlg.FileName);

                    if (fileMap[0] == "-1") 
                    {
                        MessageBox.Show("Map is not valid!", "Error Message");
                    } else
                    {
                        filepath = dlg.FileName;
                        makeMap();
                        graph = InputFile.makeGraph(filepath);
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
            if (_timer != null)
            _timer.Interval = TimeSpan.FromMilliseconds(duration);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (idx < lenPath)
            {
                if (changedPanel != null)
                {
                    if (path[idx-1].Type == TypeGrid.Lintasan)
                    {
                        var newColor = System.Windows.Media.Color.FromRgb(252, 233, 98);
                        changedPanel.Background = new SolidColorBrush(newColor);
                    }
                    
                }

                changedPanel = (StackPanel)LogicalTreeHelper.FindLogicalNode(map, "panel" + path[idx].X.ToString() + path[idx].Y.ToString());

                if (path[idx].Type == TypeGrid.Lintasan)
                {
                    var newColor = System.Windows.Media.Color.FromRgb(117, 214, 255);
                    changedPanel.Background = new SolidColorBrush(newColor);
                }
                if (path[idx].Type == TypeGrid.Treasure)
                {
                    var newColor = System.Windows.Media.Color.FromRgb(176, 154, 0);
                    changedPanel.Background = new SolidColorBrush(newColor);
                    changedPanel.Children.Clear();

                    Image myImage = new Image();
                    myImage.Source = new BitmapImage(new Uri("asset/mrkrab.png", UriKind.Relative));
                    myImage.Stretch = Stretch.Uniform;
                    myImage.HorizontalAlignment = HorizontalAlignment.Center;
                    myImage.VerticalAlignment = VerticalAlignment.Center;
                    changedPanel.Children.Add(myImage);
                }


                idx++;

            }
            else
            {
                _timer.Stop();
                idx = 0;
                changedPanel = null;
            }
        }

        private string makeRoute(List<Point> fullpath)
        {
            string res = "";
            for (int i = 0; i < fullpath.Count-1; i++)
            {
                if (i != 0)
                {
                    res += " - ";
                }
                if (fullpath[i].X == fullpath[i+1].X)
                {
                    if (fullpath[i].Y < fullpath[i+1].Y)
                    {
                        res += "R";
                    } else
                    {
                        res += "L";
                    }
                } else
                {
                    if (fullpath[i].X < fullpath[i + 1].X)
                    {
                        res += "D";
                    }
                    else
                    {
                        res += "U";
                    }
                }
            }
            return res;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (filepath == "") {
                MessageBox.Show("You haven't choose any file", "Error Message");
            }
            else if ( !bfsStatus && !dfsStatus) {
                MessageBox.Show("You haven't choose any file. Pick one, BFS or DFS", "Error Message");
            } else {
                makeMap();
                if (dfsStatus)
                {
                    Point starting = InputFile.findStartingPoint(graph);
                    int treasure = InputFile.findNumberOfTreasure(graph);
                    DFS dfs2 = new DFS(graph, starting, treasure);
                    fullpath = dfs2.getFullPath();
                    path = dfs2.getDFSPath();
                    stepLabel.Content = "Steps: " + dfs2.getStep();
                    nodeLabel.Content = "Nodes: " + dfs2.getNodesVisited();
                    execTimeLabel.Content = "Execution Time: " + dfs2.getTimeMicroS() + " μs";

                } else
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    List<List<Point>> res = MazeBFS.findPathBFS(graph);
                    watch.Stop();
                    TimeSpan time = watch.Elapsed;

                    path = res[1];
                    fullpath = res[0];
                    

                    stepLabel.Content = "Steps: " + (fullpath.Count - 1);
                    nodeLabel.Content = "Nodes: " + path.Count;
                    execTimeLabel.Content = "Execution Time: " + time.TotalMicroseconds + " μs";
                }
                route.Text = "Route: " + makeRoute(fullpath);
                lenFullPath = fullpath.Count;
                lenPath = path.Count;
                _timer.Stop();
                idx = 0;
                changedPanel = null;
                _timer.Start();
            }
        }
    }
}
