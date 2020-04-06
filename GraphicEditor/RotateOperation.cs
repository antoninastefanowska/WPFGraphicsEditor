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
    public class RotateOperation : Operation
    {
        private bool moveAnchor = false;
        private Point position;

        public RotateOperation(CanvasHolder canvasHolder) : base(canvasHolder)
        {
            OptionsControl = new RotateOptionsControl(canvasHolder);
            OperationType = OperationType.Rotate;
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
                    moveAnchor = false;
                    ProgressTimer.Start();
                }
            }
        }

        private double getAngle(Point point1, Point point2, Point point3)
        {
            return Math.Atan2(point3.Y - point1.Y, point3.X - point1.X) - Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
        }

        protected override void Progress()
        {
            HandlePoint activeHandlePoint = CanvasHolder.ActiveHandlePoint;
            Point mousePosition = Mouse.GetPosition(CanvasHolder.Canvas);
            if (moveAnchor)
                activeHandlePoint.Move(mousePosition);           
            else
            {
                double angle = getAngle(activeHandlePoint.Position, position, mousePosition);
                ShapeHolder shapeHolder = activeHandlePoint.ShapeHolder;
                shapeHolder.Rotate(angle);
                position = mousePosition;
            }
        }

        public override void Finish()
        {
            ProgressTimer.Stop();
            moveAnchor = false;
        }
    }
}
