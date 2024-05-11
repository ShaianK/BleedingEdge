using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{

    // List of maps
    public string[] maps = {"Dojo", "Space", "ThroneRoom"};
    
    // Current level chosen
    public static string level = "";

    // Objects of the map display screen
    public Image mapDisplay;
    public Sprite randomMapIMG;
    public Sprite DojoIMG;
    public Sprite SpaceIMG;
    public Sprite ThroneRoomIMG;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("MapDisplay.cs: Start() called");
        mapDisplay.sprite = randomMapIMG;
    }
    
    // Function called when the user chooses a map
    // Chagnes the "level" variable to the chosen map
    public void chooseMap(int mapNumber) {
        Debug.Log("MapDisplay.cs: chooseMap() called");
        
        if (mapNumber == 4) {
            // randomly choose a map
            int i = Random.Range(0, maps.Length);
            level = maps[i];
        }
        else if (mapNumber == 1) {
            level = "Dojo";
        }
        else if (mapNumber == 2) {
            level = "Space";
        }
        else if (mapNumber == 3) {
            level = "ThroneRoom";
        }

        // Change the map display
        changeDisplay(mapNumber);
        Debug.Log("MapDisplay.cs: level = " + level);
    }

    // Changes the map display to the chosen map
    void changeDisplay(int mapDisplayNumber) {
        Debug.Log("MapDisplay.cs: changeDisplay() called");
        if (mapDisplayNumber == 4) 
        {
            mapDisplay.sprite = randomMapIMG;
        }

        else if (mapDisplayNumber == 1)
        {
            mapDisplay.sprite = DojoIMG;
        }

        else if (mapDisplayNumber == 2) 
        {
            mapDisplay.sprite = SpaceIMG;
        }

        else if (mapDisplayNumber == 3) 
        {
            mapDisplay.sprite = ThroneRoomIMG;
        }
    }

    // Returns the chosen level
    static public string getMap()
    {
        return level;
    }

    // Starts the game with the chosen map
    public void startGame() {
        Debug.Log("MapDisplay.cs: startGame() called");

        // If the user has not chosen a map, choose a random map
        if (level == "") {
            Debug.Log("MapDisplay.cs: startGame() called, but level is empty");
            chooseMap(4);
        }

        SceneManager.LoadScene("game");
    }

}
