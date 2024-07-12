using UnityEngine;
using UnityEngine.UI;

public class scenecolor : MonoBehaviour
{
    public GameObject image;
    public Sprite blue;
    public Sprite green;
    public Sprite red;
    void Start()
    {
        if (chosenplayer.chosen.backgroundColor == "Red")
            image.GetComponent<Image>().sprite = red;
        else if (chosenplayer.chosen.backgroundColor == "Blue")
            image.GetComponent<Image>().sprite = blue;
        else
            image.GetComponent<Image>().sprite = green;
    }

}
