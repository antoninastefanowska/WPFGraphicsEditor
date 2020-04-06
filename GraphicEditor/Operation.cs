using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;
using System.Windows.Threading;

namespace GraphicEditor
{
    public abstract class Operation
    {
        public UserControl OptionsControl { get; protected set; }
        protected CanvasHolder CanvasHolder { get; set; }
        protected DispatcherTimer ProgressTimer { get; }
        public OperationType OperationType { get; protected set; }
        
        public Operation(CanvasHolder canvasHolder)
        {
            CanvasHolder = canvasHolder;

            ProgressTimer = new DispatcherTimer();
            ProgressTimer.Interval = TimeSpan.FromMilliseconds(1);
            ProgressTimer.Tick += onTimerTick;
        }

        private void onTimerTick(object sender, EventArgs e)
        {
            Progress();
        }

        public abstract void Start(object sender);

        protected abstract void Progress();

        public abstract void Finish();
    }
}
