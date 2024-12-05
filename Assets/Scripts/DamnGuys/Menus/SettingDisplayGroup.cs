using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace DamnGuys.Menus
{
    public class SettingDisplayGroup : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<GameSetting.Type, SettingDisplayBase> _settingDisplays;
        [SerializeField] private SettingDisplayBase _defaultDisplay;
        
        SettingDisplayBase _currentDisplay;
        
        public void Show(GameSetting setting)
        {
            _settingDisplays.ForEach(x => x.Value.Hide());
            
            if (_settingDisplays.TryGetValue(setting.SettingType, out var display))
            {
                display.Show(setting);
                _currentDisplay = display;
            }
            else
            {
                _defaultDisplay.Show(setting);
                _currentDisplay = _defaultDisplay;
            }
        }
        
        public void ClickLeft() => _currentDisplay.OnLeft();
        public void ClickRight() => _currentDisplay.OnRight();
        public void ClickInput() => _currentDisplay.OnInput();
    }
}