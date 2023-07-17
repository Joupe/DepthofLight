using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessary when editing UI elements
using TMPro; // Necessary when using TextMeshPro

public class JKMovementScript : MonoBehaviour
{
    float horizontalInput;      // a variable for horizontal player input
    bool isLookingRight = true; // Whether the character is facing right (needed for flipping the hero)
    public float heroSpeed = 4f;       // hero speed
    public float jumpingForce = 3f;    // hero jumping force
    

    // A SerializeFields for referencing and adjusting inside Unity
    [SerializeField] private Rigidbody2D heroRigidbody; 
    [SerializeField] private Transform groundDetector;
    [SerializeField] private LayerMask zeroLayerGround;

    // For use of a capsule collider on the hero
    CapsuleCollider2D heroBodyCollider;

    public TextMeshProUGUI gameWin; // gameWin text
    public TextMeshProUGUI researchPointsCollected; // researchPoints score
    int researchPoints;

    void Start()
    {        
        heroBodyCollider = GetComponent<CapsuleCollider2D>(); // assigns the value of the heroBodyCollider upon game initiation

        researchPoints = 0;                                            // research points, collectable from in game research material pickups
        researchPointsCollected.text = researchPoints.ToString();      // researchPointsCollected -> leads to game win condition
        gameWin.gameObject.SetActive(false);

    }


    void Update()
    {
        
        
        horizontalInput = Input.GetAxisRaw("Horizontal"); // parses the horizontal input (return value) into the horizontalInput variable
                                                          // this returns a value of either -1, 0, or +1
                                                          // depending on the direction the hero is moving

        if (Input.GetButtonDown("Jump") && IsGrounded()) // If the jump button is pressed,
                                                         // and the hero character is grounded, jump is made possible
        {
            heroRigidbody.velocity = new Vector2(heroRigidbody.velocity.x, jumpingForce); // Jump occurs by setting the y component of the 
        }                                                                                 // heroRigidbody's velocity to the jumping force

        // If the jump button is released, and the player is still moving upwards,
        // the vertical velocity will be multiplied by 0.5
        // thus it is possible to jump higher by pressing the jump button longer
        // and jump lower by just tapping the jump button
        if (Input.GetButtonUp("Jump") && heroRigidbody.velocity.y > 0f)
        {
            heroRigidbody.velocity = new Vector2(heroRigidbody.velocity.x, heroRigidbody.velocity.y * 0.5f);
        }

        FlipDirection(); // inside the update method, the FlipDirection method is called
                         // the hero character thus flips direction

    }

    private void FixedUpdate()
    {
        // Sets the x component of the heroRigidbody's velocity to the horizontal input, 
        // multiplied by the hero's speed value.
        // The y speed is set to remain at the speed it is at the current moment.
        heroRigidbody.velocity = new Vector2(horizontalInput * heroSpeed, heroRigidbody.velocity.y);
    }

    // This method (Physics2D.OverlapCircle) uses 3 parameters
    // to check whether the hero character is grounded
    // the 1st parameter is the position of the groundDetector
    // the 2nd parameter is a small radius of 0.2f 
    // the 3rd parameter is the zeroLayerGround
    // this method creates an invisible circle right at the hero's feet
    // and once it collides with the zeroLayerGround, jump is made possible
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundDetector.position, 0.2f, zeroLayerGround);
    }

    // If the hero is looking right and the horizontal input is less than zero,
    // Or if the hero is not looking right, and the horizontal input is greater than 0,
    // Then the character will flip the direction that it is facing along the x axis
    private void FlipDirection()
    {
        if (isLookingRight && horizontalInput < 0f || !isLookingRight && horizontalInput > 0f)
        {
            isLookingRight = !isLookingRight;           // Sets the isLookingRight to its opposite value
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;                        // Multiplying the x component of the hero's local scale by -1
            transform.localScale = localScale;
        }
    }

    // For the scores
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "researchMaterial")
        {

            researchPoints = researchPoints + 10;
            researchPointsCollected.text = researchPoints.ToString();

            if (researchPoints == 500)
            {
                gameWin.gameObject.SetActive(true);
            }

        }

        

    }


}