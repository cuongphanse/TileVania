using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 25f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deadkick = new Vector2(0f,10f);
    Vector2 moveInput;
    Rigidbody2D myRD2D;
    Animator myAnimator;
    CapsuleCollider2D capsuleCollider2D;
    BoxCollider2D myFeetCollider2D;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float myTrongluc;
    bool isAlive =true;
    


    // Start is called before the first frame update
    void Start()
    {
        myRD2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        myTrongluc = myRD2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive){return;}
        Run();
        FlipScrite();
        ClimbLadder();
        Die();
    }
    
    void Die(){
        
        if(myRD2D.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRD2D.velocity = deadkick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        };
    }
    void OnMove(InputValue value){
        if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue value){ 
        if(!isAlive){return;}
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        if(value.isPressed){
            myRD2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    
    void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRD2D.velocity.y);
        myRD2D.velocity = playerVelocity;
        bool hasMove = Mathf.Abs(myRD2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasMove);
    }
    void FlipScrite(){
        bool hasMove = Mathf.Abs(myRD2D.velocity.x) > Mathf.Epsilon;
        if(hasMove){
            transform.localScale = new Vector2(Mathf.Sign(myRD2D.velocity.x), 1f);
        }
    }
    void ClimbLadder(){
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRD2D.gravityScale = myTrongluc;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        Vector2 playerVelocity = new Vector2(myRD2D.velocity.x, moveInput.y * climbSpeed);
        myRD2D.velocity = playerVelocity;  
        myRD2D.gravityScale = 0f;      

        bool hasClimb = Mathf.Abs(myRD2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", hasClimb);
    }
    void OnFire(InputValue value){
        if(!isAlive){return;}
        Instantiate(bullet,gun.position,transform.rotation);
    }
}
