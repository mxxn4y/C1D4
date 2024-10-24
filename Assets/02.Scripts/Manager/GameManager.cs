using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// Event
    //������ �̺�Ʈ �߻��ߴ��� ����(�߻� Ȯ�� ����)
    public bool _prevEvent = false;
    public bool _nowEvent = false;
    //�̺�Ʈ �߻� Ȯ��(������ �߻����� ������ Ȯ�� ����)
    public float _eventProb = 0f;
    /// Event

    /// Ending
    public int _ending = -1;
    //����� ���� ���� ����(enum�� ����) 
    const int NORMAL_ENDING = 0;
    const int HAPPY_ENDING = 1;
    const int BAD_ENDING = 2;
    const int HIDDEN_ENDING = 3;
    //���� �����(�� ���� ���� ���� ���� �޼� ����)
    public bool _endingCondition = false;
    /// Ending

    ///Day
    public int day = 0;
    ///Day

    #region Event
    //�̺�Ʈ �߻� Ȯ�� ����
    public void SwitchEventProb()
    {
        if (!_prevEvent)
        {
            //Ȯ�� �ø�
        }
    }

    public void EventOccur()
    {
        _nowEvent = true;

        //�̺�Ʈ ��������
    }

    #endregion

    //�ʱ� ����(�ʿ��� �� ����)
    public void StartDay()
    {

    }

    //�Ϸ� ���� ����
    public void EndDay()
    {

    }

    //�������� �ѱ��
    public void NextDay()
    {
        ++day;
        _prevEvent = _nowEvent;

        //�ΰ� ���
    }

    //����
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
