using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class newplayer : MonoBehaviour
{
    public InputField username;
    public GameObject usergender;
    public TMP_Dropdown userage;
    public GameObject userbackgroundcolor;
    public Button addbutton;
    public GameObject maincamera;
    public TMP_Text notiftext;
    public GameObject image;
    public Sprite red;
    public Sprite green;
    public Sprite blue;
    public void Start()
    {
        addbutton.interactable = false;
        username.GetComponent<InputField>().onValueChanged.AddListener(validateinput);
        username.GetComponent<InputField>().onValueChanged.AddListener(validatename);
        userbackgroundcolor.transform.GetChild(0).GetComponent<Toggle>()
            .onValueChanged.AddListener(changered);
        userbackgroundcolor.transform.GetChild(1).GetComponent<Toggle>()
            .onValueChanged.AddListener(changeblue);
        userbackgroundcolor.transform.GetChild(2)
            .GetComponent<Toggle>().onValueChanged.AddListener(changegreen);
    }
    public void changered(bool value)
    {
        if (userbackgroundcolor.transform.GetChild(0).GetComponent<Toggle>().isOn)
        {
            image.GetComponent<Image>().sprite = red;
        }
    }
    public void changeblue(bool value)
    {
        if (userbackgroundcolor.transform.GetChild(1).GetComponent<Toggle>().isOn)
        {
            image.GetComponent<Image>().sprite = blue;
        }
    }
    public void changegreen(bool value)
    {
        if (userbackgroundcolor.transform.GetChild(2).GetComponent<Toggle>().isOn)
        {
            image.GetComponent<Image>().sprite = green;
        }
    }
    public void validateinput(string value)
    {
        if (string.IsNullOrEmpty(username.GetComponent<InputField>().text))
        {
            addbutton.interactable = false;
        }
        else 
        {
            addbutton.interactable = true;
        }
    }
    public void validatename(string name)
    {
        bool flag = true;
        foreach(Player p in playerslist.players)
        {
            if (p.name == name)
            {
                flag = false;
            }
        }
        if (flag)
        {
            notiftext.text = "";
            addbutton.interactable = true;
        }
        else
        {
            notiftext.text = "This username is already taken";
            addbutton.interactable = false;
        }
        
    }
    public void addplayer()
    {
        Player player = new Player();
        player.name = username.GetComponent<InputField>().text;
        for (int i = 0; i < 2; i++)
        {
            if (usergender.transform.GetChild(i).GetComponent<Toggle>().isOn)
            {
                player.gender = usergender.transform.GetChild(i).Find("Label")
                    .GetComponent<Text>().text;
            }
        }
        player.Age = userage.transform.Find("Label").GetComponent<TMP_Text>().text;
        for (int i = 0; i < 3; i++)
        {
            if (userbackgroundcolor.transform.GetChild(i).GetComponent<Toggle>().isOn)
            {
                player.backgroundColor = userbackgroundcolor.transform.GetChild(i).Find("Label")
                    .GetComponent<Text>().text;
            }
        }
        player.totalduration = 0;
        player.maxduration = 0;
        player.minduration = 0;
        player.games = 0;
        player.highestscore = 0;
        player.lowestscore = 0;
        playerslist.players.Add(player);
        chosenplayer.chosen = player;
        Debug.Log(chosenplayer.chosen);
    }
}

