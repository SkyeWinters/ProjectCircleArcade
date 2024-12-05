using System;
using UnityEngine;

namespace DamnGuys.SharedValues
{
    [CreateAssetMenu(menuName = "DamnGuys/SharedBool", fileName = "SO_SharedBool_")]
    public class SharedBool : ScriptableObject
    {
        [Tooltip("The current value of this bool.")]
        [SerializeField] private bool _value;
        
        public Action OnValueChanged;
        
        /// <summary>
        /// The current value of this bool.
        /// </summary>
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke();
            }
        }
        
        public static implicit operator bool(SharedBool sharedBool) => sharedBool.Value;
    }
}