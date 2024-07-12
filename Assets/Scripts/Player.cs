using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Player
{
    public string name = "";
    public string gender = "";
    public string Age = "";
    public string backgroundColor = "";
    public int games;
    public int highestscore;
    public int lowestscore;
    public float minduration;
    public float maxduration;
    public float totalduration;
    public int score;
    public float duration;
    public List<Coordinates> coordinates = new List<Coordinates>();
    public List<cupclass4> lvl4 = new List<cupclass4>();
    public List<cupclass5> lvl5 = new List<cupclass5>();
public override string ToString()
    {
        return $"Name:{name} Gender:{gender} Age:{Age} Background color:{backgroundColor}\r\n";
    }
}
public static class playerslist
{
    public static List<Player> players = new List<Player>();
}
public static class chosenplayer
{
    public static Player chosen;
}
