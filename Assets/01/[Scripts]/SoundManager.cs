using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); // "Bgm", "Effect"
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true; // bgm 재생기는 무한 반복 재생
        }
    }

    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
    }
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm) // BGM 배경음악 재생
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Effect 효과음 재생
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}"; // ??Sound 폴더 안에 저장될 수 있도록

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm) // BGM 배경음악 클립 붙이기
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else // Effect 효과음 클립 붙이기
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }
}
*/

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public static SoundManager instance;


    public AudioClip jump_sound;
    public AudioClip run_sound;
    public AudioClip die_sound;
    public AudioClip touchbulb_sound;
    public AudioClip ride_sound;
    public AudioClip rotate_sound;
    public AudioClip shoot_sound;
    public AudioClip attack_sound;
    public AudioClip ice_sound;
    public AudioClip clear_sound;
    public AudioClip water_sound;
    public AudioClip switch_sound;
    public AudioClip wall_sound;
    public AudioClip book_open;
    public AudioClip book_next;

    public int run_sound_timer;
    public int rotate_sound_timer;
    public int sound_timer_3;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip,float value)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.PlayOneShot(clip, value);

        Destroy(go, clip.length);
    }

    public void ButtonSoungPlay(AudioClip clip)
    {
        GameObject go = new GameObject("Button Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.PlayOneShot(clip, 0.5f);

        Destroy(go, clip.length);
    }


    public void BgmPlay(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.loop = true;
        bgm.volume = 0.1f;
        bgm.Play();
    }
}