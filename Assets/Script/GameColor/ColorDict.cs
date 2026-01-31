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
    }
}