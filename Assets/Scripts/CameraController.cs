using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerBody;
    Rigidbody2D PlayerRb;
    Vector2 playerPosition;
    Vector2 playerDirection;
    Vector3 cameraPosition;
    public float smoothSpeed;
    Vector3 velocity = Vector3.zero;
    float camSize = 0;

    void Start()
    {
        PlayerRb = PlayerBody.GetComponent<Rigidbody2D>();
        camSize = Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        playerPosition = PlayerBody.transform.position;
        playerDirection = PlayerBody.transform.right;

        cameraPosition = new Vector3(playerPosition.x, playerPosition.y, -10);

        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            cameraPosition,
            ref velocity,
            smoothSpeed
        );
        transform.position = smoothedPosition;
    }

    public void ChangeCamSize(int amountOfSquares)
    {
        if (amountOfSquares < 50)
        {
            Camera.main.orthographicSize = camSize * (1f + (amountOfSquares / 10f));
        }
        else
        {
            return;
        }
    }
}
