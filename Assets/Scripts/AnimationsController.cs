using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Test
{
    public class AnimationsController : MonoBehaviour
    {
        [SerializeField]
        private AnimatedObject[] animatedObjects = Array.Empty<AnimatedObject>();

        private int _selectedAnimation;

        private const string SelectedAnimationText = "{0} / {1}";

        [SerializeField]
        private Text selectText; 
        
        private void Start()
        {
            TryLoadChildren();
        }

        private void TryLoadChildren()
        {
            var objects = this.transform.GetComponentsInChildren<AnimatedObject>();
            if (objects.Length == 0)
                return;

            animatedObjects = objects;
            UpdateSelectText();
        }
        
        public void StartAnimation()
        {
            if (animatedObjects.Length == 0)
                return;
            
            animatedObjects[_selectedAnimation].Animate();
        }
        
        public void NextAnimation()
        {
            if (_selectedAnimation + 1 >= animatedObjects.Length)
                _selectedAnimation = 0;
            else
                _selectedAnimation++;
            
            UpdateSelectText();
        }
        
        public void PreviousAnimation()
        {
            if (_selectedAnimation <= 0)
                _selectedAnimation = animatedObjects.Length - 1;
            else
                _selectedAnimation--;
            
            UpdateSelectText();
        }

        private void UpdateSelectText()
        {
            if (selectText == null)
                return;

            selectText.text = string.Format(SelectedAnimationText, _selectedAnimation + 1, animatedObjects.Length);
        }
    }
}