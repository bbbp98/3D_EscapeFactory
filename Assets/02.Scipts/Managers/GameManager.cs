using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//게임 상태, 카운트 다운, 게임 오버/클리어
public enum GameState
{
    Pause,      //정지
    CountDown,  //카운트 다운
    Playing,    //실행 
    GameOver    //게임 끝
}   

public enum GameDifficulty
{
    Easy,   //0.8배속
    Normal,     //1배속
    Hard    //1.2배속
}

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Difficulty Info")]     //난이도 현재 단계
    [SerializeField] private GameDifficulty curDifficulty = GameDifficulty.Normal;
    public GameDifficulty CurDifficulty => curDifficulty;

    [Header("Difficulty Speed")]    //난이도 별 속도 
    [SerializeField] private float difficultySpeed = 1f;
    public float DifficultySpeed => difficultySpeed;

    [Header("State Info")]
    [SerializeField] private GameState curState;
    public GameState CurState => curState;

    private static GameManager _instance;

    public bool isPaused { get; private set; }
    public static GameManager Instance
    {
        get // 재시작 시 게임 매니저를 참조한 다른 스크립트에서 찾을려고 할 때 null뜰 수 있음
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
        InitGame();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)) StartGame();
        if (Input.GetKeyUp(KeyCode.R)) GameOver();
        if (Input.GetKeyUp(KeyCode.T)) SettingInGame();
    }
    //난이도 설정 메서드
    public void SetDifficulty(GameDifficulty difficulty)    
    {
        curDifficulty = difficulty;
        switch (difficulty)
        {
            case GameDifficulty.Easy:
                difficultySpeed = 0.8f;
                break;
            case GameDifficulty.Normal:
                difficultySpeed = 1f;
                break;
            case GameDifficulty.Hard:
                difficultySpeed = 1.2f;
                break;
        }
    }

    //게임 설정 초기화
    public void InitGame()
    {
        //초기 상태
        curState = GameState.Pause;
        
        //UI, Sound 초기화
        UIManager.Instance.CallUIOnOff(PanelType.InGame, true);
        UIManager.Instance.CallUIOnOff(PanelType.GameOver, false);
        UIManager.Instance.CallUIOnOff(PanelType.Settings, false);
    }

    //게임 플레이 시작
    public void StartGame()     //게임 시작 시 실행
    {
        if (curState != GameState.Pause && curState != GameState.CountDown) return;

        //플레이어 위치 초기화
        //player.transform.position = Vector3.zero;

        //게임 시작 상태로 바꾸기
        curState = GameState.Playing;
        //UI적용
        //UIManager.Instance.CallUIOnOff(PanelType.InGame, true);
        //난이도에 따른 속도 적용

        Resume();

        Debug.Log("게임 시작");
    }

    //게임 끝 났을 때 
    public void GameOver()      //플레이어 사망 시 실행
    {
        if(curState != GameState.Playing) return;

        curState = GameState.GameOver;
        //모든 사물들 정지
        //Pause();
        Debug.Log("게임 끝");
        //endPanel 띄우기
        UIManager.Instance.CallUIOnOff(PanelType.GameOver, true);

    }

    public void ReStartRoutine()    //restart 버튼 누르면 실행
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //씬 재시작
        InitGame();
        StartCoroutine(CountDown());
    }

    //게임 재시작
    //public IEnumerator ReStartRoutine()    //restart 버튼 누르면 실행
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name); //씬 재시작

    //    yield return null;

    //    player = FindObjectOfType<PlayerController>().gameObject;
    //    InitGame();
    //}

    //게임 멈추기
    public void SettingInGame()     //일시정지 버튼 누르면 실행
    {
        curState = GameState.Pause;
        //Pause();
        //settingPanel
        //UIManager.Instance.CallUIOnOff(PanelType.Settings, true);
    }
    public void Pause()
    {
        if(isPaused) return;
        isPaused = true;
        Time.timeScale = 0f;
        if (curState == GameState.Playing)
            curState = GameState.Pause;
    }
    public void Resume()
    {
        //if(!isPaused) return;
        //isPaused = false;
        Time.timeScale = 1f;
        Debug.Log(curState);
        if(curState == GameState.Pause) 
            curState = GameState.Playing;
    }

    //카운트 다운 메서드
    public IEnumerator CountDown()
    {
        curState = GameState.CountDown;

        int count = 3;

        while (count > 0)
        {
            Debug.Log(count);   //여기에 UI, SOUND 
            yield return new WaitForSeconds(1f);
            count--;
        }
        Debug.Log("시작");
        yield return new WaitForSeconds(0.5f);
        StartGame();
    }
}
