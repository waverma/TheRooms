using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain
{
    public enum GameState
    {
        MainMenu = 0,
        Play,
        Pause,
        SettingsShow,


        Closing = 999
    }

    public class Game
    {
        private GameState _state;
         
        private string TextForDialog { get; set; }
        private const string InitialSaveFileName = "";

        public Engine Save { get; private set; }

        public event Action<GameState> StateChanged;

        public Game()
        {
            ChangeStage(GameState.Play);



            // для демонстрации
            var player = new Player("Admin", new Vector(0, 0), new Inventory(24));

            var eb = new EngineBuilder();
            eb = eb.AddArea(Area.GetAreaForShow());
            eb = eb.SetCurrentArea(0);
            eb = eb.SetPlayer(player);

            Save = eb.Build();
        }

        public void SetTextForShow(string text)
        { // Kill me
            TextForDialog = text;
        }

        public string GetTextForShow()
        { // Kill me
            return TextForDialog;
        }

        private void ChangeStage(GameState state)
        {
            StateChanged?.Invoke(_state = state);
        }

        public IReadOnlyDictionary<string, Action<Game>> GetMainMenuButtonContent()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyDictionary<string, Action<Game>> GetGameMenuButtonContent()
        {
            var gameMenuButtonContent = new Dictionary<string, Action<Game>>
            { 
                ["Play/Pause"] = (Game game) =>
                {
                    ChangeStage(_state == GameState.Pause ? GameState.Play : GameState.Pause);
                },
                ["settings"] = (Game game) => ChangeStage(GameState.SettingsShow),
                //["save"] = (Game game) => ChangeStage(GameState.Closing),
                //["Exit"] = (Game game) => ChangeStage(GameState.Closing),
                // ["New game"] = (Game game) => StartGame(InitialSaveFileName),
                ["Exit"] = (Game game) => ChangeStage(GameState.Closing)
            };

            return gameMenuButtonContent;
        }

        public void StartGame(string saveFileName)
        {
            throw new NotImplementedException();
        }

        public void SetNewSave()
        {
            throw new NotImplementedException();
        }

        private void SetSave()
        {
            throw new NotImplementedException();
        }

        public void SaveAction_EventHandler()
        {
            throw new NotImplementedException();
        }

        //public bool IsWallAt(Point at)
        //{
        //    return IsWallAt(at.X, at.Y);
        //}

        //public bool IsWallAt(int x, int y)
        //{
        //    return CellCost[x, y] == 0;
        //}

        //public bool InsideMap(Point p) => InsideMap(p.X, p.Y);

        //public bool InsideMap(int x, int y)
        //{
        //    return x >= 0 && x < MapWidth && y >= 0 && y < MapHeight;
        //}

        //public void RemoveGoal()
        //{
        //    Goal = -1;
        //}
    }
}
