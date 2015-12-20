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

namespace Курсовая
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int width;
        private int heigth;
        private int number;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;

        public MainWindow()
        {
            InitializeComponent();
            CreateW();
        }

        void CreateW()
        {
            Label label1 = new Label()
            {
                Width = 60,
                Height = 30,
                Content = "Ширина"
            };
            Canvas.SetLeft(label1, 105);
            LayoutRoot.Children.Add(label1);
            textBox1 = new TextBox()
            {
                Width = 60,
                Height = 20
            };
            Canvas.SetTop(textBox1, 30);
            Canvas.SetLeft(textBox1, 105);
            LayoutRoot.Children.Add(textBox1);

            Label label2 = new Label()
            {
                Width = 60,
                Height = 30,
                Content = "Высота"
            };
            Canvas.SetTop(label2, 50);
            Canvas.SetLeft(label2, 110);
            LayoutRoot.Children.Add(label2);
            textBox2 = new TextBox()
            {
                Width = 60,
                Height = 20
            };
            Canvas.SetTop(textBox2, 80);
            Canvas.SetLeft(textBox2, 105);
            LayoutRoot.Children.Add(textBox2);

            Label label3 = new Label()
            {
                Width = 120,
                Height = 30,
                Content = "Количество мин"
            };
            Canvas.SetTop(label3, 100);
            Canvas.SetLeft(label3, 85);
            LayoutRoot.Children.Add(label3);
            textBox3 = new TextBox()
            {
                Width = 60,
                Height = 20
            };
            Canvas.SetTop(textBox3, 130);
            Canvas.SetLeft(textBox3, 105);
            LayoutRoot.Children.Add(textBox3);

            Button button = new Button()
            {
                Width = 115,
                Height = 25,
                Content = "Да начнется ИГРА!!!"
            };
            Canvas.SetTop(button, 155);
            Canvas.SetLeft(button, 75);
            LayoutRoot.Children.Add(button);
            button.Click += new RoutedEventHandler(button_click);
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            width = int.Parse(textBox1.Text);
            heigth = int.Parse(textBox2.Text);
            number = int.Parse(textBox3.Text);
            Game game = new Game(width, heigth, number);
            game.Show();
            this.Close();
        }
    }
}
