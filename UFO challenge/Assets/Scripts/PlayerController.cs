using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        SetLivesText();
    }

    void FixedUpdate ()
    {
        //movement of the player object with keyboard
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
  
        //escape key to quit game
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    //pickups inactive when collide with player and gets a point each time
    //enemy inactive when collide and player lose a life
    //end game when live is 0
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1; //track points
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1; //subtract point when hit enemy
            SetLivesText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count == 12)
        {
            transform.position = new Vector2(70.0f, 0.0f); //moving to level 2 
        }
        if (count>=20)
        {
            winText.text = "You Win!! Game created by Yi Chen";
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if(lives == 0)
        {
            Destroy(this);
            winText.text = "You lose...";
        }
    }
}
