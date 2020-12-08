using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoBackToMenu);
    }

    private void GoBackToMenu()
    {
        GameManager.Instance.LoadMenuScene();
    }
}
