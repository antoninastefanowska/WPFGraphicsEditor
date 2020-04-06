using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class AnchorPoint : HandlePoint
    {
        public AnchorPoint(Point position, ShapeHolder shapeHolder) : base(position, shapeHolder)
        {
            pointShape.Stroke = Brushes.Gray;
            pointShape.Fill = Brushes.Gray;
        }

        public override void ModifyShape() { }

        public override void TranslateShape() { }

        public override void Select()
        {
            pointShape.Stroke = Brushes.Red;
            pointShape.Fill = Brushes.Red;
        }

        public override void Unselect()
        {
            pointShape.Stroke = Brushes.Gray;
            pointShape.Fill = Brushes.Gray;
        }

        public bool ContainsShape(Shape shape)
        {
            if (ShapeHolder.Shape == shape)
                return true;
            else
                return false;
        }
    }
}
