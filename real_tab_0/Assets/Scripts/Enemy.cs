using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image hpBar;
    [SerializeField] private float maxHp;
    private float hp;
    [SerializeField] private float def;
    [SerializeField] private float atk;
    [SerializeField] private float moveSpeed;
    [SerializeField] AudioSource enemyAudio;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] private int point;
    private Animator enemyAnim;
    
    void Start()
    {
        hp = maxHp;
        transform.position = spawnPos;
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.rectTransform.localScale = new Vector3(hp / maxHp, 1, 1);
        transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            hp -= PlayerPrefs.GetFloat("damage") * 100.0f / def;
            enemyAudio.time = 0.5f;
            enemyAudio.PlayOneShot(enemyHit);
        }
        else
        {
            collision.gameObject.GetComponent<PlayerController>().OnHit(atk);
        }
       
        
        if (hp <= 0)
        {
            Invoke("EnemyDie", 0.5f);
            enemyAnim.SetBool("isDead", true);
        }
        
    }
    private void EnemyDie()
    {
        GameManager.I.AddScore(point);
        Destroy(gameObject);
    }
}
