using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class HandlePoint
    {
        private static readonly int HANDLE_POINT_SIZE = 10;
        protected Ellipse pointShape;

        public ShapeHolder ShapeHolder { get; set; }
        public Point Position { get; private set; }
        public Point PreviousPosition { get; private set; }
        public double X
        {
            get { return Position.X; }
            set { Move(new Point(value, Position.Y)); }
        }
        public double Y
        {
            get { return Position.Y; }
            set { Move(new Point(Position.X, value)); }
        }

        public HandlePoint(Point position, ShapeHolder shapeHolder)
        {
            ShapeHolder = shapeHolder;
            pointShape = new Ellipse();

            pointShape.Stroke = Brushes.Black;
            pointShape.Fill = Brushes.Black;
            pointShape.Width = HANDLE_POINT_SIZE;
            pointShape.Height = HANDLE_POINT_SIZE;

            pointShape.MouseDown += shapeHolder.CanvasHolder.MouseDownEventHandler;
            pointShape.MouseUp += shapeHolder.CanvasHolder.MouseUpEventHandler;

            ShapeHolder.CanvasHolder.Canvas.Children.Add(pointShape);
            ShapeHolder.CanvasHolder.AddHandlePoint(this);
            Canvas.SetZIndex(pointShape, 1);
            
            Move(position);
        }

        public void Move(Point position)
        {
            PreviousPosition = Position;
            Position = position;
            double centerX = position.X - (HANDLE_POINT_SIZE / 2);
            double centerY = position.Y - (HANDLE_POINT_SIZE / 2);
            Canvas.SetLeft(pointShape, centerX);
            Canvas.SetTop(pointShape, centerY);
            ShapeHolder.CanvasHolder.OnPropertyChanged("ActiveHandlePoint");
        }

        public void TranslateByVector(Point vector)
        {
            Point position = new Point();
            position.X = X + vector.X;
            position.Y = Y + vector.Y;
            Move(position);
        }

        public void RotateByPoint(AnchorPoint point, double angle)
        {
            Point position = new Point();
            double cos = Math.Cos(angle), sin = Math.Sin(angle);
            double x0 = X - point.X;
            double y0 = Y - point.Y;

            double x1 = x0 * cos - y0 * sin;
            double y1 = x0 * sin + y0 * cos;

            position.X = x1 + point.X;
            position.Y = y1 + point.Y;
                   
            Move(position);
        }

        public void ScaleByPoint(AnchorPoint point, double ratio)
        {
            Point position = new Point();
            double x0 = X - point.X;
            double y0 = Y - point.Y;

            double x1 = x0 * ratio;
            double y1 = y0 * ratio;

            position.X = x1 + point.X;
            position.Y = y1 + point.Y;
            Move(position);
        }

        public virtual void ModifyShape()
        {
            ShapeHolder.ModifyByPoint(this);
        }

        public virtual void TranslateShape()
        {
            ShapeHolder.TranslateByPoint(this);
        }

        public void Show()
        {
            pointShape.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            pointShape.Visibility = Visibility.Hidden;
        }

        public bool Contains(Ellipse pointShape)
        {
            return this.pointShape == pointShape;
        }

        public virtual void Select()
        {
            pointShape.Fill = Brushes.Blue;
            pointShape.Stroke = Brushes.Blue;
        }

        public virtual void Unselect()
        {
            pointShape.Fill = Brushes.Black;
            pointShape.Stroke = Brushes.Black;
        }
    }
}
