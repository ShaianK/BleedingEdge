using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionMenu : MonoBehaviour
{
    public GameObject Player2Interface;
    public GameObject DummyPreview;

    // Start is called before the first frame update
    void Start()
    {
        // Get the chosen gamemode from MainMenu
        string gamemode = MainMenu.getGamemode();
        
        // Depending on the gamemode, show the appropriate character
        // selection interface
        if (gamemode == "Training") 
        {
            Player2Interface.SetActive(false);
            DummyPreview.SetActive(true);
        }

        else if (gamemode == "Versus") 
        {
            Player2Interface.SetActive(true);
            DummyPreview.SetActive(false);
        }
        Debug.Log("CharacterSelectionMenu.cs: Start() called");
    }

    // Returns back to Main Menu
    public void BackToMainMenu() {
        Debug.Log("CharacterSelectionMenu.cs: BackToMainMenu() called");
        SceneManager.LoadScene("Menu");
    }
}
