using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Script
{
    public enum GameColorEnum
    {
        White,
        Black,
        Red,
        Blue,
        Yellow,
        Green,
        Orange,
        Purple
    }
    
    //helper
    public static class GameColorEnumExtensions
    {
        public static bool IsMainColor(this GameColorEnum color)
        {
            return color == GameColorEnum.Red || color == GameColorEnum.Blue || color == GameColorEnum.Yellow;
        }
        
        public static bool IsMixedColor(this GameColorEnum color)
        {
            return color == GameColorEnum.Orange || color == GameColorEnum.Purple || color == GameColorEnum.Green;
        }
    }
    
    [CreateAssetMenu(fileName = "Combo", menuName = "Kuy", order = 0)]
    public class ComboInputSO : ScriptableObject
    {
        public List<MovementEnum> Inputs = new List<MovementEnum>();
        public GameColorEnum Color;
    }
}