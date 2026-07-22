//-----------------------------------------------------------------------
// <copyright file="CalculatorViewModel .cs" company="Lifeprojects.de">
//     Class: CalculatorViewModel 
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>22.07.2026</date>
//
// <summary>
// Template für eine neue C# Standard-Klasse
// </summary>
//-----------------------------------------------------------------------

namespace System.Windows.Calculator
{
    using System;

    public class CalculatorViewModel : NotifyPropertyBase
    {
        public CalculatorViewModel ()
        {
            this.DisplayText = "0";
            this.ExpressionText = string.Empty;
            this.ErrorText = string.Empty;
        }

        #region Properties
        public string DisplayText
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string ExpressionText
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }

        public string ErrorText
        {
            get => base.GetValue<string>();
            set => base.SetValue(value);
        }
        #endregion Properties

        #region Class Methodes
        public void MyMethodes()
        {
            try
            {
            }
            catch (Exception ex)
            {
                string errorText = ex.Message;
            }
        }
        #endregion Class Methodes
    }
}
