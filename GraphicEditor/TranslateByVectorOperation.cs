using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class TranslateByVectorOperation : Operation
    {
        ShapeHolder shapeHolder;
        Point position;

        public TranslateByVectorOperation(CanvasHolder canvasHolder) : base(canvasHolder)
        {
            OptionsControl = new TranslateByVectorOptionsControl(canvasHolder);
            OperationType = OperationType.TranslateByVector;
        }

        public override void Start(object sender)
        {
            if (sender is Shape)
            {
                Shape shape = sender as Shape;
                Canvas canvas = CanvasHolder.Canvas;
                position = Mouse.GetPosition(canvas);
                shapeHolder = CanvasHolder.GetShapeHolderByShape(shape);
                CanvasHolder.ActiveShapeHolder = shapeHolder;

                ProgressTimer.Start();
            }
        }

        protected override void Progress()
        {
            Point mousePosition = Mouse.GetPosition(CanvasHolder.Canvas);
            Point vector = new Point();
            vector.X = mousePosition.X - position.X;
            vector.Y = mousePosition.Y - position.Y;
            shapeHolder.TranslateByVector(vector);
            position = mousePosition;
        }

        public override void Finish()
        {
            ProgressTimer.Stop();
        }
    }
}
