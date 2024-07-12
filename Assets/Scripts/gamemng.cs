using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemng : MonoBehaviour
{
    public GameObject game;
    public GameObject statpanel;
    public GameObject pausepanel;
    public GameObject reportpanel;
    public GameObject stats_warning;
    public GameObject history_warning;
    public GameObject Gamepanel;
    public GameObject gamefinishedpanel;
    public GameObject profile_warning;

    public TMP_Text username;
    public TMP_Text score;
    public TMP_Text duration;
    public TMP_Text levels;

    public void pausegame()
    {
        game.SetActive(false);
        statpanel.SetActive(false);
        pausepanel.SetActive(true);
        Timer.timerison = false;
    }
    public void resumegame()
    {
        game.SetActive(true);
        statpanel.SetActive(true);
        pausepanel.SetActive(false);
        Timer.timerison = true;
    }
    public void backfromreport()
    {
        reportpanel.SetActive(false);
        pausepanel.SetActive(true);
    }
    public void backfromgame()
    {
        Gamepanel.SetActive(false);
        pausepanel.SetActive(true);
    }
    public static void gamequit()
    {
        gamefinish();
        Application.Quit();
    }
    public void stats_warning_no()
    {
        stats_warning.SetActive(false);
        reportpanel.SetActive(true);
    }
    public void history_warning_no()
    {
        history_warning.SetActive(false);
        reportpanel.SetActive(true);
    }
    public void newgame()
    {
        Gamepanel.SetActive(false);
        game.SetActive(true);
        statpanel.SetActive(true);
        gamefinish();
        AddBox.lvl = 1;
        SceneManager.LoadScene("SampleScene");
    }
    public void playagain()
    {
        gamefinishedpanel.SetActive(false);
        game.SetActive(true);
        statpanel.SetActive(true);
        AddBox.lvl = 1;
        SceneManager.LoadScene("SampleScene");
    }
    public static void gamefinish()
    {
        if (AddBox.lvl != 1)
        {
            if (AddBox.lvl != 4)
            {
                chosenplayer.chosen.games++;
                save(); 
                gameended();
            }
        }
    }
    public void exitgamefinished()
    {
        AddBox.lvl = 1;
        SceneManager.LoadScene("main menu");
    }
    public void Game()
    {
        Gamepanel.SetActive(true);
        pausepanel.SetActive(false);
    }
    public void Report()
    {
        reportpanel.SetActive(true);
        pausepanel.SetActive(false);
    }
    public void stats()
    {
        reportpanel.SetActive(false);
        stats_warning.SetActive(true);
    }
    public void history()
    {
        reportpanel.SetActive(false);
        history_warning.SetActive(true);
    }
    public void profile()
    {
        profile_warning.SetActive(true);
        pausepanel.SetActive(false);
    }
    public void profile_warning_no()
    {
        profile_warning.SetActive(false);
        pausepanel.SetActive(true);
    }
    public void profile_warning_yes()
    {
        gamefinish();
        AddBox.lvl = 1;
        SceneManager.LoadScene("main menu");
    }
    public static void gameended()
    {
        if (chosenplayer.chosen.score > chosenplayer.chosen.highestscore)
        {
            if (chosenplayer.chosen.highestscore == 0)
            {
                chosenplayer.chosen.lowestscore = chosenplayer.chosen.score;
            }
            chosenplayer.chosen.highestscore = chosenplayer.chosen.score;
        }
        if (chosenplayer.chosen.score < chosenplayer.chosen.lowestscore)
        {
            chosenplayer.chosen.lowestscore = chosenplayer.chosen.score;
        }
        if (chosenplayer.chosen.duration > chosenplayer.chosen.maxduration)
        {
            if (chosenplayer.chosen.minduration == 0)
            {
                chosenplayer.chosen.minduration = chosenplayer.chosen.duration;
            }
            chosenplayer.chosen.maxduration = chosenplayer.chosen.duration;
        }
        if (chosenplayer.chosen.duration < chosenplayer.chosen.minduration)
        {
            chosenplayer.chosen.minduration = chosenplayer.chosen.duration;
        }
    }
    public void gamefinished()
    {
        save();
        gamefinishedpanel.SetActive(true);
        Gamepanel.SetActive(false);
        username.text = chosenplayer.chosen.name;
        levels.text = $"{AddBox.lvl - 1}";
        if ((AddBox.lvl - 1) == 0)
        {
            score.text = "N/A";
            duration.text = "N/A";
        }
        else
        {
            gamefinish();
            score.text = chosenplayer.chosen.score.ToString();
            duration.text = $"{Math.Truncate(chosenplayer.chosen.duration)}:" +
                $"{Math.Round(chosenplayer.chosen.duration - Math.Truncate(chosenplayer.chosen.duration), 2) * 100}";
        }
    }
    public static void save()
    {
        if (historyqueue.hqueue.Count > 9)
        {
            historyqueue.hqueue.Dequeue();
        }
        historyqueue.hqueue.Enqueue(currentgame.h);
    }
}
