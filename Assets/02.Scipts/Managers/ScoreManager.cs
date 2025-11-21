using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Scores")]

    [SerializeField] private int curScore;

    [SerializeField] private int bestScore;

    void Awake()
    {
        // 싱글톤
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public void AddScore(int num)
    {
        curScore += num;

        // play sfx
        SoundManager.Instance.OnOffClickAudio(ClickSounds.Score, true);
    }

    public void SaveScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if(curScore > bestScore)
        {
            bestScore = curScore;
            PlayerPrefs.SetInt("BestScore", curScore);

            AchievementManager.Instance.AdjustProgress("score", curScore);
        }
    }

    public int GetCurScore()
    {
        return curScore;
    }

    public int GetBestScore()
    {      
        return bestScore;
    }

    public void ResetCurScore()
    {
        curScore = 0;
        scoreText.text = "0";
    }

    public void ResetBestScore()
    {
       PlayerPrefs.SetInt("BestScore", 0);
    }

}
