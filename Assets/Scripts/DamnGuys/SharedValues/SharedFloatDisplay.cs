using TMPro;
using UnityEngine;

namespace DamnGuys.SharedValues
{
    public class SharedFloatDisplay : MonoBehaviour
    {
        [Tooltip("The shared float to display.")]
        [SerializeField] private SharedFloat _sharedFloat;
        [Tooltip("The text component to display the shared float.")]
        [SerializeField] private TMP_Text _text;
        [Tooltip("The format to display the shared float.")]
        [SerializeField] private string _format = "0.00";
        [Tooltip("The prefix to display before the shared float.")]
        [SerializeField] private string _prefix = "";
        
        private void OnEnable()
        {
            _sharedFloat.OnValueChanged += OnValueChanged;
            OnValueChanged();
        }
        
        private void OnDisable()
        {
            _sharedFloat.OnValueChanged -= OnValueChanged;
        }
        
        private void OnValueChanged()
        {
            _text.text = $"{_prefix}{_sharedFloat.Value.ToString(_format)}";
        }
    }
}