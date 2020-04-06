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
    public class CurveHolder : ShapeHolder
    {
        private List<Point> CurvePoints { get; set; }

        private List<Line> Lines { get; set; }

        public int T { get; set; }

        public CurveHolder(CanvasHolder canvasHolder, int T) : base(canvasHolder)
        {
            CurvePoints = new List<Point>(T + 1);
            Lines = new List<Line>(T);
            this.T = T;
            
            Line line;
            for (int i = 0; i < T; i++)
            {
                line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = CanvasHolder.CurrentStrokeThickness;
                line.Visibility = Visibility.Hidden;
                Lines.Add(line);
                CanvasHolder.Canvas.Children.Add(line);

                CurvePoints.Add(new Point());
            }
            CurvePoints.Add(new Point());
        }

        public override void CreateByPoint(HandlePoint activeHandlePoint)
        {
            HandlePoints.Add(activeHandlePoint);
        }

        public override void ModifyByPoint(HandlePoint activeHandlePoint)
        {
            int n = HandlePoints.Count - 1, k;
            double b, bp, tp = 0, t;
            Point point, characteristic;

            for (int i = 0; i < T; i++)
            {
                point = new Point();
                point.X = 0;
                point.Y = 0;

                t = (double)i / T;
                b = Math.Pow(1 - t, n);

                tp = 1;
                bp = b;
                for (int j = 0; j <= n; j++)
                {
                    characteristic = HandlePoints[j].Position;
                    k = j == 0 || j == n ? 1 : n;
                    point.X += (k * bp * tp * characteristic.X);
                    point.Y += (k * bp * tp * characteristic.Y);
                    bp /= (1 - t);
                    tp *= t;
                }
                CurvePoints[i] = point;
            }
            
            point = new Point();
            characteristic = HandlePoints[n].Position;
            point.X = characteristic.X;
            point.Y = characteristic.Y;
            CurvePoints[T] = point;
            
            for (int i = 0; i < T; i++)
            {
                Lines[i].X1 = CurvePoints[i].X;
                Lines[i].Y1 = CurvePoints[i].Y;

                Lines[i].X2 = CurvePoints[i + 1].X;
                Lines[i].Y2 = CurvePoints[i + 1].Y;

                if (Lines[i].Visibility == Visibility.Hidden)
                    Lines[i].Visibility = Visibility.Visible;
            }
        }

        public override void TranslateByPoint(HandlePoint activeHandlePoint) { }

        public override void TranslateByVector(Point vector) { }

        public override void Rotate(double angle) { }

        public override void Scale(double ratio) { }
    }
}
