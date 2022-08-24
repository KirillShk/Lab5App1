using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace Lab5_2App1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (Paint.EditingMode == InkCanvasEditingMode.Ink)
            {
                Paint.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            else
                Paint.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void ToggleButton_Click_1(object sender, RoutedEventArgs e)
        {
            Paint.DefaultDrawingAttributes.Color = Colors.Red;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Paint.Strokes.Clear();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int widht = (int)Paint.ActualWidth;
            int height = (int)Paint.ActualHeight;
            RenderTargetBitmap rtb = new RenderTargetBitmap(widht, height, 96, 96, PixelFormats.Default);
            rtb.Render(Paint);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Сохранить в формате (*.png)|*.png | Сохранить файл (*.*)| *.*";
            if (save.ShowDialog() == true)
            {
                FileStream fs = File.Open(save.FileName, FileMode.OpenOrCreate);
                encoder.Save(fs);
                fs.Close();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Image img = new Image();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Открыть формат (*.png) |*.png| Все файлы (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {      
               string selectedFileName = dlg.FileName;
               BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);  
                bitmap.EndInit();
                img.Source = bitmap;
                Paint.Children.Add(img);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
 