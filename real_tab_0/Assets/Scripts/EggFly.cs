using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggFly : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float flySpeed;
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Transform>().position.y < gameObject.transform.position.y)
        {
            gameObject.transform.Translate(new Vector3(0, -flySpeed * Time.deltaTime, 0));
        }
        else
        {
            gameObject.transform.Translate(new Vector3(0, flySpeed * Time.deltaTime, 0));
        }
    }
}
