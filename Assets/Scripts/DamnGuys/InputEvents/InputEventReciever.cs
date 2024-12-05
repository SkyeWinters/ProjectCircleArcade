using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DamnGuys.InputEvents
{
    public class InputEventReciever : MonoBehaviour
    {
        private enum InputSource { Left, Right, Both }
        private enum InputType { Tap, HoldProgressed, HoldReleased }
        
        [SerializeField] private List<InputEvent> _events;

        private readonly List<InputEvent> _registeredEvents = new();
        
        private void OnEnable()
        { 
            _registeredEvents.Clear();
            _events.ForEach(x => x.Register());
            _events.ForEach(x => _registeredEvents.Add(x));
        }

        private void OnDisable()
        {
            _registeredEvents.ForEach(x => x.Unregister());
        }

        [Button]
        public void Refresh()
        {
            _registeredEvents.ForEach(x => x.Unregister());
            _registeredEvents.Clear();
            _events.ForEach(x => x.Register());
            _events.ForEach(x => _registeredEvents.Add(x));
        }

        [Serializable]
        public class InputEvent
        {
            [SerializeField] private InputSource _source;
            [SerializeField] private InputType _type;
            [SerializeField, HideIf("_type", InputType.HoldProgressed)] private UnityEvent _event;
            [SerializeField, ShowIf("_type", InputType.HoldProgressed)] private UnityEvent<float> _normalizedEvent;
            
            public void Register()
            {
                if (_type == InputType.HoldProgressed)
                {
                    ObtainNormalizedUnityEvent(_source, _type)?.AddListener(_normalizedEvent.Invoke);
                    return;
                }
                ObtainUnityEvent(_source, _type)?.AddListener(_event.Invoke);
            }
            
            public void Unregister()
            {
                if (_type == InputType.HoldProgressed)
                {
                    ObtainNormalizedUnityEvent(_source, _type)?.RemoveListener(_normalizedEvent.Invoke);
                    return;
                }
                ObtainUnityEvent(_source, _type)?.RemoveListener(_event.Invoke);
            }
            
            private UnityEvent ObtainUnityEvent(InputSource source, InputType type)
            {
                if (InputEventManager.Instance == null) return null;
                
                var listener = source switch
                {
                    InputSource.Left => InputEventManager.Instance.LeftHand,
                    InputSource.Right => InputEventManager.Instance.RightHand,
                    InputSource.Both => InputEventManager.Instance.BothHands,
                    _ => null
                };
            
                if (listener == null) return null;
            
                return type switch
                {
                    InputType.Tap => listener.OnTap,
                    InputType.HoldReleased => listener.OnHoldReleased,
                    _ => null
                };
            }
        
            private UnityEvent<float> ObtainNormalizedUnityEvent(InputSource source, InputType type)
            {
                var listener = source switch
                {
                    InputSource.Left => InputEventManager.Instance.LeftHand,
                    InputSource.Right => InputEventManager.Instance.RightHand,
                    InputSource.Both => InputEventManager.Instance.BothHands,
                    _ => null
                };
            
                if (listener == null) return null;
            
                return type switch
                {
                    InputType.HoldProgressed => listener.OnHoldProgressed,
                    _ => null
                };
            }
        }
    }
}