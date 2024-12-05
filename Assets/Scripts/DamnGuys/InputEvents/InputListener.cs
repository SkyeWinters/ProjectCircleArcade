using System;
using UnityEngine;
using UnityEngine.Events;

namespace DamnGuys.InputEvents
{
    [Serializable]
    public class InputListener
    { 
        private float _holdTime;
        private float _timer;
        private bool _tapped;
        private float _delay;
            
        /// <summary>
        /// Will return true if the user is holding the input but has not yet crossed the tap threshold
        /// </summary>
        public bool WaitingToTap => IsHolding && !_tapped;
        
        /// <summary>
        /// Will return true if the user is holding the input
        /// </summary>
        public bool IsHolding { get; private set; }
        
        /// <summary>
        /// Invoked when the user taps the input
        /// </summary>
        public UnityEvent OnTap { get; } = new();
        
        /// <summary>
        /// Invoked when the user releases the input after holding for the specified duration
        /// </summary>
        public UnityEvent OnHoldReleased { get; } = new();
        
        /// <summary>
        /// Invoked when the user releases the input before the specified duration
        /// </summary>
        public UnityEvent OnHoldCanceled { get; } = new();
        
        /// <summary>
        /// Invoked every frame the user is holding the input. Returns the progress of the hold as a float between 0 and 1
        /// </summary>
        public UnityEvent<float> OnHoldProgressed { get; } = new();
        
        /// <summary>
        /// Handle a new input state for the listener. If the input is pressed, the listener will reset its state.
        /// If the input is released, the listener will determine which hold event to invoke.
        /// </summary>
        public void OnInput(bool isPressed, float holdTime, float delayToAct = 0)
        {
            if (isPressed)
            {
                Reset(holdTime, delayToAct);
            }
            else if (IsHolding)
            {
                if (Time.time - _timer > _holdTime + _delay)
                {
                    OnHoldReleased.Invoke();
                }
                else
                {
                    OnHoldCanceled.Invoke();
                }
                    
                IsHolding = false;
            }
        }
        
        /// <summary>
        /// Progress the hold event if the user is holding the input
        /// </summary>
        public void Update()
        {
            if (!IsHolding) return;
            
            if (_tapped)
            {
                OnHoldProgressed.Invoke((Time.time - _timer - _delay) / _holdTime);
            }
            else if (Time.time - _timer > _delay)
            {
                OnTap.Invoke();
                _tapped = true;
            }
        }
            
        /// <summary>
        /// Cancels the input without firing any events
        /// </summary>
        public void Cancel()
        {
            IsHolding = false;
        }

        private void Reset(float holdTime, float delayToAct)
        {
            _timer = Time.time;
            _tapped = false;
            _delay = delayToAct;
            IsHolding = true;
            _holdTime = holdTime;
        }
    }
}