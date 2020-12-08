using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConceptArtButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoToConceptArtScene);
    }

    private void GoToConceptArtScene()
    {
        GameManager.Instance.LoadConceptArtScene();
    }
}
