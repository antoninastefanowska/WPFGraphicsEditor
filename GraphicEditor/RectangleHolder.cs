using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace GraphicEditor
{
    public class RectangleHolder : ShapeHolder
    {
        public RectangleHolder(CanvasHolder canvasHolder) : base(canvasHolder) { }

        public override void CreateByPoint(HandlePoint activeHandlePoint)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = CanvasHolder.CurrentStrokeThickness;
            HandlePoints[0] = activeHandlePoint;
            HandlePoints[1] = new HandlePoint(activeHandlePoint.Position, this);
            CanvasHolder.Canvas.Children.Add(rectangle);
            Shape = rectangle;
        }

        public override void ModifyByPoint(HandlePoint activeHandlePoint)
        {
            Rectangle rectangle = (Rectangle)Shape;
            bool index = activeHandlePoint == HandlePoints[1];
            if (!index)
            {
                Canvas.SetLeft(rectangle, HandlePoints[1].X);
                Canvas.SetTop(rectangle, HandlePoints[1].Y);
                if (activeHandlePoint.X - HandlePoints[1].X > 0)
                    rectangle.Width = activeHandlePoint.X - HandlePoints[1].X;
                if (activeHandlePoint.X - HandlePoints[1].Y > 0)
                    rectangle.Height = activeHandlePoint.Y - HandlePoints[1].Y;
            }
            else
            {
                Canvas.SetLeft(rectangle, activeHandlePoint.X);
                Canvas.SetTop(rectangle, activeHandlePoint.Y);
                if (HandlePoints[0].X - activeHandlePoint.X > 0)
                    rectangle.Width = HandlePoints[0].X - activeHandlePoint.X;
                if (HandlePoints[0].Y - activeHandlePoint.Y > 0)
                    rectangle.Height = HandlePoints[0].Y - activeHandlePoint.Y;
            }
        }

        public override void TranslateByPoint(HandlePoint activeHandlePoint)
        {
            Rectangle rectangle = (Rectangle)Shape;
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

                Canvas.SetLeft(rectangle, HandlePoints[1].X);
                Canvas.SetTop(rectangle, HandlePoints[1].Y);
                if (activeHandlePoint.X - HandlePoints[1].X > 0)
                    rectangle.Width = activeHandlePoint.X - HandlePoints[1].X;
                if (activeHandlePoint.X - HandlePoints[1].Y > 0)
                    rectangle.Height = activeHandlePoint.Y - HandlePoints[1].Y;
            }
            else
            {
                translateVector.X = activeHandlePoint.X - previousPosition.X;
                translateVector.Y = activeHandlePoint.Y - previousPosition.Y;
                absolutePosition.X = HandlePoints[0].X + translateVector.X / 3;
                absolutePosition.Y = HandlePoints[0].Y + translateVector.Y / 3;

                HandlePoints[0].Move(absolutePosition);

                Canvas.SetLeft(rectangle, activeHandlePoint.X);
                Canvas.SetTop(rectangle, activeHandlePoint.Y);
                if (HandlePoints[0].X - activeHandlePoint.X > 0)
                    rectangle.Width = HandlePoints[0].X - activeHandlePoint.X;
                if (HandlePoints[0].Y - activeHandlePoint.Y > 0)
                    rectangle.Height = HandlePoints[0].Y - activeHandlePoint.Y;
            }
        }

        public override void TranslateByVector(Point vector) { }

        public override void Rotate(double angle) { }

        public override void Scale(double ratio) { }
    }
}
