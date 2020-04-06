using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class CanvasHolder : INotifyPropertyChanged
    {
        public MouseButtonEventHandler MouseDownEventHandler { get; }
        public MouseButtonEventHandler MouseUpEventHandler { get; }

        public Canvas Canvas { get; set; }
        public List<ShapeHolder> ShapeHolders { get; set; }
        public List<HandlePoint> HandlePoints { get; set; }
        private Operation[] Operations { get; set; }

        private Operation activeOperation;
        public Operation ActiveOperation
        {
            get { return activeOperation; }
            set
            {
                activeOperation = value;
                OnPropertyChanged("ActiveOperation");
            }
        }
        public ShapeHolder ActiveShapeHolder { get; set; }

        private HandlePoint activeHandlePoint;
        public HandlePoint ActiveHandlePoint
        {
            get { return activeHandlePoint; }
            set
            {
                activeHandlePoint = value;
                OnPropertyChanged("ActiveHandlePoint");
            }
        }

        public ShapeHolderType CurrentShapeHolderType { get; set; }
        public double CurrentStrokeThickness { get; set; }

        public CanvasHolder(Canvas canvas)
        {
            Canvas = canvas;
            ShapeHolders = new List<ShapeHolder>();
            HandlePoints = new List<HandlePoint>();

            MouseDownEventHandler = new MouseButtonEventHandler(onMouseDown);
            MouseUpEventHandler = new MouseButtonEventHandler(onMouseUp);

            Canvas.MouseDown += MouseDownEventHandler;
            Canvas.MouseUp += MouseUpEventHandler;

            CurrentStrokeThickness = 5;
            CurrentShapeHolderType = ShapeHolderType.Line;

            Operations = new Operation[7];
            Operations[(int)OperationType.Draw] = new DrawOperation(this);
            Operations[(int)OperationType.DrawPointByPoint] = new DrawPointByPointOperation(this);
            Operations[(int)OperationType.Modify] = new ModifyOperation(this);
            Operations[(int)OperationType.Translate] = new TranslateOperation(this);
            Operations[(int)OperationType.TranslateByVector] = new TranslateByVectorOperation(this);
            Operations[(int)OperationType.Rotate] = new RotateOperation(this);
            Operations[(int)OperationType.Scale] = new ScaleOperation(this);
            ActiveOperation = Operations[(int)OperationType.Draw];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        public void AddShapeHolder(ShapeHolder shapeHolder)
        {
            ShapeHolders.Add(shapeHolder);
        }

        public void AddHandlePoint(HandlePoint handlePoint)
        {
            HandlePoints.Add(handlePoint);
        }

        public HandlePoint GetHandlePointByShape(Ellipse pointShape)
        {
            foreach (HandlePoint handlePoint in HandlePoints)
                if (handlePoint.Contains(pointShape))
                    return handlePoint;
            throw new Exception("No such handle point on canvas.");
        }

        public ShapeHolder GetShapeHolderByShape(Shape shape)
        {
            foreach (ShapeHolder shapeHolder in ShapeHolders)
                if (shapeHolder.ContainsShape(shape))
                    return shapeHolder;
            throw new Exception("No such shape on canvas.");
        }

        public void SwitchOperation(OperationType operationType)
        {
            UnselectHandlePoint();
            ActiveShapeHolder = null;
            ActiveOperation = Operations[(int)operationType];
        }

        public void SwitchShapeHolderType(ShapeHolderType shapeHolderType)
        {
            CurrentShapeHolderType = shapeHolderType;
        }

        public void SelectHandlePoint(HandlePoint handlePoint)
        {
            if (ActiveHandlePoint != null)
                ActiveHandlePoint.Unselect();
            ActiveHandlePoint = handlePoint;
            ActiveHandlePoint.Select();
        }

        public void UnselectHandlePoint()
        {
            if (ActiveHandlePoint != null)
            {
                ActiveHandlePoint.Unselect();
                ActiveHandlePoint = null;
            }
        }

        private void onMouseDown(object sender, MouseButtonEventArgs e)
        {
            ActiveOperation.Start(sender);
        }

        private void onMouseUp(object sender, MouseButtonEventArgs e)
        {
            ActiveOperation.Finish();
        }
    }
}
