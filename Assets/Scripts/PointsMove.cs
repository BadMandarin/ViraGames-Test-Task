using DefaultNamespace.Test;
using UnityEngine;

public class PointsMove : AnimatedObject
{
    public Vector3 StartPoint = new Vector3(-10, 0, 0);
    public Vector3 EndPoint = new Vector3(10, 0, 0);
    public float MoveSpeed = 5;
    
    protected override void StartAnimation()
    {
        base.StartAnimation();
        TargetObject.transform.position = StartPoint;
    }

    protected override bool DoAnimation(float delta)
    {
        var objTransform = TargetObject.transform;
        if (objTransform.position == EndPoint)
            return false;
        
        var position = objTransform.position;
        position = Vector3.MoveTowards(position, EndPoint, delta * MoveSpeed);
        objTransform.position = position;
        
        return true;
    }
}
