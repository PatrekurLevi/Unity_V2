using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private float speed = 14.0f;
    private float turnSpeed = 150.0f;
    private float horizontalInput;
    private float forwardInput;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public static float live = 2;
    public Text livetext;
    public Text GameOver;
    public Text StartGame;
    public Text WonGame;
    public bool isGrounded;
    public bool isAlive;
    public Transform Target;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        //Kíkir hvort Player sé tengdur jörð
        isGrounded = true;
    }


    void Update()
    {
        if (isAlive == true)
        {

        
        // This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Player áfram
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Snúa player
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        // Leyfir Player að hoppa
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            }
        }

        //Lives
        livetext.text = "Lives: " + live;

        if (live == 0)
        {
        //Ef player hefur 0 life = Game Over
            GameOver.gameObject.SetActive(true);
            isAlive = false;
        }
        //Ef playerinn missir öll líf = Reload og aftur 2 Líf
        if (live == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            GameOver.gameObject.SetActive(false);
            SceneManager.LoadScene("SampleScene");
            live = 2;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame.gameObject.SetActive(false);
            isAlive = true;
        }
        //Ef playerinn nær 14 stigum þá vinnur hann.
        if (live == 14 )
        {
            WonGame.gameObject.SetActive(true);

        }

    }


    
    private void OnCollisionEnter(Collision collision)
    {
        //Ef playerinn rekst við hindrun þá respawnast hann.
        if (collision.gameObject.tag == "Respawn")
        {
            //Playerinn missir eitt líf
            live = live - 1;
            SceneManager.LoadScene("SampleScene");
        }
        //Ef playerinn rekst á coin fær hann stig.
        if (collision.gameObject.tag == "Coin")
        {

            live = live + 1;
        }
        //Ef playerinn fer yfir hvítu 'línuna' færist hann á næsta stig.
        if (collision.gameObject.tag == "Finish")
        {

            transform.position = Target.position;
        }


    }


}
