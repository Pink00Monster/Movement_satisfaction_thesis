using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;

    void Start()
    {
        Time.timeScale = 0;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        Time.timeScale = 1;
    }
}
