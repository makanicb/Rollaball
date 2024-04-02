using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Rigidbody of player
    private Rigidbody rb;
    //Components of player movement
    private float movementX;
    private float movementY;
    //Magnitude of player movement
    public float speed = 0.0f;
    public float jump = 0.0f;
    //Number of jumps the player can make
    public int maxJumps = 0;
    private int jumps;
    //Text to display count
    public TextMeshProUGUI countText;
    //Win Text
    public GameObject winText;
    //Keep score
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        //Get the player's rigidbody
        rb = GetComponent<Rigidbody>();
        //Set initial score
        count = 0;
        SetCountText();
        //Deactivate win text
        winText.SetActive(false);
        //set the player to ungrounded
        jumps = maxJumps;
    }

    //FixedUpdate runs on a fixed interval
    private void FixedUpdate()
    {
        //Combine the movement components into a vector
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //Apply the vector to the player as a force
        rb.AddForce(movement * speed);
    }

    //Runs when OnMove is triggered by the player's InputSystem
    void OnMove (InputValue movementValue)
    {
        //Get the movement value from the InputSystem
        Vector2 movementVector = movementValue.Get<Vector2>();
        //Break the movement value down into its components
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void OnJump()
    {
        if(jumps > 0)
        {
            rb.AddForce(new Vector3(0.0f, jump), ForceMode.Impulse);
            jumps--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 7)
        {
            winText.SetActive(true);
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumps = maxJumps;
        }
    }
}
