using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.GameColor
{
    [Serializable]
    public struct ColorDictEntry
    {
        public GameColorEnum colorKey;
        public Color colorValue;
    }

    [CreateAssetMenu(fileName = "ColorMap", menuName = "ColorMap", order = 0)]
    public class ColorDict : ScriptableObject
    {
        public List<ColorDictEntry> Data;

        public Color GetColor(GameColorEnum colorEnum)
        {
            foreach(ColorDictEntry c in Data)
            {
                if(c.colorKey == colorEnum)
                {
                    return c.colorValue;
                }
            }

            return Color.black;
        }
        
        public bool MixColors(GameColorEnum colorA, GameColorEnum colorB, out GameColorEnum resultColor)
        {
            if(colorA == GameColorEnum.Red && colorB == GameColorEnum.Blue ||
               colorA == GameColorEnum.Blue && colorB == GameColorEnum.Red)
            {
                resultColor = GameColorEnum.Purple;
            }
            else if(colorA == GameColorEnum.Red && colorB == GameColorEnum.Yellow ||
                    colorA == GameColorEnum.Yellow && colorB == GameColorEnum.Red)
            {
                resultColor =  GameColorEnum.Orange;
            }
            else if(colorA == GameColorEnum.Blue && colorB == GameColorEnum.Yellow ||
                    colorA == GameColorEnum.Yellow && colorB == GameColorEnum.Blue)
            {
                resultColor =  GameColorEnum.Green;
            }
            else
            {
                resultColor =  GameColorEnum.Black; // Invalid mix
            }
            
            return resultColor != GameColorEnum.Black;
        }
    }
}