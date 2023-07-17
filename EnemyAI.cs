using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    public float speed; //set speed for the enemy
    public GameObject target; //set the target for the enemy
    public float minDistance; //set the minimum distance when the enemy "detects" the character and starts to chase it
    bool isFacingRight = false; //set the enemy to face right
    public AudioSource audioSource; //set the audio source for the enemy in close proximity


    // Update is called once per frame
    void Update()
    {
        var position = target.transform.position;
        var position1 = gameObject.transform.position;

        ////while the enemy is idle, it will move back and forth in a little area
        //if (Vector2.Distance(transform.position, target.transform.position) > minDistance)
        //{
        //    transform.position =
        //        Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        //}

        minDistance =
            Vector2.Distance(position1,
                position); //checks the distance between two objects and returns it in the minDistance
        Vector2 direction =
            position - position1; //set the direction for enemy to go towards with

        if (minDistance < 9)
        {
            // if the enemy is facing left and the character is to the right, the enemy will flip
            if (direction.x > 0 && !isFacingRight)
            {
                Flip();
            }
            // if the enemy is facing right and the character is to the left, the enemy will flip
            else if (direction.x < 0 && isFacingRight)
            {
                Flip();
            }

            // //if the distance is less than 8, the enemy will move towards the character
            // transform.Translate(direction * speed * Time.deltaTime);

            //move enemy towards the character
            gameObject.transform.position =
                Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
            
        }
    }

    //while the enemy is moving close to the character, the enemy will play a sound that gets louder the closer it gets based off the playersoundpref
    public void PlaySound()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }


        audioSource.volume = (PlayerPrefs.GetFloat("SFXVolume") * ((20 / minDistance) / minDistance));
    }

    public void ReduceSound()
    {
        if (audioSource.isPlaying == true)
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= 0.1f;
            }

            audioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        audioSource.Stop();
    }

    void Flip()
    {
        //flips the enemy
        isFacingRight = !isFacingRight;
        Vector3 scaler = gameObject.transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}