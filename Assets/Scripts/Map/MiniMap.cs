using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform target;
    public float zoomLevel;
    public float width;
    public bool lockRotation = true;

    Vector2 xRot = Vector2.right;
    Vector2 yRot = Vector2.up;

    // Update is called once per frame
    void LateUpdate()
    {
        if(!lockRotation)
        {
            xRot = new Vector2(target.right.x, -target.right.z);
            yRot = new Vector2(target.forward.x, target.forward.z);
        }
    }

    public Vector2 TransformPosition(Vector3 pos)
    {
        Vector3 offset;
        offset = pos - target.position;
        Vector2 newPos = offset.x * xRot;
        newPos += offset.z * yRot;

        newPos *= zoomLevel;

        return newPos;
    }

    //public Vector3 TransformRotation(Vector3 rot)

    public Vector2 MoveInside(Vector2 point)
    {
        Rect mapRect = GetComponent<RectTransform>().rect;
        point = Vector2.Max(point, mapRect.min); // prevents player point from leaving top or left side of map
        point = Vector2.Min(point, mapRect.max); // prevents player point from leaving bottom or right side of map
        return point;
    }

}
