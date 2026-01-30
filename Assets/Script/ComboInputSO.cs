using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Script
{
    [CreateAssetMenu(fileName = "Combo", menuName = "Kuy", order = 0)]
    public class ComboInputSO : ScriptableObject
    {
        public List<MovementEnum> Inputs = new List<MovementEnum>();
        public Color Color;
    }
}