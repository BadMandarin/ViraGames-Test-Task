using UnityEngine;

namespace DefaultNamespace.Test
{
    public class SpiralMove : AnimatedObject
    {
        public float MoveSpeed = 150;
        public float Step = 0.1f; 
        public int Branches = 10; 
        public SpiralDirection Direction = SpiralDirection.Outside;

        private Vector3 _spiralCenter = new Vector3(0, 0, 0);
        
        private float _size;
        private float _time;
        private float _maxRad;

        public enum SpiralDirection
        {
            Inside = 1,
            Outside = -1
        }

        protected override void StartAnimation()
        {
            base.StartAnimation();
            var objTransform = TargetObject.transform;
            objTransform.position = _spiralCenter;

            _size = Direction == SpiralDirection.Inside ? 0 : (Step * ((Branches - 1) * 360 / MoveSpeed)); 
            _time = 0;
            _maxRad = Branches * 360 * Mathf.Deg2Rad;
        }

        protected override bool DoAnimation(float delta)
        {
            var objTransform = TargetObject.transform;
            
            var movement = delta * MoveSpeed;
            var angle = _time * movement;
            
            if (angle >= _maxRad)
                return false;

            var position = TargetObject.transform.position;
            var newPos = new Vector3(Mathf.Sin(angle) * _size, Mathf.Cos(angle) * _size, position.z);
            objTransform.position = Vector3.MoveTowards(position, newPos, movement);

            var stepDelta = Step * delta;
            _size += (Direction == SpiralDirection.Inside ? stepDelta : -stepDelta);
            
            _time += delta;
            
            return true;
        }
    }
}