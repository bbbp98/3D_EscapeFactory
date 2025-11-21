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
        ScoreManager.Instance.SaveScore();
        curScoreNum = ScoreManager.Instance.GetCurScore();
        bestScoreNum = ScoreManager.Instance.GetBestScore();
        // 현재 점수 적용
        StartCoroutine(ElevateNum(curScoreNum, curScore));
        // 최고 점수 적용
        StartCoroutine(ElevateNum(bestScoreNum, bestScore));

        if(curScoreNum == bestScoreNum)
        {
            // 최고 점수 갱신 연출 적용
            bestScoreText.gameObject.SetActive(true);
            StartCoroutine(ElevateText(bestScoreText));
        }
        else
        {
            bestScoreText.gameObject.SetActive(false);
        }
        
    }

    

    IEnumerator ElevateNum(int num, TextMeshProUGUI targetText)
    {
        int n = 0;
        float gapTime = 3f / (num > 0 ? num : 3);

        Debug.Log(gapTime);

        if(gapTime > 1f)
        {
            gapTime = 0.1f;
        }

        if(num > 0)
        {
            while(n < num)
            {
                n += 100;
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
