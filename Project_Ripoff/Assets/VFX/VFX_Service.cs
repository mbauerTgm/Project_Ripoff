using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
/**
 * Die Klasse bietet eine Funktionalität als VFX Service.
 * Dabei können andere Klassen visuelle Effekte ausführen ohne gekoppelt zu sein 
 * bzw. sich gegeseitig zu kennen.
 * Dokumentation/Guideline zur Implementierung ist unter 
 * "https://projekte.tgm.ac.at/youtrack/articles/RIP-A-9/Observer-Pattern-Guideline-Ripoff-Disposable-Heroes" zu finden.
 * @author Viktor Bublinskyy
 * @version 1.0.0
*/
public class VFX_Service : MonoBehaviour
{
    [Serializable]
    public struct VisualEffectItem
    {
        public string visualName;
        public VisualEffect effect;
        [Range(0.5f, 5f)] public float playRate;
        public float destroyAfter;
    }

    [SerializeField] private List<VisualEffectItem> visualEffects;
    private Messaging_Service messaging_Service;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }

    private void OnEnable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.playVFXEvent += PlayVFX;
        }
    }

    private void OnDisable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.playVFXEvent -= PlayVFX;
        }
    }

    private void PlayVFX(string vfxName, Vector3 position)
    {
        VisualEffectItem foundVfx = visualEffects.Find(item => item.visualName == vfxName);

        if (foundVfx.visualName != null && foundVfx.effect != null)
        {
            VisualEffect newVfxInstance = Instantiate(foundVfx.effect, position, Quaternion.identity);
            newVfxInstance.playRate = foundVfx.playRate;
            newVfxInstance.Play();
            Destroy(newVfxInstance.gameObject, foundVfx.destroyAfter);
        }
        else
        {
            Debug.LogWarning($"VFX '{vfxName}' nicht gefunden oder Prefab fehlt!");
        }
    }
}
