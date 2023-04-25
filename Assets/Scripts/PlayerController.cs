using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;
    private float originalSpeed;
    private float speedMultiplier = 1f;

    private int desiredLane = 1; // 0 on left lane, 1 on middle lane, 2 on right lane
    public float laneDistance = 2; // distance between lanes

    public float jumpForce;
    public float gravity = -20;

    public bool grounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Animator anim;
    private bool sliding = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalSpeed = forwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!GameController.gameStart)
            return;

        // Increase speed
        // if(forwardSpeed < maxSpeed)
        //     forwardSpeed += 0.1f * Time.deltaTime;

        speedMultiplier = originalSpeed / forwardSpeed;

        anim.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        grounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);
        
        if(controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isGrounded", grounded);
                FindObjectOfType<AudioManager>().PlaySound("Jump");
                Jump();
            }

       }
       else
       {
            anim.SetBool("isGrounded", !grounded);
            direction.y += gravity * Time.deltaTime;
       }

       if(Input.GetKeyDown(KeyCode.DownArrow) && !sliding)
       {
           FindObjectOfType<AudioManager>().PlaySound("Slide");
           StartCoroutine(Slide());
       }

        // Gather inputs on which lane player should be
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if(desiredLane == 3)
                desiredLane = 2;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if(desiredLane == -1)
                desiredLane = 0;
        }

        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     GameController.paused = true;
        // }

        // Calculate where player should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        
        if(transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
        
    }

    private void FixedUpdate() 
    {
        if(!GameController.gameStart)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }    

    private void Jump()
    {
        direction.y = jumpForce;
    }
    
    private IEnumerator Slide()
    {
        sliding = true;
        anim.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.1f * speedMultiplier);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        anim.SetBool("isSliding", false);
        sliding = false;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if(hit.transform.tag == "Obstacle")
        {
            
            GameController.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
            FindObjectOfType<AudioManager>().StopSound("MainTheme");
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Coin")
        {
            forwardSpeed += 0.05f;
            FindObjectOfType<AudioManager>().PlaySound("CoinPickUp");

        }
    }
}
