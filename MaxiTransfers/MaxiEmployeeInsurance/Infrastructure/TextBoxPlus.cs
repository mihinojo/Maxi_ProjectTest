using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaxiEmployeeInsurance
{
    public class TextBoxPlus : TextBox
    {
        static TextBoxPlus()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxPlus), new FrameworkPropertyMetadata(typeof(TextBoxPlus)));
        }

        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }
            set { SetValue(WaterMarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterMark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("WaterMark", typeof(string), typeof(TextBoxPlus), new PropertyMetadata(string.Empty));


        public Brush LabelForeground
        {
            get { return (Brush)GetValue(LabelForegroundProperty); }
            set { SetValue(LabelForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelForegroundProperty =
            DependencyProperty.Register("LabelForeground", typeof(Brush), typeof(TextBoxPlus), new PropertyMetadata(new BrushConverter().ConvertFromString("#122E4F")));


        public Brush WaterMarkForeground
        {
            get { return (Brush)GetValue(WaterMarkForegroundProperty); }
            set { SetValue(WaterMarkForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterMarkForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterMarkForegroundProperty =
            DependencyProperty.Register("WaterMarkForeground", typeof(Brush), typeof(TextBoxPlus), new PropertyMetadata(new BrushConverter().ConvertFromString("#474747")));


        public Brush BorderBrushSelection
        {
            get { return (Brush)GetValue(BorderBrushSelectionProperty); }
            set { SetValue(BorderBrushSelectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrushSelection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBrushSelectionProperty =
            DependencyProperty.Register("BorderBrushSelection", typeof(Brush), typeof(TextBoxPlus), new PropertyMetadata(new BrushConverter().ConvertFromString("#ffffff")));

        public int IsControl
        {
            get { return (int)GetValue(IsControlProperty); }
            set { SetValue(IsControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsControlProperty =
            DependencyProperty.Register("IsControl", typeof(int), typeof(TextBoxPlus), new PropertyMetadata(0));



        public Boolean LabelUp
        {
            get { return (Boolean)GetValue(LabelUpProperty); }
            set { SetValue(LabelUpProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelUp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelUpProperty =
            DependencyProperty.Register("LabelUp", typeof(Boolean), typeof(TextBoxPlus), new PropertyMetadata(false));




        public double LabelFontSize
        {
            get { return (double)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double), typeof(TextBoxPlus), new PropertyMetadata(20d));



        public double WaterMarkFontSize
        {
            get { return (double)GetValue(WaterMarkFontSizeProperty); }
            set { SetValue(WaterMarkFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterMarkFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterMarkFontSizeProperty =
            DependencyProperty.Register("WaterMarkFontSize", typeof(double), typeof(TextBoxPlus), new PropertyMetadata(30d));


        public Boolean IsSelect
        {
            get { return (Boolean)GetValue(IsSelectProperty); }
            set { SetValue(IsSelectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectProperty =
            DependencyProperty.Register("IsSelect", typeof(Boolean), typeof(TextBoxPlus), new PropertyMetadata(false));


    }
}
