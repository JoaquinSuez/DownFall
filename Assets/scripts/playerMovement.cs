using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // referencias
    private Rigidbody2D rb;
    // timers
    private bool startTimer;
    private float startTimerLimit = .5f;
    private float i;
    private float j;
    private float k;
    private bool coyoteTime;
    public float coyoteTimeLimit = 0.1f;
    // movimiento
    public bool wallJumping;
    public bool isWalled;
    public float wallJumpYPlus;
    public bool isGrounded;
    public bool jumpnt = true;
    public bool jumpin;
    public float jumpSpeed;
    public float movspeed;
    public float maxSpeed;
    // combate
    public bool shootin;
    public float shootinTimer;
    public float bulletKnockback;
    public bool shootable;
    public GameObject bulletPrefab;
    public Transform spawntrans;
    //
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        startTimer = true;
    }
    private void Update()
    {
        if (rb.velocity.magnitude >maxSpeed)
        {
            rb.velocity = (Vector2)Vector3.ClampMagnitude((Vector3)rb.velocity,maxSpeed);
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1);
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * movspeed,rb.velocity.y);
        if  (Input.GetKeyDown(KeyCode.Space))    
        {
            if(isGrounded||coyoteTime)
            {
                if(GameManager.instance.collider_abj)
                {
                    Jump();
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) && shootin)
        {
            shootin = false;
        }
        if (Input.GetKeyUp(KeyCode.Space) && (jumpin||wallJumping) && !shootin)
        {
            Jumpnt();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && isWalled && !wallJumping)
        {
            WallJump();
        }
        if(Input.GetKey(KeyCode.Space) && (coyoteTime||wallJumping||jumpnt) && !wallJumping && GameManager.instance.ammoCurrent>0)
        {
            Shoot();
        }
        if (coyoteTime)
        {
            i += Time.deltaTime;
            if (i>=coyoteTimeLimit)
            {
                i = 0;
                coyoteTime = false;
            }
        }
        if (startTimer)
        {   
            j += Time.deltaTime;
            if (j >= startTimerLimit)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(rb.velocity.y+0.001f, rb.velocity.y);
                startTimer = false;
                j = 0;
            }
        }
        if(shootin && GameManager.instance.ammoCurrent>0)
        {
            k+=Time.deltaTime;
            if (k>=shootinTimer)
            {
                Bullet();
                k = 0;
            }
        }
    }
    private void Bullet()
    {
        GameManager.instance.ammoCurrent--;
        rb.AddForce (new Vector2(rb.velocity.x,bulletKnockback),ForceMode2D.Impulse);
        GameManager.instance.UI.UpdateAmmoCounter();

        //Instantiate(bulletPrefab, new Vector3(spawntrans.position.x, spawntrans.position.y,1), Quaternion.identity);
        GameObject bullet = ObjectPool.objPoolInst.GetPooledObject();
        if(bullet != null)
        {
            bullet.transform.position = spawntrans.position;
            bullet.SetActive(true);
            bullet.GetComponent<bullet>().StartFunc();
        }
    }
    private void Shoot()
    {
        if(!wallJumping)
        shootin = true;
        else
        shootin = false;
    }
    private void WallJump()
    {
        rb.AddForce(new Vector2(rb.velocity.x,(jumpSpeed+-rb.velocity.y+wallJumpYPlus)), ForceMode2D.Impulse);
        wallJumping = true;
    }
    private void Jumpnt()
    {
        if(!isWalled)
        {
            wallJumping = false;
        }
        jumpin = false;
        jumpnt = true;
        rb.AddForce(new Vector2(0,-rb.velocity.y/1.3f), ForceMode2D.Impulse);
    }
    private void Jump()
    {
        jumpin = true;
        if (coyoteTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        rb.AddForce(new Vector2(0,jumpSpeed), ForceMode2D.Impulse);
    }
    private void RefreshAmmo()
    {
        GameManager.instance.ammoCurrent = GameManager.instance.ammoMax;
        GameManager.instance.UI.UpdateAmmoCounter();
    }
    private void HitGround()
    {
        RefreshAmmo();
        shootable = false;
        isGrounded = true;
        wallJumping = false;
        jumpin = false;
        jumpnt = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("suelo")&&GameManager.instance.collider_abj)
        {
            HitGround();
        }
        if(other.gameObject.CompareTag("muros"))
        {
            isWalled = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("suelo"))
        {
            isGrounded = false;
            if (!jumpin)
            {
            coyoteTime = true;
            jumpnt = true;
            }
        }
        if(other.gameObject.CompareTag("muros"))
        {
            isWalled = false;
        }
    }
    public void GoTo(Vector2 position)
    {
        this.transform.position = position;
        rb.velocity = new Vector2(rb.velocity.x,0);
    }
}