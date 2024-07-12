using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class customGameController : MonoBehaviour
{
    public List<Button> boxs = new List<Button>();
    // the list that containt all the  buttons

    [SerializeField]
    private Sprite backImg;
    // the back ground img


    [SerializeField]
    private Sprite Noneimg;
    // the X img

    public Sprite[] lvl1_sprites;
    public Sprite[] lvl2_sprites;
    public Sprite[] lvl3_sprites;

    // to put all the img in here

    public List<Sprite> gamePuzzles = new List<Sprite>();
    // to take some puzzles and put it in the btns List

    public customAddBox addBox;
    public GridLayoutGroup gridLayoutGroup;

    public bool playbackfinished;

    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessName, secondGuessName;

    public GameObject gamepanel;
    public GameObject endpanel;
    public void awakefuntion()
    {
        lvl1_sprites = Resources.LoadAll<Sprite>("GameMemroyImgs");
        lvl2_sprites = Resources.LoadAll<Sprite>("Shapes");
        lvl3_sprites = Resources.LoadAll<Sprite>("lvl3");
        System.Array.Sort(lvl3_sprites, (a, b) =>
        {
            int aNumber = int.Parse(System.IO.Path.GetFileNameWithoutExtension(a.name));
            int bNumber = int.Parse(System.IO.Path.GetFileNameWithoutExtension(b.name));
            return aNumber.CompareTo(bNumber);
        });
        addBox = GameObject.FindGameObjectWithTag("link").GetComponent<customAddBox>();
        Debug.Log("no:" + addBox.number_of_box);
        float n = Mathf.Sqrt(addBox.number_of_box);
        Debug.Log("column: " + n);
        int int_N = (int)n;
        //print(int_N);
        gridLayoutGroup.constraintCount = int_N;

        if (addBox.number_of_box == 9)
        {
            gridLayoutGroup.cellSize = new Vector2(180, 180);
            gridLayoutGroup.spacing = new Vector2(50, 50);
        }
        else if (addBox.number_of_box == 16)
        {
            gridLayoutGroup.cellSize = new Vector2(170, 170);
            gridLayoutGroup.spacing = new Vector2(35, 30);
        }
        else if (addBox.number_of_box == 25)
        {
            gridLayoutGroup.cellSize = new Vector2(150, 150);
            gridLayoutGroup.spacing = new Vector2(35, 30);
        }



    }
    // Start is called before the first frame update
    void Start()
    {
        awakefuntion();
        Debug.Log(customAddBox.lvl);
        GetBoxRef();
        ClicktheBox();
        AddGameImgs();
        Shuffle(gamePuzzles);
        StartCoroutine(showimages());
        gameGuesses = gamePuzzles.Count / 2;
        StartCoroutine(playgame());
    }

    // Update is called once per frame
    void Update()
    {

    }


    void GetBoxRef()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleBox");
        // so we give all the perbabs a tag and get all the buttons here in the list
        // to list all the perfabs first in the list -boxs-
        // and set the backgournd img the all buttons


        for (int i = 0; i < objects.Length; i++)
        {
            boxs.Add(objects[i].GetComponent<Button>());
            boxs[i].image.sprite = backImg;
        }
        for (int i = 0; i < boxs.Count; i++)
        {
            if (boxs[i].name == "none_box")
            {

                boxs[i].image.sprite = Noneimg;
                boxs[i].interactable = false;
                boxs.Remove(boxs[i]);


            }
        }


    }

    public void ClicktheBox()
    {
        //foreach (Button btn in boxs)
        //{
        //    print(btn.name);
        //}
        for (int i = 0; i < boxs.Count; i++)
        {
            int index = i;
            boxs[i].onClick.AddListener(() => TO_DO(index));
        }
    }

    void TO_DO(int index)
    {
        string name = boxs[index].gameObject.name;
        //print($"box name is {name}");

        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(name);
            //print(boxs[firstGuessIndex].name);
            boxs[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            firstGuessName = gamePuzzles[firstGuessIndex].name;
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(name);
            //print(boxs[secondGuessIndex].name);

            boxs[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            secondGuessName = gamePuzzles[secondGuessIndex].name;


            // 
            if ((firstGuessIndex != secondGuessIndex))
            {
                countGuesses++;
                StartCoroutine(CheckIfThePuzzlesMatch());
            }
            else
                secondGuess = false;
            //CheckIfThePuzzlesMatch();
            //if (firstGuessName == secondGuessName)
            //    print("Match");
            //else
            //    print("Don't Match");
        }
    }

    void AddGameImgs()
    {
        int n = boxs.Count;
        int index = 0;


        //if (n == 9)
        //{
        //    //Button temp = boxs[4];
        //    //boxs[8] = boxs[4];
        //    //boxs[8] = boxs[4];
        //    //boxs[4].image.sprite = Noneimg;
        //    boxs[8].image.sprite = Noneimg;
        //    //boxs[8].interactable = false;
        //    Button temp = boxs[4];
        //    boxs[8] = boxs[4];
        //}
        for (int i = 0; i < n; i++)
        {
            Debug.Log("No: " + addBox.number_of_box);
            if (index == n / 2)
                index = 0;
            if (addBox.number_of_box == 9)
                gamePuzzles.Add(lvl1_sprites[index]);
            else if (addBox.number_of_box == 16)
                gamePuzzles.Add(lvl2_sprites[index]);
            else if (addBox.number_of_box == 25)
                gamePuzzles.Add(lvl3_sprites[index]);

            index++;
        }

    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f); // so when u felib the second pic and its worng it doesn't
        // && boxs[int.Parse(firstGuessName)] != boxs[int.Parse(secondGuessName)])
        //int a =  boxs[int.Parse(firstGuessName)].GetHashCode();
        //int b = boxs[int.Parse(secondGuessName)].GetHashCode();
        //print(a + b);
        if (firstGuessName == secondGuessName)
        {
            //yield return new WaitForSeconds(.5f);

            boxs[firstGuessIndex].interactable = false;
            boxs[secondGuessIndex].interactable = false;


            boxs[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            boxs[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();

        }
        else
        {
            boxs[firstGuessIndex].image.sprite = backImg;
            boxs[secondGuessIndex].image.sprite = backImg;
        }

        //yield return new WaitForSeconds(.5f);
        firstGuess = secondGuess = false;

    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        //print($"countCorrectGuesses is {countCorrectGuesses} and gameGuesses is {gameGuesses} ");
        if (countCorrectGuesses == gameGuesses)
        {
            print("Game Finished");
            print($"It took u {countGuesses} guesses to finish the game");
            if (!playbackfinished)
                addBox.nextLevel();
        }
    }
    void Shuffle(List<Sprite> list)
    {
        int index = History.index;
        List<historyclass> chosengame = historyqueue.hqueue.ToList();
        List<Sprite> listduplicate = new List<Sprite>(list);
        Coordinates stoplvl = new Coordinates(-1, -1, -1, -1);
        if (customAddBox.lvl == 1)
        {
            for (int i = 1; i < chosengame[index].coordinates.Count; i++)
            {
                if (chosengame[index].coordinates[i].Equals(stoplvl)) break;
                list[chosengame[index].coordinates[i].box1] = listduplicate[chosengame[index].coordinates[i].img1 - 1];
                list[chosengame[index].coordinates[i].box2] = listduplicate[chosengame[index].coordinates[i].img2 - 1];
            }
        }
        else if (customAddBox.lvl == 2)
        {
            int level2 = chosengame[index].coordinates.IndexOf(stoplvl, 1);
            Debug.Log(level2);
            for (int i = level2 + 1; i < chosengame[index].coordinates.Count; i++)
            {
                if (chosengame[index].coordinates[i].Equals(stoplvl)) break;
                list[chosengame[index].coordinates[i].box1] = listduplicate[chosengame[index].coordinates[i].img1 - 1];
                list[chosengame[index].coordinates[i].box2] = listduplicate[chosengame[index].coordinates[i].img2 - 1];
            }
        }
        else
        {
            int level3 = chosengame[index].coordinates.IndexOf(stoplvl,
                chosengame[index].coordinates.IndexOf(stoplvl, 1) + 1);
            Debug.Log(level3);
            for (int i = level3 + 1; i < chosengame[index].coordinates.Count; i++)
            {
                Debug.Log("box1: " + chosengame[index].coordinates[i].box1 + "img1: " + chosengame[index].coordinates[i].img1 +
                    "box2: " + chosengame[index].coordinates[i].box2 + "img2: " + chosengame[index].coordinates[i].img2);
                if (chosengame[index].coordinates[i].Equals(stoplvl)) break;
                list[chosengame[index].coordinates[i].box1] = listduplicate[chosengame[index].coordinates[i].img1 - 1];
                list[chosengame[index].coordinates[i].box2] = listduplicate[chosengame[index].coordinates[i].img2 - 1];
            }
        }
    }
    IEnumerator playgame()
    {
        int index = History.index;
        List<historyclass> chosengame = historyqueue.hqueue.ToList();
        Coordinates stoplvl = new Coordinates(-1, -1, -1, -1);
        if (customAddBox.lvl == 1)
        {
            for (int i = 1; i < chosengame[index].coordinates.Count; i++)
            {
                if (chosengame[index].coordinates[i].Equals(stoplvl)) break;
                yield return new WaitForSeconds(2f);
                boxs[chosengame[index].coordinates[i].box1].onClick.Invoke();
                yield return new WaitForSeconds(1f);
                boxs[chosengame[index].coordinates[i].box2].onClick.Invoke();
                if (i == chosengame[index].coordinates.Count - 1)
                {
                    playbackfinished = true;
                    yield return new WaitForSeconds(1f);
                    endgameplay();
                }
            }
        }
        else if (customAddBox.lvl == 2)
        {
            int level2 = chosengame[index].coordinates.IndexOf(stoplvl, 1);
            for (int i = level2 + 1; i < chosengame[index].coordinates.Count; i++)
            {
                if (chosengame[index].coordinates[i].Equals(stoplvl)) break;
                yield return new WaitForSeconds(2f);
                boxs[chosengame[index].coordinates[i].box1].onClick.Invoke();
                yield return new WaitForSeconds(1f);
                boxs[chosengame[index].coordinates[i].box2].onClick.Invoke();
                if (i == chosengame[index].coordinates.Count - 1)
                {
                    playbackfinished = true;
                    yield return new WaitForSeconds(1f);
                    endgameplay();
                }
            }
        }
        else
        {
            int level3 = chosengame[index].coordinates.IndexOf(stoplvl,
                chosengame[index].coordinates.IndexOf(stoplvl, 1) + 1);
            for (int i = level3 + 1; i < chosengame[index].coordinates.Count; i++)
            {
                if (chosengame[index].coordinates[i].Equals(stoplvl)) break;
                yield return new WaitForSeconds(2f);
                boxs[chosengame[index].coordinates[i].box1].onClick.Invoke();
                yield return new WaitForSeconds(1f);
                boxs[chosengame[index].coordinates[i].box2].onClick.Invoke();
                if (i == chosengame[index].coordinates.Count - 1)
                {
                    playbackfinished = true;
                    yield return new WaitForSeconds(1f);
                    if (chosengame[index].levels == 3)
                    {
                        endgameplay();
                    }
                    else
                    {
                        SceneManager.LoadScene("customlvl4");
                    }
                }
            }
        }
    }
    IEnumerator showimages()
    {
        for (int i = 0; i < boxs.Count; i++)
        {
            boxs[i].interactable = false;
            boxs[i].image.sprite = gamePuzzles[i];
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < boxs.Count; i++)
        {
            boxs[i].image.sprite = backImg;
        }
    }
    public void endgameplay()
    {
        gamepanel.SetActive(false);
        endpanel.SetActive(true);
    }
    public void playagain()
    {
        customAddBox.lvl = 1;
        SceneManager.LoadScene("custom memory game");
    }
}
