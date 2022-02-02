using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float runSpeed = 15f;
    public float rollSpeed = 20f;
    public float jumpSpeed = 25f;
    public float checkRadius = .5f;
    private float moveSpeed = 10f;
    public bool isJumping = false;
    public float jumpForce = 10f;
    public float bounceForce = 5f;
    public float jumpTimer = 0;
    public float jumpTimerMax = 1f;
    public bool isGrounded = false;
    public bool isFalling = false;

    public float gravityScale = 1f;
    public float fallGravityMultiplier = 2f;

    public float rollForce = 100f;

    public float rollTimeMax = 0.25f;
    public float rollTimer = 0f;
    public bool isRolling = false;
    private float rollDirection = 0;

    public Transform myfoot;
    public Transform myHands;

    private float moveHorizontal = 0f;

    private Rigidbody2D rig;

    [SerializeField]
    private List<PlayerAnimator> anims;
    private int currPlayer = 0;
    [SerializeField]
    private List<GameObject> deathEffects;
    private PlayerSoundSystem playerSound;

    private PlayerCarry playerCarry;

    private Crate selectedCrate;

    private bool isCarrying = false;

    public bool gamePlaying = false;
    public bool gameStarting = true;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerCarry = GetComponent<PlayerCarry>();
        playerSound = GetComponent<PlayerSoundSystem>();
    }

    void Update() {
        if(gamePlaying) {
            if(isFalling || isJumping || !isGrounded)
                CheckGrounded();
            moveHorizontal = Input.GetAxis("Horizontal");

            CheckForCrate();

            if(!isRolling || selectedCrate || isCarrying) {
                
                if(Input.GetButtonDown("Fire3") && selectedCrate) {
                    playerCarry.Pickup(selectedCrate);
                    isCarrying = true;
                } else if(Input.GetButtonDown("Fire3") && isCarrying) {
                    playerCarry.Throw();
                    isCarrying = false;
                } else if(Input.GetButtonDown("Fire3") && isGrounded && !isJumping) {
                    Roll();
                }

                if(Input.GetButton("Fire3") && !isRolling) {
                    anims[currPlayer].Run();
                    moveSpeed = runSpeed;
                }

                if(Input.GetButtonUp("Fire3") && !isRolling)
                {
                    anims[currPlayer].StopRun();
                    moveSpeed = walkSpeed;
                }
            }

            if(Input.GetButtonDown("Jump") && (isGrounded || isRolling)) 
            {
                Jump();
            }

            if(isRolling) {
                rollTimer += Time.deltaTime;
                //transform.Translate(Vector2.right * moveSpeed * rollDirection * Time.deltaTime);
                
                if(rollTimer >= rollTimeMax) {
                    isRolling = false;
                    anims[currPlayer].StopRoll();
                    rollTimer = 0;
                    rollDirection = 0;
                    moveSpeed = walkSpeed;
                }
            }

            if(Input.GetButtonUp("Jump")) {
                isFalling = true;
                isJumping = false;
            } 

            if(Input.GetButton("Jump") && isJumping && !isGrounded && !isFalling) {
                if(jumpTimer <= jumpTimerMax) {
                    jumpTimer += Time.deltaTime;
                } else {
                    isFalling = true;
                    isJumping = false;
                }
            }

            if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Joystick1Button6)){
                SwitchCharacter();
            }

            if(rig.velocity.x == 0) {
                anims[currPlayer].StopRun();
                anims[currPlayer].Stop();
            }
        } else if(gameStarting) {
            if(rig.velocity.y > -0.05f) {
                anims[currPlayer].GameStarted();
                gameStarting = false;
                gamePlaying = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* if(isJumping) {
            jumpTimer += Time.deltaTime;
                transform.Translate(Vector2.up * jumpSpeed * Time.deltaTime);
            if((jumpTimer >= jumpTimerStart && isFalling) || jumpTimer >= jumpTimerCurrent) {
                isJumping = false;
                jumpTimer = 0;
                jumpTimerCurrent = jumpTimerStart;
                isFalling = false;
            }
        } */
        if(gamePlaying) {
            if(isJumping && !isFalling && jumpTimer <= jumpTimerMax) {
                //rig.velocity = Vector2.up * jumpForce;
                rig.velocity = new Vector2(rig.velocity.x, 1 * jumpForce);
            }

            if(!isRolling) {
                Move();

                if(rig.velocity.x > 0.1f || rig.velocity.x < -0.1f) {
                    anims[currPlayer].Walk();
                } else {
                    anims[currPlayer].Stop();
                }
            }

            if(rig.velocity.y < 0) {
                rig.gravityScale = gravityScale * fallGravityMultiplier;
                isFalling = true;
            } else {
                rig.gravityScale = gravityScale;
            }
        }
    }

    private void Move() 
    {
        //transform.Translate(Vector2.right * moveSpeed * moveHorizontal * Time.deltaTime);
        //rig.MovePosition((Vector2)transform.position + (Vector2.right * moveHorizontal) * moveSpeed * Time.deltaTime);
        rig.velocity = new Vector2(moveHorizontal * moveSpeed, rig.velocity.y);

        if(rig.velocity.x > 0.1f) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if(rig.velocity.x < -0.1f) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    private void Jump() 
    {
        //rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //rig.velocity = Vector2.up * jumpForce;
        //jumpTimerCurrent = jumpTimerStart;
        isJumping = true;
        isFalling = false;
        jumpTimer = 0;
        anims[currPlayer].Jump();
        playerSound.PlayJumpSound();
    }

    private void Roll() {
        isRolling = true;
        //moveSpeed = rollSpeed;
        rollDirection = Input.GetAxis("Horizontal");
        rig.AddForce(Vector2.right * rollDirection * rollForce, ForceMode2D.Impulse);
        anims[currPlayer].Roll();
        // Also Shrink the collider
    }

    public bool IsRolling() {
        return isRolling;
    }

    private void CheckGrounded() {
        LayerMask groundMask = LayerMask.GetMask("Ground", "Enemy");
        /* if(Physics2D.Raycast(myfoot.position, Vector2.down, .1f, groundMask)) {
            Debug.DrawLine(myfoot.position, myfoot.position + Vector3.down * .1f, Color.green);
            isGrounded = true;
        } else {
            Debug.DrawLine(myfoot.position, myfoot.position + Vector3.down * .1f, Color.red);
            isGrounded = false;
        } */
        Collider2D coll = Physics2D.OverlapBox(myfoot.position, new Vector2(checkRadius, checkRadius / 2), 0, groundMask);
        if(coll){
            Enemy enemy = coll.GetComponent<Enemy>();
            if(enemy) {
                enemy.Damage();
                Jump();
            }
            
            isGrounded = true;
            if(isFalling) {
                isFalling = false;
                jumpTimer = 0;
            }
        } else {
            isGrounded = false;
        }
    }

    private void CheckForCrate() {
        LayerMask groundMask = LayerMask.GetMask("Pickup");
        Collider2D coll = Physics2D.OverlapBox(myHands.position, new Vector2(checkRadius, checkRadius), 0, groundMask);
        if(coll){
            if(coll.GetComponent<Crate>()) {
                selectedCrate = coll.GetComponent<Crate>();
            }
        } else {
            selectedCrate = null;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawCube(myfoot.position, new Vector2(checkRadius, checkRadius / 2));
        Gizmos.DrawCube(myHands.position, new Vector2(checkRadius, checkRadius));
    }

    public Transform GetFoot() {
        return myfoot;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(isRolling && other.gameObject.GetComponent<Crate>()) {
            playerCarry.Pickup(other.gameObject.GetComponent<Crate>());
            isCarrying = true;
        }
    }

    public void Bounce() {
        if(Input.GetButton("Jump")) {
           Jump();
        } else {
            rig.velocity = Vector2.up * bounceForce;
            isJumping = false;
        }
    }

    public void SwitchCharacter() {
        anims[currPlayer].gameObject.SetActive(false);
        currPlayer++;

        if(currPlayer == anims.Count) {
            currPlayer = 0;
        }

        anims[currPlayer].gameObject.SetActive(true);
    }

    public void SetCharacter(int player) {
        currPlayer = player;
        anims[currPlayer].gameObject.SetActive(true);
        GetComponent<PlayerHealth>().SetDeathEffect(deathEffects[currPlayer]);
    }

    public void Won() {
        gamePlaying = false;
        GetComponent<PlayerScore>().enabled = false;
        anims[currPlayer].StopRun();
        anims[currPlayer].Walk();
        rig.velocity = new Vector2(walkSpeed, rig.velocity.y);
    }
}
