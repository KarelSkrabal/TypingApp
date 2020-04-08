using System.Windows.Forms;

namespace TypingAppWF
{
    interface IMarkText
    {
        void MarkText(RichTextBox richTextBox, ref int position);
    }
}
