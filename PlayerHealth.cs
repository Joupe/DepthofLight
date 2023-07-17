using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; //maximum health is set up for the player character
    public int health; //variable player characters health, tracks players current health in the game


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; //when the game starts, characters health is set up to what ever maxHealth is
    }

    //function that enemy is going to call when colliding with the player and taking damage
    //damage value is sent from the enemy's script
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(
                false); //if the player's health is less than or equal to 0, the player character is disabled
        }
    }
}