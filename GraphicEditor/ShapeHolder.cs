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
    public abstract class ShapeHolder
    {
        public CanvasHolder CanvasHolder { get; set; }
        public List<HandlePoint> HandlePoints { get; private set; }
        public AnchorPoint AnchorPoint { get; set; }
        public Shape Shape { get; protected set; }

        public ShapeHolder(CanvasHolder canvasHolder)
        {
            HandlePoints = new List<HandlePoint>();
            CanvasHolder = canvasHolder;
            CanvasHolder.AddShapeHolder(this);
        }

        public abstract void CreateByPoint(HandlePoint activeHandlePoint);

        public abstract void ModifyByPoint(HandlePoint activeHandlePoint);

        public abstract void TranslateByPoint(HandlePoint activeHandlePoint);

        public abstract void TranslateByVector(Point vector);

        public abstract void Rotate(double angle);

        public abstract void Scale(double ratio);

        public HandlePoint GetPointByShape(Ellipse pointShape)
        {
            foreach (HandlePoint handlePoint in HandlePoints)
                if (handlePoint.Contains(pointShape))
                    return handlePoint;
            throw new Exception("No such handle point on shape.");
        }

        public bool ContainsShape(Shape shape)
        {
            if (Shape == shape)
                return true;
            else
                return false;
        }

        public virtual void FromString(string input) { }
    }
}
