using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class Main : MonoBehaviour
{
    //ref to the first image
    public Image FirstImage;

    //ref to the second image
    public Image SecondImage;

    //ref to the third image
    public Image ThirdImage;

    // standing cup
    public Sprite newImage;

    //lying cup
    public Sprite oldImage;
    //ref to ball img
    public GameObject ball;

    //ref to the button
    public GameObject button;

    //ref to the Resualt Contianer
    public GameObject resH;

    public GameObject winHolder;
    //ref to the Resualt text
    public TMP_Text res;
    
    public TMP_Text watt;
    public TMP_Text ratt;

    public GameObject[] cups;
    private EventTrigger[] triggers=new EventTrigger[3];

    private List<string> winWords = new List<string>() { "Fantastic!", "Superb!", "Now you’ve got it.",
                                                         "That’s it exactly.", "Congratulations!", "Wow! I’m impressed",
                                                           "Good remembering", "Outstanding", "Keep going" };

    private List<string> loseWords = new List<string>() { "give it another shot", "Keep trying", "try again"};

    private static int WAttempts=0;
    private static int RAttempts=0;

    public cupclass4 cupclass = new cupclass4();
    public GameObject pause;

    // Start is called before the first frame update
    void Start()
    {
        Timer.timerison = true;
        watt.text = WAttempts.ToString();
        ratt.text = RAttempts.ToString();
        for (int i = 0; i < cups.Length; i++)
        {
            triggers[i] = cups[i].GetComponent<EventTrigger>();
            triggers[i].enabled = false;
        }
        pause.SetActive(false);
    }



    //function to the change the image
    public void changeImg()
    {
        FirstImage.sprite = newImage;
        SecondImage.sprite = newImage;
        ThirdImage.sprite = newImage;
        ball.SetActive(false);
        button.SetActive(false);
        StartCoroutine(CupMixer());
    }





    public IEnumerator CupMixer(float wfs = .3f)
    {



        int rsize = Random.Range(4, 11);
        while (rsize > 0)
        {
            //get random Cup from 1 to 3 to swap it 
            int rand = Random.Range(1, 4);

            GameObject selected = GameObject.Find("" + rand);
            cupclass.cupsmoved.Add(rand);
            int rand1;
            do
            {
                rand1 = Random.Range(1, 4);
            } while (rand1 == rand);

            GameObject selected1 = GameObject.Find("" + rand1);
            cupclass.cupsmoved.Add(rand);

            rsize--;

            float cup1x = selected.transform.position.x;
            float cup2x = selected1.transform.position.x;
            float des = Mathf.Abs(cup1x - cup2x);

            StartCoroutine(selected.TranslateOverTime(80, Vector3.down * 20));
            yield return new WaitForSecondsRealtime(wfs);
            StartCoroutine(selected1.TranslateOverTime(80, Vector3.up * 20));
            yield return new WaitForSecondsRealtime(wfs);
            if (cup1x < cup2x)
            {
                StartCoroutine(selected.TranslateOverTime(50, Vector3.right * des));
                yield return new WaitForSecondsRealtime(wfs);
                StartCoroutine(selected1.TranslateOverTime(50, Vector3.left * des));
                yield return new WaitForSecondsRealtime(wfs);

            }
            else
            {
                StartCoroutine(selected1.TranslateOverTime(50, Vector3.right * des));
                yield return new WaitForSecondsRealtime(wfs);
                StartCoroutine(selected.TranslateOverTime(50, Vector3.left * des));
                yield return new WaitForSecondsRealtime(wfs);

            }
            StartCoroutine(selected.TranslateOverTime(80, Vector3.up * 20));
            yield return new WaitForSecondsRealtime(wfs);
            StartCoroutine(selected1.TranslateOverTime(80, Vector3.down * 20));


            yield return new WaitForSecondsRealtime(wfs * 3);
        }

        ball.transform.position = cups[1].transform.position;
        for (int i = 0; i < cups.Length; i++)
        {
            triggers[i].enabled = true;

        }
        pause.SetActive(true);
    }
    
    public void Winner()
    {
        RAttempts++;
        cupclass.chosencup = 2;
        int rand = Random.Range(0, winWords.Count-1);//[0-6]
        res.text = $"{winWords[rand]} ";
        flipCups();
        winHolder.SetActive(true);
        pause.SetActive(false);
        //SceneManager.LoadScene("lvl5");
    }

    public void False1()
    {
        WAttempts++;
        cupclass.chosencup = 1;
        int rand = Random.Range(0, loseWords.Count - 1);
        res.text = $"{loseWords[rand]} ";
        flipCups();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void False3()
    {
        WAttempts++;
        cupclass.chosencup = 3;
        int rand = Random.Range(0, loseWords.Count - 1);
        res.text = $"{loseWords[rand]} ";
        flipCups();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void flipCups()
    {
        resH.SetActive(true);

        FirstImage.sprite = oldImage;
        SecondImage.sprite = oldImage;
        ThirdImage.sprite = oldImage;
        ball.SetActive(true);
        Timer.timerison = false;
     }
    public void nextLevel() {
        cuplass4list.lvl4.Add(cupclass);
        chosenplayer.chosen.lvl4.AddRange(cuplass4list.lvl4);
        savelvl();

        SceneManager.LoadScene("lvl5");
    }
    public void gamefinish()
    {
        savelvl();
        if (historyqueue.hqueue.Count > 9)
        {
            historyqueue.hqueue.Dequeue();
        }
        historyqueue.hqueue.Enqueue(currentgame.h);
    }
    public void savelvl()
    {
        currentgame.h.levels = 4;
        foreach (cupclass4 c in chosenplayer.chosen.lvl4)
        {
            currentgame.h.lvl4.Add(c);
        }
    }
    public void playAgain()
    {
        cuplass4list.lvl4.Add(cupclass);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        gamefinish();
        Application.Quit();
    }
}
public static class Movement
{
    public static IEnumerator TranslateOverTime(this GameObject g, int milliseconds, Vector3 movement)
    {
        for (int i = 0; i < milliseconds; i++)
        {
            g.transform.Translate(movement / milliseconds, Space.World);
            yield return new WaitForSecondsRealtime(.001f);
        }
    }

}
