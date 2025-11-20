using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countMessage;

    public bool IsRunning = true;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurState == GameState.CountDown && IsRunning)
        {
            StartCoroutine(CountDown());
            IsRunning = false;
        }
    }

    void OnEnable()
    {
        IsRunning = true;
    }

    public IEnumerator CountDown()
    {
        int count = 3;
        countMessage.text = countMessage.text;

        while (count > 0)
        {
            switch (count)
            {
                case 3:
                    countMessage.text = "3";
                    //countMessage.text = "안녕하세요! \n'5늘의 TIL은 뭘까요?' 조\n입니다";
                        break;
                case 2:
                    countMessage.text = "2";
                    //countMessage.text = "게임 플레이 시연 영상을\n 시작하겠습니다";
                    break;
                case 1:
                    countMessage.text = "1";
                    //countMessage.text = "시청해주시는 모든 분들 \n행복하길 바랍니다";
                    break;
                        
                        
            }
            Debug.Log(countMessage.text);
            yield return new WaitForSecondsRealtime(1f);
            count--;
        }
        countMessage.text = "!!!시 작!!!";
        yield return new WaitForSecondsRealtime(0.5f);

        GameManager.Instance.StartGame();
        UIManager.Instance.CallUIOnOff(PanelType.Countdown, false);
    }
}
