using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{
    public int startingHealth = 1; // The starting health of the enemy unit
    public int currentHealth; // Tracks the current health of the enemy as it takes damage
    public AudioSource audioSource; //set the audio source for the enemy taking damage;
    public AudioSource deathAudioSource; //set the audio source for the enemy dying;

    public GameObject itemDrops;//what loot to spawn

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth; // at game start, the current health is set
        // to the value of the current health
    }

    // a function that the enemy unity will call, once the gunfire projectile makes contact
    // with the 2D collider of the enemy
    // the damage quantity is retrieved via the gunfire projectile script
    public void RecieveDamage(int damageQuantity)
    {
        //Play the audio clip
        audioSource.Play();

        currentHealth -= damageQuantity;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            ItemDrop();
        }
    }

    private void OnDestroy()
    {
        deathAudioSource.Play();
    }

    //loot system for the boss to drop the Blue Esca
   private void ItemDrop()
    {
          Instantiate(itemDrops, transform.position, Quaternion.identity);
    }

}