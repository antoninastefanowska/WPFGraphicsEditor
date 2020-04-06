using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraphicEditor
{
    public class DrawPointByPointOperation : Operation
    {
        public DrawPointByPointOperation(CanvasHolder canvasHolder) : base(canvasHolder)
        {
            OptionsControl = new DrawPointByPointOptionsControl(canvasHolder);
            OperationType = OperationType.DrawPointByPoint;
        }

        public override void Start(object sender)
        {
            if (sender is Canvas)
            {
                Canvas canvas = (Canvas)sender;
                Point mousePosition = Mouse.GetPosition(canvas);
                ShapeHolder shapeHolder = CanvasHolder.ActiveShapeHolder;

                if (shapeHolder == null)
                {
                    shapeHolder = ShapeHolderFactory.CreateShapeHolder(CanvasHolder);
                    CanvasHolder.ActiveShapeHolder = shapeHolder;
                }
                HandlePoint newHandlePoint = new HandlePoint(mousePosition, shapeHolder);
                shapeHolder.CreateByPoint(newHandlePoint);

                if (shapeHolder.HandlePoints.Count > 2)
                    shapeHolder.ModifyByPoint(newHandlePoint);
            }
        }

        protected override void Progress() { }

        public override void Finish() { }
    }
}
