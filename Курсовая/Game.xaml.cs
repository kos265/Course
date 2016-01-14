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
using System.Windows.Shapes;
using System.Collections;

namespace Курсовая
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Window
    {
private static int width;
        private static int heigth;
        private static int number;

        public Game(int width, int heigth, int number)
        {
            Game.width = width;
            Game.heigth = heigth;
            Game.number = number;
            ArrayOfMine = new BitArray(width * heigth);
            ButtonArray = new Button[width * heigth];
            InitializeComponent();
            InitGame();
        }
        Uri Bomb = new Uri("pack://application:,,,/bomb.png");
        Uri Flag = new Uri("pack://application:,,,/flag.png");
        static int ActualStep = 0;
        BitArray ArrayOfMine;
        Button[] ButtonArray;
        Random random = new Random();

        void InitGame(bool breaks = false, int count = 0)
        {
            for (int i = 0; i < ArrayOfMine.Length; i++)
            {
                if (count == number)
                {
                    break;
                }
                if (!ArrayOfMine[i])
                {
                    ArrayOfMine[i] = random.Next(0, 100) < 5 && random.Next(0, 100) > 80 ? true : false;
                }
                else
                {
                    continue;
                }
                if (ArrayOfMine[i])
                {
                    count++;
                }
            }

            if (count < number)
            {
                InitGame(true, count);
            }
            if (breaks)
            {
                return;
            }

            this.Width = (playField.Width = 20 * width) + 20;
            this.Height = (playField.Height = 20 * heigth) + 40;
            CreateButton(width * heigth);

        }

        void CreateButton(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Button button = new Button()
                {
                    Width = 20,
                    Height = 20,
                    Margin = new Thickness(0),
                    Tag = i,
                    Background = new SolidColorBrush(Color.FromArgb(70, 66, 99, 225))
                };
                button.Click += new RoutedEventHandler(button_Click);
                button.MouseRightButtonUp += new MouseButtonEventHandler(button_MouseRightButtonUp);
                playField.Children.Add(button);
                ButtonArray[i] = button;
            }
        }

        void button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            int index = (int)button.Tag;
            if (button.Content == null)
            {
                SetButton(button, TypeButton.Flag);
            }
            else
            {
                button.Content = null;
            }
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = (int)button.Tag;
            if (ArrayOfMine[index])
            {
                for (int i = 0; i < ArrayOfMine.Length; i++)
                    if (ArrayOfMine[i])
                    {
                        SetButton(ButtonArray[i], TypeButton.Bomb);
                    }
                MessageBox.Show("Вы взлетели на воздуx:(", "Конец");
            }
            else
            {
                Step(index);
                //this.Title = ActualStep.ToString();
                if (ActualStep == width * heigth - number)
                {
                    MessageBox.Show("Странно, но Вы выжили...", "Конец");
                }
            }

        }

        void Step(int index)
        {
            int[] round = new int[8];
            int row = index / width;
            int column = row * width - index;

            round[0] = width * (row - 1) + (index % width);
            round[1] = width * (row + 1) + (index % heigth);
            round[2] = round[0] + 1;
            round[3] = round[0] - 1;
            round[4] = round[1] + 1;
            round[5] = round[1] - 1;
            round[6] = index + 1;
            round[7] = index - 1;

            int count = 0;
            for (int i = 0; i < round.Length; i++)
                if (ContainsIndex(round[i], index, row, column))
                {
                    if (ArrayOfMine[round[i]])
                    {
                        count++;
                    }
                }

            if (count == 0)//если рядом с выбранной клеткой нет мин
            {
                SetButton(ButtonArray[index], TypeButton.None);
                ButtonArray[index].Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                ActualStep += 1;
                for (int i = 0; i < round.Length; i++)
                {
                    if (ContainsIndex(round[i], index, row, column))
                    {
                        if (ButtonArray[round[i]].IsEnabled)
                        {
                            Step(round[i]);
                        }
                    }
                }
            }
            else
            {
                SetButton(ButtonArray[index], TypeButton.Number, count);
                ButtonArray[index].Background = new SolidColorBrush(Color.FromRgb(210, 210, 210));
                ButtonArray[index].Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                ActualStep += 1;
            }
        }

        bool ContainsIndex(int index, int baseInd, int baseRow, int baseCol)
        {
            if (index >= 0 && index < ArrayOfMine.Length)
            {
                int row = index / width;
                int column = row * width - index;
                if (Math.Abs(baseRow - row) > 1) return false;
                if (Math.Abs(baseCol - column) > 1) return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        void SetButton(Button button, TypeButton type, int number = 0)
        {
            BitmapImage bitmap = new BitmapImage();

            if (type == TypeButton.None)
            {
                button.IsEnabled = false;
                return;
            }

            if (type == TypeButton.Bomb)
            {
                bitmap = new BitmapImage(Bomb);
                button.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                button.IsEnabled = false;
            }

            if (type == TypeButton.Flag)
                bitmap = new BitmapImage(Flag);

            if (type != TypeButton.Number)
            {
                Image image = new Image()
                {
                    Source = bitmap
                };
                Grid grid = new Grid();
                grid.Children.Add(image);
                button.Content = grid;
            }

            if (type == TypeButton.Number)
            {
                button.Content = number;
                button.IsEnabled = false;
            }

        }
    }

    public enum TypeButton
    {
        Bomb,
        Flag,
        Number,
        None
    }
}
