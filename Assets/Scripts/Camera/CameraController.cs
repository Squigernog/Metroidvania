using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int width = 640;
    [SerializeField] int height = 360;
    [SerializeField] bool isFullscreen = false;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(width, height, isFullscreen);
    }

    
}
