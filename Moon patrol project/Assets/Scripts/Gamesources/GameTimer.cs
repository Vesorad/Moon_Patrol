using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    [Tooltip("Game time in s")] public int time;

    bool start = true;

    void Awake()
    {
        time = 0;
        timerText.SetText(time.ToString());
    }

    public IEnumerator StartTimerCourutine()
    {
        do
        {
            timerText.SetText(time.ToString());
            time += 1;
            yield return new WaitForSeconds(1f);
        } while (start);
    }

    public void ResetTimer()
    {
        start = false;
        time = 0;
        timerText.SetText(time.ToString());
    }
    public void StartTimer()
    {
        start = true;
        StartCoroutine(StartTimerCourutine());
    }
    public void StopTimer()
    {
        start = false;
    }
}
