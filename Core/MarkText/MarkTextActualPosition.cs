using Core;
using System.Drawing;
using System.Windows.Forms;

namespace TypingAppWF
{
    class MarkTextActualPosition : IMarkText
    {
        public void MarkText(RichTextBox richTextBox, ref int position)
        {
            var lessonText = richTextBox.Text;
            var s1 = lessonText[position].ToString();//this letter will be colored
            var s2 = lessonText.Substring(position + 1);
            richTextBox.Text = string.Empty;
            richTextBox.AppendText(s1, Color.Red);
            richTextBox.AppendText(s2);
        }
    }
}
