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

namespace algoritm_map
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int[,] doubleArray = new int[13, 13];
            Random r = new Random();
            for (int i = 0; i < doubleArray.GetLength(0); i++)
            {
                for (int j = 0; j < doubleArray.GetLength(1); j++)
                {
                    doubleArray[i, j] = r.Next(2);
                }
            }

            for (int i = 0; i < doubleArray.GetLength(0); i++)
            {
                for (int j = 0; j < doubleArray.GetLength(1); j++)
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    SolidColorBrush foreGroundBrush = new SolidColorBrush();
                    foreGroundBrush.Color = Colors.Red;
                    if (doubleArray[i, j] == 0)
                    {
                        mySolidColorBrush.Color = Color.FromArgb(255, 0, 255, 0);
                    }
                    else
                    {
                        mySolidColorBrush.Color = Colors.Black;
                    }
                    mapMain.Children.Add(new Label()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(30 * i, 30 * j, 0, 0),
                        Content = doubleArray[i, j],
                        Height = 30,
                        Width = 30,
                        Foreground = foreGroundBrush,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Background = mySolidColorBrush

                    });
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in mapMain.Children.OfType<Label>().ToList())
            {
                mapMain.Children.Remove(element);
            }

            MazeGenerator mazeGenerator = new MazeGenerator(15, 15);
            int[,] maze = mazeGenerator.GenerateMaze();
            int[,] doubleArray = new int[13, 13];
            Random r = new Random();
            for (int i = 0; i < doubleArray.GetLength(0); i++)
            {
                for (int j = 0; j < doubleArray.GetLength(1); j++)
                {
                    doubleArray[i, j] = r.Next(2);
                }
            }

            for (int i = 0; i < doubleArray.GetLength(0); i++)
            {
                for (int j = 0; j < doubleArray.GetLength(1); j++)
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    SolidColorBrush foreGroundBrush = new SolidColorBrush();
                    foreGroundBrush.Color = Colors.Red;
                    if (doubleArray[i, j] == 0)
                    {
                        mySolidColorBrush.Color = Color.FromArgb(255, 0, 255, 0);
                    }
                    else
                    {
                        mySolidColorBrush.Color = Colors.Black;
                    }
                    mapMain.Children.Add(new Label()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(30 * i, 30 * j, 0, 0),
                        Content = doubleArray[i, j],
                        Height = 30,
                        Width = 30,
                        Foreground = foreGroundBrush,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Background = mySolidColorBrush

                    });
                }

            }
        }
        public class MazeGenerator
        {
            private int[,] maze;
            private Random random;

            public MazeGenerator(int rows, int columns)
            {
                maze = new int[rows, columns];
                random = new Random();
            }

            public int[,] GenerateMaze()
            {
                InitializeMaze();
                CreateMazePath(0, 0);
                return maze;
            }

            private void InitializeMaze()
            {
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        maze[i, j] = 1; // Set all cells as walls initially
                    }
                }
            }

            private void CreateMazePath(int row, int col)
            {
                maze[row, col] = 0; // Mark the current cell as part of the maze

                // Define the four possible directions (up, down, left, right)
                int[] directions = { 0, 1, 2, 3 };
                ShuffleArray(directions);

                foreach (var direction in directions)
                {
                    int newRow = row + (direction == 0 ? -2 : direction == 1 ? 2 : 0);
                    int newCol = col + (direction == 2 ? -2 : direction == 3 ? 2 : 0);

                    if (IsValid(newRow, newCol) && maze[newRow, newCol] == 1)
                    {
                        maze[row + (direction == 0 ? -1 : direction == 1 ? 1 : 0), col + (direction == 2 ? -1 : direction == 3 ? 1 : 0)] = 0;
                        CreateMazePath(newRow, newCol);
                    }
                }
            }

            private bool IsValid(int row, int col)
            {
                return row >= 0 && row < maze.GetLength(0) && col >= 0 && col < maze.GetLength(1);
            }

            private void ShuffleArray(int[] array)
            {
                for (int i = array.Length - 1; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }



        public class RecursiveBacktrackingMazeGenerator
        {
            private int[,] maze;
            private Random random;

            public RecursiveBacktrackingMazeGenerator(int rows, int columns)
            {
                maze = new int[rows, columns];
                random = new Random();
            }

            public int[,] GenerateMaze()
            {
                InitializeMaze();
                CreateMazePath(1, 1);
                return maze;
            }

            private void InitializeMaze()
            {
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        maze[i, j] = 1; // Set all cells as walls initially
                    }
                }
            }

            private void CreateMazePath(int row, int col)
            {
                maze[row, col] = 0; // Mark the current cell as part of the maze

                // Define the four possible directions (up, down, left, right)
                int[] directions = { 0, 1, 2, 3 };
                ShuffleArray(directions);

                foreach (var direction in directions)
                {
                    int newRow = row + (direction == 0 ? -2 : direction == 1 ? 2 : 0);
                    int newCol = col + (direction == 2 ? -2 : direction == 3 ? 2 : 0);

                    if (IsValid(newRow, newCol) && maze[newRow, newCol] == 1)
                    {
                        maze[row + (direction == 0 ? -1 : direction == 1 ? 1 : 0), col + (direction == 2 ? -1 : direction == 3 ? 1 : 0)] = 0;
                        CreateMazePath(newRow, newCol);
                    }
                }
            }

            private bool IsValid(int row, int col)
            {
                return row >= 0 && row < maze.GetLength(0) && col >= 0 && col < maze.GetLength(1);
            }

            private void ShuffleArray(int[] array)
            {
                for (int i = array.Length - 1; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in mapMain.Children.OfType<Label>().ToList())
            {
                mapMain.Children.Remove(element);
            }
            MazeGenerator mazeGenerator = new MazeGenerator(15, 15);
            int[,] maze = mazeGenerator.GenerateMaze();
            
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    SolidColorBrush foreGroundBrush = new SolidColorBrush();
                    foreGroundBrush.Color = Colors.Red;
                    if (maze[i, j] == 0)
                    {
                        mySolidColorBrush.Color = Color.FromArgb(255, 0, 255, 0);
                    }
                    else
                    {
                        mySolidColorBrush.Color = Colors.Black;
                    }
                    mapMain.Children.Add(new Label()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(30 * i, 30 * j, 0, 0),
                        Content = maze[i, j],
                        Height = 30,
                        Width = 30,
                        Foreground = foreGroundBrush,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Background = mySolidColorBrush

                    });
                }

            }

        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (UIElement element in mapMain.Children.OfType<Label>().ToList())
            {
                mapMain.Children.Remove(element);
            }
            RecursiveBacktrackingMazeGenerator mazeGenerator = new RecursiveBacktrackingMazeGenerator(15, 15);
            int[,] maze = mazeGenerator.GenerateMaze();

            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    SolidColorBrush foreGroundBrush = new SolidColorBrush();
                    foreGroundBrush.Color = Colors.Red;
                    if (maze[i, j] == 0)
                    {
                        mySolidColorBrush.Color = Color.FromArgb(255, 0, 255, 0);
                    }
                    else
                    {
                        mySolidColorBrush.Color = Colors.Black;
                    }
                    mapMain.Children.Add(new Label()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(30 * i, 30 * j, 0, 0),
                        Content = maze[i, j],
                        Height = 30,
                        Width = 30,
                        Foreground = foreGroundBrush,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Background = mySolidColorBrush

                    });
                }

            }

        }
    }
}
