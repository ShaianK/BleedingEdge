using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeInOverlay;
    public GameObject audioController;
    
    static string gamemode = "";

    // Audio objects
    public Slider masterVolume, musicVolume, sfxVolume;
    public TextMeshProUGUI masterVolumeText, musicVolumeText, sfxVolumeText;

    // Start is called before the first frame update
    void Start()
    {
        // Reset game progress
        gameProgress.roundsWon[0] = 0;
        gameProgress.roundsWon[1] = 0;
        gameProgress.damageDealt[0] = 0;
        gameProgress.damageDealt[1] = 0;

        // Fade in from the unity logo
        if (SceneManager.GetActiveScene().name == "Menu") 
        {
            fadeInOverlay.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
            Destroy(fadeInOverlay, 1);
        } 

        // Load audio settings
        loadSettings();
        applyAudioSettings();
    }

    // Quit Game
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    // Enter Lore Scene
    public void EnterLore() 
    {
        Debug.Log("Lore");
        SceneManager.LoadScene("Lore");
    }

    // Enter Character Selection Scene
    public void chooseGameMode(string gamemodeChoice) 
    {
        gamemode = gamemodeChoice;
        Debug.Log ("Game Mode: " + gamemode);
        SceneManager.LoadScene("GameSetup");
    }

    // Get gamemode... important for Game Setup process
    static public string getGamemode() 
    {
        return gamemode;
    }

    public void updateVolume(string volumeType) 
    {
        // update volume text based on slider change
        if (volumeType == "Master") 
        {
            masterVolumeText.text = Math.Round(masterVolume.value * 100).ToString() + "%";
        } 

        else if (volumeType == "Music") 
        {
            musicVolumeText.text = Math.Round(musicVolume.value * 100).ToString() + "%";
        } 

        else if (volumeType == "Sound Effects") 
        {
            sfxVolumeText.text = Math.Round(sfxVolume.value * 100).ToString() + "%";
        }
    }

    void loadSettings() {
        // load settings from PlayerConfig

        masterVolume.value = PlayerConfig.volumeLevels[0];
        musicVolume.value = PlayerConfig.volumeLevels[1];
        updateVolume("Master");
        updateVolume("Music");
    }

    public void applyAudioSettings() 
    {
        PlayerConfig.volumeLevels[0] = masterVolume.value;
        PlayerConfig.volumeLevels[1] = musicVolume.value;

        AudioController audioControllerScript = audioController.GetComponent<AudioController>();
        audioControllerScript.setMusicVolume(PlayerConfig.volumeLevels[0] * PlayerConfig.volumeLevels[1] * 0.1f);
        audioControllerScript.setSoundEffectVolume(masterVolume.value * sfxVolume.value * 0.1f);
    }
}
