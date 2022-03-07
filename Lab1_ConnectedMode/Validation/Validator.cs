using System;
using System.Text.RegularExpressions;

namespace Lab1_ConnectedMode.Validation
{
    public static class Validator
    {
        // cehck if the id is valid, one input
        public static bool IsValidId(string input)
        {
            if (input == null)
            {
                return true;
            }
            else if (!Regex.IsMatch(input, @"^\d*$"))
            {

                return false;
            }
            return true;

        }

        // check if the ID is valid, two inputs
        public static bool IsValidId(string input, int length)
        {
            if (!Regex.IsMatch(input, @"^\d{" + length + "}$"))
            {
                return false;
            }
            return true;

        }
        // check if the name is a valid one
        public static bool IsValidName(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < input.Length; i++)
            {

                if ((!(Char.IsLetter(input[i]))) && (!(Char.IsWhiteSpace(input[i]))))
                {
                    return false;
                }

            }
            return true;
        }

    }
}
