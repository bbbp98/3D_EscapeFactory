using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 상태, 카운트 다운, 게임 오버/클리어
public enum GameDifficulty
{
    Easy,
    Normal,
    Hard
}

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController playerController;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    private void Start()
    {
        
    }

    public void InitGame()
    {
        StartGame();
        //if (~ ~)
            //GameOver();
    }
    public void StartGame()
    {
        Debug.Log("게임 시작");
        //모든 사물들을 첫 위치로 초기화
        
    }
    public void GameOver()
    {
        //모든 사물들 정지
        //endPanel 띄우기
    }
    public void ReStart()
    {
        //모든 사물들 처음으로 초기화
    }
    public void StopGame()
    {
        //사물 정지
        //일시정지 UI띄우기
    }
}
