using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddBox : MonoBehaviour
{
    [SerializeField]
    private GameObject box;
    // the perfab ref
    
    [SerializeField]
    private Transform puzzleField;
    // the panel ref
    // the panel have the grid layout that format the bottons

    public  int number_of_box;
    public static int lvl = 1;
    public TMP_Text lvlText;
    public GameController game;
    // Start is called before the first frame update\


    
    public void nextLevel()
    {
        game.lvlfinished();
        lvl++;
        currentgame.h.name = chosenplayer.chosen.name;
        currentgame.h.date = DateTime.Today.Date.ToString("d");
        currentgame.h.score = chosenplayer.chosen.score;
        currentgame.h.totalduration = chosenplayer.chosen.duration;
        currentgame.h.levels = lvl - 1;
        foreach (Coordinates c in chosenplayer.chosen.coordinates)
        {
            currentgame.h.coordinates.Add(c);
        }
        chosenplayer.chosen.coordinates.Clear();
        if (lvl != 4)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
        {
            chosenplayer.chosen.games++;
            gamemng.gameended();
            SceneManager.LoadScene("lvl4");
        }
    }

    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        gamemng.gamefinish();
        Application.Quit();
    }


    void Awake()
    {
        lvlText.text = lvl.ToString();

        if (lvl == 1)
        {
            
            number_of_box = 9;
        }
        else if (lvl == 2)
            number_of_box = 16;
        else if (lvl == 3)
            number_of_box = 25;

        int s = 0;
        int e = number_of_box;
        int index = 0;
        //int index = 0;
        while (s < e)
        {
            // instantiate other bottons from the
            GameObject box_copy = Instantiate(box);
            
            if ( (number_of_box == 9) && (s == 4))
            {
               
                box_copy.name = "none_box";
              
                s++;
            }
            else if ((number_of_box == 25) && (s == 12))
            {
                box_copy.name = "none_box";
                s++;
               
            }
            else
            {
                
                // given a name to each prefab to use it late...
                box_copy.name = index.ToString();
                index++;
                    s++;
                
            }

            // so all the instantiated buttons be the chiled of the puzzle Field that we made 
            box_copy.transform.SetParent(puzzleField, false);

        }



       

    }

    // Update is called once per frame
    
    void Update()
    {
        
    }
}
