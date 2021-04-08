using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
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
    // Start is called before the first frame update
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
        isGrounded = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (isAlive == true)
        {

        
        // This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // We move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // We turn the vehicle
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

        //transform.Translate(Vector3.up * Time.deltaTime * speed * forwardInput);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        }

        livetext.text = "Lives: " + live;

        if (live == 0)
        {
            GameOver.gameObject.SetActive(true);
            isAlive = false;
        }

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
    }


    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            //Destroy(gameObject);
            live = live - 1;
            SceneManager.LoadScene("SampleScene");
        }

        if (collision.gameObject.tag == "Coin")
        {

            live = live + 1;
        }

        if (collision.gameObject.tag == "Finish")
        {

            transform.position = Target.position;
        }


    }


}
