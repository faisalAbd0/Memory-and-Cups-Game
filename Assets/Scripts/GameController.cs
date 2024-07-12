using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public List<Button> boxs = new List<Button>();
    // the list that containt all the  buttons

    [SerializeField]
    private Sprite backImg;
    // the back ground img

    public TMP_Text Score;
    public static int score;
    [SerializeField]
    private Sprite Noneimg;
    // the X img

    public Sprite[] lvl1_sprites;
    public Sprite[] lvl2_sprites;
    public Sprite[] lvl3_sprites;

    // to put all the img in here

    public List<Sprite> gamePuzzles = new List<Sprite>();
    // to take some puzzles and put it in the btns List

    public AddBox addBox;

    public Timer mytimer;
    

    public GridLayoutGroup gridLayoutGroup;
    
    private bool firstGuess, secondGuess; 
    private int countGuesses;  
    private int countCorrectGuesses;
    private int countWGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessName, secondGuessName;
   
    public TMP_Text rightG;
    public TMP_Text wrongG;
    public GameObject Continer;
    public GameObject game;
    public AudioClip[] audioClips;
    public TMP_Text username;

    public GameObject pause;

    public List<Coordinates> lvlcoordinates = new List<Coordinates>();
    void Awake()
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
        addBox = GameObject.FindGameObjectWithTag("link").GetComponent<AddBox>();
        mytimer= GameObject.FindGameObjectWithTag("mytimer").GetComponent<Timer>();
        float n = Mathf.Sqrt(addBox.number_of_box);
        int int_N = (int) n;
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
        else if ( addBox.number_of_box == 25)
        {
            gridLayoutGroup.cellSize = new Vector2(150, 150);
            gridLayoutGroup.spacing = new Vector2(35, 30);
        }

        

    }
    // Start is called before the first frame update
    void Start()
    {
        username.text = chosenplayer.chosen.name;
        lvlcoordinates.Add(new Coordinates(-1, -1, -1, -1));
        GetBoxRef();
        ClicktheBox();
        AddGameImgs();
        Shuffle(gamePuzzles);
        StartCoroutine(showimages());
        gameGuesses = gamePuzzles.Count / 2;
        
        
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

     
        for (int i=0; i < objects.Length; i++)
        {
            boxs.Add(objects[i].GetComponent<Button>());
            boxs[i].image.sprite = backImg;
        }
        for (int i = 0; i < boxs.Count; i++)
        {
            if (boxs[i].name == "none_box") {
                
                boxs[i].image.sprite = Noneimg;
                boxs[i].interactable = false;
                boxs.Remove(boxs[i]);
                break;

            }
        }


    }

    public void ClicktheBox()
    {
        //foreach (Button btn in boxs)
        //{
        //    print(btn.name);
        //}
       
        foreach (Button b in boxs)
        {
            b.onClick.AddListener(() => TO_DO());
        }
    }
   
    void TO_DO()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        //print($"box name is {name}");

        if (!firstGuess)
        {
            
            firstGuess = true;
            firstGuessIndex = int.Parse(name);
            //print(boxs[firstGuessIndex].name);
            boxs[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            firstGuessName = gamePuzzles[firstGuessIndex].name;
        }
        else if (!secondGuess) {
            
            secondGuess = true;
            secondGuessIndex = int.Parse(name);
            //print(boxs[secondGuessIndex].name);

            boxs[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            secondGuessName = gamePuzzles[secondGuessIndex].name;


            // 
            if (firstGuessIndex != secondGuessIndex)
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
        for (int i = 0; i < n ; i++)
        {
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
            GetComponent<AudioSource>().PlayOneShot(audioClips[2]);

            boxs[firstGuessIndex].interactable = false;
            boxs[secondGuessIndex].interactable = false;


            boxs[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            boxs[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            lvlcoordinates.Add(new Coordinates(firstGuessIndex, int.Parse(firstGuessName), secondGuessIndex, int.Parse(secondGuessName)));

            CheckIfTheGameIsFinished();

        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
            countWGuesses++;

            //to decrease the score
            if (score > 20)
            {
                score -= 20;
                Score.text = score.ToString();
            }

            lvlcoordinates.Add(new Coordinates(firstGuessIndex, int.Parse(firstGuessName), secondGuessIndex, int.Parse(secondGuessName)));
            boxs[firstGuessIndex].image.sprite = backImg;
            boxs[secondGuessIndex].image.sprite = backImg;

            wrongG.text = countWGuesses.ToString();
        }

        //yield return new WaitForSeconds(.5f);
        firstGuess = secondGuess = false;
        
    }

    void CheckIfTheGameIsFinished() 
    {
        countCorrectGuesses++;

        //to increase the score 
        score += 100;
        Score.text = score.ToString();


        rightG.text = countCorrectGuesses.ToString();
        //print($"countCorrectGuesses is {countCorrectGuesses} and gameGuesses is {gameGuesses} ");
        if (countCorrectGuesses == gameGuesses) {
            pause.SetActive(false);
            //print("Game Finished");
            //print($"It took u {countGuesses} guesses to finish the game");
            //yield return new WaitForSeconds(1F);


            //addBox.nextLevel();
            Continer.SetActive(true);
            game.SetActive(false);

            //to stop timer
            Timer.timerison = false;
            
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i=0; i<list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count); 
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    public void lvlfinished()
    {
        chosenplayer.chosen.score += score;
        float time = Timer.minutes + Timer.seconds / 100;
        chosenplayer.chosen.totalduration += time;
        chosenplayer.chosen.duration += time;
        if(chosenplayer.chosen.totalduration - System.Math.Truncate(chosenplayer.chosen.totalduration) > 59)
        {
            chosenplayer.chosen.totalduration = (float)System.Math.Truncate(chosenplayer.chosen.totalduration) + 1;
            chosenplayer.chosen.totalduration = (float)(chosenplayer.chosen.totalduration - System.Math.Truncate(chosenplayer.chosen.totalduration)) - 60;
        }
        chosenplayer.chosen.coordinates.AddRange(lvlcoordinates);
    }
    IEnumerator showimages()
    {
        pause.SetActive(false);
        for (int i = 0; i < boxs.Count; i++)
        {
            boxs[i].interactable = false;
            boxs[i].image.sprite = gamePuzzles[i];
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < boxs.Count; i++)
        {
            boxs[i].image.sprite = backImg;
            boxs[i].interactable = true;
        }
        pause.SetActive(true);
    }

}