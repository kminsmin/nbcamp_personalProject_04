using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    private Vector3 spawnPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    void Start()
    {
        player = GameObject.Find("Player");
        spawnPos = player.transform.position;
        transform.position = spawnPos + new Vector3(xOffset, yOffset, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (transform.position.x > 15)
        {
            Destroy(gameObject);
        }
    }
}
