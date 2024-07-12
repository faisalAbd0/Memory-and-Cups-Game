using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerprefs1 : MonoBehaviour
{
    public void Start()
    {
        loadplayerdata();
        loadhistorydata();
        foreach (Player p in playerslist.players)
        {
            Debug.Log(p);
        }
    }
    void OnApplicationQuit()
    {
        // Save the profile data to PlayerPrefs
        // and save the PlayerPrefs data to disk
        SaveProfileData();
        SaveHistorydata();
    }

    public static void SaveProfileData()
    {
        // Get the current count of saved profiles, or initialize it to 0 if it doesn't exist
        int count = 0;
        //List<Player> templayers = savesystem.loadplayer();
        foreach (Player player in playerslist.players)
        {
            // Store the profile data with unique keys based on the profile count
            PlayerPrefs.SetString("name" + count, player.name);
            PlayerPrefs.SetString("gender" + count, player.gender);
            PlayerPrefs.SetString("Age" + count, player.Age);
            PlayerPrefs.SetString("Background color" + count, player.backgroundColor);
            PlayerPrefs.SetInt("highest score" + count, player.highestscore);
            PlayerPrefs.SetInt("lowest score" + count, player.lowestscore);
            PlayerPrefs.SetInt("games" + count, player.games);
            PlayerPrefs.SetFloat("min duration" + count, player.minduration);
            PlayerPrefs.SetFloat("max duration" + count, player.maxduration);
            PlayerPrefs.SetFloat("total duration" + count, player.totalduration);
            count++;
        }
        // Increment the profile count and store it back
        PlayerPrefs.SetInt("profileCount", count);

        // Save the PlayerPrefs data to disk
        PlayerPrefs.Save();
    }
    void loadplayerdata()
    {
        int playernum = PlayerPrefs.GetInt("profileCount");
        Debug.Log(playernum);
        for (int i = 0; i < playernum; i++)
        {
            Player player = new Player();
            player.name = PlayerPrefs.GetString("name" + i);
            player.gender = PlayerPrefs.GetString("gender" + i);
            player.Age = PlayerPrefs.GetString("Age" + i);
            player.backgroundColor = PlayerPrefs.GetString("Background color" + i);
            player.minduration = PlayerPrefs.GetFloat("min duration" + i);
            player.maxduration = PlayerPrefs.GetFloat("max duration" + i);
            player.totalduration = PlayerPrefs.GetFloat("total duration" + i);
            player.highestscore = PlayerPrefs.GetInt("highest score" + i);
            player.lowestscore = PlayerPrefs.GetInt("lowest score" + i);
            player.games = PlayerPrefs.GetInt("games" + i);
            playerslist.players.Add(player);
        }
    }
    public void deleteall()
    {
        foreach (Player player in playerslist.players.ToList())
        {
            playerslist.players.Remove(player);
        }
        foreach (historyclass h in historyqueue.hqueue.ToList())
        {
            historyqueue.hqueue.Dequeue();
        }
        PlayerPrefs.DeleteAll();
    }
    public static void SaveHistorydata()
    {
        // Get the current count of saved profiles, or initialize it to 0 if it doesn't exist
        int count = 0;
        //List<Player> templayers = savesystem.loadplayer();
        foreach (historyclass h in historyqueue.hqueue)
        {
            // Store the profile data with unique keys based on the profile count
            PlayerPrefs.SetString("hname" + count, h.name);
            PlayerPrefs.SetString("date" + count, h.date);
            PlayerPrefs.SetFloat("totalduration" + count, h.totalduration);
            PlayerPrefs.SetInt("score" + count, h.score);
            PlayerPrefs.SetInt("levels" + count, h.levels);
            int i;
            for (i = 0; i < h.coordinates.Count; i++)
            {
                PlayerPrefs.SetInt("coordinates box1 " + count + "," + i, h.coordinates[i].box1);
                PlayerPrefs.SetInt("coordinates box2 " + count + "," + i, h.coordinates[i].box2);
                PlayerPrefs.SetInt("coordinates img1 " + count + "," + i, h.coordinates[i].img1);
                PlayerPrefs.SetInt("coordinates img2 " + count + "," + i, h.coordinates[i].img2);
            }
            int j;
            for (j = 0; j < h.lvl4.Count; j++)
            {
                int z;
                for (z = 0; z < h.lvl4[j].cupsmoved.Count; z++)
                {
                    PlayerPrefs.SetInt("cupmoved4 " + count + "," + j + "," + z, h.lvl4[j].cupsmoved[z]);
                }
                PlayerPrefs.SetInt("cupmovedcount4 " + count + "," + j,z);
                PlayerPrefs.SetInt("cupchosen4 " + count + "," + j, h.lvl4[j].chosencup);
            }
            PlayerPrefs.SetInt("lvl4count " + count, j);
            int a;
            for (a = 0; a < h.lvl5.Count; a++)
            {
                int z;
                for (z = 0; z < h.lvl5[a].cupsmoved.Count; z++)
                {
                    PlayerPrefs.SetInt("cupmoved5 " + count + "," + a + "," + z, h.lvl5[a].cupsmoved[z]);
                }
                PlayerPrefs.SetInt("cupmovedcount5 " + count + "," + a, z);
                PlayerPrefs.SetInt("cupchosen5 " + count + "," + a, h.lvl5[a].chosencup);
            }
            PlayerPrefs.SetInt("lvl5count " + count, a);
            PlayerPrefs.SetInt("coordinatesCount " + count, i);
            count++;
        }
        // Increment the profile count and store it back
        PlayerPrefs.SetInt("historyCount", count);

        // Save the PlayerPrefs data to disk
        PlayerPrefs.Save();
    }
    void loadhistorydata()
    {
        int historynum = PlayerPrefs.GetInt("historyCount");
        Debug.Log(historynum);
        for (int i = 0; i < historynum; i++)
        {
            int coordinatesnum = PlayerPrefs.GetInt("coordinatesCount " + i);
            historyclass h = new historyclass();
            h.name = PlayerPrefs.GetString("hname" + i);
            h.date = PlayerPrefs.GetString("date" + i);
            h.totalduration = PlayerPrefs.GetFloat("totalduration" + i);
            h.score = PlayerPrefs.GetInt("score" + i);
            h.levels = PlayerPrefs.GetInt("levels" + i);
            h.coordinates = new List<Coordinates>();
            for (int j = 0; j < coordinatesnum; j++)
            {
                //Debug.Log(i + " " + j);
                int box1 = PlayerPrefs.GetInt("coordinates box1 " + i + "," + j);
                int box2 = PlayerPrefs.GetInt("coordinates box2 " + i + "," + j);
                int img1 = PlayerPrefs.GetInt("coordinates img1 " + i + "," + j);
                int img2 = PlayerPrefs.GetInt("coordinates img2 " + i + "," + j);
                h.coordinates.Add(new Coordinates(box1, img1, box2, img2));
            }
            int lvl4num = PlayerPrefs.GetInt("lvl4count " + i);
            h.lvl4 = new List<cupclass4>();
            for(int z = 0; z < lvl4num; z++)
            {
                Debug.Log("z" + z);
                cupclass4 c = new cupclass4();
                int cupmovednum = PlayerPrefs.GetInt("cupmovedcount4 " + i + "," + z);
                for(int x = 0;x<cupmovednum; x++)
                {
                    int y = PlayerPrefs.GetInt("cupmoved4 " + i + "," + z + "," + x);
                    c.cupsmoved.Add(y);
                }
                c.chosencup = PlayerPrefs.GetInt("cupchosen4 " + i + "," + z);
                Debug.Log(c.chosencup);
                h.lvl4.Add(c);
            }
            int lvl5num = PlayerPrefs.GetInt("lvl5count " + i);
            h.lvl5 = new List<cupclass5>();
            for (int z = 0; z < lvl5num; z++)
            {
                Debug.Log("z" + z);
                cupclass5 c = new cupclass5();
                int cupmovednum = PlayerPrefs.GetInt("cupmovedcount5 " + i + "," + z);
                for (int x = 0; x < cupmovednum; x++)
                {
                    int y = PlayerPrefs.GetInt("cupmoved5 " + i + "," + z + "," + x);
                    c.cupsmoved.Add(y);
                }
                c.chosencup = PlayerPrefs.GetInt("cupchosen5 " + i + "," + z);
                h.lvl5.Add(c);
            }
            historyqueue.hqueue.Enqueue(h);
        }
    }
}
