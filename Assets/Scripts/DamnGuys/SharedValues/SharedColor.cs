using UnityEngine;

namespace DamnGuys.SharedValues
{
    [CreateAssetMenu(menuName = "Custom/Color", fileName = "SO_SharedColor_", order = 0)]
    public class SharedColor : ScriptableObject
    {
        [Tooltip("The color tracked")]
        [SerializeField] private Color _color;
        
        public Color Color
        {
            get => _color;
            set => _color = value;
        }
        
        public static implicit operator Color(SharedColor sharedColor) => sharedColor.Color;
    }
}