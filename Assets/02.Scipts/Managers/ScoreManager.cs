using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBestScore();
        }
    }

    public void AddScore(int num)
    {
        curScore += num;
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
    }

    private void ResetBestScore()
    {
       PlayerPrefs.SetInt("BestScore", 0); 
    }

}
