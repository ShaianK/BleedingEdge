using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour
{

    public Image P1image;
    public TextMeshProUGUI P1roundsWon;
    public TextMeshProUGUI P1damageDealt;

    public Image P2image;
    public TextMeshProUGUI P2roundsWon;
    public TextMeshProUGUI P2damageDealt;

    public GameObject P1WinScreen;
    public GameObject P2WinScreen;

    // get stats and set them here
    int P1roundsWonInt = 0;
    int P1damageDealtInt = 0;

    int P2roundsWonInt = 0;
    int P2damageDealtInt = 0;

    // get player 1 and 2 image

    // Start is called before the first frame update
    void Start()
    {

        // Show the correct win screen depending on who won
        if (gameProgress.roundsWon[0] == 2)
        {
            P1WinScreen.SetActive(true);
            P2WinScreen.SetActive(false);
        }

        else if (gameProgress.roundsWon[1] == 2)
        {
            P1WinScreen.SetActive(false);
            P2WinScreen.SetActive(true);
        } 

        // Get the stats
        P1roundsWonInt = gameProgress.roundsWon[0];
        P2roundsWonInt = gameProgress.roundsWon[1];
        P1damageDealtInt = gameProgress.damageDealt[1]; 
        P2damageDealtInt = gameProgress.damageDealt[0];

        // Set the text for the stats
        P1roundsWon.text = ("Rounds Won:") + P1roundsWonInt.ToString();
        P1damageDealt.text = ("Damage Dealt:") + P1damageDealtInt.ToString();

        P2roundsWon.text = ("Rounds Won:") + P2roundsWonInt.ToString();
        P2damageDealt.text = ("Damage Dealt:") + P2damageDealtInt.ToString();

        //P1image.sprite = 
        //P2image.sprite = 
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
