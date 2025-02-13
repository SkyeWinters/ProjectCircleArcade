using System;
using System.Collections;
using UnityEngine;

namespace DamnGuys.InputEvents
{
    public class AlignmentChecker : MonoBehaviour
    {
        [SerializeField] private GameObject _display;
        [SerializeField] private bool _requiresLandscape;

        private bool _inLandscape;

        private void Start()
        {
            Screen.autorotateToPortrait = true;
        }
    }
}