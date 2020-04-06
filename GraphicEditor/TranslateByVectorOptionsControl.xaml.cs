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
    public partial class TranslateByVectorOptionsControl : UserControl
    {
        private CanvasHolder CanvasHolder { get; set; }
        private static readonly Regex REGEX = new Regex("[^0-9.-]+");

        public TranslateByVectorOptionsControl(CanvasHolder canvasHolder)
        {
            CanvasHolder = canvasHolder;
            InitializeComponent();
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = REGEX.IsMatch(e.Text);
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            Point vector = new Point();
            vector.X = Convert.ToDouble(tbVectorX.Text);
            vector.Y = Convert.ToDouble(tbVectorY.Text);

            CanvasHolder.ActiveShapeHolder.TranslateByVector(vector);
        }
    }
}
