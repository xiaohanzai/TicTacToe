using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void SetText()
    {
        if (textMeshPro.text != "")
        {
            return;
        }

        string s = GameManager.Instance.NextClickCharacter();
        textMeshPro.text = s;
        if (s == "O")
        {
            textMeshPro.color = Color.blue;
        }
        else
        {
            textMeshPro.color = Color.red;
        }
        GameManager.Instance.CounterPlusOne();
    }

    public void ResetText()
    {
        textMeshPro.text = "";
    }
}
