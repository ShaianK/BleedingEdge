using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    // Character database
    public CharacterDatabase characterDB;

    // Player 1
    public TextMeshProUGUI P1CharacterName;
    public Image P1CharacterImage;
    public Image P1CharacterHeadshot;

    // Player 2
    public TextMeshProUGUI P2CharacterName;
    public Image P2CharacterImage;
    public Image P2CharacterHeadshot;

    public static int selectedCharacter1 = 0;
    public static int selectedCharacter2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacter();
        Debug.Log("CharacterManager.cs: Start() called");
    }

    // Cycle to next character
    public void NextOption(int player) {
        if (player == 1) {
            selectedCharacter1++;
            if (selectedCharacter1 >= characterDB.CharacterCount) {
                selectedCharacter1 = 0;
            }
        } else if (player == 2) {
            selectedCharacter2++;
            if (selectedCharacter2 >= characterDB.CharacterCount) {
                selectedCharacter2 = 0;
            }
        }
        UpdateCharacter();
        Debug.Log("CharacterManager.cs: NextOption() called");
    }

    // Cycle to previous character
    public void PreviousOption(int player) {
        if (player == 1) {
            selectedCharacter1--;
            if (selectedCharacter1 < 0) {
                selectedCharacter1 = characterDB.CharacterCount - 1;
            }
        } else if (player == 2) {
            selectedCharacter2--;
            if (selectedCharacter2 < 0) {
                selectedCharacter2 = characterDB.CharacterCount - 1;
            }
        }
        UpdateCharacter();
        Debug.Log("CharacterManager.cs: PreviousOption() called");
    }

    public void UpdateCharacter() {
        // Set player 1 side
        Character character = characterDB.GetCharacter(selectedCharacter1);
        P1CharacterName.text = character.characterName;
        P1CharacterImage.sprite = character.characterImage;
        P1CharacterHeadshot.sprite = character.characterHeadshot;

        // Set player 2 side
        character = characterDB.GetCharacter(selectedCharacter2);
        P2CharacterName.text = character.characterName;
        P2CharacterImage.sprite = character.characterImage;
        P2CharacterHeadshot.sprite = character.characterHeadshot;
        Debug.Log("CharacterManager.cs: UpdateCharacter() called");

        Debug.Log("CharacterManager.cs: selectedCharacter1 = " + selectedCharacter1);
        Debug.Log("CharacterManager.cs: selectedCharacter2 = " + selectedCharacter2);
    }

    // Get the selected player one character
    public static int getP1Character() {
        return selectedCharacter1;
    }

    // Get the selected player two character
    public static int getP2Character() {
        return selectedCharacter2;
    }
}
