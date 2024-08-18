using System;
using Scriptes.Const;
using UnityEngine;

namespace Scriptes.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private const string MusicMute = "MusicMute";
        private const string SfXMute = "SFXMute";

        [SerializeField] Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        private bool _isMusicMuted;       
        private bool _isSfXMuted;         
        private bool _isAudioMuted;         
    
        public bool IsOffMusic => _isMusicMuted;
        public bool IsOffSound => _isSfXMuted;
        public bool IsAudioMuted => _isMusicMuted && _isSfXMuted;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void OnEnable()
        {
            InitialSound();
            PlayMusic(AudioConst.MusicMainLobby);
        }

        public void StopPlayMusic() => 
            musicSource.Stop();

        public void StopPlaySfx()
        {
            sfxSource.Stop();
        }

        public void PlayMusic(string name)
        {
            Sound sound = Array.Find(musicSounds, x => x.name == name);

            if (sound != null)
            {
                musicSource.clip = sound.clip;
                musicSource.Play();
            }
        }

        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfxSounds, x => x.name == name);

            if (s != null)
            {
                sfxSource.PlayOneShot(s.clip);
            }
        }

        public void ToggleMusic()
        {
            _isMusicMuted = !_isMusicMuted; 
            musicSource.mute = _isMusicMuted;
            PlayerPrefs.SetInt(MusicMute, _isMusicMuted ? 1 : 0);
        }


        public void ToggleSFX()
        {
            _isSfXMuted = !_isSfXMuted;
            sfxSource.mute = !sfxSource.mute;
            PlayerPrefs.SetInt(SfXMute, sfxSource.mute ? 1 : 0);
        }

        public void ToggleAudio()
        {
            ToggleSFX();
            ToggleMusic();
        }

        private void SetMusicLoop(bool isLoop) => 
            musicSource.loop = isLoop;


        private void InitialSound()
        {
            int musicMute = PlayerPrefs.GetInt(MusicMute, 0);
            int sfxMute = PlayerPrefs.GetInt(SfXMute, 0);

            _isMusicMuted = musicMute == 1;
            _isSfXMuted = sfxMute == 1;

            musicSource.mute = _isMusicMuted;
            sfxSource.mute = _isSfXMuted;
        
            SetMusicLoop(true);
        }
    }
}
