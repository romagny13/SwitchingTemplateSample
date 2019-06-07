using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ControlLib
{
    public class XamlIcon : Control
    {
        static XamlIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XamlIcon), new FrameworkPropertyMetadata(typeof(XamlIcon)));
        }

        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Geometry), typeof(XamlIcon), new PropertyMetadata(null));

        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(XamlIcon), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
    }
}
