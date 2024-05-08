using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSign : MonoBehaviour
{
    
    private HUD hud;
    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindObjectOfType<HUD>();
        hud.timer = 60;
        hud.coins = 0;
        hud.health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hud.nextscene++;
            SceneManager.LoadScene(hud.nextscene);
        }
    }
    public void Reset()
    {
        SceneManager.LoadScene(hud.nextscene);
    }
}
