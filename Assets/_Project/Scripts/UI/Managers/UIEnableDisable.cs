using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEnableDisable : Singleton<UIEnableDisable>
{

    bool uiON = false;

    public delegate void InventoryCB();

    public InventoryCB UIOn;
    public InventoryCB UIOff;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (uiON)
            {
                foreach (GameObject g in SceneManager.GetSceneByName("Main").GetRootGameObjects())
                {
                    g.SetActive(true);
                }
                foreach (GameObject g in SceneManager.GetSceneByName("UI").GetRootGameObjects())
                {
                    g.SetActive(false);
                }
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                if (UIOff != null) UIOff();
                uiON = false;
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
                if (UIOn != null) UIOn();
                uiON = true;
            }
        }
    }
}
