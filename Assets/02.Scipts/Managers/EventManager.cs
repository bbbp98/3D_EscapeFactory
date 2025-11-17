using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//게임 효과 관리  

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
