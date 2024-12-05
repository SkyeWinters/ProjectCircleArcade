using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DamnGuys.SharedValues
{
    [CreateAssetMenu(menuName = "Custom/Character Options Pool", fileName = "SO_CharacterOptionsPool_NewPool", order = 0)]
    public class CharacterOptionsPool : ScriptableObject
    {
        [SerializeField] private Pool<SharedCharacter.Character> _characters;
        [SerializeField] private Pool<Color> _colors;
        
        public void Reset()
        {
            _characters.Reset();
            _colors.Reset();
        }
        
        public Pool<SharedCharacter.Character> Characters => _characters;
        public Pool<Color> Colors => _colors;

        [Serializable]
        public class Pool<T>
        {
            [SerializeField] private List<T> _pool;
            
            private readonly List<T> _used = new();
            
            public void Reset()
            {
                _used.Clear();
            }
            
            public int GetIndexOf(T value)
            {
                return _pool.IndexOf(value);
            }
            
            public T GetUnused()
            {
                return _pool.First(value => !_used.Contains(value));
            }

            public bool IsUsed(T value)
            {
                return _used.Contains(value);
            }
            
            public void AddUsed(T value)
            {
                if (!_used.Contains(value)) _used.Add(value);
            }
            
            public void RemoveUsed(T value)
            {
                if (_used.Contains(value)) _used.Remove(value);
            }
            
            public T GetNextUnused(T value)
            {
                RemoveUsed(value);
                
                var unused = _pool.Where(c => !_used.Contains(c)).ToList();
                var currentIndex = unused.IndexOf(value);
                var nextIndex = (currentIndex + 1) % unused.Count;
                
                AddUsed(unused[nextIndex]);
                
                return unused[nextIndex];
            }

            public T GetPreviousUnused(T value)
            {
                RemoveUsed(value);
                
                var unused = _pool.Where(c => !_used.Contains(c)).ToList();
                var currentIndex = unused.IndexOf(value);
                var nextIndex = (currentIndex - 1 + unused.Count) % unused.Count;
                
                AddUsed(unused[nextIndex]);
                
                return unused[nextIndex];
            }
            
            public List<T> PoolList => _pool;
        }
    }
}