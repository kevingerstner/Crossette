using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public delegate void OnGameEnded();
    public event OnGameEnded onGameEnded;

    // Start is called before the first frame update
    void Start()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach(Player p in players)
        {
            p.onPlayerDeath += EndGame;
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void EndGame(PlayerNum player)
    {
        onGameEnded?.Invoke();
        Debug.Log("TEST");
    }

    public void LoadControlsScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadPrototypeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadConceptArtScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
