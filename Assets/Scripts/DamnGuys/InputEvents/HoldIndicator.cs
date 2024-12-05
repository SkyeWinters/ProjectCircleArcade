using UnityEngine;
using UnityEngine.UI;

namespace DamnGuys.InputEvents
{
    public class HoldIndicator : MonoBehaviour
    {
        [Tooltip("The indicator displaying the hold progress")]
        [SerializeField] private GameObject _indicator;
        [Tooltip("The progress bar")]
        [SerializeField] private Image _fill;
        
        private float _timer;

        private void OnEnable()
        {
            InputEventManager.Instance.BothHands.OnHoldCanceled.AddListener(Hide);
            InputEventManager.Instance.BothHands.OnHoldProgressed.AddListener(DisplayTime);
            InputEventManager.Instance.BothHands.OnHoldReleased.AddListener(Hide);
            
            _indicator.SetActive(false);
        }

        private void OnDisable()
        {
            if (InputEventManager.Instance == null) return;
            
            InputEventManager.Instance.BothHands.OnHoldCanceled.RemoveListener(Hide);
            InputEventManager.Instance.BothHands.OnHoldProgressed.RemoveListener(DisplayTime);
            InputEventManager.Instance.BothHands.OnHoldReleased.RemoveListener(Hide);
        }

        private void DisplayTime(float time)
        {
            _fill.fillAmount = time;
            _indicator.SetActive(true);
        }

        private void Hide()
        {
            _indicator.SetActive(false);
        }
    }
}