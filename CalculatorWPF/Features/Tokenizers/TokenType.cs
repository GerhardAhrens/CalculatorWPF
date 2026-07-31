//-----------------------------------------------------------------------
// <copyright file="TokenType.cs" company="Lifeprojects.de">
//     Class: TokenType
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>27.07.2026</date>
//
// <summary>
// Template für eine neue Enum-Klasse
// </summary>
//-----------------------------------------------------------------------

namespace System.Windows.Calculator
{
    public enum TokenType
    {
        Number,

        Plus,
        Minus,
        Multiply,
        Divide,

        Percent,

        Power,

        LeftParenthesis,
        RightParenthesis,

        Identifier,

        Comma,

        End,
        String,
    }
}
