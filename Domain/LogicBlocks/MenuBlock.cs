using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.Domain.LogicBlocks
{
    public class MenuBlock
    {
        public event Action MenuBlockChanged;

        public IReadOnlyDictionary<string, Action<Game>> GetGameMenuButtonContent()
        {
            var gameMenuButtonContent = new Dictionary<string, Action<Game>>
            {
                ["Play/Pause"] = (Game game) =>
                {
                    //ChangeStage(_state == GameState.Pause ? GameState.Play : GameState.Pause);
                },
                ["settings"] = (Game game) =>
                {
                    //ChangeStage(GameState.SettingsShow);
                },
                ["save"] = (Game game) =>
                {
                    //ChangeStage(GameState.Closing);
                },
                ["saves"] = (Game game) =>
                {
                    //ChangeStage(GameState.Closing);
                },
                ["exit"] = (Game game) =>
                {
                    //ChangeStage(GameState.Closing);
                },
                //["New game"] = (Game game) => StartGame(InitialSaveFileName)
            };

            return gameMenuButtonContent;
        }
    }
}
