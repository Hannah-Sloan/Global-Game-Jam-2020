using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEnableDisable : Singleton<UIEnableDisable>
{

    bool uiON = false;

    public delegate void InventoryCB();

    public InventoryCB UIOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (uiON)
            {
            }
            else
            {
                foreach (GameObject g in SceneManager.GetSceneByName("Main").GetRootGameObjects())
                {
                    g.SetActive(false);
                }
                foreach (GameObject g in SceneManager.GetSceneByName("UI").GetRootGameObjects())
                {
                    g.SetActive(true);
                }
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                UIOn();
            }
        }
    }
}
