using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml.Schema;
using TMPro;
using UnityEngine;

public class statsmanager : MonoBehaviour
{
    public int gamesplayed;
    public int profilenumber;
    public int highestscore;
    public int lowestscore;
    public float maxduration;
    public float minduration;
    public float totalduration;
    public TMP_Text gamesplayedtext;
    public TMP_Text profilenumbertext;
    public TMP_Text highestscoretext;
    public TMP_Text lowestscoretext;
    public TMP_Text maxdurationtext;
    public TMP_Text mindurationtext;
    public TMP_Text totaldurationtext;
    // Start is called before the first frame update

    void Start()
    {
        
        
        gamesplayed = playerslist.players.Select(s => s.games).Sum();
        profilenumber = playerslist.players.Count();
        highestscore = playerslist.players.Select(s => s.highestscore).Max();
        lowestscore = playerslist.players.Select(s => s.lowestscore).Min();
        maxduration = playerslist.players.Select(s => s.maxduration).Max();
        minduration = playerslist.players.Select(s => s.minduration).Min();
        totalduration = playerslist.players.Select(s => s.totalduration).Sum();

        gamesplayedtext.text = gamesplayed.ToString();
        profilenumbertext.text = profilenumber.ToString();
        highestscoretext.text = highestscore.ToString();
        lowestscoretext.text = lowestscore.ToString();
        maxdurationtext.text = $"{Math.Truncate(maxduration)}:{Math.Round(maxduration - Math.Truncate(maxduration), 2) * 100}";
        mindurationtext.text = $"{Math.Truncate(minduration)}:{Math.Round(minduration - Math.Truncate(minduration), 2) * 100}";
        totaldurationtext.text = $"{Math.Truncate(totalduration)}:{Math.Round(totalduration - Math.Truncate(totalduration), 2) * 100}";
    }
    
}
