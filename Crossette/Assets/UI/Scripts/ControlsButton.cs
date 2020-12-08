using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(GoToControlsScene);
    }

    private void GoToControlsScene()
    {
        GameManager.Instance.LoadControlsScene();
    }
}
