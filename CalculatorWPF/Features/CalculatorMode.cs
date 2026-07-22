//-----------------------------------------------------------------------
// <copyright file="CalculatorMode.cs" company="Lifeprojects.de">
//     Class: CalculatorMode
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>GERHARD-G6\gerha - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>22.07.2026</date>
//
// <summary>
// Template für eine neue Enum-Klasse
// </summary>
//-----------------------------------------------------------------------

namespace System.Windows.Calculator
{
    using System.ComponentModel;

    public enum CalculatorMode : int
    {
        [Description("Keine Auswahl")]
        None = 0,
        [Description("Classic Mode (mit Tasten)")]
        Classic = 1,
        [Description("Zeilen Modus zur Formeleingabe")]
        Expression = 2
    }
}
