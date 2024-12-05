using UnityEngine;

namespace DamnGuys.SharedValues
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MatchColor : MonoBehaviour
    {
        [Tooltip("The color to match")]
        [SerializeField] private SharedColor _color;
        
        private MeshRenderer _meshRenderer;
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        
        private void Start()
        {
            _meshRenderer.material.color = _color;
        }
    }
}