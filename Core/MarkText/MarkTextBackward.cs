using Core;
using System.Drawing;
using System.Windows.Forms;

namespace TypingAppWF
{
    class MarkTextBackward : IMarkText
    {
        public void MarkText(RichTextBox richTextBox, ref int position)
        {
            var lessonText = richTextBox.Text;
            var s1 = lessonText.Substring(0, position - 1);
            var s2 = lessonText[position - 1].ToString();//this letter will be colored
            var s3 = lessonText.Substring(position);
            richTextBox.Text = string.Empty;
            richTextBox.AppendText(s1);
            richTextBox.AppendText(s2, Color.Red);
            richTextBox.AppendText(s3);
            position--;
        }
    }
}
