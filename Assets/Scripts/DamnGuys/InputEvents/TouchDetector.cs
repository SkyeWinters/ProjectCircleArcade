using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Utilities;

namespace DamnGuys.InputEvents
{
    public class TouchDetector : MonoBehaviour
    {
        [SerializeField] private InputEventManager _inputEventManager;
        
        private bool _leftBeingTouched;
        private bool _rightBeingTouched;
        
        private void OnEnable()
        {
            EnhancedTouchSupport.Enable(); // Enable Enhanced Touch
        }

        private void Update()
        {
            if (Touchscreen.current == null) return; // No touchscreen detected

            // Get all active touches
            var touches = Input.touches;

            var leftTapped = false;
            var rightTapped = false;

            foreach (var touch in touches)
            {
                var touchPosition = touch.position;
                float screenWidth = Screen.width;

                if (touchPosition.x < screenWidth / 2)
                    leftTapped = true;
                else
                    rightTapped = true;
            }

            if (_leftBeingTouched != leftTapped)
            {
                _leftBeingTouched = leftTapped;
                _inputEventManager.OnInput(Controller.PlayerBinding.PlayerOne, _leftBeingTouched);
            }
            
            if (_rightBeingTouched != rightTapped)
            {
                _rightBeingTouched = rightTapped;
                _inputEventManager.OnInput(Controller.PlayerBinding.PlayerTwo, _rightBeingTouched);
            }
        }
    }
}