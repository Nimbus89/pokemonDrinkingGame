using UnityEngine;
using System.Collections;

public static class StringUtils {

    private static string _lineWrap(string _str, int _charsPerLine, bool _forceWrap)
    {
        if (_str.Length < _charsPerLine)
            return _str;


        string result = "",
                buf = "";						// Stores current word

        int cursor = 0,					// Position within _str
            charCount = 0;					// # of chars on current line

        bool bLineEmpty = false;				// if a new line was added to the result &
        // buf has not been appended.

        while (cursor < _str.Length)
        {
            buf += _str[cursor];				// add next character to buffer
            charCount++;						// increment count of chars on current line

            if (_str[cursor] == ' ')			// if space is encountered
            {
                result += buf;					// write the buffer to the result and clear
                buf = "";
                bLineEmpty = false;				// Something added since the last carriage return
            }
            else if (_str[cursor] == '\n')		// manual carriage return encountered
            {
                result += buf;					// write the buffer to the result and clear (buf contains the \n already)
                buf = "";
                charCount = 0;					// Start new line so reset character count.
            }

            if (charCount >= _charsPerLine) 	// if charCount has reached max chars per line
            {
                if (!bLineEmpty)				// If line has something in it.
                {
                    result += '\n';				// Start a new line in the result
                    charCount = buf.Length;		// reset character count to current buf size as ths will be placed on the new line.
                    bLineEmpty = true;			// Newest line is empty
                }
                else if (_forceWrap)			// else if the line is empty and we want to force word to wrap
                {
                    result += buf + '\n';		// write the buffer to the result with a carriage return
                    buf = "";					// clear the buffer
                    bLineEmpty = true;			// Newest line is empty
                    charCount = 0;				// Start new line so reset character count.
                }
            }

            cursor++;							// Goto next character
        }

        result += buf;							// add any remaining characters in the buffer
        return result;
    }

    public static string[] splitIntoLines(string text, int lineSize) {
        string splitText = _lineWrap(text, lineSize, true);
        return splitText.Split('\n');
    }



}
