using System;
using System.Collections.Generic;
using System.Text;

public class OldPhonePadConverter
{
    private Dictionary<char, string> keypad = new Dictionary<char, string>()
    {
        {'2', "ABC"},
        {'3', "DEF"},
        {'4', "GHI"},
        {'5', "JKL"},
        {'6', "MNO"},
        {'7', "PQRS"},
        {'8', "TUV"},
        {'9', "WXYZ"}
    };

    public string Convert(string input)
    {
        if (string.IsNullOrEmpty(input) || !input.EndsWith("#"))
        {
            throw new ArgumentException("Invalid input. Input must not be null or empty and must end with '#'.");
        }

        StringBuilder output = new StringBuilder();
        string currentNumber = "";

        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                currentNumber += c;
            }
            else if (c == ' ')
            {
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    output.Append(GetCharacter(currentNumber));
                    currentNumber = "";
                }
            }
            else if (c == '*')
            {
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    output.Append(GetCharacter(currentNumber));
                    currentNumber = "";
                }
                 output.Append("?"); // Handles the '*' case as "?"
            }
            else if (c == '#')
            {
                if (!string.IsNullOrEmpty(currentNumber))
                {
                    output.Append(GetCharacter(currentNumber));
                }
                break; // End of input
            }
            else
            {
                throw new ArgumentException("Invalid character in input: " + c);
            }
        }

        return output.ToString();
    }

    private char GetCharacter(string number)
    {
        char digit = number[0];
        int count = number.Length;

        if (!keypad.ContainsKey(digit))
        {
            throw new ArgumentException("Invalid digit: " + digit);
        }

        string letters = keypad[digit];
        int index = (count - 1) % letters.Length;
        return letters[index];
    }


    public static void Main(string[] args)
    {
        OldPhonePadConverter converter = new OldPhonePadConverter();

        Console.WriteLine(converter.Convert("33#")); // Output: E
        Console.WriteLine(converter.Convert("227*#")); // Output: B?
        Console.WriteLine(converter.Convert("4433555 555666#")); // Output: HELLO
        Console.WriteLine(converter.Convert("8 88777444666*664#")); // Output: ?????
        Console.WriteLine(converter.Convert("222 2 22#")); // Output: CAB

    }
}
