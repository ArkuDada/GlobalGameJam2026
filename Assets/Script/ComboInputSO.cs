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
    
    [CreateAssetMenu(fileName = "Combo", menuName = "Kuy", order = 0)]
    public class ComboInputSO : ScriptableObject
    {
        public List<MovementEnum> Inputs = new List<MovementEnum>();
        public GameColorEnum Color;
    }
}