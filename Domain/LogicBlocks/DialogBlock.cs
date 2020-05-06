using System;

namespace TheRooms.Domain.LogicBlocks
{
    public class DialogBlock
    { // TODO ИЗМЕНИТЬ ВМЕСТЕ С ДИАЛОГАМИ
        public Dialog PlayerDialog { get; private set; }
        public Dialog CreatureDialog { get; private set; }

        public event Action DialogBlockChanged;

        public void ChangePlayerDialog(Dialog newDialog)
        {
            PlayerDialog = newDialog;
            DialogBlockChanged?.Invoke();
        }

        public void ChangeCreatureDialog(Dialog newDialog)
        {
            CreatureDialog = newDialog;
            DialogBlockChanged?.Invoke();
        }

        public string GetNextPlayerString()
        {
            return PlayerDialog?.GetNextLine();
        }

        public string GetNextCreatureString()
        {
            return CreatureDialog?.GetNextLine();
        }

        public string GetPreviousPlayerString()
        {
            return PlayerDialog?.GetPreviousLine();
        }

        public string GetPreviousCreatureString()
        {
            return CreatureDialog.GetPreviousLine();
        }
    }
}
