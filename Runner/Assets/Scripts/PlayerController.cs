using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    Animator anim;
    private Vector3 direction;
    public float forwardSpeed;
    private int currentLane = 1;
    public float laneWidth = 4;
    public float jumpForce ;
    public float Gravity = -20;
    Vector3 newPosition;
    public Transform m_NewTransform;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        newPosition.Set(m_NewTransform.position.x-30, m_NewTransform.position.y-50, -30);
        transform.position = newPosition;
        Debug.Log(transform.position.x);
        Debug.Log(m_NewTransform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        forwardSpeed = 15 + PlayerManager.numberOfCoins;
        direction.z = forwardSpeed;
        if(controller.isGrounded)
        {
          if (Input.GetKeyDown(KeyCode.UpArrow))
          {
            direction.y= -1;
            Jump();
          }
        }
        else
        {
           direction.y += Gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
           currentLane++;
           if(currentLane == 3){
              currentLane =2;
           }
        }
          
           
        if (Input.GetKeyDown(KeyCode.LeftArrow) ){
          currentLane--; 
          if(currentLane == -1){
            currentLane = 0;
          }
        }
        
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if( currentLane == 0 ){
          targetPosition += Vector3.left * laneWidth;
        }
        else if (currentLane == 2){
          targetPosition += Vector3.right * laneWidth;
        }

        // transform.position = targetPosition;
        // controller.center = controller.center;
        if (transform.position == targetPosition)
          return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized*25*Time.deltaTime;
        if (moveDir.sqrMagnitude<diff.sqrMagnitude)
          controller.Move(moveDir);
        else
          controller.Move(diff); 
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }
    
    private void Jump()
    {
      direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
      if (hit.transform.tag == "Obstacle")
      {
        FindObjectOfType<AudioManager>().StopSound("MainTheme");
        PlayerManager.gameOver = true;
        forwardSpeed = 0;
      }
    }
}
