using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardMove : MonoBehaviour
{
    public Rigidbody2D hazardRigid;
    //public Transform attackPoint;
    //public LayerMask enemyLayer;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCombat Player = gameObject.GetComponent<PlayerCombat>();
        //StartCoroutine(Player.hazardHit(attackPoint, 5, enemyLayer));
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        hazardRigid.velocity = new Vector2(13 * 40, hazardRigid.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D targetManCollided)
    {
        //GameManager gameManagerScriptthingy = GameObject.Find("spawnFighters").GetComponent<GameManager>();
        GameManager.personHit = targetManCollided.tag;
        Debug.Log(GameManager.personHit);
        Destroy(gameObject);
    }
}
