# Calculator Projekt

![NET](https://img.shields.io/badge/NET-10-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2026](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)

# Projekt
Dieses Demo dient dazu eine UserControl mit einem Rechner zu erstellen, der einen Mode als klassischen Taschenrechner aber umgeschaltet auf einen Rechner mit einer Expression Eingabezeile. Die Rechenfunktionen sind erweiterbar. 

## GUI Classic Calculator

<img src="CalculatorClassic.png" style="width:650px;"/>

## GUI Expression Calaculator

<img src="CalculatorExpression.png" style="width:650px;"/>

### Tokenizer


### Expression Parser


### CalculatorEngine

## Funktionsauswahl für den Expression Calaculator

<img src="CalculatorAuswahlFunktionen.png" style="width:650px;"/>


## Sonderfunktion Lookup
Die Lookup Funktion ist keine klassische Funktion wie Datum oder Umrechnung. Die LookupFuktion benötigt eine Datenquelle (in das Projekt fest als DataTable eingebaut) um Auswertungen durchführen zu können.

<img src="Lookup_Ergebnis.png" style="width:650px;"/>

```csharp
/* Laden von Daten zur Lookup-Auswertung*/
DataTable dtKunden = LadeKunden();
DataTable dtArtikel = LadeArtikel();
DataTableLookupProvider provider = new();
provider.Add("Kunden", dtKunden, "A");
provider.Add("Artikel", dtArtikel, "A");

var lookupFunction = new LookupFunction
{
    Provider = provider
};

this._functionRegistry.Register(lookupFunction);

_evaluator = new Evaluator(this, _functionRegistry);

```
Im Beispiel ist der Provider auf ein DataTabele ausgelegt, es ist aber auch eine beliebige andere Datenquelle denkbar.


![Version](https://img.shields.io/badge/Version-1.0.2026.8-yellow.svg)
- Erste Erstellung Version vom 22.07 2026
