using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD hud;
    
    private ExitSign sign;

    public int nextscene;
    
    public int coins;
    public int health;
    public float timer;
    
    // Start is called before the first frame update
    public void Start()
    {
        if (hud != null && hud != this)
        {
            Destroy(gameObject);
        }
        else
        {
            hud = this;
            DontDestroyOnLoad(gameObject);

            coins = 0;
            health = 5;
            timer = 60;
            sign = GameObject.FindObjectOfType<ExitSign>();

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (timer <= 0)
        {
            coins = 0;
            health = 5;
            timer = 60;
            sign.Reset();
        }
        /*if (Input.GetKeyDown(Keycode.R))
        {
            sign.Reset();
        }*/
        if (health <= 0)
        {
            coins = 0;
            health = 5;
            timer = 60;
            sign.Reset();
        }
        
        timer -= Time.deltaTime;

    }
}
