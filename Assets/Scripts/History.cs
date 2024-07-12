using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class History : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public Transform Name;
    public Transform date;
    public Transform duration;
    public Transform score;
    public Transform levels;

    public static int index;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public Transform movesentry;
    public Transform movescontainer;
    public Transform levelsentry;
    public Transform step;
    public Transform move;
    public Transform level;

    public static bool exitplayback;
    void Start()
    {
        entryContainer = transform.Find("historyentrycontainer");
        entryTemplate = entryContainer.Find("historyentrytemplate");

        entryTemplate.gameObject.SetActive(false);

        int templateheight = 60;
        List<historyclass> history = historyqueue.hqueue.ToList();
        for (int i = 0; i < history.Count; i++)
        {
            Transform entrytransform = Instantiate(entryTemplate, entryContainer);

            Name = entrytransform.Find("Player (1)");
            date = entrytransform.Find("Date (1)");
            duration = entrytransform.Find("Duration (1)");
            score = entrytransform.Find("Score (1)");
            levels = entrytransform.Find("Levels (1)");

            Name.GetComponent<TMP_Text>().text = history[i].name;
            date.GetComponent<TMP_Text>().text = history[i].date;
            duration.GetComponent<TMP_Text>().text = $"{Math.Truncate(history[i].totalduration)}:{Math.Round(history[i].totalduration - Math.Truncate(history[i].totalduration), 2) * 100}";
            score.GetComponent<TMP_Text>().text = history[i].score.ToString();
            levels.GetComponent<TMP_Text>().text = history[i].levels.ToString();

            RectTransform entryrecttransform = entrytransform.GetComponent<RectTransform>();
            entryrecttransform.anchoredPosition = new Vector2(15, (float)167.4 - templateheight * i);
            entrytransform.gameObject.SetActive(true);
        }
        if (exitplayback)
        {
            Debug.Log(index);
            panel1.SetActive(false);
            panel2.SetActive(true);
            exitplayback = false;
        }
    }
    public void showoptions()
    {
        GameObject pressedbutton = EventSystem.current.currentSelectedGameObject;
        index = ((int)(167.4 - pressedbutton.GetComponent<RectTransform>().anchoredPosition.y)) / 60;
        Debug.Log(pressedbutton.GetComponent<RectTransform>().anchoredPosition.y);
        Debug.Log(index);
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    public void backfromoptions()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }
    public void backfrommoves()
    {
        panel3.SetActive(false);
        panel2.SetActive(true);
    }
    public void showmoves()
    {
        panel2.SetActive(false);
        panel3.SetActive(true);

        movescontainer = panel3.transform.Find("scroll area").Find("movesContainer");
        movesentry = movescontainer.Find("movesEntry");
        levelsentry = movescontainer.Find("levelEntry");

        List<historyclass> history = historyqueue.hqueue.ToList();
        int templateheight = 60;
        int lvlcount = 0;
        int i;
        int stepcount = 1;

        movesentry.gameObject.SetActive(false);
        Debug.Log(history[index].coordinates.Count);
        for (i = 0; i < history[index].coordinates.Count; i++)
        {
            Debug.Log(i);
            if (history[index].coordinates[i].box1 != -1)
            {
                Debug.Log("box1 " + history[index].coordinates[i].box1);
                Transform entrytransform = Instantiate(movesentry, movescontainer);

                step = entrytransform.Find("step");
                move = entrytransform.Find("move");

                step.GetComponent<TMP_Text>().text = $"{stepcount++}";
                move.GetComponent<TMP_Text>().text = $"Tile {history[index].coordinates[i].box1} " +
                    $"{history[index].coordinates[i].box2}";

                RectTransform rectentrytransform = entrytransform.GetComponent<RectTransform>();
                rectentrytransform.anchoredPosition = new Vector2(-5, (float)152 - templateheight * i);
                entrytransform.gameObject.SetActive(true);
            }
            else
            {
                stepcount = 1;
                lvlcount++;
                Debug.Log("level " + lvlcount);
                if (lvlcount == 1) continue;
                Transform entrytransform = Instantiate(levelsentry, movescontainer);

                level = entrytransform.Find("level");

                level.GetComponent<TMP_Text>().text = $"level {lvlcount}";

                RectTransform rectentrytransform = entrytransform.GetComponent<RectTransform>();
                rectentrytransform.anchoredPosition = new Vector2(-5, (float)152 - templateheight * i);
                entrytransform.gameObject.SetActive(true);
            }
        }
        Debug.Log("count " + history[index].lvl4.Count);
        if (history[index].lvl4.Count > 0)
        {
            Transform entrytransform = Instantiate(levelsentry, movescontainer);

            level = entrytransform.Find("level");

            level.GetComponent<TMP_Text>().text = "level 4";

            RectTransform rectentrytransform = entrytransform.GetComponent<RectTransform>();
            rectentrytransform.anchoredPosition = new Vector2(-5, (float)152 - templateheight * i);
            entrytransform.gameObject.SetActive(true);
            i++;
        }
        for(int j = 0; j < history[index].lvl4.Count; j++)
        {
            Transform entrytransform = Instantiate(movesentry, movescontainer);

            step = entrytransform.Find("step");
            move = entrytransform.Find("move");

            step.GetComponent<TMP_Text>().text = $"{stepcount++}";
            move.GetComponent<TMP_Text>().text = $"Cup {history[index].lvl4[j].chosencup}";

            RectTransform rectentrytransform = entrytransform.GetComponent<RectTransform>();
            rectentrytransform.anchoredPosition = new Vector2(-5, (float)152 - templateheight * i);
            entrytransform.gameObject.SetActive(true);
            i++;
        }
        if (history[index].lvl5.Count > 0)
        {
            Transform entrytransform = Instantiate(levelsentry, movescontainer);

            level = entrytransform.Find("level");

            level.GetComponent<TMP_Text>().text = "level 5";

            RectTransform rectentrytransform = entrytransform.GetComponent<RectTransform>();
            rectentrytransform.anchoredPosition = new Vector2(-5, (float)152 - templateheight * i);
            entrytransform.gameObject.SetActive(true);
            i++;
        }
        for (int j = 0; j < history[index].lvl5.Count; j++)
        {
            Transform entrytransform = Instantiate(movesentry, movescontainer);

            step = entrytransform.Find("step");
            move = entrytransform.Find("move");

            step.GetComponent<TMP_Text>().text = $"{stepcount++}";
            move.GetComponent<TMP_Text>().text = $"Cup {history[index].lvl5[j].chosencup}";

            RectTransform rectentrytransform = entrytransform.GetComponent<RectTransform>();
            rectentrytransform.anchoredPosition = new Vector2(-5, (float)152 - templateheight * i);
            entrytransform.gameObject.SetActive(true);
            i++;
        }
    }
}
