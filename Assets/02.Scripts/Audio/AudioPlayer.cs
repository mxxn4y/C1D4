using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//오디오소스 컴포넌트 자동 추가
[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    //재생하려는 소스 이름
    public string ClipName
    {
        get
        {
            //클립이 있을 경우 클립 이름 반환
            return audioSource.clip != null ? audioSource.clip.name : null;
        }
    }

    
    //오디오소스 컴포넌트
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioMixerGroup _audioMixer, float _delay, bool _isLoop)
    {
        //오디오 믹스랑 반복 여부 설정, 딜레이 후 플레이
        audioSource.outputAudioMixerGroup = _audioMixer;
        audioSource.loop = _isLoop;
        audioSource.PlayDelayed(_delay);

        if(_isLoop == false)
        {
            //루프 상태가 아니라면 한 번 재생하고 삭제
            StartCoroutine(DestroyAudioFinish(audioSource.clip.length + _delay));
        }
    }

    public IEnumerator DestroyAudioFinish(float _clipLength)
    {
        yield return new WaitForSeconds(_clipLength);
        Destroy(gameObject);
    }
    
    //받은 클립을 AudioSource clip에 추가함
    public void InitSound(AudioClip _clip)
    {
        audioSource.clip = _clip;
    }
}
