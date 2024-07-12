using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class texthover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text[] texts;
    public Color textcolor;
    public string color = "#C3A549";
    void Start()
    {
        texts = GetComponentsInChildren<TMP_Text>();
        ColorUtility.TryParseHtmlString(color, out textcolor);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (TMP_Text text in texts)
        {
            text.color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (TMP_Text text in texts)
        {
            text.color = textcolor;
        }
    }
}
