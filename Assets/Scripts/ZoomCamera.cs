using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public float zoomSensitivity = 10;

    private float camSize;

    private float mouseInput;

    [SerializeField]
    private float minClamp = 3;

    [SerializeField]
    private float maxClamp = 10;

    [SerializeField]
    private SquareManager _squareManager;

    void Start()
    {
        camSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        mouseInput = Input.GetAxis("Mouse ScrollWheel");
        if (mouseInput != 0)
        {
            CamZoom();
        }
    }

    public void CamZoom()
    {
        maxClamp = 10 + (_squareManager.AmountOfSquares / 2);
        camSize = Camera.main.orthographicSize;

        camSize -= mouseInput * zoomSensitivity;
        camSize = Mathf.Clamp(camSize, minClamp, maxClamp);
        Camera.main.orthographicSize = camSize;
    }
}
