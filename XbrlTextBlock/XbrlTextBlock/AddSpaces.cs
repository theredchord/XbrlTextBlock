﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XbrlTextBlock
{
    public class AddSpaces
    {
        public string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
