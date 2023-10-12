using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float gravityMod;
    public float jumpForce;
    public float moveSpeed;
    private bool isOnAir = false;
    private bool doubleJump = true;
    private Rigidbody2D playerRb;
    private float yBound = 8.0f;
    private float xBound = 13.5f;
    private float shootCount = 0.0f;
    private float shootInt = 1.5f;
    private float shootCool = 0.5f;
    private float maxHp = 100.0f;
    private float hp;
    private float hitCool = 1.0f;
    public int isMove;
    [SerializeField] private Image hpBar;
    
    public GameObject bolt;
    private AudioSource playerAudio;
    [SerializeField] private AudioClip boltSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity *= gravityMod;
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        PlayerPrefs.SetFloat("damage", 10); // 임시로 써둔 데미지
        hp = maxHp;
        transform.position = new Vector3(-12, -4, 0);
        isMove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.transform.localScale = new Vector3(hp / maxHp, 1, 1);
        shootCount += Time.deltaTime;
        if ( hitCool > 0)
        {
            hitCool -= Time.deltaTime;
        }
        else if ( hitCool < 0 )
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        {
            
        }
        if (Input.GetKeyDown(KeyCode.W) && isOnAir == false || Input.GetKeyDown(KeyCode.W) && doubleJump == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
            anim.SetBool("onAir", true);
            
            if (isOnAir == true)
            {
                doubleJump = false;

            }
            isOnAir = true;


        }
        if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);   
        }
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }


        transform.Translate(new Vector3(1, 0, 0)*Time.deltaTime*Input.GetAxis("Horizontal")*moveSpeed);
       
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed * isMove);
        

        if (Input.GetKeyDown(KeyCode.Space) && shootCount > shootCool)
        {
            Instantiate(bolt);
            anim.SetBool("isShooting", true);
            playerAudio.time = 1.2f;
            playerAudio.PlayOneShot(boltSound);
            shootCount = 0.0f;
        }
        if (anim.GetBool("isShooting")==true)
        {
            shootInt -= Time.deltaTime;
            if (shootInt < 0.0f) 
            {
                anim.SetBool("isShooting", false);
                shootInt = 1.5f;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("onAir", false);
        }
        isOnAir = false;
        doubleJump = true;
    }
    public void OnHit(float damage)
    {
        if (hitCool <0)
        {
            hp -= damage;
            playerAudio.time = 0.5f;
            playerAudio.PlayOneShot(hitSound);
            if (hp < 0)
            {
                hp = 0;
                GameManager.I.GameOver();
                Physics2D.gravity /= gravityMod;
            }
            hitCool = 1.0f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 100f / 255f);
        }
        
    }

    public void PlayerJump()
    {
        if (isOnAir == false || doubleJump == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
            anim.SetBool("onAir", true);
            if (isOnAir == true)
            {
                doubleJump = false;

            }
            isOnAir = true;
        }
    }

    public void PlayerShoot()
    {
        if (shootCount > shootCool)
        {
            Instantiate(bolt);
            anim.SetBool("isShooting", true);
            playerAudio.time = 1.2f;
            playerAudio.PlayOneShot(boltSound);
            shootCount = 0.0f;
        }
    }

    public void TriggerMove(int direction)
    {
        isMove = direction;
    }


    public void TriggerNeutral()
    {
        isMove = 0;
    }

}
