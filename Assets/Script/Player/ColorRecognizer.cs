using System;
using System.Collections.Generic;
using NUnit.Framework;
using Script;
using UnityEngine;

public class ColorRecognizer : MonoBehaviour
{

    public List<ComboInputSO> Combos;

    public List<MovementEnum> recentInput;

    public bool GetColor(List<MovementEnum> input, out GameColorEnum color)
    {
        bool isMatch = false;
        color = GameColorEnum.Black; // Default color
        foreach(var combo in Combos)
        {
            //Get last n input equal to length of combo inputs
            int comboLength = combo.Inputs.Count;

            if(input.Count < combo.Inputs.Count)
                continue;

            recentInput = input.GetRange(input.Count - comboLength, comboLength);

            bool isSequenceMatch = true;

            for(int i = 0; i < comboLength; i++)
            {
                if(combo.Inputs[i] != recentInput[i])
                {
                    isSequenceMatch = false;
                    break;
                }
            }

            if(isSequenceMatch)
            {
                isMatch = true;
                color = combo.Color;
                break;
            }
        }

        return isMatch; // Default color if no match found
    }
}