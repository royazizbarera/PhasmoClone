using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Constants
{
    public static class LevelSizeConst
    {
        public enum LevelSize {Tiny, Small, Medium, Big };

        public enum MapMinHuntDurationEnum
        {
            Tiny = 8,
            Small = 14,
            Medium = 22,
            Big = 30,
        }  
        
        public enum MapMaxHuntDurationEnum
        {
            Tiny = 13,
            Small = 25,
            Medium = 35,
            Big = 50,
        }

        public static int MapMinHuntDuration(string levelSize)
        {
            return (int)Enum.Parse(typeof(MapMinHuntDurationEnum), levelSize);
        }

        public static int MapMaxHuntDuration(string levelSize)
        {
            return (int)Enum.Parse(typeof(MapMaxHuntDurationEnum), levelSize);
        }
    }
}