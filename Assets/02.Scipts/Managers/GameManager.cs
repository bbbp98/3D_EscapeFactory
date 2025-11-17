using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 상태, 카운트 다운, 게임 오버/클리어

public class GameManager : MonoBehaviour
{
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

            if (EventManager.Instance == null)
                new GameObject("EventManager").AddComponent<EventManager>();
        }
        else
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
