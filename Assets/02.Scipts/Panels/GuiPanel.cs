using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuiPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreNumTmp;

    void FixedUpdate()
    {
        scoreNumTmp.text = ScoreManager.Instance.GetCurScore().ToString();
    }
}
