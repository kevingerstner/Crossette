using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        GameManager.Instance.onGameEnded += ShowUI;
    }

    private void ShowUI()
    {
        Invoke(nameof(EnableUI), 2.0f);
    }
    private void EnableUI()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
