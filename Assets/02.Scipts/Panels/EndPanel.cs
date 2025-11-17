using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EndPanel : MonoBehaviour
{
    public TextMeshProUGUI curScore;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI bestScoreText;

    public int curScoreNum;

    public int bestScoreNum;

    private void OnEnable()
    {
        UpdateEndPanel();
    }

    private void UpdateEndPanel()
    {
        // 현재 점수 적용
        StartCoroutine(ElevateNum(9999, curScore));
        // 최고 점수 적용
        StartCoroutine(ElevateNum(9999, bestScore));
        // 최고 점수 갱신 연출 적용
        StartCoroutine(ElevateText(bestScoreText));
    }

    private void GetScore()
    {
        // ScoreManager로부터 현재 점수, 최고 점수 획득
    }

    IEnumerator ElevateNum(int num, TextMeshProUGUI targetText)
    {
        int n = 0;
        float gapTime = 3f / (num > 0 ? num : 1);

        Debug.Log(gapTime);

        if(gapTime > 1f)
        {
            gapTime = 0.25f;
        }

        if(num > 0)
        {
            while(n < num)
            {
                n += 10;
                if(n > num) n = num;
                targetText.text = n.ToString();

                yield return new WaitForSeconds(gapTime);
            } 
        }  
    }

    IEnumerator ElevateText(TextMeshProUGUI targetText)
    {
        string texts = targetText.text;

        targetText.text = string.Empty;

        if(texts.Length > 0)
        {
            for(int i = 0; i < texts.Length; i++)
            {
                targetText.text = texts.Substring(0, i+1);

                yield return new WaitForSeconds(0.1f);
            }
        }  
    }
}
