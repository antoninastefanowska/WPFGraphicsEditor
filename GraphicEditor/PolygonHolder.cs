using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class PolygonHolder : ShapeHolder
    {
        public PolygonHolder(CanvasHolder canvasHolder) : base(canvasHolder)
        {
            Polygon polygon = new Polygon();
            polygon.Stroke = Brushes.Black;
            polygon.Fill = Brushes.Black;
            polygon.StrokeThickness = CanvasHolder.CurrentStrokeThickness;
            polygon.Visibility = Visibility.Hidden;

            polygon.MouseDown += canvasHolder.MouseDownEventHandler;
            polygon.MouseUp += canvasHolder.MouseUpEventHandler;

            Shape = polygon;
            CanvasHolder.Canvas.Children.Add(polygon);
        }

        public override void CreateByPoint(HandlePoint activeHandlePoint)
        {
            HandlePoints.Add(activeHandlePoint);
        }

        public override void ModifyByPoint(HandlePoint activeHandlePoint)
        {
            PointCollection points = new PointCollection();
            foreach (HandlePoint handlePoint in HandlePoints)
                points.Add(handlePoint.Position);

            Polygon polygon = Shape as Polygon;
            polygon.Points = points;

            if (polygon.Visibility == Visibility.Hidden)
                polygon.Visibility = Visibility.Visible;
        }

        public override void TranslateByPoint(HandlePoint activeHandlePoint) { }

        public override void TranslateByVector(Point vector)
        {
            foreach (HandlePoint handlePoint in HandlePoints)
                handlePoint.TranslateByVector(vector);
            ModifyByPoint(null);
        }

        public override void Rotate(double angle)
        {
            foreach (HandlePoint handlePoint in HandlePoints)
                handlePoint.RotateByPoint(AnchorPoint, angle);
            ModifyByPoint(null);
        }

        public override void Scale(double ratio)
        {
            foreach (HandlePoint handlePoint in HandlePoints)
                handlePoint.ScaleByPoint(AnchorPoint, ratio);
            ModifyByPoint(null);
        }

        public override string ToString()
        {
            string output = "";
            string divider = "";
            for (int i = 0; i < HandlePoints.Count; i++)
            {
                output += divider;
                string x = Convert.ToString(HandlePoints[i].X);
                string y = Convert.ToString(HandlePoints[i].Y);
                divider = " ";
                output += x + " " + y;
            }
            return output;
        }

        public override void FromString(string input)
        {
            string[] words = input.Split(null);
            for (int i = 0; i < words.Length; i += 2)
            {
                double x = Convert.ToDouble(words[i]);
                double y = Convert.ToDouble(words[i + 1]);
                Point position = new Point(x, y);
                HandlePoint handlePoint = new HandlePoint(position, this);
                CreateByPoint(handlePoint);
            }
            ModifyByPoint(null);
        }
    }
}
