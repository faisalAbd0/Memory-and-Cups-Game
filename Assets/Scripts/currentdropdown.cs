using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class currentdropdown : MonoBehaviour
{
    //public SaveSystem saveSystem;
    public Dropdown dropdown;
    public Button play;
    public GameObject image;
    public Sprite red;
    public Sprite green;
    public Sprite blue;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.options.Clear();
        foreach (Player p in playerslist.players)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = p.name });
        }
        dropdown.onValueChanged.AddListener(valuechanged);
        play.interactable = false;
        dropdown.GetComponent<Dropdown>().onValueChanged.AddListener(validateplayer);
    }

    public void validateplayer(int value)
    {
        if(dropdown.GetComponent<Dropdown>().value == -1)
        {
            play.interactable = false;
        }
        else
        {
            play.interactable = true;
        }
    }
    public void valuechanged(int value)
    {
        string temp = dropdown.transform.Find("Label").GetComponent<Text>().text;
        foreach (Player player in playerslist.players)
        {
            if(temp == player.name)
            {
                chosenplayer.chosen = player;
            }
        }
        if (chosenplayer.chosen.backgroundColor == "Red")
            image.GetComponent<Image>().sprite = red;
        else if (chosenplayer.chosen.backgroundColor == "Blue")
            image.GetComponent<Image>().sprite = blue;
        else
            image.GetComponent<Image>().sprite = green;
        Debug.Log(chosenplayer.chosen);
    }
}
