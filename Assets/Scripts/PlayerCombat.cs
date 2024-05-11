using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator P1Animation;

    // Update is called once per frame
    public bool isHitCombat;

    // Light attack
    public IEnumerator AttackOne(float time, Transform attackPoint, float attackRange, LayerMask enemylayer, bool isHit, bool isAttacking) {
        yield return new WaitForSeconds(time);
        Collider2D[] hitEnemies1 = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemylayer);
        foreach (Collider2D enemy in hitEnemies1)
        {
            isHit = true;
            isHitCombat = isHit;
            Debug.Log(enemy);
        }
        yield return new WaitForSeconds(0.3f);
    }

    // Light combo attack
    public IEnumerator lightCombo(float time, Transform attackPoint, float attackRange, LayerMask enemylayer, bool isHit, bool isAttacking)
    {
        Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemylayer);
        foreach (Collider2D enemy in hitEnemies2)
        {
            isHit = true;
            isHitCombat = isHit;
            Debug.Log(enemy);
        }
        yield return new WaitForSeconds(0.3f);
    }

    // Heavy attack
    public IEnumerator AttackTwo(float time, Transform attackPoint, float attackRange, LayerMask enemylayer, bool isHit, bool isAttacking)
    {
        yield return new WaitForSeconds(time);
        Collider2D[] hitEnemies1 = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemylayer);
        foreach (Collider2D enemy in hitEnemies1)
        {
            isHit = true;
            isHitCombat = isHit;
            Debug.Log("You hit" + enemy);
        }
        yield return new WaitForSeconds(0.3f);
    }

    // Heavy combo attack
    public IEnumerator heavyCombo(float time, Transform attackPoint, float attackRange, LayerMask enemylayer, bool isHit, bool isAttacking)
    {
        yield return new WaitForSeconds(time);
        Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemylayer);
        foreach (Collider2D enemy in hitEnemies2)
        {
            isHit = true;
            isHitCombat = isHit;
            Debug.Log("You hit" + enemy);
        }
        yield return new WaitForSeconds(0.3f);
    }
    public IEnumerator hazardHit(Transform attackPoint, float attackRange, LayerMask enemylayer, bool isHit, bool isAttacking)
    {
        Collider2D[] hazardStrike = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemylayer);
        foreach (Collider2D enemy in hazardStrike)
        {
            isHit = true;
            isHitCombat = isHit;
            Debug.Log(enemy);
        }
        yield return new WaitForSeconds(0.3f);
    }

    void Update()
    {
        // Reset isHitCombat to back to false
        isHitCombat = false;
        //P1Animation.SetBool("hitTaken", false);
    }

    // Get if the player is hit
    public bool getIsHit()
    {
        return isHitCombat;
    }
}
