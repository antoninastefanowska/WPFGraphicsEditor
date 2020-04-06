using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class ModifyOperation : Operation
    {
        public ModifyOperation(CanvasHolder canvasHolder) : base(canvasHolder)
        {
            OptionsControl = new ModifyOptionsControl(canvasHolder);
            OperationType = OperationType.Modify;
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
            Point mousePosition = Mouse.GetPosition(CanvasHolder.Canvas);
            activeHandlePoint.Move(mousePosition);
            activeHandlePoint.ModifyShape();
        }

        public override void Finish()
        {
            ProgressTimer.Stop();
        }
    }
}
