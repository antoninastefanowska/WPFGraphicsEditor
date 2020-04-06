using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class DrawPointByPointOptionsControl : UserControl
    {
        private CanvasHolder CanvasHolder { get; set; }
        private static readonly Regex REGEX = new Regex("[^0-9]+");

        public DrawPointByPointOptionsControl(CanvasHolder canvasHolder)
        {
            CanvasHolder = canvasHolder;
            InitializeComponent();
            DataContext = canvasHolder;
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = REGEX.IsMatch(e.Text);
        }

        private void Curve_OnSelected(object sender, RoutedEventArgs e)
        {
            CanvasHolder.CurrentShapeHolderType = ShapeHolderType.Curve;
        }
        
        private void Polygon_OnSelected(object sender, RoutedEventArgs e)
        {
            CanvasHolder.CurrentShapeHolderType = ShapeHolderType.Polygon;
        }

        private void AddPoint_OnClick(object sender, RoutedEventArgs e)
        {
            Point position = new Point();
            position.X = Convert.ToDouble(tbPositionX.Text);
            position.Y = Convert.ToDouble(tbPositionY.Text);

            ShapeHolder shapeHolder = CanvasHolder.ActiveShapeHolder;
            if (shapeHolder == null)
            {
                shapeHolder = ShapeHolderFactory.CreateShapeHolder(CanvasHolder);
                CanvasHolder.ActiveShapeHolder = shapeHolder;
            }
            HandlePoint newHandlePoint = new HandlePoint(position, shapeHolder);
            shapeHolder.CreateByPoint(newHandlePoint);

            if (shapeHolder.HandlePoints.Count > 2)
                shapeHolder.ModifyByPoint(newHandlePoint);
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ShapeHolder shapeHolder = CanvasHolder.ActiveShapeHolder;
                string path = dialog.FileName;
                StreamWriter outputFile = new StreamWriter(path);
                outputFile.WriteLine(shapeHolder.ToString());
                outputFile.Close();
            }
        }

        private void Load_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ShapeHolder shapeHolder = ShapeHolderFactory.CreateShapeHolder(CanvasHolder);
                CanvasHolder.ActiveShapeHolder = shapeHolder;
                string path = dialog.FileName;
                StreamReader inputFile = new StreamReader(path);
                string input = inputFile.ReadLine();
                shapeHolder.FromString(input);
                inputFile.Close();
            }
        }
    }
}
