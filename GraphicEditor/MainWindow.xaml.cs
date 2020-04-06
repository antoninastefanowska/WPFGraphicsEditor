using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GraphicEditor
{
    public partial class MainWindow : Window
    {
        private CanvasHolder CanvasHolder { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Canvas canvas = this.Canvas;
            CanvasHolder = new CanvasHolder(canvas);
            DataContext = CanvasHolder;
        }

        private void Draw_OnCheck(object sender, RoutedEventArgs e)
        {
            if (CanvasHolder != null)
                CanvasHolder.SwitchOperation(OperationType.Draw);
        }

        private void Modify_OnCheck(object sender, RoutedEventArgs e)
        {
            CanvasHolder.SwitchOperation(OperationType.Modify);
        }

        private void Translate_OnCheck(object sender, RoutedEventArgs e)
        {
            CanvasHolder.SwitchOperation(OperationType.Translate);
        }

        private void DrawPointByPoint_OnCheck(object sender, RoutedEventArgs e)
        {
            CanvasHolder.SwitchOperation(OperationType.DrawPointByPoint);
        }

        private void TranslateByVector_OnCheck(object sender, RoutedEventArgs e)
        {
            CanvasHolder.SwitchOperation(OperationType.TranslateByVector);
        }

        private void Rotate_OnCheck(object sender, RoutedEventArgs e)
        {
            CanvasHolder.SwitchOperation(OperationType.Rotate);
        }

        private void Scale_OnCheck(object sender, RoutedEventArgs e)
        {
            CanvasHolder.SwitchOperation(OperationType.Scale);
        }
    }
}
