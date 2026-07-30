namespace System.Windows.Calculator
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public static class ButtonKeyBehavior
    {


        public static readonly DependencyProperty RegisterKeyProperty =
            DependencyProperty.RegisterAttached("RegisterKey", typeof(bool), typeof(ButtonKeyBehavior), new PropertyMetadata(false, OnRegisterKeyChanged));

        public static bool GetRegisterKey(DependencyObject obj) => (bool)obj.GetValue(RegisterKeyProperty);
        public static void SetRegisterKey(DependencyObject obj, bool value) => obj.SetValue(RegisterKeyProperty, value);

        private static void OnRegisterKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button button && (bool)e.NewValue)
            {
                button.Loaded += (s, ae) => CreateKeyBindings(button);
            }
        }

        private static void CreateKeyBindings(Button button)
        {
            string contentText = button.Content?.ToString();
            if (string.IsNullOrEmpty(contentText)) return;

            if (TryGetKeysForContent(contentText, out Key mainKey, out Key numPadKey))
            {
                // Sucht das übergeordnete Fenster, in dem die Buttons liegen
                Window parentWindow = Window.GetWindow(button);

                if (parentWindow != null)
                {
                    // Die Bindings werden dem FENSTER hinzugefügt, nicht dem Button!
                    parentWindow.InputBindings.Add(CreateBinding(button, mainKey));
                    parentWindow.InputBindings.Add(CreateBinding(button, numPadKey));
                }
                else
                {
                    // Falls das Fenster beim Laden noch nicht bereit ist, warten wir darauf
                    button.Dispatcher.BeginInvoke(new System.Action(() => {
                        Window win = Window.GetWindow(button);
                        if (win != null)
                        {
                            win.InputBindings.Add(CreateBinding(button, mainKey));
                            win.InputBindings.Add(CreateBinding(button, numPadKey));
                        }
                    }));
                }
            }
        }

        private static KeyBinding CreateBinding(Button button, Key key)
        {
            var binding = new KeyBinding { Key = key };

            // Verbindet das KeyBinding direkt mit dem Command des Buttons
            BindingOperations.SetBinding(binding, InputBinding.CommandProperty, new Binding("Command") { Source = button });

            // Verbindet das KeyBinding direkt mit dem Content des Buttons
            BindingOperations.SetBinding(binding, InputBinding.CommandParameterProperty,new Binding("Content") { Source = button });

            return binding;
        }

        private static bool TryGetKeysForContent(string content, out Key mainKey, out Key numPadKey)
        {
            mainKey = Key.None;
            numPadKey = Key.None;

            if (content == "1")
            {
                mainKey = Key.D1; 
                numPadKey = Key.NumPad1; 
                return true; 
            }

            if (content == "2")
            { 
                mainKey = Key.D2; 
                numPadKey = Key.NumPad2; 
                return true; 
            }

            // Hier können Sie weitere Tasten (3-9, +, -, etc.) einfach ergänzen

            return false;
        }
    }
}
