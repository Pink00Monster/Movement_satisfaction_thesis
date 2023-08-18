using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerTracing : MonoBehaviour
{

    public TMP_Text textScore;
    public float timer;
    public float score;
    public bool done;
    private float seconds;
    private float minutes;
    private float milliseconds;
    private float oldTime;

    // Start is called before the first frame update
    void Start()
    {
        done = false;
        score = 0f;
        timer = 0f;
        textScore.text = timer.ToString() + "\nScore: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            timer += Time.deltaTime;
            milliseconds = (int)((timer * 1000) % 1000);
            seconds = timer % 60;
            minutes = timer / 60;
            textScore.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000") + "\nScore: " + score.ToString();
            oldTime = timer;
        }
    }
}
