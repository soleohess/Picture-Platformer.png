using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour

{
    // Start is called before the first frame update
    public void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        transform.SetParent(player.transform);
        transform.position = player.transform.position + new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
