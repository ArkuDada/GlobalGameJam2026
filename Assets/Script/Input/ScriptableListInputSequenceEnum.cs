using UnityEngine;
using Obvious.Soap;

[CreateAssetMenu(fileName = "scriptable_list_" + nameof(InputSequenceEnum), menuName = "Soap/ScriptableLists/"+ nameof(InputSequenceEnum))]
public class ScriptableListInputSequenceEnum : ScriptableList<InputSequenceEnum>
{
    
}

[System.Serializable]
public class ScriptableListInputSequenceEnumReadOnly : ScriptableListReadOnly<ScriptableListInputSequenceEnum, InputSequenceEnum>
{
}
