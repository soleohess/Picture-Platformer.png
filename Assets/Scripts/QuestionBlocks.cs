using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class QuestionBlocks : MonoBehaviour
{
    [SerializeField] private float randomNumber;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        randomNumber = Random.Range(0, 1);
        if (randomNumber > 0)
        {
            Instantiate(GameObject.FindWithTag("Coin"));
        }
    }
}
