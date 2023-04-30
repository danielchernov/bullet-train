using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _BackgroundRenderer;
    private Color _backgroundColor1;
    private Color _backgroundColor2;

    //private bool _backgroundColorActive = false;

    public float duration = 1f;
    private float currentTime = 0;
    private float percentage = 0;

    void Start()
    {
        _backgroundColor1 = _BackgroundRenderer.color;
        _backgroundColor2 = _backgroundColor1;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        percentage = Mathf.Clamp(currentTime / duration, 0f, 1f);

        _BackgroundRenderer.color = Color.Lerp(_backgroundColor1, _backgroundColor2, percentage);

        if (percentage == 1)
        {
            _backgroundColor1 = _backgroundColor2;
            _backgroundColor2 = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                0.1f
            );

            currentTime = 0;
        }
    }
}
