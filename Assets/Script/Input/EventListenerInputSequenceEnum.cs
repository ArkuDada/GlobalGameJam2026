using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

[AddComponentMenu("Soap/EventListeners/EventListener"+nameof(InputSequenceEnum))]
public class EventListenerInputSequenceEnum : EventListenerGeneric<InputSequenceEnum>
{
    [SerializeField] private EventResponse[] _eventResponses = null;
    protected override EventResponse<InputSequenceEnum>[] EventResponses => _eventResponses;

    [System.Serializable]
    public class EventResponse : EventResponse<InputSequenceEnum>
    {
        [SerializeField] private ScriptableEventInputSequenceEnum _scriptableEvent = null;
        public override ScriptableEvent<InputSequenceEnum> ScriptableEvent => _scriptableEvent;

        [SerializeField] private InputSequenceEnumUnityEvent _response = null;
        public override UnityEvent<InputSequenceEnum> Response => _response;
    }

    [System.Serializable]
    public class InputSequenceEnumUnityEvent : UnityEvent<InputSequenceEnum>
    {
        
    }
}
