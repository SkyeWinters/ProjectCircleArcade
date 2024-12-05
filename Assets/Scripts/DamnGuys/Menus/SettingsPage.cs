using System.Collections.Generic;
using UnityEngine;

namespace DamnGuys.Menus
{
    public class SettingsPage : MonoBehaviour
    {
        [Tooltip("The display for this settings page.")]
        [SerializeField] private GameObject _display;
        [SerializeField] private List<SettingDisplayGroup> _optionDisplays;
        [SerializeField] private List<GameSetting> _options;
        
        private int _selectedOptionIndex;

        public void Display()
        {
            _selectedOptionIndex = 0;
            _display.SetActive(true);
            RefreshDisplay();
        }
        
        public void Hide()
        {
            _display.SetActive(false);
        }
        
        public void MoveSelection(int direction)
        {
            _selectedOptionIndex = (_selectedOptionIndex + direction + _options.Count) % _options.Count;
            RefreshDisplay();
        }
        
        public void Click()
        {
            _optionDisplays[1].ClickInput();
        }
        
        public void ClickLeft()
        {
            _optionDisplays[1].ClickLeft();
        }
        
        public void ClickRight()
        {
            _optionDisplays[1].ClickRight();
        }
        
        private void RefreshDisplay()
        {
            var previousOption = GetWrappedIndex(_selectedOptionIndex, -1);
            var nextOption = GetWrappedIndex(_selectedOptionIndex, 1);
            
            _optionDisplays[0].Show(_options[previousOption]);
            _optionDisplays[1].Show(_options[_selectedOptionIndex]);
            _optionDisplays[2].Show(_options[nextOption]);
        }

        private int GetWrappedIndex(int index, int change)
        {
            return (index + change + _options.Count) % _options.Count;
        }
    }
}