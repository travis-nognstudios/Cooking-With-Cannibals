using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasketballTimer : MonoBehaviour
{

    public TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        BasketballManager.Instance.timer -= Time.deltaTime;
        timerText.text = "Time: " + BasketballManager.Instance.timer.ToString("F0");
    }
}
