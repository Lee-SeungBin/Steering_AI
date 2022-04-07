using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Vector2 MousePosition;
    public Camera camera;
    void Awake()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = camera.ScreenToWorldPoint(MousePosition);
            this.transform.position = MousePosition;
        }
    }
}
