// Jacob Faulk
// The script representing the hitbox of the enemy. Reports damage to the enemy if hit.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    // deducts (damageAmount) health from the enemy
    public void DeductHealth(int damageAmount)
    {
        transform.parent.gameObject.GetComponent<EnemyScript>().hit(damageAmount);
    }

}
