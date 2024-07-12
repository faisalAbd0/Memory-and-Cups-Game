using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanage : MonoBehaviour
{
    public void gotonewplayer()
    {
        SceneManager.LoadScene("New player");
    }
    public void gotocurrentplayer()
    {
        SceneManager.LoadScene("Current player");
    }
    public void goback()
    {
        SceneManager.LoadScene("main menu");
    }
    public void go()
    {
        SceneManager.LoadScene("zero scene");
    }
    public void play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void showstats()
    {
        gamemng.gamefinish();
        SceneManager.LoadScene("Stats");
    }
    public void exitgame()
    {
        gamemng.gamequit();
    }
    public void showhistory()
    {
        gamemng.gamefinish();
        SceneManager.LoadScene("History");
    }
    public void playback()
    {
        customAddBox.lvl = 1;
        SceneManager.LoadScene("custom memory game");
    }
    public void exitplayback()
    {
        SceneManager.LoadScene("History");
        History.exitplayback = true;
    }
    private void OnApplicationQuit()
    {
        playerprefs1.SaveHistorydata();
        playerprefs1.SaveProfileData();
    }
    public void exitstats()
    {
        AddBox.lvl = 1;
        SceneManager.LoadScene("main menu");
    }
}
