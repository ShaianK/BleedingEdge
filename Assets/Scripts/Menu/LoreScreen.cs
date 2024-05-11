using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoreScreen : MonoBehaviour {

    public GameObject cname;
    public GameObject lore;
    public GameObject quote;
    public Image CharacterImage;
    public Sprite samuraiImage;
    public Sprite kingImage;
    public Sprite huntressImage;

    // Text Components
    TextMeshProUGUI nameText;
    TextMeshProUGUI loreText;
    TextMeshProUGUI quoteText;

    void Start () {
        nameText = cname.GetComponent<TextMeshProUGUI>();
        loreText = lore.GetComponent<TextMeshProUGUI>();
        quoteText = quote.GetComponent<TextMeshProUGUI>();
        loadSamurai();
    }

    public void returnToMain () {
        SceneManager.LoadScene("Menu");
    }

    /*
    public void showName (string characterName, int currentNameIndex)
    {
        if ( (characterName.Length) == currentNameIndex)
        {
            
        }

        else
        {
            
            nameText.text += characterName[currentNameIndex];
            currentNameIndex += 1;

            async void Start()
            {
                await Task.Delay(1000);
            }
            
            Task.Delay(200).ContinueWith(_ =>
            {
                Debug.Log(nameText.text);
                showName(characterName, currentNameIndex);
            });
        }
    }
    */

    public void loadKing () {
        nameText.text = "King";
        //showName("King", 0);
        quoteText.text = "I will defend my kingdom and its people with all my strength and all my honor.";
        loreText.text = "The king is the ruler of a powerful and prosperous kingdom, renowned for his wisdom, strength, and courage. He is a skilled warrior and strategist, having fought in many battles and won numerous victories for his people. The king is beloved by his subjects, who see him as a fair and just leader who always puts their needs first. He fights not for personal glory, but to protect the ones he loves and ensure a brighter future for all.";
        CharacterImage.sprite = kingImage;
    }
    
    public void loadSamurai () {
        nameText.text = "Samurai";
        //showName("Samurai", 0);
        quoteText.text = "A true samurai is guided by honor and compassion, never allowing his passions to cloud his judgment.";
        loreText.text = "The samurai is a warrior of ancient Japan, trained in the way of the sword and the code of honor known as bushido. He is a master of the katana, a long, curved sword with a single edge. The samurai is a skilled and deadly fighter, able to cut down his opponents with ease. He believes that the path to enlightenment lies through discipline, duty, and self-control. Despite his warrior's spirit, the samurai is a man of peace at heart. He only fights to protect the innocent and uphold justice.";
        CharacterImage.sprite = samuraiImage;
    }
}
