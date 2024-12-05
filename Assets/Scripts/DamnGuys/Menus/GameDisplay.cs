using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DamnGuys.Menus
{
    public class GameDisplay : SettingDisplayBase
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _screenshot;

        private int _gameIndex;
        private UnityEvent<int> _onSelect;

        public override void Show(GameSetting setting)
        {
            _titleText.text = setting.Title;
            _gameIndex = setting.GameIndex;
            _screenshot.sprite = setting.Screenshot;
            _onSelect = setting.SwitchEvent;
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            _titleText.text = "";
            gameObject.SetActive(false);
        }

        public override void OnInput()
        {
            _onSelect?.Invoke(_gameIndex);
        }
    }
}