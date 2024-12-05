using System;
using UnityEngine;

namespace DamnGuys.SharedValues
{
    [CreateAssetMenu(menuName = "DamnGuys/Difficulty Tracker", fileName = "SO_DifficultyTracker_", order = 0)]
    public class SharedDifficultyTracker : ScriptableObject
    {
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        [Tooltip("The current difficulty of the tracker.")]
        [SerializeField] private Difficulty _currentDifficulty;
        
        /// <summary>
        /// Will be invoked when the difficulty changes to a new value
        /// </summary>
        public Action<Difficulty> OnDifficultyChanged;
        
        /// <summary>
        /// The current difficulty of the tracker.
        /// </summary>
        public Difficulty CurrentDifficulty
        {
            get => _currentDifficulty;
            set
            {
                if (value == _currentDifficulty) return;
                
                _currentDifficulty = value;
                OnDifficultyChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// The number of subdivisions per beat for the current difficulty.
        /// </summary>
        public float NotesPerBeat
        {
            get
            {
                return CurrentDifficulty switch
                {
                    Difficulty.Easy => 0.5f,
                    Difficulty.Medium => 1,
                    Difficulty.Hard => 2,
                    _ => 1
                };
            }
        }
    }
}