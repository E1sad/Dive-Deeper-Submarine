using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG
{
  public class MusicAndSoundManager : MonoBehaviour{
    //[Header("Variables")]

    [Header("Links")]
    [SerializeField] private AudioSource _soundEffectsSource;
    [SerializeField] private AudioSource _backgroundMusicSource;
    [SerializeField] private AudioSource _engineSoundSource;
    [SerializeField] private AudioSource _waterSoundSource;
    [SerializeField] private AudioSource _walkingSoundSource;
    [SerializeField] private AudioClip _buttonSound;

    //Internal varibales
    public static MusicAndSoundManager Instance;

    #region My Methods
    public void PlaySoundEffects(AudioClip clip) {_soundEffectsSource.PlayOneShot(clip);}
    public void StopSoundEffects() { _soundEffectsSource.mute = true; _soundEffectsSource.Stop(); }
    public void PlayBackgroundMusic(AudioClip clip) {
      _backgroundMusicSource.clip = clip; _backgroundMusicSource.Play();
    }
    public void StopBackgroundMusic() { _backgroundMusicSource.Stop(); }
    public void EngineSound(AudioClip clip) {_engineSoundSource.clip = clip; _engineSoundSource.Play();}
    public void StopEngineSound() { _engineSoundSource.Stop(); }
    public void WaterSound(AudioClip clip) {_waterSoundSource.clip = clip; _waterSoundSource.Play();}
    public void StopWaterSound() {  _waterSoundSource.Stop(); }
    public void WalkingSound(AudioClip clip) {_walkingSoundSource.clip = clip; _walkingSoundSource.Play(); }
    public void StopWalkingSound() { _walkingSoundSource.Stop();}
    public void StopAllSounds() {
     _backgroundMusicSource.Stop(); _engineSoundSource.Stop();_waterSoundSource.Stop(); 
      _walkingSoundSource.Stop();
    }
    public void ButtonSound() {PlaySoundEffects(_buttonSound);}
    public void MuteMusic() {_backgroundMusicSource.mute = true; }
    public void UnMuteMusic() { _backgroundMusicSource.mute = false; }
    public void MuteSounds() {
      _soundEffectsSource.mute = true; _engineSoundSource.mute = true; _waterSoundSource.mute = true;
      _walkingSoundSource.mute = true; 
    }
    public void UnMuteSounds() {
      _soundEffectsSource.mute = false; _engineSoundSource.mute = false; _waterSoundSource.mute = false;
      _walkingSoundSource.mute = false;
    }
    #endregion

    #region Unity's Methods
    private void Awake() {
      if (Instance == null) {
        Instance = this;
        DontDestroyOnLoad(gameObject);}
      else { Destroy(gameObject);}
    }
    #endregion
  }
}
