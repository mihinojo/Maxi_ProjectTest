using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaxiEmployeeInsurance
{
    public class ValidatePhoneNumber
    {
        #region Attached Property ValidateNumbers

        static bool AltGrIsPressed;
        static bool CtrlIsPressed;
        public static readonly DependencyProperty OnlyNumbersProperty
           = DependencyProperty.RegisterAttached("OnlyNumbers", typeof(bool), typeof(ValidatePhoneNumber),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnOnlyNumbersChanged)));

        private static void OnOnlyNumbersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) return;
            var textBox = d as TextBox;
            if (textBox == null) return;
            textBox.KeyUp -= TextBoxKeyUp;
            textBox.KeyUp += TextBoxKeyUp;
            textBox.KeyDown -= TextBoxKeyDown;
            textBox.KeyDown += TextBoxKeyDown;
            textBox.PreviewKeyDown += TextBoxKeyDown;
            textBox.TextChanged += TextBoxChanged;
        }
        private static void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBoxPlus)
            {
                var objTextBox = sender as TextBoxPlus;
                if (objTextBox != null)
                    objTextBox.Text = Regex.Replace(objTextBox.Text, "[^0-9]", "");
            }
        }

        private static void TextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                AltGrIsPressed = false;
            }
            if (CtrlIsPressed && (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl))
            {
                CtrlIsPressed = false;
                if (sender is TextBoxPlus)
                {
                    var objTextBox = sender as TextBoxPlus;
                    if (objTextBox != null)
                        objTextBox.Text = Regex.Replace(objTextBox.Text, "[^0-9]", "");
                }
            }
            if (e.Key == Key.Space)
                e.Handled = false;
        }

        private static void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Space)
                e.Handled = true;
            if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                AltGrIsPressed = true;
            }
            if (sender is TextBoxPlus)
            {
                if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                {
                    CtrlIsPressed = true;
                }
            }
            if (e.Key == Key.Delete) //Borrar con Tecla Suprimir
            {
                if (sender is TextBoxPlus)
                {
                    var objTextBox = sender as TextBoxPlus;
                    if (objTextBox != null)
                    {
                        var pos = objTextBox.SelectionStart;
                        if (objTextBox.SelectionLength == objTextBox.Text.Length)
                        {
                            objTextBox.Text = string.Empty;
                            e.Handled = true;
                        }
                        else
                        if (objTextBox.Text.Length <= 0 || pos == objTextBox.Text.Length)
                            e.Handled = true;
                        else
                        {
                            objTextBox.Text = objTextBox.Text.Remove(pos, 1);
                            objTextBox.SelectionStart = pos;
                            e.Handled = true;
                        }
                    }
                }
            }
            if (AltGrIsPressed == true)
            {
                e.Handled = true;
            }
            if (e.Handled == false && (e.Key < Key.D0 || e.Key > Key.D9))
            {
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    if (e.Key != Key.Back && e.Key != Key.Tab && e.Key != Key.Space && e.Key != Key.Right && e.Key != Key.Left )
                    {
                        e.Handled = true;
                    }
                }
            }
            if(CtrlIsPressed && e.Key == Key.V)
            {
                e.Handled = false;               
                    
            }

        }

        public static void SetOnlyNumbers(DependencyObject dependencyObject, bool onlyNumbers)
        {
            if (!ReferenceEquals(null, dependencyObject))            
                dependencyObject.SetValue(OnlyNumbersProperty, onlyNumbers);   
        }

        public static bool GetOnlyNumbers(DependencyObject dependencyObject)
        {
            if (!ReferenceEquals(null, dependencyObject))
                return (bool)dependencyObject.GetValue(OnlyNumbersProperty);
            return false;
        }

        #endregion Attached Property ValidateNumbers
    }
}
