using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerText : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach(Player p in players)
        {
            p.onPlayerDeath += SetWinnerText;
        }
    }

    private void SetWinnerText(PlayerNum num)
    {
        // Alpyne Died
        if(num == PlayerNum.P2)
        {
            GetComponent<TextMeshProUGUI>().text = "Tricorna Wins!";
        }
        else if (num == PlayerNum.P1)
        {
            GetComponent<TextMeshProUGUI>().text = "Alpyne Wins!";
        }
    }
}
