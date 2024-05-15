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
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision");
        randomNumber = Random.Range(0, 10);
        if (randomNumber > 0)
        {
            Instantiate(coin, new Vector3(0, 2, 0), quaternion.identity, GameObject.FindWithTag("TheLevel").transform);
        }
    }
}
