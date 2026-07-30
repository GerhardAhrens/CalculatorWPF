namespace System.Windows.Calculator
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    using System;
    using System.Collections.Generic;

    public static class ButtonKeyBehavior
    {
        // Speichert die zugewiesenen Bindings pro Button, um sie beim Entladen sauber zu entfernen
        private static readonly Dictionary<Button, List<KeyBinding>> RegisteredBindings = new();

        // Zentrales Mapping für Zahlen und Operationstasten
        private static readonly Dictionary<string, (Key Main, Key NumPad)> KeyMap = new(StringComparer.OrdinalIgnoreCase)
    {
        // Zahlen
        { "1", (Key.D1, Key.NumPad1) },
        { "2", (Key.D2, Key.NumPad2) },
        { "3", (Key.D3, Key.NumPad3) },
        { "4", (Key.D4, Key.NumPad4) },
        { "5", (Key.D5, Key.NumPad5) },
        { "6", (Key.D6, Key.NumPad6) },
        { "7", (Key.D7, Key.NumPad7) },
        { "8", (Key.D8, Key.NumPad8) },
        { "9", (Key.D9, Key.NumPad9) },
        { "0", (Key.D0, Key.NumPad0) },

        // Operationstasten (Sowohl mathematische Zeichen als auch Text-Bezeichner abgedeckt)
        { "+", (Key.OemPlus, Key.Add) },
        { "-", (Key.OemMinus, Key.Subtract) },
        { "×", (Key.Multiply, Key.Multiply) }, // Oft dieselbe Taste oder Shift+D1
        { "÷", (Key.Divide, Key.Divide) },
        { ",", (Key.OemComma, Key.Decimal) },
        { ".", (Key.OemPeriod, Key.Decimal) },

        // Steuerungstasten
        { "Enter", (Key.Return, Key.Enter) },
        { "Return", (Key.Return, Key.Enter) },
        { "=", (Key.Return, Key.Enter) }, // Häufig bei Taschenrechnern gewollt
        { "⌫", (Key.Back, Key.Back) },
        { "Clear", (Key.Delete, Key.Delete) },
        { "C", (Key.Delete, Key.Delete) }
    };

        public static readonly DependencyProperty RegisterKeyProperty =
            DependencyProperty.RegisterAttached("RegisterKey", typeof(bool), typeof(ButtonKeyBehavior), new PropertyMetadata(false, OnRegisterKeyChanged));

        public static bool GetRegisterKey(DependencyObject obj) => (bool)obj.GetValue(RegisterKeyProperty);
        public static void SetRegisterKey(DependencyObject obj, bool value) => obj.SetValue(RegisterKeyProperty, value);

        private static void OnRegisterKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button button)
            {
                if ((bool)e.NewValue)
                {
                    button.Loaded += OnButtonLoaded;
                    button.Unloaded += OnButtonUnloaded;
                }
                else
                {
                    button.Loaded -= OnButtonLoaded;
                    button.Unloaded -= OnButtonUnloaded;
                }
            }
        }

        private static void OnButtonLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Verzögerte Ausführung, damit das Window sicher initialisiert ist
                button.Dispatcher.BeginInvoke(new Action(() => RegisterBindingsForButton(button)));
            }
        }

        private static void OnButtonUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                UnregisterBindingsForButton(button);
            }
        }

        private static void RegisterBindingsForButton(Button button)
        {
            Window parentWindow = Window.GetWindow(button);
            if (parentWindow == null)
            {
                return;
            }

            string contentText = button.Content?.ToString();
            if (string.IsNullOrEmpty(contentText))
            {
                return;
            }

            // Nutzt das neue Dictionary-Mapping
            if (KeyMap.TryGetValue(contentText, out var keys))
            {
                // Verhindert doppelte Registrierung bei wiederholtem Laden
                UnregisterBindingsForButton(button);

                var buttonBindings = new List<KeyBinding>();

                // Haupttaste erstellen (z.B. OemPlus oder D1)
                if (keys.Main != Key.None)
                {
                    var mainBinding = CreateBinding(button, keys.Main);
                    parentWindow.InputBindings.Add(mainBinding);
                    buttonBindings.Add(mainBinding);
                }

                // NumPad-Taste erstellen (z.B. Add oder NumPad1)
                if (keys.NumPad != Key.None && keys.NumPad != keys.Main)
                {
                    var numPadBinding = CreateBinding(button, keys.NumPad);
                    parentWindow.InputBindings.Add(numPadBinding);
                    buttonBindings.Add(numPadBinding);
                }

                // Bindings für das spätere Entladen zwischenspeichern
                if (buttonBindings.Count > 0)
                {
                    RegisteredBindings[button] = buttonBindings;
                }
            }
        }

        private static void UnregisterBindingsForButton(Button button)
        {
            Window parentWindow = Window.GetWindow(button);

            if (parentWindow != null && RegisteredBindings.TryGetValue(button, out var bindings))
            {
                foreach (var binding in bindings)
                {
                    parentWindow.InputBindings.Remove(binding);
                }
                RegisteredBindings.Remove(button);
            }
        }

        private static KeyBinding CreateBinding(Button button, Key key)
        {
            var binding = new KeyBinding { Key = key };

            BindingOperations.SetBinding(binding, InputBinding.CommandProperty,
                new Binding("Command") { Source = button });

            BindingOperations.SetBinding(binding, InputBinding.CommandParameterProperty,
                new Binding("Content") { Source = button });

            return binding;
        }
    }
}
