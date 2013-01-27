using UnityEngine;

public class WinTransitionManager : MonoBehaviour
{
    private float winCountdown = 0.0f;
    private bool didWin = false;

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name.Equals("Player"))
        {
            winCountdown = 2.0f;
            didWin = true;

        }
    }

    void Update()
    {

        if (didWin)
        {
            winCountdown -= Time.deltaTime;
            if (winCountdown <= 0.0f)
            {
                Application.LoadLevel("WinScreen");
                didWin = false;

            }

        }
    }
}


