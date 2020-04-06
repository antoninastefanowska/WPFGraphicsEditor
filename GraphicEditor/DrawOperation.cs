using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraphicEditor
{
    public class DrawOperation : Operation
    {
        public DrawOperation(CanvasHolder canvasHolder) : base(canvasHolder)
        {
            OptionsControl = new DrawOptionsControl(canvasHolder);
            OperationType = OperationType.Draw;
        }

        public override void Start(object sender)
        {
            if (sender is Canvas)
            {
                Canvas canvas = (Canvas)sender;
                Point mousePosition = Mouse.GetPosition(canvas);

                ShapeHolder newShapeHolder = ShapeHolderFactory.CreateShapeHolder(CanvasHolder);
                HandlePoint newHandlePoint = new HandlePoint(mousePosition, newShapeHolder);
                newShapeHolder.CreateByPoint(newHandlePoint);
                CanvasHolder.SelectHandlePoint(newHandlePoint);

                ProgressTimer.Start();
            }
        }

        protected override void Progress()
        {
            HandlePoint activeHandlePoint = CanvasHolder.ActiveHandlePoint;
            Point mousePosition = Mouse.GetPosition(activeHandlePoint.ShapeHolder.CanvasHolder.Canvas);
            activeHandlePoint.Move(mousePosition);
            activeHandlePoint.ModifyShape();
        }

        public override void Finish()
        {
            ProgressTimer.Stop();
            CanvasHolder.UnselectHandlePoint();
        }
    }
}
