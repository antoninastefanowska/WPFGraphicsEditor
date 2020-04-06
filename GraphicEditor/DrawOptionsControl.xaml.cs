using System;
using System.Collections.Generic;
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
    public partial class DrawOptionsControl : UserControl
    {
        private CanvasHolder CanvasHolder { get; set; }
        private static readonly Regex REGEX = new Regex("[^0-9]+");

        public DrawOptionsControl(CanvasHolder canvasHolder)
        {
            CanvasHolder = canvasHolder;
            InitializeComponent();
            DataContext = CanvasHolder;
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = REGEX.IsMatch(e.Text);
        }

        private void Line_OnSelected(object sender, RoutedEventArgs e)
        {
            CanvasHolder.CurrentShapeHolderType = ShapeHolderType.Line;
        }

        private void Rectangle_OnSelected(object sender, RoutedEventArgs e)
        {
            CanvasHolder.CurrentShapeHolderType = ShapeHolderType.Rectangle;
        }

        private void Circle_OnSelected(object sender, RoutedEventArgs e)
        {
            CanvasHolder.CurrentShapeHolderType = ShapeHolderType.Circle;
        }

        private void Draw_OnClick(object sender, RoutedEventArgs e)
        {
            Point position1 = new Point();
            Point position2 = new Point();

            position1.X = Convert.ToDouble(tbPositionX1.Text);
            position1.Y = Convert.ToDouble(tbPositionY1.Text);
            position2.X = Convert.ToDouble(tbPositionX2.Text);
            position2.Y = Convert.ToDouble(tbPositionY2.Text);

            ShapeHolder newShapeHolder = ShapeHolderFactory.CreateShapeHolder(CanvasHolder);
            HandlePoint newHandlePoint = new HandlePoint(position1, newShapeHolder);
            newShapeHolder.CreateByPoint(newHandlePoint);
            CanvasHolder.SelectHandlePoint(newHandlePoint);

            newHandlePoint.Move(position2);
            newHandlePoint.ModifyShape();
        }
    }
}
