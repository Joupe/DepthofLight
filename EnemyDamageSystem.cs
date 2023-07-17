using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSystem : MonoBehaviour
{
    public int damage; //variable for damage
    public PlayerHealth playerHealth; //connect to the players health system

    //check up if the collided target is the player
    //if true then calls up the players health systems TakeDamage function
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Hero")
    //    {
    //        playerHealth.TakeDamage(damage);
    //    }
    //}


}
