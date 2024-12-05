using UnityEngine;

namespace DamnGuys.SharedValues
{
    [CreateAssetMenu(menuName = "Custom/Character", fileName = "SO_SharedCharacter_", order = 0)]
    public class SharedCharacter : ScriptableObject
    {
        public enum Character
        {
            Taylor,
            Alex,
            Jack
        }
        
        [Tooltip("The character tracked")]
        [SerializeField] private Character _character;
        
        public Character Value
        {
            get => _character;
            set => _character = value;
        }
        
        public static implicit operator Character(SharedCharacter sharedCharacter) => sharedCharacter.Value;
    }
}