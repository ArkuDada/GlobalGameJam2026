using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

[AddComponentMenu("Soap/EventListeners/EventListener"+nameof(MovementResult))]
public class EventListenerMovementResult : EventListenerGeneric<MovementResult>
{
    [SerializeField] private EventResponse[] _eventResponses = null;
    protected override EventResponse<MovementResult>[] EventResponses => _eventResponses;

    [System.Serializable]
    public class EventResponse : EventResponse<MovementResult>
    {
        [SerializeField] private ScriptableEventMovementResult _scriptableEvent = null;
        public override ScriptableEvent<MovementResult> ScriptableEvent => _scriptableEvent;

        [SerializeField] private MovementResultUnityEvent _response = null;
        public override UnityEvent<MovementResult> Response => _response;
    }

    [System.Serializable]
    public class MovementResultUnityEvent : UnityEvent<MovementResult>
    {
        
    }
}
