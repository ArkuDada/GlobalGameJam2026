using System.Collections.Generic;
using Script;
using UnityEngine;

public class ColorRecognizer : MonoBehaviour
{
    public List<ComboInputSO> Combos;

    public List<MovementEnum> recentInput;

    public bool GetColor(List<MovementEnum> input, out Color color)
    {
        bool isMatch = false;
        color = Color.black;
        foreach(var combo in Combos)
        {
            //Get last n input equal to length of combo inputs
            int comboLength = combo.Inputs.Count;
            recentInput = input.GetRange(input.Count - comboLength, comboLength);

            if(combo.Inputs.Count != recentInput.Count)
                continue;

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