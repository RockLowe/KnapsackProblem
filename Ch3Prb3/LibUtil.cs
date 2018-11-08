/****************************************************************/
/*                                                              */
/*  Class: CIS 430 - Artificial Intelligence                    */
/*                                                              */
/*  Program: LibUtil.CS                                         */
/*                                                              */
/*  Programmer: Dr. Oakes                                       */
/*                                                              */
/*  Purpose: Create LibUtil namespace library (DLL).            */
/*           Contains definitions for the following classes.    */
/*           1. ConsoleApp                                      */
/*           2. StringMethods                                   */
/*                                                              */
/****************************************************************/

using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

// Begin namespace StudentListLib
namespace LibUtil
{

  /**************************************************************/
  /* 1. ConsoleApp Class                                        */
  /**************************************************************/

  public class ConsoleApp
  {
    public static void ClrScr()
    {
      Console.BackgroundColor = ConsoleColor.DarkBlue;
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.Clear();
    }
  
    public static void Pause()
    {
      Console.Write("Press any key to continue . . . ");
      Console.ReadKey(true);
      Console.WriteLine();
    }

    public static char GetCh()
    {
      return Console.ReadKey(true).KeyChar;
    }

    public static char GetChE()
    {
      return Console.ReadKey(false).KeyChar;
    }

    public static void Exit()
    {
      Environment.Exit(0);
    }

  } // ConIO

  /**************************************************************/
  /* 2. StringMethods Class                                     */
  /**************************************************************/
  
  public class StringMethods
  {
    public static string SpaceDelimit(string stringValue)
    {
      if (stringValue!=null)
      {
        stringValue = stringValue.Trim();
        while (Regex.IsMatch(stringValue,"  "))
          stringValue = stringValue.Replace("  "," ");
      } 
      return stringValue;
    }

    public static string[] ParseCsvString(string stringValue)
    {
      int      i = 0;
      string[] words = null;
      Regex    regex = new Regex(@"""[^""\r\n]*""|'[^'\r\n]*'|[^,\r\n]+");
      Match    matchResults = null;

      if (stringValue!=null)
      {
        stringValue = stringValue.Trim();
        matchResults = regex.Match(stringValue);
        while (matchResults.Success) 
        {
          i++;
          matchResults = matchResults.NextMatch();
        }
        words        = new string[i];
        matchResults = regex.Match(stringValue);
        i = 0;
        for (i=0; i<words.Length; i++) 
        {
          words[i]     = matchResults.Value.Replace("\"","");
          matchResults = matchResults.NextMatch();
        }
      }
      return words;
    }

  } // StringMethods
  
} // End namespace Util

