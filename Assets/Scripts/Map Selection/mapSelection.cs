using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapSelection : MonoBehaviour
{
    public SpriteRenderer levelIMG;

    // Map sprites
    public Sprite DojoIMG;
    public Sprite SpaceIMG;
    public Sprite ThroneRoomIMG;

    // Chosen Mmap name
    static public string mapName;

    void Start()
    {
        // Get the chosen map from MapDisplay
        mapName = MapDisplay.getMap();
        Debug.Log("mapSelection.cs: mapName = " + mapName);

        // Depending on the map name chosen, change the level sprite
        if (mapName == "Dojo")
        {
            levelIMG.sprite = DojoIMG;
        }

        else if (mapName == "Space")
        {
            levelIMG.sprite = SpaceIMG;
        }
        
        else if (mapName == "ThroneRoom")
        {
            levelIMG.sprite = ThroneRoomIMG;
        }
    }
}
