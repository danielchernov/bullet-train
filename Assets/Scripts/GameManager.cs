using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UI_Manager _UIManager;

    [SerializeField]
    GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pauseMenu.activeSelf)
            {
                _UIManager.OpenPauseMenu();
            }
            else
            {
                _UIManager.Resume();
            }
        }
    }
}
