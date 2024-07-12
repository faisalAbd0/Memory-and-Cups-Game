using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time;
    public static bool timerison = false;
    public static float minutes;
    public static float seconds;
    public TMP_Text timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        timerison = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerison)
        {
            time += Time.deltaTime;
            updatetimer(time);
        }
    }
    public void updatetimer(float currenttime)
    {
        currenttime += 1;
        minutes = Mathf.FloorToInt(currenttime / 60);
        seconds = Mathf.FloorToInt(currenttime % 60);
        timer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


}