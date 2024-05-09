using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    bool right;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, 1.5f))
        {
            right = true;
        } else if (Physics2D.Raycast(transform.position, Vector2.right, 1.5f))
        {
            right = false;
        }

        if (right)
        {
            transform.position = transform.position + new Vector3(Time.deltaTime, 0, 0);

        } else {
            transform.position = transform.position + new Vector3(-1 * Time.deltaTime, 0, 0);
        }

        renderer.flipX = right;
    }
}
