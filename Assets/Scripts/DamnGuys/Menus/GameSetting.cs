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
        [SerializeField] private Type _type;
        [SerializeField] private string _title;
        
        [ShowIf("_type", Type.Volume)]
        [SerializeField] private SharedFloat _volume;
        
        [ShowIf("_type", Type.Bool)]
        [SerializeField] private SharedBool _bool;
        
        [ShowIf("_type", Type.Action)]
        [SerializeField] private UnityEvent _action;
        
        [ShowIf("_type", Type.Game)]
        [SerializeField] private int _gameIndex;
        [SerializeField] private Sprite _screenshot;
        [SerializeField] private UnityEvent<int> _switchEvent;
        
        public Type SettingType => _type;
        public string Title => _title;
        public SharedFloat Volume => _volume;
        public SharedBool Bool => _bool;
        public UnityEvent Action => _action;
        public int GameIndex => _gameIndex;
        public Sprite Screenshot => _screenshot;
        public UnityEvent<int> SwitchEvent => _switchEvent;
    }
}