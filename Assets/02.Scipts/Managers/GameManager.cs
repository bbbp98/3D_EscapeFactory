using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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

public enum PetType { None, BlueRobot, YellowRobot, Cat }

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;

    private float speedRate = 0.02f;
    private float runtimeSpeed = 1f;

    [Header("State Info")]
    [SerializeField] private GameState curState;
    public GameState CurState => curState;

    public Dictionary<PetType, bool> petUnlocks = new Dictionary<PetType, bool>(); // 펫 해금여부
    public PetType equippedPet = PetType.None; // 펫 장착

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

            petUnlocks[PetType.BlueRobot] = true;
            petUnlocks[PetType.YellowRobot] = false;
            petUnlocks[PetType.Cat] = true;
            equippedPet = PetType.BlueRobot;

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
        if(curState == GameState.Playing)
        {
            runtimeSpeed += speedRate * Time.deltaTime;
        }
    }
    
    public float TotalSpeedMultiplier()
    {
        return  runtimeSpeed;
    }

    //게임 설정 초기화
    public void InitGame()
    {
        //초기 상태
        curState = GameState.Pause;
        
        // 누적속도 초기화
        runtimeSpeed = 1f;

        UIManager.Instance.CallUIOnOff(PanelType.GameOver, false);
        UIManager.Instance.CallUIOnOff(PanelType.Settings, false);
    }

    //게임 플레이 시작
    public void StartGame()     //게임 시작 시 실행
    {
        if (curState != GameState.Pause && curState != GameState.CountDown) return;


        //게임 시작 상태로 바꾸기
        curState = GameState.Playing;

        Resume();

        Debug.Log("게임 시작");
    }

    //게임 끝 났을 때 
    public void GameOver()      //플레이어 사망 시 실행
    {
        if(curState != GameState.Playing) return;

        curState = GameState.GameOver;

        Debug.Log("게임 끝");
 
        UIManager.Instance.CallUIOnOff(PanelType.GameOver, true);

    }

    public void ReStartRoutine()    //restart 버튼 누르면 실행
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //씬 재시작
        InitGame();
        UseCountdown();
    }

    //게임 멈추기
    public void SettingInGame()     //일시정지 버튼 누르면 실행
    {
        curState = GameState.Pause;
        Pause();
        
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        if (curState == GameState.Playing)
            curState = GameState.Pause;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        Debug.Log(curState);
        if(curState == GameState.Pause) 
            curState = GameState.Playing;
    }

    //카운트 다운 메서드
    public void UseCountdown()
    {
        curState = GameState.CountDown;
        Pause();
        UIManager.Instance.CallUIOnOff(PanelType.Countdown, true);
    }
    

}
