//using System;
//using System.CodeDom;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TheRooms.MFUGE
//{
//    public class Engine
//    {
        

//        private Area[] Areas;
//        private int CurrentArea;
//        private Player Player;

//        public event Action StateChanged;

//        public Engine(Area[] areas, Player player, int currentArea = 0)
//        {
//            Areas = areas;
//            Player = player;
//            CurrentArea = currentArea > -1 && currentArea < Areas.Length
//                ? currentArea : 0;

//            if (Areas.Length == 0)
//                CurrentArea = -1;
//        }

//        //public Area GetCurrentArea()
//        //{
//        //    if (CurrentArea < 0 || CurrentArea >= Areas.Length)
//        //        return null;
//        //    return Areas[CurrentArea];
//        //}

//        //public bool TryChangeArea(int index)
//        //{
//        //    if (index <= -1 || index >= Areas.Length) return false;
//        //    CurrentArea = index;
//        //    return true;
//        //}




//        //
//        // Возможно, стоит перенести в Area
//        //
        
//    }

//    public class EngineBuilder
//    { // I don`t know how this test
//        private readonly List<Area> _areas;
//        private Player _player;
//        private int CurrentArea { get; set; }

//        public EngineBuilder()
//        {
//            _areas = new List<Area>();
//        }

//        public EngineBuilder AddArea(Area area)
//        {
//            _areas.Add(area);
//            return this;
//        }

//        public EngineBuilder SetPlayer(Player player)
//        {
//            _player = player;
//            return this;
//        }

//        public EngineBuilder SetCurrentArea(int areaIndex)
//        {
//            CurrentArea = areaIndex;
//            return this;
//        }

//        public Engine Build()
//        {
//            var index = CurrentArea > -1
//                        && CurrentArea < _areas.Count
//                ? CurrentArea : 0;
//            return new Engine(_areas.ToArray(), _player, index);
//        }
//    }
//}