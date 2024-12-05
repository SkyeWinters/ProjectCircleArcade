using System;
using DamnGuys.SharedValues;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DamnGuys.Menus
{
    [Serializable]
    public class GameSetting
    {
        public enum Type { Volume, Bool, Action, Game }
        [SerializeField] private string _title;
        
        [SerializeField] private int _gameIndex;
        [SerializeField] private Sprite _screenshot;
        
        public Type SettingType => Type.Game;
        public string Title => _title;
        public int GameIndex => _gameIndex;
        public Sprite Screenshot => _screenshot;
    }
}