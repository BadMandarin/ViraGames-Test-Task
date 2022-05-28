using UnityEngine;

namespace DefaultNamespace.Test
{
    public class CircleMove : AnimatedObject
    {
        public int CirclesAmount = 1;
        public Vector3 PivotPosition = new Vector3(0, 0 , 0);
        public float TurningSpeed = 10;
        public float Radius = 3;
        public MoveDirection Direction = MoveDirection.Right;

        private Vector3 SelectedDirection => Direction == MoveDirection.Left ? Vector3.forward : -Vector3.forward;
        private float _angle;
        private float _maxAngle;
        private readonly float _scaler = 10;
        
        public enum MoveDirection
        {
            Right,
            Left
        }

        protected override void StartAnimation()
        {
            base.StartAnimation();
            var objTransform = TargetObject.transform;
            objTransform.position = new Vector3(PivotPosition.x, PivotPosition.y + Radius, PivotPosition.z);

            _angle = 0;
            _maxAngle = 360 * CirclesAmount;
        }

        protected override bool DoAnimation(float delta)
        {
            if (_angle >= _maxAngle)
                return false;
            
            var rotate = delta * TurningSpeed * _scaler;
            TargetObject.transform.RotateAround(PivotPosition, SelectedDirection, rotate);
            _angle += rotate;

            return true;
        }
    }
}