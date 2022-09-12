using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiamondKata
{
    public class Diamond
    {
        private const char _firstAlphabetChar = 'A';
        private const string InvalidCharMessage = "Invalid character";

        public Diamond()
        { 
        }

        public List<string>  Generate(char maxChar)
        {
            IsValidAlphabet(maxChar);

            var maxDiamondRows = (maxChar - _firstAlphabetChar + 1) * 2 - 1;

            List<string> _rows = new List<string>();
            //Half of diamond
            for (char currentChar = _firstAlphabetChar; currentChar <= maxChar; currentChar++)
            {
                string row = GenerateRow(currentChar, maxChar, maxDiamondRows);
                _rows.Add(row);
            }
            List<string> _bottomRows = Enumerable.Reverse(_rows).ToList();
            
            _rows.AddRange(_bottomRows.Skip(1));
            
            return _rows;
        }

        private bool IsValidAlphabet(char v)
        {
            if (!Char.IsLetter(v))
                throw new Exception(InvalidCharMessage);

            return true;
        }

        private string GenerateRow(char currentChar, char maxChar, int maxDiamondRows)
        {
            var externalPadding = maxChar - currentChar;
            var sb = new StringBuilder();

            sb.Append(new string(' ', externalPadding));
            
            sb.Append(currentChar);

            if (currentChar != _firstAlphabetChar)
            {              
                var internalPadding = maxDiamondRows - externalPadding * 2 - 2;
                sb.Append(new string(' ', internalPadding));
                sb.Append(currentChar);
            }
            
            sb.Append(new string(' ', externalPadding));
            return sb.ToString();
        }          
    }
}
