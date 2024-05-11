using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    // Prefabs for the characters
    public GameObject PlayerOnePrefab;
    public GameObject PlayerTwoPrefab;
    public GameObject DummyPrefab;

    public GameObject Shuriken;
    public GameObject Coin;
    public GameObject Meteor;

    // Prefabs for the UI
    public HealthBar HealthBar;
    public GameObject pauseMenu;

    // Each object for each number
    public GameObject countDown;
    public GameObject numberOne;
    public GameObject numberTwo;
    public GameObject numberThree;

    // Sprite for popup when a player wins
    public GameObject P1WinsLogo;
    public GameObject P2WinsLogo;

    // Objects for the first and second round fillins
    public GameObject OneFirstRound;
    public GameObject OneSecondRound;
    public GameObject TwoFirstRound;
    public GameObject TwoSecondRound;

    // Sprites for the first and second round fillins
    public Sprite P1Fillin;
    public Sprite P2Fillin;

    // Player one and two game setup choices
    public int P1charChoice;
    public int P2charChoice;
    public string chosenGamemode;
    public string chosenMap;

    // Initial health of the players
    public int playerOneHP = 100;
    public int playerTwoHP = 100;
 
    // Is the left position occupied
    bool leftPositionOccupied;

    // Possible game states
    static public bool playerDead;
    static public bool unpausedCount;
    static public bool isFrozen;
    bool isPaused;
    static public string personHit = "";

    // Points for each player
    int P1Points;
    int P2Points;

    int P1Score;
    int P2Score;

    string P1Character;
    string P2Character;

    int roundsPlayed = 0;

    private float nextActionTime = 3.0f;
    public float period = 3f;


    // Start is called before the first frame update
    public void Start()
    {
         
    // Start the countdown and set the initial game state
    StartCoroutine(countDownTimer());
        leftPositionOccupied = false;
        isPaused = false;
        unpausedCount = false;
        chosenGamemode = MainMenu.getGamemode();
        chosenMap = MapDisplay.getMap();
        P1charChoice = CharacterManager.getP1Character();
        P2charChoice = CharacterManager.getP2Character();

        // If the mode is training, spawn the dummy and chosen character
        if (chosenGamemode == "Training") 
        {
            if (P1charChoice == 0) 
            {
                spawnSamurai();
            }

            else if (P1charChoice == 1) 
            {
                spawnKing();
            }
            
            spawnDummy();
            Debug.Log("Training has been selected");
            Debug.Log("P1: " + P1charChoice);
        }

        // If the mode is versus, spawn both characters
        else 
        {
            // Player one character choice 
            if (P1charChoice == 0) {
                spawnSamurai();
            }

            else if (P1charChoice == 1) {
                spawnKing();
            }

            // Player two character choice
            if (P2charChoice == 0) {
                spawnSamurai();
            }

            else if (P2charChoice == 1) {
                spawnKing();
            }

            Debug.Log("Versus has been selected");
        }
    }

    // Spawning in the Samurai character
    public void spawnSamurai()
    {
        GameObject Samurai = Instantiate(PlayerOnePrefab) as GameObject;
        PlayerMovement samuraiScript = Samurai.GetComponent<PlayerMovement>();
        
        // if the left side spawn is occupied, spawn the character on the right side
        if (!leftPositionOccupied) { 
            Samurai.transform.position = new Vector2(201, 291);
            leftPositionOccupied = true;
            samuraiScript.setPlayerInfo(true, "Samurai");
            Samurai.tag = "Player 1";
        }
        else {
            Samurai.transform.position = new Vector2(1050, 291);
            samuraiScript.setPlayerInfo(false, "Samurai");
            Samurai.tag = "Player 2";
        }

        Debug.Log("Samurai has been spawned");
    }

    // Spawning in the King character
    public void spawnKing()
    {
        GameObject King = Instantiate(PlayerTwoPrefab) as GameObject;
        PlayerMovement kingScript = King.GetComponent<PlayerMovement>();

        // if the left side spawn is occupied, spawn the character on the right side
        if (!leftPositionOccupied) { 
            King.transform.position = new Vector2(201, 291);
            leftPositionOccupied = true;
            kingScript.setPlayerInfo(true, "King");
            King.tag = "Player 1";
        }
        else {
            King.transform.position = new Vector2(1050, 291);
            kingScript.setPlayerInfo(false, "King");
            King.tag = "Player 2";
        }
    
        Debug.Log("King has been spawned");
    }

    // Spawning in the dummy
    public void spawnDummy()
    {
        GameObject callDummy = Instantiate(DummyPrefab) as GameObject;
        callDummy.transform.position = new Vector2(1050, 291);
        callDummy.tag = "Player 2";
        Debug.Log("Dummy has been spawned");
    }

    public void spawnShuriken()
    {
        GameObject enviorHazard = Instantiate(Shuriken) as GameObject;
        enviorHazard.transform.position = HazardStartPosition(-8);
        Debug.Log("ShurikenSpawned");

        enviorHazard.GetComponent<Rigidbody2D>().AddForce(transform.right * 400, ForceMode2D.Impulse);

    }

    public void spawnMeteor()
    {
        GameObject enviorHazard = Instantiate(Meteor) as GameObject;
        enviorHazard.transform.position = HazardStartPosition(-8);
        Debug.Log("ShurikenSpawned");

        enviorHazard.GetComponent<Rigidbody2D>().AddForce(transform.right * 400, ForceMode2D.Impulse);
    }

    public void spawnCoin()
    {
        GameObject enviorHazard = Instantiate(Coin) as GameObject;
        enviorHazard.transform.position = HazardStartPosition(-8);

        Debug.Log("ShurikenSpawned");
        
        enviorHazard.GetComponent<Rigidbody2D>().AddForce(transform.right * 400, ForceMode2D.Impulse);
    }

    // Generate a random vector but the x value is always a certain value
    public Vector2 HazardStartPosition(int x)
    {
        Vector2 randomVector = new Vector2(x, UnityEngine.Random.Range(160, 500));
        return randomVector;
    }

    // Get the pause count
    static public bool getPauseStatus()
    {
        return unpausedCount;
    }

    // Returning to the main menu through the pause menu
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Get if the game is frozen
    static public bool getPausedStatus()
    {
        return isFrozen;
    }

    // Get if a player is dead
    static public bool getPlayerStatus()
    {
        return playerDead;
    }

    // Countdown timer before the round starts
    IEnumerator countDownTimer()
    {
        isFrozen = true;
        numberThree.SetActive(true);
        yield return new WaitForSeconds(1);
        numberThree.SetActive(false);
        numberTwo.SetActive(true);
        yield return new WaitForSeconds(1);
        numberTwo.SetActive(false);
        numberOne.SetActive(true);
        yield return new WaitForSeconds(1);
        numberOne.SetActive(true);
        yield return new WaitForSeconds(1);
        numberOne.SetActive(false);
        isFrozen = false;
    }

    // Managing round wins
    public IEnumerator roundWon(int winner)
    {

        // Get current game points
        P1Points = gameProgress.roundsWon[0];
        P2Points = gameProgress.roundsWon[1];
        isFrozen = true;

        // If player one wins a round
        if (winner == 2)
        {
            Debug.Log("Check for points " + P1Points);
            if (P1Points == 0)
            {
                SpriteRenderer P1Round1 = OneFirstRound.GetComponent<SpriteRenderer>();
                P1Round1.sprite = P1Fillin;
            }
            else if (P1Points == 1)
            {
                SpriteRenderer P1Round2 = OneSecondRound.GetComponent<SpriteRenderer>();
                P1Round2.sprite = P1Fillin;
            }
            P1WinsLogo.SetActive(true);
            gameProgress.roundsWon[0] += 1;
            Debug.Log(gameProgress.roundsWon[0]);
            yield return new WaitForSeconds(3);
            P1WinsLogo.SetActive(false);
        }

        // If player two wins a round
        else if (winner == 1)
        {
            if (P2Points == 0)
            {
                SpriteRenderer P2Round1 = TwoFirstRound.GetComponent<SpriteRenderer>();
                P2Round1.sprite = P2Fillin;
            }
            else if (P2Points == 1)
            {
                SpriteRenderer P2Round2 = TwoSecondRound.GetComponent<SpriteRenderer>();
                P2Round2.sprite = P2Fillin;
            }
            P2WinsLogo.SetActive(true);
            gameProgress.roundsWon[1] += 1;
            Debug.Log(gameProgress.roundsWon[1]);
            yield return new WaitForSeconds(3);
            P2WinsLogo.SetActive(false);
        }
        roundsPlayed += 1;

        // If the game is over (2 rounds won) then load the game end scene
        if (gameProgress.roundsWon[0] == 2)
        {
            SceneManager.LoadScene("GameEnd");
        }

        else if (gameProgress.roundsWon[1] == 2)
        {
            SceneManager.LoadScene("GameEnd");
        }

        else
        {
            isFrozen = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    void Update()
    {

        // if curent scene is the game scene
        if (SceneManager.GetActiveScene().name == "Game") {
            if (Time.timeSinceLevelLoad - 5  > nextActionTime && !isFrozen)
                {
                    nextActionTime += period;
                    if (chosenMap == "Dojo")
                    {   

                        spawnShuriken();
                    }

                    else if (chosenMap == "Space")
                    {
                        spawnMeteor();
                    }

                    else
                    {
                        spawnCoin();
                    }
                }
        }
    
        //StartCoroutine(HazardCoolDown());

        // Fill in the round win sprites if the player has won a round
        if (gameProgress.roundsWon[0] == 1)
        {
            SpriteRenderer P1Round1 = OneFirstRound.GetComponent<SpriteRenderer>();
            P1Round1.sprite = P1Fillin;
        }
        if (gameProgress.roundsWon[1] == 1)
        {
            SpriteRenderer P2Round1 = TwoFirstRound.GetComponent<SpriteRenderer>();
            P2Round1.sprite = P2Fillin;
        }

        // Object references
        GameObject Player1 = GameObject.FindGameObjectWithTag("Player 1");
        GameObject Player2 = GameObject.FindGameObjectWithTag("Player 2");
        PlayerCombat PlayerOneAttack = Player1.GetComponent<PlayerCombat>();
        PlayerCombat PlayerTwoAttack = Player2.GetComponent<PlayerCombat>();
        Animator P1Animator = Player1.GetComponent<Animator>();
        Animator P2Animator = Player2.GetComponent<Animator>();

        if (playerTwoHP > 0 && playerOneHP > 0)
        {


            if (personHit != "")
            {
                Debug.Log("SSHHSHSHS");
                if (personHit == "Player 2")
                {
                    P2Animator.SetBool("hitTaken", true);
                    playerTwoHP -= 5;
                    HealthBar.set_maxhealth(playerTwoHP, "Player 2");
                    gameProgress.damageDealt[1] += PlayerMovement.damageTaken;
                    Debug.Log("SHURIKEN WENT POW");
                    Debug.Log(playerTwoHP);
                    if (playerTwoHP <= 0)
                    {
                        P2Animator.SetTrigger("passedAway");
                        playerDead = true;
                        Debug.Log("Death One");
                        StartCoroutine(roundWon(2));
                        playerDead = false;
                    }

                }
                if (personHit == "Player 1")
                {
                    playerOneHP -= 5;
                    gameProgress.damageDealt[0] += PlayerMovement.damageTaken;
                    HealthBar.set_maxhealth(playerOneHP, "Player 1");
                    Debug.Log("SHURIKEN WENT OTHER POW");
                    Debug.Log(playerOneHP);
                    if (playerOneHP <= 0)
                    {
                        P1Animator.SetTrigger("passedAway");
                        playerDead = true;
                        Debug.Log("Death 0");
                        StartCoroutine(roundWon(1));
                        playerDead = false;
                    }
                }
                personHit = "";
            }

            // If player one land a hit on player two, deal damage and update the health bar
            if (PlayerOneAttack.getIsHit())
            {
                P2Animator.SetBool("hitTaken", true);
                playerTwoHP -= PlayerMovement.damageTaken;
                HealthBar.set_maxhealth(playerTwoHP, "Player 2");
                gameProgress.damageDealt[1] += PlayerMovement.damageTaken;
                Debug.Log("Player One Hit Player Two");
                Debug.Log(playerTwoHP);
                if (playerTwoHP <= 0)
                {
                    P2Animator.SetBool("hitTaken", false);
                    P2Animator.SetTrigger("passedAway");
                    playerDead = true;
                    Debug.Log("Death Two");
                    StartCoroutine(roundWon(2));
                    playerDead = false;
                }
                if (chosenGamemode == "Training")
                {
                    Player2.GetComponent<dummyScript>().broIsDown();
                    if (playerTwoHP <= 0)
                    {
                        Player2.transform.position = new Vector2(Player2.transform.position.x, 1018);
                        playerTwoHP = 100;
                        HealthBar.set_maxhealth(playerTwoHP, "Player 2");
                    }
                }
            }

            else
            {
                P2Animator.SetBool("hitTaken", false);
                if (chosenGamemode == "Training")
                {
                    Player2.GetComponent<dummyScript>().broIsUp();
                }
            }

            // If the player lands a hit on the dummy, deal damage and update the health bar
            if (chosenGamemode != "Training")
            {
                if (PlayerTwoAttack.getIsHit())
                {
                    playerOneHP -= PlayerMovement.damageTaken;
                    gameProgress.damageDealt[0] += PlayerMovement.damageTaken;
                    HealthBar.set_maxhealth(playerOneHP, "Player 1");
                    Debug.Log("Player Two Hit Player One");
                    Debug.Log(playerOneHP);
                    if (playerOneHP <= 0)
                    {
                        P1Animator.SetTrigger("passedAway");
                        playerDead = true;

                        Debug.Log("Death Training");
                        StartCoroutine(roundWon(1));
                        playerDead = false;
                    }

                }
            }
        }

        // If the player presses the escape key, pause the game
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            if (isPaused == false)
            {
                isFrozen = true;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                //StartCoroutine(countDownTimer());
                isPaused = false;
                unpausedCount = true;
                isFrozen = false;
            }
        }

    }
}