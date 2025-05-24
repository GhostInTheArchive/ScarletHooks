using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScarletHooks.Utils;

public static class AnsiStringExtensions {
  private const string Reset = "\u001b[0m";
  private const string BoldCode = "\u001b[1m";
  private const string ItalicCode = "\u001b[3m";
  private const string UnderlineCode = "\u001b[4m";

  private static readonly string[] HighlightAnsiColors = [
    "\u001b[38;5;135m"
  ];

  private const string TextColor = "\u001b[37m";
  private const string ErrorTextColor = "\u001b[91m";
  private const string WarningTextColor = "\u001b[93m";
  private const string HighlightErrorColor = "\u001b[91m";
  private const string HighlightWarningColor = "\u001b[93m";

  public static string Bold(this string text) => $"{BoldCode}{text}{Reset}";
  public static string Italic(this string text) => $"{ItalicCode}{text}{Reset}";
  public static string Underline(this string text) => $"{UnderlineCode}{text}{Reset}";
  public static string Red(this string text) => $"\u001b[31m{text}{Reset}";
  public static string Green(this string text) => $"\u001b[32m{text}{Reset}";
  public static string Blue(this string text) => $"\u001b[34m{text}{Reset}";
  public static string Yellow(this string text) => $"\u001b[33m{text}{Reset}";
  public static string White(this string text) => $"\u001b[37m{text}{Reset}";
  public static string Black(this string text) => $"\u001b[30m{text}{Reset}";
  public static string Orange(this string text) => $"\u001b[38;5;208m{text}{Reset}";
  public static string Lime(this string text) => $"\u001b[38;5;10m{text}{Reset}";
  public static string Gray(this string text) => $"\u001b[90m{text}{Reset}";
  public static string Ansi(this string ansiCode, string text) => $"{ansiCode}{text}{Reset}";

  public static string FormatAnsi(this string text, List<string> highlightColors = null) {
    highlightColors ??= [HighlightAnsiColors[0]];
    return ApplyAnsiFormatting(text, TextColor, highlightColors);
  }

  public static string FormatAnsiError(this string text) {
    return ApplyAnsiFormatting(text, ErrorTextColor, [HighlightErrorColor]);
  }

  public static string FormatAnsiWarning(this string text) {
    return ApplyAnsiFormatting(text, WarningTextColor, [HighlightWarningColor]);
  }

  private static string ApplyAnsiFormatting(string text, string baseColor, List<string> highlightColors) {
    var boldPattern = @"\*\*(.*?)\*\*";
    var italicPattern = @"\*(.*?)\*";
    var underlinePattern = @"__(.*?)__";
    var highlightPattern = @"~(.*?)~";

    var result = Regex.Replace(text, boldPattern, m => Bold(m.Groups[1].Value));
    result = Regex.Replace(result, italicPattern, m => Italic(m.Groups[1].Value));
    result = Regex.Replace(result, underlinePattern, m => Underline(m.Groups[1].Value));

    int highlightIndex = 0;

    result = Regex.Replace(result, highlightPattern, m => {
      string color = highlightIndex < highlightColors.Count ? highlightColors[highlightIndex] : HighlightAnsiColors[0];
      highlightIndex++;
      return Ansi(color, m.Groups[1].Value);
    });

    return $"{baseColor}{result}{Reset}";
  }
}
