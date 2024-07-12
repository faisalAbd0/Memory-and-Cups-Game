using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
  
    public GameObject maincamera;
    public Sprite blue;
    public Sprite red;
    public Sprite green;

    void Start()
    {
        if (chosenplayer.chosen.backgroundColor == "Red")
        {
            maincamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            maincamera.GetComponent<Camera>().backgroundColor = Color.clear;
            maincamera.GetComponent<Camera>().cullingMask = 0;

            // Create a new material with the red background sprite
            Material redBackgroundMaterial = new Material(Shader.Find("Unlit/Transparent"));
            redBackgroundMaterial.mainTexture = red.texture;

            // Assign the material to the camera's background
            maincamera.GetComponent<Camera>().targetTexture = null;
            maincamera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            maincamera.GetComponent<Camera>().cullingMask = -1;
            maincamera.GetComponent<Camera>().GetComponent<Renderer>().sharedMaterial = redBackgroundMaterial;
        }
        else if (chosenplayer.chosen.backgroundColor == "Blue")
        {
            // Similar steps as above, but with blue background sprite
            Material blueBackgroundMaterial = new Material(Shader.Find("Unlit/Transparent"));
            blueBackgroundMaterial.mainTexture = blue.texture;

            // Assign the material to the camera's background
            maincamera.GetComponent<Camera>().targetTexture = null;
            maincamera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            maincamera.GetComponent<Camera>().cullingMask = -1;
            maincamera.GetComponent<Camera>().GetComponent<Renderer>().sharedMaterial = blueBackgroundMaterial;
        }
        else
        {
            // If no specific background color is chosen, set a green background sprite
            Material greenBackgroundMaterial = new Material(Shader.Find("Unlit/Transparent"));
            greenBackgroundMaterial.mainTexture = green.texture;

            // Assign the material to the camera's background
            maincamera.GetComponent<Camera>().targetTexture = null;
            maincamera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            maincamera.GetComponent<Camera>().cullingMask = -1;
            maincamera.GetComponent<Camera>().GetComponent<Renderer>().sharedMaterial = greenBackgroundMaterial;
        }
    }
}
