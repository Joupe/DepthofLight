using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    private Rigidbody2D lootRb; //talks with the loot rigidbody
    public float dropForce = 5; //how much loot jumps in the air

    // Start is called before the first frame update
    void Start()
    {
        lootRb = GetComponent<Rigidbody2D>(); //script now knows that the lootRb refers to the loots rigidbody
        lootRb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse); //set up the jump effect, first the direction to up and with what force
        //then Impulse activates the force immediatelly
    }
}
