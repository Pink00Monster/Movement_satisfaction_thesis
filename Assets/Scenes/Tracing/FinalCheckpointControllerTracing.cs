using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCheckpointControllerTracing : MonoBehaviour
{
    private ScoreManagerTracing scoreManager;

    void Start()
    {
        scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManagerTracing>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scoreManager.done = true;
        }
    }
}
