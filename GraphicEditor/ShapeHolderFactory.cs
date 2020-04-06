using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor
{
    public class ShapeHolderFactory
    {
        public static ShapeHolder CreateShapeHolder(CanvasHolder canvasHolder)
        {
            switch (canvasHolder.CurrentShapeHolderType)
            {
                case ShapeHolderType.Line:
                    return new LineHolder(canvasHolder);
                case ShapeHolderType.Rectangle:
                    return new RectangleHolder(canvasHolder);
                case ShapeHolderType.Circle:
                    return new CircleHolder(canvasHolder);
                case ShapeHolderType.Curve:
                    return new CurveHolder(canvasHolder, 20);
                case ShapeHolderType.Polygon:
                    return new PolygonHolder(canvasHolder);
                default:
                    return null;
            }
        }
    }
}
