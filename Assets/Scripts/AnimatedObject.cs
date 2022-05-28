using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Test
{
    public abstract class AnimatedObject : MonoBehaviour
    {
        private bool _inAnimation;
        protected GameObject TargetObject;
        
        private WaitForFixedUpdate _coroutineWait;
        private TrailRenderer _trail;
        
        protected virtual void Start()
        {
            _coroutineWait = new WaitForFixedUpdate();
            
            TargetObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            TargetObject.transform.parent = this.transform;
            
            TargetObject.transform.localScale *= 0.5f;// new Vector3(0.5f, 0.5f, 0.5f);
            
            _trail = TargetObject.AddComponent<TrailRenderer>();
            _trail.widthMultiplier = 0.1f;
            _trail.time = 5;
            _trail.material.color = Color.cyan;
            
            TargetObject.SetActive(false);
        }
        
        private IEnumerator AnimationProcess()
        {
            StartAnimation();
            
            while (DoAnimation(Time.fixedDeltaTime))
            {
                yield return _coroutineWait;
            }

            EndAnimation();
        }

        protected virtual bool DoAnimation(float delta)
        {
            return false;
        }
        
        protected virtual void StartAnimation()
        {
            _inAnimation = true;
            TargetObject.SetActive(true);
        }
        
        protected virtual void EndAnimation()
        {
            _inAnimation = false;
            _trail.Clear();
            TargetObject.SetActive(false);
        }

        public void Animate()
        {
            if (_inAnimation)
                return;
            
            StartCoroutine(AnimationProcess());
        }
    }
}