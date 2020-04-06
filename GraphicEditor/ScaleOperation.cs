using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class ScaleOperation : Operation
    {
        private bool moveAnchor;
        private Point position;
        private double oldDistance;

        public ScaleOperation(CanvasHolder canvasHolder) : base(canvasHolder)
        {
            OptionsControl = new ScaleOptionsControl(canvasHolder);
            OperationType = OperationType.Scale;
        }

        public override void Start(object sender)
        {
            if (sender is Ellipse)
            {
                Ellipse pointShape = (Ellipse)sender;
                HandlePoint point = CanvasHolder.GetHandlePointByShape(pointShape);
                if (point is AnchorPoint)
                {
                    CanvasHolder.SelectHandlePoint(point);
                    moveAnchor = true;
                    ProgressTimer.Start();
                }
            }
            else if (sender is Shape)
            {
                Shape shape = (Shape)sender;
                ShapeHolder shapeHolder = CanvasHolder.GetShapeHolderByShape(shape);
                AnchorPoint anchorPoint = shapeHolder.AnchorPoint;
                Point mousePosition = Mouse.GetPosition(CanvasHolder.Canvas);
                if (anchorPoint == null)
                {
                    anchorPoint = new AnchorPoint(mousePosition, shapeHolder);
                    shapeHolder.AnchorPoint = anchorPoint;
                    CanvasHolder.SelectHandlePoint(anchorPoint);
                    moveAnchor = true;
                    ProgressTimer.Start();
                }
                else
                {
                    position = mousePosition;
                    oldDistance = getDistance(anchorPoint.Position, mousePosition);
                    moveAnchor = false;
                    ProgressTimer.Start();
                }
            }
        }

        private double getDistance(Point point1, Point point2)
        {
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }

        protected override void Progress()
        {
            HandlePoint activeHandlePoint = CanvasHolder.ActiveHandlePoint;
            Point mousePosition = Mouse.GetPosition(CanvasHolder.Canvas);
            if (moveAnchor)
                activeHandlePoint.Move(mousePosition);
            else
            {
                double distance = getDistance(activeHandlePoint.Position, mousePosition);
                double ratio = distance / oldDistance;
                ShapeHolder shapeHolder = activeHandlePoint.ShapeHolder;
                shapeHolder.Scale(ratio);
                position = mousePosition;
                oldDistance = distance;
            }
        }

        public override void Finish()
        {
            ProgressTimer.Stop();
            moveAnchor = false;
        }
    }
}
