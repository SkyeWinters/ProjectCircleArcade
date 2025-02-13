using UnityEngine;
using Utilities;

namespace DamnGuys.InputEvents
{
    public class InputEventManager : SingletonMonoBehavior<InputEventManager>
    {
        [Tooltip("The amount of time needed for a hold to be considered a hold")]
        [SerializeField] private float _holdTime = 2f;
        
        [Tooltip("The maximum time between both players tapping for the event to trigger")]
        [SerializeField] private float _jointThreshold = 0.05f;

        /// <summary>
        /// The Input Listener responsible for left hand interactions
        /// </summary>
        public InputListener LeftHand { get; } = new();
        
        /// <summary>
        /// The Input Listener responsible for right hand interactions
        /// </summary>
        public InputListener RightHand { get; } = new();
        
        /// <summary>
        /// The Input Listener responsible for combined interactions
        /// </summary>
        public InputListener BothHands { get; } = new();

        private void OnEnable()
        {
            Controller.OnNotePressed += OnInput;
        }
        
        private void OnDisable()
        {
            Controller.OnNotePressed -= OnInput;
        }
        
        public void OnInput(Controller.PlayerBinding input, bool isPressed)
        {
            switch (input)
            {
                case Controller.PlayerBinding.PlayerOne:
                    LeftHand.OnInput(isPressed, _holdTime, _jointThreshold);
                    break;
                case Controller.PlayerBinding.PlayerTwo:
                    RightHand.OnInput(isPressed, _holdTime, _jointThreshold);
                    break;
                default:
                    Debug.LogError("Invalid input type received in InputEvents", gameObject);
                    break;
            }

            CheckBothHandsInput(isPressed);
        }

        private void CheckBothHandsInput(bool isPressed)
        {
            if (isPressed && LeftHand.WaitingToTap && RightHand.WaitingToTap)
            {
                BothHands.OnInput(true, _holdTime);
                LeftHand.Cancel();
                RightHand.Cancel();
            }

            if (!isPressed && BothHands.IsHolding)
            {
                BothHands.OnInput(false, _holdTime);
            }
        }

        private void Update()
        {
            LeftHand.Update();
            RightHand.Update();
            BothHands.Update();
        }
    }
}