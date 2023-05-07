using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _WASD;

    [SerializeField]
    private GameObject _Spacebar;

    [SerializeField]
    private GameObject _Zoom;

    [SerializeField]
    private GameObject _GrabSquare;

    [SerializeField]
    private GameObject _EvadeSquare;

    [SerializeField]
    private Animator _WASDAnimator;

    [SerializeField]
    private Animator _SpacebarAnimator;

    [SerializeField]
    private Animator _ZoomAnimator;

    [SerializeField]
    private Animator _GrabSquareAnimator;

    [SerializeField]
    private Animator _EvadeSquareAnimator;

    [SerializeField]
    private SquareManager _squareManager;

    Coroutine tutorial;

    void Start()
    {
        if (_squareManager.BestScore == 0)
        {
            tutorial = StartCoroutine(TutorialRoutine(5));
        }
    }

    IEnumerator TutorialRoutine(float firstPause)
    {
        _WASD.SetActive(false);
        _Spacebar.SetActive(false);
        _Zoom.SetActive(false);
        _GrabSquare.SetActive(false);
        _EvadeSquare.SetActive(false);

        yield return new WaitForSeconds(firstPause);
        _WASD.SetActive(true);
        yield return new WaitForSeconds(5);
        _WASDAnimator.SetTrigger("isDisabled");
        yield return new WaitForSeconds(2);
        _WASD.SetActive(false);

        yield return new WaitForSeconds(2);
        _Spacebar.SetActive(true);
        yield return new WaitForSeconds(5);
        _SpacebarAnimator.SetTrigger("isDisabled");
        yield return new WaitForSeconds(2);
        _Spacebar.SetActive(false);

        yield return new WaitForSeconds(2);
        _Zoom.SetActive(true);
        yield return new WaitForSeconds(5);
        _ZoomAnimator.SetTrigger("isDisabled");
        yield return new WaitForSeconds(2);
        _Zoom.SetActive(false);

        yield return new WaitForSeconds(2);
        _GrabSquare.SetActive(true);
        yield return new WaitForSeconds(5);
        _GrabSquareAnimator.SetTrigger("isDisabled");
        yield return new WaitForSeconds(2);
        _GrabSquare.SetActive(false);

        yield return new WaitForSeconds(2);
        _EvadeSquare.SetActive(true);
        yield return new WaitForSeconds(5);
        _EvadeSquareAnimator.SetTrigger("isDisabled");
        yield return new WaitForSeconds(2);
        _EvadeSquare.SetActive(false);
    }

    public void ShowTutorial()
    {
        if (tutorial != null)
        {
            StopCoroutine(tutorial);
        }
        tutorial = StartCoroutine(TutorialRoutine(0));
    }
}
