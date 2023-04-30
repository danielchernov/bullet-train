using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{
    public int AmountOfSquares = 0;

    [SerializeField]
    private PlayerController _player;
    private Rigidbody2D _connectedRb;
    private Transform lastSquare;
    private Vector3 spawnPos;

    public IEnumerator AddSquare(GameObject square)
    {
        AmountOfSquares++;

        Animator squareAnimator = square.GetComponentInChildren<Animator>();
        Quaternion squareRotation = square.transform.rotation;
        _player.transform.localScale *= 1.005f;

        HingeJoint2D hingeJoint = square.AddComponent<HingeJoint2D>();

        if (AmountOfSquares == 1)
        {
            lastSquare = _player.transform;
            spawnPos = new Vector3(-1.4f, 0, 0);
        }
        else if (AmountOfSquares > 1)
        {
            lastSquare = transform.GetChild(transform.childCount - 1);
            spawnPos = new Vector3(-1.2f, 0, 0);
        }

        square.transform.parent = lastSquare.transform;
        square.transform.rotation = lastSquare.transform.rotation;
        square.transform.localPosition = spawnPos;
        square.transform.parent = transform;

        _connectedRb = lastSquare.GetComponent<Rigidbody2D>();

        hingeJoint.autoConfigureConnectedAnchor = false;
        hingeJoint.connectedBody = _connectedRb;

        if (AmountOfSquares == 1)
        {
            hingeJoint.anchor = new Vector2(0.65f, 0);
            hingeJoint.connectedAnchor = new Vector2(-0.75f, 0);
        }
        else if (AmountOfSquares > 1)
        {
            hingeJoint.anchor = new Vector2(0.6f, 0);
            hingeJoint.connectedAnchor = new Vector2(-0.6f, 0);
        }

        JointAngleLimits2D limits = hingeJoint.limits;
        limits.min =
            ((squareRotation.eulerAngles.z) - 40) - square.transform.rotation.eulerAngles.z;
        limits.max =
            ((squareRotation.eulerAngles.z) + 40) - square.transform.rotation.eulerAngles.z;
        hingeJoint.limits = limits;

        hingeJoint.enabled = true;

        square.GetComponent<BoxCollider2D>().isTrigger = false;

        yield return new WaitForSeconds(1f);

        squareAnimator.SetTrigger("isGrowing");
    }
}
