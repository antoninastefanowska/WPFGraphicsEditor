using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public partial class TranslateOptionsControl : UserControl
    {
        private CanvasHolder CanvasHolder { get; set; }
        private static readonly Regex REGEX = new Regex("[^0-9]+");

        public TranslateOptionsControl(CanvasHolder canvasHolder)
        {
            CanvasHolder = canvasHolder;
            InitializeComponent();
            DataContext = CanvasHolder;
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = REGEX.IsMatch(e.Text);
        }

        private void Position_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (CanvasHolder.ActiveHandlePoint != null && CanvasHolder.ActiveOperation.OperationType == OperationType.Translate)
                CanvasHolder.ActiveHandlePoint.TranslateShape();
        }
    }
}
