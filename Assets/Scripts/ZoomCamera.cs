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

    [SerializeField]
    private Transform _zoomBar;

    void Start()
    {
        camSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        mouseInput = Input.GetAxis("Zoom");
        if (mouseInput != 0)
        {
            CamZoom();
        }
    }

    public void CamZoom()
    {
        maxClamp = 14 + (_squareManager.AmountOfSquares / 1.5f);
        camSize = Camera.main.orthographicSize;

        camSize -= mouseInput * zoomSensitivity;
        camSize = Mathf.Clamp(camSize, minClamp, maxClamp);
        Camera.main.orthographicSize = camSize;

        ChangeZoomBar();
    }

    public void ChangeZoomBar()
    {
        Vector3 barSize = new Vector3(Mathf.InverseLerp(maxClamp, minClamp, camSize), 10, 1);
        _zoomBar.localScale = barSize;
    }
}
