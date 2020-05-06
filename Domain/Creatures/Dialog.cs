using System.Collections.Generic;
using System.Linq;

namespace TheRooms.Domain.Creatures
{
    public enum CurrentTextOwner
    {
        Player = 0,
        Creature = 1
    }

    public class Dialog
    {
        private readonly Stack<string> playerNextText = new Stack<string>();
        private readonly Stack<string> creatureNextText = new Stack<string>();
        private readonly Stack<string> playerPrevText = new Stack<string>();
        private readonly Stack<string> creaturePrevText = new Stack<string>();

        private readonly Stack<CurrentTextOwner> nextPointer = new Stack<CurrentTextOwner>();
        private readonly Stack<CurrentTextOwner> prevPointer = new Stack<CurrentTextOwner>();

        public Dialog(IEnumerable<string> text, string owner)
        {
            foreach (var line in text)
            {
                var currentSpeech = string.Join("", line.TrimStart(':').Skip(1).ToArray()).TrimStart(':', ' ');
                
                if (int.Parse(line[2].ToString()) == 0)
                    playerNextText.Push("Я: " + currentSpeech);
                else
                    creatureNextText.Push(owner + ": " + currentSpeech);
                nextPointer.Push((CurrentTextOwner)int.Parse(line[2].ToString()));
            }

            var a = new Stack<string>();
            while (playerNextText.Count != 0)
                a.Push(playerNextText.Pop());
            playerNextText = a;

            a = new Stack<string>();
            while (creatureNextText.Count != 0)
                a.Push(creatureNextText.Pop());
            creatureNextText = a;

            var b = new Stack<CurrentTextOwner>();
            while (nextPointer.Count != 0)
                b.Push(nextPointer.Pop());
            nextPointer = b;
        }

        public (CurrentTextOwner, string) GetNextLine()
        {
            if (nextPointer.Count == 0) return (CurrentTextOwner.Player, null);
            prevPointer.Push(nextPointer.Pop());

            if(prevPointer.Peek() == CurrentTextOwner.Player)
            {
                playerPrevText.Push(playerNextText.Pop());
                return (CurrentTextOwner.Player, playerPrevText.Peek());
            }
            creaturePrevText.Push(creatureNextText.Pop());
            return (CurrentTextOwner.Creature, creaturePrevText.Peek());
        }

        public (CurrentTextOwner, string) GetPreviousLine()
        {
            if (prevPointer.Count == 0) return (CurrentTextOwner.Player, null);
            nextPointer.Push(prevPointer.Pop());

            if (nextPointer.Peek() == CurrentTextOwner.Player)
            {
                playerNextText.Push(playerPrevText.Pop());
                return (CurrentTextOwner.Player, playerNextText.Peek());
            }
            creatureNextText.Push(creaturePrevText.Pop());
            return (CurrentTextOwner.Creature, creatureNextText.Peek());
        }
    }
}
