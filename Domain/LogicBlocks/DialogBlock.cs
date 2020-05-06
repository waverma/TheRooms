using System;
using TheRooms.Domain.Creatures;

namespace TheRooms.Domain.LogicBlocks
{
    public class DialogBlock
    { // TODO ИЗМЕНИТЬ ВМЕСТЕ С ДИАЛОГАМИ
        public Dialog Dialog { get; private set; }

        public event Action DialogBlockChanged;

        public void ChangeDialog(Dialog newDialog)
        {
            Dialog = newDialog;
            DialogBlockChanged?.Invoke();
        }
    }
}
