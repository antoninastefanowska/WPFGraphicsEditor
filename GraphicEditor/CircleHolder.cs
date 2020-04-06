using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class CircleHolder : ShapeHolder
    {
        public CircleHolder(CanvasHolder canvasHolder) : base(canvasHolder) { }

        public override void CreateByPoint(HandlePoint activeHandlePoint)
        {
            Ellipse circle = new Ellipse();
            circle.Stroke = Brushes.Black;
            circle.StrokeThickness = CanvasHolder.CurrentStrokeThickness;
            HandlePoints[0] = activeHandlePoint;
            HandlePoints[1] = new HandlePoint(activeHandlePoint.Position, this);
            CanvasHolder.Canvas.Children.Add(circle);
            Shape = circle;
        }

        public override void ModifyByPoint(HandlePoint activeHandlePoint)
        {
            Ellipse circle = (Ellipse)Shape;
            bool index = activeHandlePoint == HandlePoints[1];
            if (!index)
            {
                double radius = Math.Sqrt(Math.Pow(activeHandlePoint.X - HandlePoints[1].X, 2) + Math.Pow(activeHandlePoint.Y - HandlePoints[1].Y, 2));
                Canvas.SetLeft(circle, HandlePoints[1].X - radius);
                Canvas.SetTop(circle, HandlePoints[1].Y - radius);
                circle.Width = radius * 2;
                circle.Height = circle.Width;
            }
        }

        public override void TranslateByPoint(HandlePoint activeHandlePoint)
        {
            Ellipse circle = (Ellipse)Shape;
            bool index = activeHandlePoint == HandlePoints[1];
            Point translateVector = new Point();
            Point absolutePosition = new Point();
            Point previousPosition = activeHandlePoint.PreviousPosition;
            if (!index)
            {
                translateVector.X = activeHandlePoint.X - previousPosition.X;
                translateVector.Y = activeHandlePoint.Y - previousPosition.Y;
                absolutePosition.X = HandlePoints[1].X + translateVector.X / 3;
                absolutePosition.Y = HandlePoints[1].Y + translateVector.Y / 3;

                HandlePoints[1].Move(absolutePosition);

                double radius = Math.Sqrt(Math.Pow(activeHandlePoint.X - HandlePoints[1].X, 2) + Math.Pow(activeHandlePoint.Y - HandlePoints[1].Y, 2));
                Canvas.SetLeft(circle, HandlePoints[1].X - radius);
                Canvas.SetTop(circle, HandlePoints[1].Y - radius);
                circle.Width = radius * 2;
                circle.Height = circle.Width;
            }
            else
            {
                translateVector.X = activeHandlePoint.X - previousPosition.X;
                translateVector.Y = activeHandlePoint.Y - previousPosition.Y;
                absolutePosition.X = HandlePoints[0].X + translateVector.X / 3;
                absolutePosition.Y = HandlePoints[0].Y + translateVector.Y / 3;

                HandlePoints[0].Move(absolutePosition);

                double radius = Math.Sqrt(Math.Pow(HandlePoints[0].X - activeHandlePoint.X, 2) + Math.Pow(HandlePoints[0].Y - activeHandlePoint.Y, 2));
                Canvas.SetLeft(circle, activeHandlePoint.X - radius);
                Canvas.SetTop(circle, activeHandlePoint.Y - radius);
                circle.Width = radius * 2;
                circle.Height = circle.Width;
            }
        }

        public override void TranslateByVector(Point vector) { }

        public override void Rotate(double angle) { }

        public override void Scale(double ratio) { }
    }
}
