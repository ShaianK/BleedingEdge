using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarP1;
    public Gradient healthChangeP1;
    public Image fillColourP1;

    public Slider healthBarP2;
    public Gradient healthChangeP2;
    public Image fillColourP2;

    // Set the max health of the player
    public void set_maxhealth(int health, string player)
    {
        if (player == "Player 1")
        {
            Debug.Log("PLAYER 1 MAXHEALTH");
            healthBarP1.value = health;
            fillColourP1.color = healthChangeP1.Evaluate(1f);
        }

        else
        {
            Debug.Log("PLAYER 2 MAXHEALTH");
            healthBarP2.value = health;
            fillColourP2.color = healthChangeP2.Evaluate(1f);
        }
            
    }

    // Set the health bar of the player
    public void set_health(int health, string player)
    {
        if (player == "Player 1")
        {
            Debug.Log("PLAYER 1 SETHEALTH");
            healthBarP1.value = health;
            fillColourP1.color = healthChangeP1.Evaluate(healthBarP1.normalizedValue);
        }

        else
        {
            Debug.Log("PLAYER 2 SETHEALTH");
            healthBarP2.value = health;
            fillColourP2.color = healthChangeP2.Evaluate(healthBarP2.normalizedValue);
        }
    }
}
