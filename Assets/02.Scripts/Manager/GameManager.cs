using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// Event
    //이전에 이벤트 발생했는지 여부(발생 확률 조절)
    public bool _prevEvent = false;
    public bool _nowEvent = false;
    //이벤트 발생 확률(이전에 발생하지 않으면 확률 높임)
    public float _eventProb = 0f;
    /// Event

    /// Ending
    public int _ending = -1;
    //상수로 엔딩 종류 지정(enum을 쓸까) 
    const int NORMAL_ENDING = 0;
    const int HAPPY_ENDING = 1;
    const int BAD_ENDING = 2;
    const int HIDDEN_ENDING = 3;
    //가위 썼는지(진 엔딩 보기 위한 조건 달성 여부)
    public bool _endingCondition = false;
    /// Ending

    ///Day
    public int day = 0;
    ///Day

    #region Event
    //이벤트 발생 확률 조절
    public void SwitchEventProb()
    {
        if (!_prevEvent)
        {
            //확률 올림
        }
    }

    public void EventOccur()
    {
        _nowEvent = true;

        //이벤트 종류마다
    }

    #endregion

    //초기 세팅(필요할 것 같음)
    public void StartDay()
    {

    }

    //하루 최종 세팅
    public void EndDay()
    {

    }

    //다음날로 넘기기
    public void NextDay()
    {
        ++day;
        _prevEvent = _nowEvent;

        //부가 기능
    }

    //엔딩
    public void Ending()
    {
        if (_ending == NORMAL_ENDING)
        {

        }
        else if (_ending == HAPPY_ENDING)
        {

        }
        else if (_ending == BAD_ENDING)
        {

        }
        else if (_ending == HIDDEN_ENDING)
        {

        }

    }

    private void Update()
    {

    }
}
