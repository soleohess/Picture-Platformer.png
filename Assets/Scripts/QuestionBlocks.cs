using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class QuestionBlocks : MonoBehaviour
{
    [SerializeField] private int randomNumber;
    public GameObject coin;
    public GameObject grass;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision");
            Instantiate(coin, gameObject.transform.parent.transform.position + new Vector3(0, 1, 0), quaternion.identity, GameObject.FindWithTag("TheLevel").transform);
            Instantiate(grass, gameObject.transform.parent.transform.position, quaternion.identity, GameObject.FindWithTag("TheLevel").transform);
            Destroy(gameObject.transform.parent.gameObject);
        }
        
        
    }
}
