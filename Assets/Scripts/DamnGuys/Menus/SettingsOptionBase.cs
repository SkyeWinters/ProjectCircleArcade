using UnityEngine;
using UnityEngine.UI;

namespace DamnGuys.Menus
{
    public abstract class SettingsOptionBase : MonoBehaviour
    {
        [Tooltip("The highlight object to display when the option is selected.")]
        [SerializeField] private Image _highlight;
        [SerializeField] private Color _highlightColor;
        
        private Color _originalColor;
        
        private void Awake()
        {
            _originalColor = _highlight.color;
        }

        /// <summary>
        /// Will update the UI to show that the option is selected.
        /// </summary>
        public virtual void OnSelect()
        {
            _highlight.color = _highlightColor;
        }

        /// <summary>
        /// Will update the UI to show that the option is deselected.
        /// </summary>
        public virtual void OnDeselect()
        {
            _highlight.color = _originalColor;
        }
        
        /// <summary>
        /// Will perform the corresponding action for the option.
        /// </summary>
        public abstract void Click();
    }
}