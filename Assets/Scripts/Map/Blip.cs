using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player and enemy points on a minimap
/// </summary>
public class Blip : MonoBehaviour
{
    public Transform target;
    public bool keepInBounds = true;

    MiniMap map;
    RectTransform myRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        map = GetComponentInParent<MiniMap>();
        myRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 newPos = map.TransformPosition(target.position);

        if (keepInBounds)
            newPos = map.MoveInside(newPos);

        myRectTransform.localPosition = newPos;
    }
}
