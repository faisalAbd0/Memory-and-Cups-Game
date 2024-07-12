using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class customlvl4 : MonoBehaviour
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


    //ref to the Resualt Contianer
    public GameObject resH;

    //ref to the Resualt text
    public TMP_Text res;

    public TMP_Text watt;
    public TMP_Text ratt;

    public GameObject[] cups;
    private EventTrigger[] triggers = new EventTrigger[3];

    private List<string> winWords = new List<string>() { "Fantastic!", "Superb!", "Now you’ve got it.",
                                                         "That’s it exactly.", "Congratulations!", "Wow! I’m impressed",
                                                           "Good remembering", "Outstanding", "Keep going" };

    private List<string> loseWords = new List<string>() { "give it another shot", "Keep trying", "try again" };

    private static int WAttempts = 0;
    private static int RAttempts = 0;

    public GameObject gamepanel;
    public GameObject endpanel;

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
        StartCoroutine(changeImg());
    }



    //function to the change the image
    IEnumerator changeImg()
    {
        yield return new WaitForSeconds(0.1f);
        List<historyclass> chosengame = historyqueue.hqueue.ToList();
        FirstImage.sprite = newImage;
        SecondImage.sprite = newImage;
        ThirdImage.sprite = newImage;
        ball.SetActive(false);
        StartCoroutine(CupMixer());
    }





    public IEnumerator CupMixer(float wfs = .1f)
    {

        int cupmove = 0;
        List<historyclass> chosengame = historyqueue.hqueue.ToList();
        int rsize = Random.Range(4, 11);
        while (rsize > 0)
        {
            int index = History.index;
            
            //get random Cup from 1 to 3 to swap it 

            GameObject selected = GameObject.Find("" + chosengame[index].lvl4[customlvl4mng.attempt].cupsmoved[cupmove]);
            Debug.Log("first cup" + chosengame[index].lvl4[customlvl4mng.attempt].cupsmoved[cupmove]);

            GameObject selected1 = GameObject.Find("" + chosengame[index].lvl4[customlvl4mng.attempt].cupsmoved[cupmove + 1]);
            Debug.Log("second cup" + chosengame[index].lvl4[customlvl4mng.attempt].cupsmoved[cupmove+1]);
            cupmove++;

            rsize--;

            float cup1x = selected.transform.position.x;
            float cup2x = selected1.transform.position.x;
            float des = Mathf.Abs(cup1x - cup2x);

            StartCoroutine(selected.TranslateOverTime(10, Vector3.down * 20));
            yield return new WaitForSecondsRealtime(wfs);
            StartCoroutine(selected1.TranslateOverTime(10, Vector3.up * 20));
            yield return new WaitForSecondsRealtime(wfs);
            if (cup1x < cup2x)
            {
                StartCoroutine(selected.TranslateOverTime(3, Vector3.right * des));
                yield return new WaitForSecondsRealtime(wfs);
                StartCoroutine(selected1.TranslateOverTime(3, Vector3.left * des));
                yield return new WaitForSecondsRealtime(wfs);

            }
            else
            {
                StartCoroutine(selected1.TranslateOverTime(3, Vector3.right * des));
                yield return new WaitForSecondsRealtime(wfs);
                StartCoroutine(selected.TranslateOverTime(3, Vector3.left * des));
                yield return new WaitForSecondsRealtime(wfs);

            }
            StartCoroutine(selected.TranslateOverTime(10, Vector3.up * 20));
            yield return new WaitForSecondsRealtime(wfs);
            StartCoroutine(selected1.TranslateOverTime(10, Vector3.down * 20));


            yield return new WaitForSecondsRealtime(wfs * 3);
        }

        ball.transform.position = cups[1].transform.position;
        for (int i = 0; i < cups.Length; i++)
        {
            triggers[i].enabled = true;

        }
        foreach (GameObject cup in cups)
        {
            if (cup.name == chosengame[History.index].lvl4[customlvl4mng.attempt].chosencup.ToString())
            {
                Debug.Log("chosen" + chosengame[History.index].lvl4[customlvl4mng.attempt].chosencup.ToString());
                ExecuteEvents.Execute(cup, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                yield return new WaitForSeconds(2);
            }
        }
        customlvl4mng.attempt++;
        if (customlvl4mng.attempt == chosengame[History.index].lvl4.Count)
        {
            if (chosengame[History.index].levels == 5)
            {
                nextLevel();
            }
            else
            {
                endgameplay();
            }
        }
        else
        {
            playAgain();
        }
    }

    public void Winner()
    {
        RAttempts++;
        int rand = Random.Range(0, winWords.Count - 1);//[0-6]
        res.text = $"{winWords[rand]} ";
        flipCups();
        //SceneManager.LoadScene("lvl5");
    }

    public void False1()
    {
        WAttempts++;
        int rand = Random.Range(0, loseWords.Count - 1);
        res.text = $"{loseWords[rand]} ";
        flipCups();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void False3()
    {
        WAttempts++;
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
    public void nextLevel()
    {

        SceneManager.LoadScene("customlvl5");
    }
    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void endgameplay()
    {
        gamepanel.SetActive(false);
        endpanel.SetActive(true);
    }
    public void playagain()
    {
        customlvl4mng.attempt = 0;
        customAddBox.lvl = 1;
        SceneManager.LoadScene("custom memory game");
    }
}

