using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl5mng : MonoBehaviour
{
    public GameObject gamepanel;
    public GameObject pausepanel;
    public GameObject gamepausepanel;
    public GameObject reportpanel;
    public GameObject gamefinishedpanel;
    public GameObject profile_warning;
    public GameObject stats_warning;
    public GameObject history_warning;

    public TMP_Text username;
    public TMP_Text score;
    public TMP_Text duration;
    public TMP_Text levels;

    public Timer timer;

    public void pausegame()
    {
        gamepanel.SetActive(false);
        pausepanel.SetActive(true);
        Timer.timerison = false;
    }
    public void resumegame()
    {
        gamepanel.SetActive(true);
        pausepanel.SetActive(false);
        Timer.timerison = true;
    }
    public void exitgame()
    {
        save();
        Application.Quit();
    }
    public void gotogamepausepanel()
    {
        gamepausepanel.SetActive(true);
        pausepanel.SetActive(false);
    }
    public void newgame()
    {
        save();
        AddBox.lvl = 1;
        gamepausepanel.SetActive(false);
        gamepanel.SetActive(true);
        SceneManager.LoadScene("SampleScene");
    }
    public void endgame()
    {
        save();
        gamepausepanel.SetActive(false);
        gamefinishedpanel.SetActive(true);
        username.text = chosenplayer.chosen.name;
        levels.text = "4";
        score.text = chosenplayer.chosen.score.ToString();
        duration.text = $"{System.Math.Truncate(chosenplayer.chosen.duration)}:" +
                $"{System.Math.Round(chosenplayer.chosen.duration - System.Math.Truncate(chosenplayer.chosen.duration), 2) * 100}";
    }
    public void backfromgamepause()
    {
        gamepausepanel.SetActive(false);
        pausepanel.SetActive(true);
    }
    public void gotoprofilepanel()
    {
        pausepanel.SetActive(false);
        profile_warning.SetActive(true);
    }
    public void profile_warning_yes()
    {
        save();
        AddBox.lvl = 1;
        profile_warning.SetActive(false);
        SceneManager.LoadScene("main menu");
    }
    public void profile_warning_no()
    {
        profile_warning.SetActive(false);
        pausepanel.SetActive(true);
    }
    public void gotoreportpanel()
    {
        reportpanel.SetActive(true);
        pausepanel.SetActive(false);
    }
    public void gotostats()
    {
        save();
        stats_warning.SetActive(true);
        reportpanel.SetActive(false);
    }
    public void stats_warning_yes()
    {
        save();
        stats_warning.SetActive(false);
        SceneManager.LoadScene("Stats");
    }
    public void stats_warning_no()
    {
        stats_warning.SetActive(false);
        reportpanel.SetActive(true);
    }
    public void gotohistory()
    {
        history_warning.SetActive(true);
        reportpanel.SetActive(false);
    }
    public void history_warning_yes()
    {
        save();
        history_warning.SetActive(false);
        SceneManager.LoadScene("History");
    }
    public void history_warning_no()
    {
        history_warning.SetActive(false);
        reportpanel.SetActive(true);
    }
    public void backfromreport()
    {
        reportpanel.SetActive(false);
        pausepanel.SetActive(true);
    }
    public void playagain()
    {
        AddBox.lvl = 1;
        gamefinishedpanel.SetActive(false);
        gamepanel.SetActive(true);
        SceneManager.LoadScene("Sample Scene");
    }
    public void save()
    {
        if (historyqueue.hqueue.Count > 9)
        {
            historyqueue.hqueue.Dequeue();
        }
        historyqueue.hqueue.Enqueue(currentgame.h);
    }
}
