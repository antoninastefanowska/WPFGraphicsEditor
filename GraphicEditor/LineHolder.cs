using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class LineHolder : ShapeHolder
    {
        public LineHolder(CanvasHolder canvasHolder) : base(canvasHolder) { }

        public override void CreateByPoint(HandlePoint activeHandlePoint)
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = CanvasHolder.CurrentStrokeThickness;
            HandlePoints[0] = activeHandlePoint;
            HandlePoints[1] = new HandlePoint(activeHandlePoint.Position, this);
            CanvasHolder.Canvas.Children.Add(line);
            Shape = line;
        }

        public override void ModifyByPoint(HandlePoint activeHandlePoint)
        {
            Line line = (Line)Shape;
            bool index = activeHandlePoint == HandlePoints[1];
            if (!index)
            {
                line.X1 = activeHandlePoint.X;
                line.Y1 = activeHandlePoint.Y;
                line.X2 = HandlePoints[1].X;
                line.Y2 = HandlePoints[1].Y;
            }
            else
            {
                line.X1 = HandlePoints[0].X;
                line.Y1 = HandlePoints[0].Y;
                line.X2 = activeHandlePoint.X;
                line.Y2 = activeHandlePoint.Y;
            }
        }

        public override void TranslateByPoint(HandlePoint activeHandlePoint)
        {
            Line line = (Line)Shape;
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

                line.X1 = activeHandlePoint.X;
                line.Y1 = activeHandlePoint.Y;
                line.X2 = HandlePoints[1].X;
                line.Y2 = HandlePoints[1].Y;
            }
            else
            {
                translateVector.X = activeHandlePoint.X - previousPosition.X;
                translateVector.Y = activeHandlePoint.Y - previousPosition.Y;
                absolutePosition.X = HandlePoints[0].X + translateVector.X / 3;
                absolutePosition.Y = HandlePoints[0].Y + translateVector.Y / 3;

                HandlePoints[0].Move(absolutePosition);

                line.X1 = HandlePoints[0].X;
                line.Y1 = HandlePoints[0].Y;
                line.X2 = activeHandlePoint.X;
                line.Y2 = activeHandlePoint.Y;
            }
        }

        public override void TranslateByVector(Point vector) { }

        public override void Rotate(double angle) { }

        public override void Scale(double ratio) { }
    }
}
