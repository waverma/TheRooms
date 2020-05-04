using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TheRooms.Domain
{
    public class Dialog
    { // No test, just something
        private readonly IReadOnlyList<string> _text;
        private int Pointer { get; set; }
        public bool IsDialogCompleted => Pointer >= _text.Count();

        public Dialog(IReadOnlyList<string> text)
        {
            _text = text;
        }

        public string GetNextLine()
        {
            return IsDialogCompleted ? "" : _text[Pointer++];
        }

        public string GetPreviousLine()
        {
            if (Pointer == 0) return "";
            return IsDialogCompleted ? null : _text[--Pointer];
        }
    }
}
