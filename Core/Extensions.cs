using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Core
{
    public static class Extensions
    {
        public static string ReplaceAt(this string value, int index, char newchar)
        {
            if (value.Length <= index)
                return value;
            else
                return string.Concat(value.Select((c, i) => i == index ? newchar : c));
        }

        public static string GetWord(this string value, int index)
        {
            if (index >= value.Length || value[index].Equals(' '))
                return string.Empty;

            string leftText = value.Substring(0, index);
            string rightText = value.Substring(index + 1);
            int leftSpace = leftText.LastIndexOf(' ');
            int rightSpace = (rightText.IndexOf(' ') != -1) ? rightText.IndexOf(' ') : 0;
            return leftText.Substring(leftSpace + 1) + value[index] + rightText.Substring(0, rightSpace);
        }

        public static void AppendText(this RichTextBox richTextBox, string text, Color color)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(text);
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }
    }
}
