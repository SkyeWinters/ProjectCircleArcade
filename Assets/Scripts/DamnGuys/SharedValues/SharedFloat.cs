using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DamnGuys.SharedValues
{
    [CreateAssetMenu(menuName = "DamnGuys/SharedFloat", fileName = "SO_SharedFloat_")]
    public class SharedFloat : ScriptableObject
    {
        [Tooltip("The current value of this float.")]
        [SerializeField] private float _value;
     
        public Action OnValueChanged;
        
        /// <summary>
        /// The current value of this float.
        /// </summary>
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke();
            }
        }
        
        [Button]
        public void BroadcastValue() => OnValueChanged?.Invoke();
        
        public static implicit operator float(SharedFloat sharedFloat) => sharedFloat.Value;
    }
}