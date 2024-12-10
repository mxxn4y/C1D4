using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

//BGM인지 이펙트 사운드인지
public enum SoundType
{
    BGM,
    EFFECT,
}
public class AudioManager : MonoSingleton<AudioManager>
{
    //오디오 믹스
    [SerializeField] private AudioMixer audioMixer;
    //사운드 목록
    [SerializeField] private AudioClip[] audioClipList;

    //오디오 이름으로 클립 찾게 딕셔너리
    private Dictionary<string, AudioClip> audioClipDictionary;

    //현재 BGM 이름
    private string currentBGM = null;
    //BGM 오디오 소스
    private AudioSource bgmAudioSource;

    //루프중인 오디오 리스트
    private List<AudioPlayer> loopInstantiatedAudio;

    //Awake 오버라이드
    protected override void Awake()
    {
        //부모 Awake 호출
        base.Awake();

        //사운드 목록 딕셔너리에 추가
        audioClipDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in audioClipList)
        {
            audioClipDictionary.Add(clip.name, clip);
            Debug.Log("Added clip: " + clip.name);
        }

        loopInstantiatedAudio = new List<AudioPlayer>();
    }

    private void Start()
    {
        
    }

    public AudioClip GetClip(string _clipName)
    {
        //이름에 해당하는 오디오 클립 반환
        if (audioClipDictionary.TryGetValue(_clipName, out AudioClip clip))
            return clip;

        //없을 경우 null
        return null;
    }

    //효과음 재생
    public void PlayAudio(string _clipName, bool _isLoop, SoundType _soundType, float _delay = 0f)
    {
        //게임 오브젝트 생성
        GameObject effectObj = new GameObject("SoundPlayer" + _soundType.ToString());
        //오디오 플레이어 스크립트 붙임 (+자동으로 오디오 소스도 붙음)
        AudioPlayer audioPlayer = effectObj.AddComponent<AudioPlayer>();

        if(_isLoop == true)
        {
            //반복하는 이펙트 사운드는 나중에 삭제해줘야 함
            AddLoopList(audioPlayer);
        }

        //오디오 소스 clip에 해당하는 클립 추가
        audioPlayer.InitSound(GetClip(_clipName));
        audioPlayer.Play(audioMixer.FindMatchingGroups(_soundType.ToString())[0], _delay, _isLoop);
    }

    private void AddLoopList(AudioPlayer _aoudioPlayer)
    {
        //루프 중인 사운드 리스트에 추가
        loopInstantiatedAudio.Add(_aoudioPlayer);
    }

    public void StopAudio(string _clipName)
    {
        //루프 중인 사운드 종료
        for (int i = loopInstantiatedAudio.Count-1; i >=0; i--)
        {
            AudioPlayer audioPlayer = loopInstantiatedAudio[i];

            if(audioPlayer == null || audioPlayer.ClipName == _clipName)
            {
                //루프 리스트에서 해당 오브젝트 삭제
                loopInstantiatedAudio.RemoveAt(i);

                //오디오 소스 자체가 존재하지 않을 경우 오브젝트 삭제
                if (audioPlayer != null)
                    Destroy(audioPlayer.gameObject);
            }
        }
    }

}
