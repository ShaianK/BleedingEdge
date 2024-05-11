using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D P1RigidBody;
    public Animator P1Animation;
    string Character;

    // Ground related variables
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // Attack related variables
    public Transform attackPoint;
    public LayerMask enemylayer;
    public float attackRange = 0.5f;
    static public int damageTaken;
    public bool isHit;
    public bool isAttacking;

    // Movement related variables
    int movemultiplier = 40;
    public bool playerFlipped;
    int jumps;

    // Conditionans for the player
    bool isPlayerOne;
    bool isPlayerDead;
    bool isPlayerFrozen;

    // Cooldown for attacks
    float comboTiming = 0.5f;
    float NextLight = 0;
    float NextHeavy = 0;

    // Defieing the controls for each player
    public KeyCode[] controls;
    KeyCode[] Player1Controls = { (KeyCode) System.Enum.Parse(typeof(KeyCode), "W"), 
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "A"), 
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "S"), 
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "D"), 
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "V"), 
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "B") 
                                };

    KeyCode[] Player2Controls = { (KeyCode) System.Enum.Parse(typeof(KeyCode), "UpArrow"),
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "LeftArrow"),
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "DownArrow"),
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "RightArrow"),
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "K"),
                                  (KeyCode) System.Enum.Parse(typeof(KeyCode), "L") 
                                };

    // Flip the player sprite when moving left or right
    void flipX() {
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }

    // Get information from the GameManager about which player this is
    public void setPlayerInfo (bool playerOne, string characterName) {
        isPlayerOne = playerOne;
        Character = characterName;
        Debug.Log("Player: " + Character + ", " + isPlayerOne);

        // set controls
        controls = isPlayerOne ? Player1Controls : Player2Controls;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the player to be frozen in place
        P1RigidBody.freezeRotation = true;
        playerFlipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the status of the player from the GameManager
        isPlayerDead = GameManager.getPlayerStatus();
        isPlayerFrozen = GameManager.getPausedStatus();

        if (isPlayerDead == false)
        {
            if (isPlayerFrozen == false)
            {
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

                // Moving up
                if (Input.GetKeyDown(controls[0]) == true)
                {
                    P1Animation.SetBool("isJumping", true);
                    P1Animation.SetBool("isIdle", false);

                    // Reset jumps if the player is on the ground
                    if (isGrounded == true)
                    {
                        jumps = 0;
                    }

                    // Allow the player to jump twice if have not already jumped twice in the air
                    if (jumps < 2)
                    {
                        // Move the player up
                        P1RigidBody.velocity = Vector2.up * 13 * movemultiplier;

                        // Increment the number of jumps
                        jumps++;
                    }
                }

                // Movement down
                else if (Input.GetKey(controls[2]) == true)
                {
                    if (isGrounded == false)
                    {
                        P1Animation.SetBool("isRunning", false);
                        P1Animation.SetBool("isJumping", false);
                        P1Animation.SetBool("isFalling", true);
                        P1Animation.SetBool("isIdle", false);

                        // Move the player down
                        P1RigidBody.velocity = new Vector2(P1RigidBody.velocity.x, -13 * movemultiplier);
                    }
                    else
                    {
                        P1Animation.SetBool("isFalling", false);
                        P1Animation.SetBool("isIdle", true);
                    }
                }

                // Movement left
                else if (Input.GetKey(controls[1]) == true)
                {
                    // Flip the player sprite if they are not already flipped
                    if (playerFlipped == false)
                    {
                        flipX();
                        playerFlipped = true;
                    }

                    // Set the animation to running if the player is on the ground
                    if (isGrounded)
                    {
                        P1Animation.SetBool("isRunning", true);
                        P1Animation.SetBool("inAirRunning", false);
                    }

                    // Set the animation to inAirRunning if the player is in the air
                    else
                    {
                        P1Animation.SetBool("inAirRunning", true);
                        P1Animation.SetBool("isRunning", false);
                    }

                    P1Animation.SetBool("isFalling", false);
                    P1Animation.SetBool("isIdle", false);

                    // Move the player left
                    P1RigidBody.velocity = new Vector2(-13 * movemultiplier, P1RigidBody.velocity.y);
                }

                // Movement right
                else if (Input.GetKey(controls[3]) == true)
                {
                    // Flip the player sprite if they are flipped
                    if (playerFlipped == true)
                    {
                        flipX();
                        playerFlipped = false;
                    }
                    
                    // Set the animation to running if the player is on the ground
                    if (isGrounded)
                    {
                        P1Animation.SetBool("isRunning", true);
                        P1Animation.SetBool("inAirRunning", false);
                    }

                    // Set the animation to inAirRunning if the player is in the air
                    else
                    {
                        P1Animation.SetBool("inAirRunning", true);
                        P1Animation.SetBool("isRunning", false);
                    }

                    P1Animation.SetBool("isFalling", false);
                    P1Animation.SetBool("isIdle", false);

                    // Move the player right
                    P1RigidBody.velocity = new Vector2(13 * movemultiplier, P1RigidBody.velocity.y);
                }

                // If the player is not moving in any way, return to idle animation
                else
                {
                    P1Animation.SetBool("isRunning", false);
                    P1Animation.SetBool("inAirRunning", false);
                    P1Animation.SetBool("isJumping", false);
                    P1Animation.SetBool("isFalling", false);
                    P1Animation.SetBool("isIdle", true);
                }

                // If the current time is greater than the next time the player can attack, reset the attack
                if (Time.time >= NextHeavy)
                {
                    P1Animation.SetBool("heavyCombo", false);
                }

                if (Time.time >= NextLight)
                {
                    P1Animation.SetBool("lightCombo", false);
                }

                // If the player is not attacking, allow them to attack
                if (!isAttacking)
                {
                    PlayerCombat Player = gameObject.GetComponent<PlayerCombat>();

                    // If the player is light attacking and the current time is less 
                    // than the next time the player can attack, set the animation to light combo
                    // and attack
                    if ((Input.GetKeyDown(controls[4]) == true) && (Time.time < NextLight) && isGrounded == true)
                    {
                        P1Animation.SetBool("lightCombo", true);
                        damageTaken = 8;
                        isAttacking = true;
                        StartCoroutine(Player.lightCombo(0.2f, attackPoint, attackRange, enemylayer, isHit, isAttacking));
                        Debug.Log("Light Combo");
                        isAttacking = false;
                    }

                    // If the player is light attacking, set the animation to light attack and attack
                    else if (Input.GetKeyDown(controls[4]) == true && isGrounded == true)
                    {
                        P1Animation.SetBool("attackOne", true);
                        damageTaken = 5;
                        isAttacking = true;
                        StartCoroutine(Player.AttackOne(0.3f, attackPoint, attackRange, enemylayer, isHit, isAttacking));
                        Debug.Log("Attack One");
                        isAttacking = false;
                        NextLight = Time.time + comboTiming;
                    }

                    // If the player is heavy attacking and the current time is less
                    // than the next time the player can attack, set the animation to heavy combo
                    // and attack
                    else if ((Input.GetKeyDown(controls[5]) == true) && (Time.time < NextHeavy) && isGrounded == true)
                    {
                        P1Animation.SetBool("heavyCombo", true);
                        damageTaken = 7;
                        isAttacking = true;
                        StartCoroutine(Player.heavyCombo(0.2f, attackPoint, attackRange, enemylayer, isHit, isAttacking));
                        isAttacking = false;
                    }

                    // If the player is heavy attacking, set the animation to heavy attack and attack
                    else if (Input.GetKeyDown(controls[5]) == true && isGrounded == true)
                    {
                        P1Animation.SetBool("attackTwo", true);
                        damageTaken = 8;
                        isAttacking = true;
                        StartCoroutine(Player.AttackTwo(0.3f, attackPoint, attackRange, enemylayer, isHit, isAttacking));
                        Debug.Log("Attack Two");
                        isAttacking = false;
                        NextHeavy = Time.time + comboTiming;
                    }

                    // If the player is not attacking, set all attack animations to false
                    else
                    {
                        P1Animation.SetBool("attackOne", false);
                        P1Animation.SetBool("attackTwo", false);
                        isHit = false;
                    }
                }
            }
        }
    } 
}
