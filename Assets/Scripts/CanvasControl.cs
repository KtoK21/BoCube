using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasControl : MonoBehaviour
{
    public GameObject Panel;
    public Text TimeText;
    public Text FinishText;
    float countTime = 0;

    void Update()
    {
        ActivePanel();

        if (!StaticVariable.IsGamePaused && !StaticVariable.IsGameFinished)
            TimeCalc();

        if (StaticVariable.IsGameFinished)
            FinishText.gameObject.SetActive(true);

    }
    void ActivePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (StaticVariable.IsGamePaused)
            {
                Panel.SetActive(false);

                StaticVariable.IsGamePaused = false;
            }

            else
            {
                Panel.SetActive(true);
                StaticVariable.IsGamePaused = true;
            }
        }
    }

    void TimeCalc()
    {
        countTime += Time.deltaTime;

        int min = Mathf.FloorToInt(countTime / 60);
        float sec = countTime % 60;
        if (sec < 10)
            TimeText.text = min + ":0" + sec;
        else
            TimeText.text = min + ":" + sec;
    }
}
