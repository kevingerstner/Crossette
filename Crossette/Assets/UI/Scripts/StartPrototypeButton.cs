using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPrototypeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoToPrototypeScene);
    }

    private void GoToPrototypeScene()
    {
        GameManager.Instance.LoadPrototypeScene();
    }
}
