using System.Windows.Forms;

namespace TypingAppWF
{
    public class MarkTextController
    {
        internal void MarkText(RichTextBox richTextBox, ref int position)
        {
            IMarkText marker = SelectMarkDirection((char?)richTextBox.Tag, position);
            marker.MarkText(richTextBox, ref position);
        }

        private IMarkText SelectMarkDirection(char? key, int position)
        {
            if (key == null && position == 0)
                return new MarkTextActualPosition();
            if ((key == (char)Keys.Back))
                return new MarkTextBackward();
            return new MartTextForward();
        }
    }
}
