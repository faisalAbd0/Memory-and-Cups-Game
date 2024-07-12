using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class historyclass
{
    public string name;
    public string date;
    public float totalduration;
    public int score;
    public int levels;
    public List<Coordinates> coordinates = new List<Coordinates>();
    public List<cupclass4> lvl4 = new List<cupclass4>();
    public List<cupclass5> lvl5 = new List<cupclass5>();
}
public static class historyqueue
{
    public static Queue<historyclass> hqueue = new Queue<historyclass>();
}
public static class currentgame
{
    public static historyclass h = new historyclass();
}

