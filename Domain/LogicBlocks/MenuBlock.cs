using System;
using System.Collections.Generic;

namespace TheRooms.Domain.LogicBlocks
{
    public class MenuBlock
    {// todo УБРАТЬ ЖЕСТКУЮ ПРИВЯЗКУ К ВЬЮ И ВООБЩЕ РЕАЛИЗОВАТЬ ЭТУ ХРЕНЬ
        public event Action MenuBlockChanged;

        public IReadOnlyDictionary<string, Action<Game>> GetGameMenuButtonContent()
        {
            var gameMenuButtonContent = new Dictionary<string, Action<Game>>
            {
                ["Play/Pause"] = (Game game) =>
                {
                    // TODO заморозка\разморозка всех потоков, кроме главного
                },
                ["settings"] = (Game game) =>
                {
                    /* TODO добавить формочку для меню настроек
                     * TODO звук\масштаб\управление
                     */
                },
                ["save"] = (Game game) =>
                {
                    // TODO сохранить текущий Game в папку последнего сохранения
                },
                ["saves"] = (Game game) =>
                {
                    // TODO создать форму для просмотра сохранений
                    // TODO выбрать любое сохранение
                },
                ["exit"] = (Game game) =>
                {
                    // TODO предложить сохранить и выйти
                }
            };

            return gameMenuButtonContent;
        }
    }
}
