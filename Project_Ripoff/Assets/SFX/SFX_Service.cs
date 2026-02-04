using System;
using System.Collections.Generic;
using UnityEngine;
/**
 * Die Klasse bietet eine Funktionalität als SFX Service.
 * Dabei können andere Klassen Soundeffekte ausführen ohne gekoppelt zu sein 
 * bzw. sich gegeseitig zu kennen.
 * Dokumentation/Guideline zur Implementierung ist unter 
 * "https://projekte.tgm.ac.at/youtrack/articles/RIP-A-9/Observer-Pattern-Guideline-Ripoff-Disposable-Heroes" zu finden.
 * @author Viktor Bublinskyy
 * @version 1.0.0
 */
public class SFX_Service : MonoBehaviour
{
    [Serializable]
    public struct SoundEffect
    {
        public string soundName;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
    }

    [SerializeField] private List<SoundEffect> soundEffects;

    private Messaging_Service messaging_Service;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }

    private void OnEnable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.playSFXEvent += PlaySound;
            messaging_Service.playUISFXEvent += PlaySoundUI;
        }
    }

    private void OnDisable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.playSFXEvent -= PlaySound;
            messaging_Service.playUISFXEvent -= PlaySoundUI;
        }
    }

    private void PlaySound(string sfxName, Vector3 position)
    {
        SoundEffect foundSfx = soundEffects.Find(item => item.soundName == sfxName);
        if (foundSfx.clip != null)
        {
            float vol = foundSfx.volume > 0 ? foundSfx.volume : 1f;
            AudioSource.PlayClipAtPoint(foundSfx.clip, position, vol);
        }
        else
        {
            Debug.LogWarning($"Sound '{sfxName}' nicht gefunden oder Clip fehlt!");
        }
    }

    private void PlaySoundUI(string sfxName)
    {
        SoundEffect foundSfx = soundEffects.Find(item => item.soundName == sfxName);
        if (foundSfx.clip != null)
        {
            float vol = foundSfx.volume > 0 ? foundSfx.volume : 1f;
            AudioSource uiAudioSource = GetComponent<AudioSource>();
            uiAudioSource.PlayOneShot(foundSfx.clip, vol);
        }
        else
        {
            Debug.LogWarning($"UI-Sound '{sfxName}' nicht gefunden!");
        }
    }
}