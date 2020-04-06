using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class TranslateOperation : Operation
    {
        public TranslateOperation(CanvasHolder canvasHolder) : base(canvasHolder)
        {
            OptionsControl = new TranslateOptionsControl(canvasHolder);
            OperationType = OperationType.Translate;
        }

        public override void Start(object sender)
        {
            if (sender is Ellipse)
            {
                Ellipse pointShape = (Ellipse)sender;
                HandlePoint activeHandlePoint = CanvasHolder.GetHandlePointByShape(pointShape);
                CanvasHolder.SelectHandlePoint(activeHandlePoint);

                ProgressTimer.Start();
            }
        }

        protected override void Progress()
        {
            HandlePoint activeHandlePoint = CanvasHolder.ActiveHandlePoint;
            Point mousePosition = Mouse.GetPosition(activeHandlePoint.ShapeHolder.CanvasHolder.Canvas);
            activeHandlePoint.Move(mousePosition);
            activeHandlePoint.TranslateShape();
        }

        public override void Finish()
        {
            ProgressTimer.Stop();
        }
    }
}
