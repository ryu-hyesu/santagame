using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class SoundManager : MonoBehaviour
{
   public AudioMixer masterMixer;
   public Slider audioSlider;
   public TextMeshProUGUI buttonText;

   public void AudioControlBGM(){
    float sound = audioSlider.value;

    if(sound == -40f) masterMixer.SetFloat("BGM", -80);
    else masterMixer.SetFloat("BGM", sound);
   }

   public void AudioControlSFX(){
      float sound = audioSlider.value;

      if(sound == -40f) masterMixer.SetFloat("SFX", -80);
      else masterMixer.SetFloat("SFX", sound);
   }

   public void AudioControlMASTER(){
      float sound = audioSlider.value;

      if(sound == -40f) masterMixer.SetFloat("Master", -80);
      else masterMixer.SetFloat("Master", sound);
   }

   public void ToggleAudioVolume(){
      AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
      buttonText.text = AudioListener.volume == 0 ? "SOUND OFF" : "SOUND ON";
   }
}
