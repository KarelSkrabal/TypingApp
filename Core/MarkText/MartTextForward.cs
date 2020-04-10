using Core;
using System.Drawing;
using System.Windows.Forms;

namespace TypingAppWF
{
    internal class MartTextForward : IMarkText
    {
        public void MarkText(RichTextBox richTextBox, ref int position)
        {
            //check if next position in the text is less then very last letter of the text
            if (richTextBox.Text.Length > position + 1)
            {
                position++;
                var lessonText = richTextBox.Text;
                var s1 = lessonText.Substring(0, position);
                var s2 = lessonText[position].ToString();//this text will be colored
                var s3 = lessonText.Substring(position + 1);
                richTextBox.Text = string.Empty;
                richTextBox.AppendText(s1);
                richTextBox.AppendText(s2, Color.Red);
                richTextBox.AppendText(s3);
            }
        }
    }
}
