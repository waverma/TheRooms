using TheRooms.interfaces;
using TheRooms.MFUGE;

namespace TheRooms.Domain
{
    public class TickHandler
    {
        public bool MultiMapMode { get; set; }
        private readonly Game game;

        public readonly int TickInterval;

        public TickHandler(Game game, int interval)
        {
            TickInterval = interval;
            MultiMapMode = false;
            this.game = game;
        }

        public void Click(Vector cellLocation)
        {
            game.DialogBlock.ChangeDialog(null);
            if (game.AreaBlock.GetCurrentArea().PlayerLocation == cellLocation || !game.AreaBlock.GetCurrentArea().InBounds(cellLocation))
                return;

            var currentCell = game.AreaBlock.GetCurrentArea().Map[cellLocation.X, cellLocation.Y];
            if (currentCell is null) return;
            if (currentCell.Creature is null)
            {
                game.InventoryBlock.RemoveRightInventory();
                var path = game.AreaBlock.GetCurrentArea().FindPathOrDefault(game.AreaBlock.GetCurrentArea().PlayerLocation, cellLocation);

                if (path == null) return;
                game.PlayerStateBlock.Player.SetPath(new Path(path));
                return;
            }

            if (!game.AreaBlock.GetCurrentArea().PlayerLocation.IsNeighboringVector(cellLocation))
            {
                if (currentCell.Creature == null)
                    return;
                if (game.PlayerStateBlock.Player.Hand is IGun gun)
                    gun.TryShot(currentCell);
                return;
            }
            // интерактирование с существом
            var creatureAction = game.AreaBlock.GetCurrentArea().Map[cellLocation.X, cellLocation.Y].Creature.GetActionOnClick();
            creatureAction(game);
        }

        public void OnTick()
        {
            if (MultiMapMode)
                foreach (var area in game.AreaBlock.GetAreas())
                    RunMap(area);
            else
                RunMap(game.AreaBlock.GetCurrentArea());

            if (game.InventoryBlock.RightInventory != null)
                foreach (var rightInventoryItem in game.InventoryBlock.RightInventory.Items)
                    rightInventoryItem?.GetAction()(game);

            if (game.InventoryBlock.LeftInventory != null)
                foreach (var rightInventoryItem in game.InventoryBlock.LeftInventory.Items)
                    rightInventoryItem?.GetAction()(game);

            game.PlayerStateBlock.Player.GetAction()(game);
        }

        private void RunMap(Area area)
        {
            foreach (var cell in area.Map)
            {
                cell?.Creature?.GetAction()(game);
                cell?.Ground?.GetAction()(game);
                cell?.Sky?.GetAction()(game);
            }
        }
    }
}
