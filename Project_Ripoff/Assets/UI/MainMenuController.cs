using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/**
 * Die Klasse bietet eine Funktionalität für den Main Menu und die Einstellungen.
 * Dabei werden die Einstellungen implementiert, sowie die Knöpfe die die einzelnen Bildschirme verbindet.
 * @author Viktor Bublinskyy
 * @version 1.0.0
 */
public class MainMenuController : MonoBehaviour
{
    //----------------------
    //Definitionen von überlegenden Variablen und Elementen
    //----------------------
    private VisualElement root;
    private Messaging_Service messaging_Service;
    private VisualElement mainButtons;
    private VisualElement settingsPanel;
    private VisualElement levelSelectPanel;
    // Einzelne UI Elemente
    private DropdownField languageDropdown;
    private Toggle tutorialToggle;
    private Toggle hudToggle;
    private DropdownField displayModeDropdown;
    private DropdownField resolutionDropdown;
    private DropdownField qualityDropdown;
    private Toggle vSyncToggle;
    private Slider masterSlider;
    private Slider musicSlider;
    private Slider sfxSlider;
    // Einstellungsvariablen
    private Resolution[] availableResolutions;
    private string cachedLanguage;
    private bool cachedTutorial;
    private bool cachedHud;
    private int cachedDisplayModeIndex;
    private int cachedResolutionIndex;
    private int cachedQualityIndex;
    private bool cachedVSync;
    private float cachedMasterVol;
    private float cachedMusicVol;
    private float cachedSfxVol;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }
    /**
     * Initialisiert alle Elemente und verbindet die Verbindungsknöpfe und Schließknöpfe mit den Methoden die definiert wurden. 
     * Und fügt sich zum Messaging Service hinzu  
     */
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        mainButtons = root.Q<VisualElement>("MainButtons");
        settingsPanel = root.Q<VisualElement>("Settings");
        levelSelectPanel = root.Q<VisualElement>("LevelSelect");
        root.Q<Button>("button-play").clicked += OnPlayClicked;
        root.Q<Button>("button-settings").clicked += OnSettingsClicked;
        root.Q<Button>("button-exit").clicked += OnExitClicked;
        settingsPanel.Q<Button>("button-close").clicked += CancelSettings;
        levelSelectPanel.Q<Button>("button-close").clicked += OnCloseLevelSelectClicked;
        levelSelectPanel.Q<Button>("button-level1").clicked += OnLevel1Clicked;
        root.Q<Button>("button-apply").clicked += ApplySettings;
        root.Q<Button>("button-cancel").clicked += CancelSettings;
        languageDropdown = root.Q<DropdownField>("dropdown-language");
        tutorialToggle = root.Q<Toggle>("toggle-tutorial");
        hudToggle = root.Q<Toggle>("toggle-hud");
        displayModeDropdown = root.Q<DropdownField>("dropdown-displaymode");
        resolutionDropdown = root.Q<DropdownField>("dropdown-resolution");
        qualityDropdown = root.Q<DropdownField>("dropdown-quality");
        vSyncToggle = root.Q<Toggle>("toggle-vsync");
        masterSlider = root.Q<Slider>("slider-mastervolume");
        musicSlider = root.Q<Slider>("slider-musicvolume");
        sfxSlider = root.Q<Slider>("slider-sfxvolume");
        InitializeDropdowns();
        mainButtons.style.display = DisplayStyle.Flex;
        settingsPanel.style.display = DisplayStyle.None;
        levelSelectPanel.style.display = DisplayStyle.None;

        if (messaging_Service != null)
        {
            messaging_Service.toggleSettings += toggleSettings;
        }
        RegisterUISounds();
    }
    /** 
     * Deabonniert zum Event
     */
    private void OnDisable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.toggleSettings -= toggleSettings;
        }
    }

    private void PlayUISound(string soundName)
    {
        if (messaging_Service != null)
        {
            messaging_Service.playUISFXEvent?.Invoke(soundName);
        }
    }

    private void RegisterUISounds()
    {
        root.Query<Button>().ForEach(RegisterEvents);
        root.Query<Toggle>().ForEach(RegisterEvents);
        root.Query<DropdownField>().ForEach(RegisterEvents);
        root.Query<VisualElement>(className: "unity-tab__header").ForEach(RegisterEvents);
    }

    private void RegisterEvents(VisualElement element)
    {
        element.UnregisterCallback<MouseEnterEvent>(OnHover);
        element.UnregisterCallback<ClickEvent>(OnClick);

        element.RegisterCallback<MouseEnterEvent>(OnHover);
        element.RegisterCallback<ClickEvent>(OnClick);
    }

    private void OnHover(MouseEnterEvent evt)
    {
        PlayUISound("UIHover");
    }

    private void OnClick(ClickEvent evt)
    {
        if (evt.currentTarget is VisualElement ve && !ve.enabledSelf) return;

        PlayUISound("UIClick");
    }
    /**
     * Befüllt alle Dropdowns (Sprache, DisplayMode, Quality) in den Einstellungen mit den erwünschten Daten.
     */
    private void InitializeDropdowns()
    {
        languageDropdown.choices = new List<string> { "English", "Deutsch" };
        displayModeDropdown.choices = new List<string> { "Fullscreen", "Windowed" };
        qualityDropdown.choices = new List<string>(QualitySettings.names);

        availableResolutions = Screen.resolutions.Select(r => new Resolution { width = r.width, height = r.height }).Distinct().ToArray();
        List<string> resOptions = new List<string>();
        foreach (var r in availableResolutions)
        {
            resOptions.Add($"{r.width}x{r.height}");
        }
        resolutionDropdown.choices = resOptions;
    }

    /**
     * Holt sich die gespeicherten Daten und gibt sie in die cache Werte.
     */ 
    private void LoadSystemSettingsToCache()
    {
        cachedLanguage = PlayerPrefs.GetString("Language", "English");
        cachedTutorial = PlayerPrefs.GetInt("Tutorials", 1) == 1;
        cachedHud = PlayerPrefs.GetInt("ShowHUD", 1) == 1;

        if (PlayerPrefs.HasKey("DisplayModeIndex"))
        {
            cachedDisplayModeIndex = PlayerPrefs.GetInt("DisplayModeIndex");
        }else
        {
            cachedDisplayModeIndex = Screen.fullScreen ? 0 : 1;
        }

        if (PlayerPrefs.HasKey("QualityIndex"))
        {
            cachedQualityIndex = PlayerPrefs.GetInt("QualityIndex");
        }else
        {
            cachedQualityIndex = QualitySettings.GetQualityLevel();
        }
        if (PlayerPrefs.HasKey("VSync"))
        {
            cachedVSync = PlayerPrefs.GetInt("VSync") == 1;
        }else
        {
            cachedVSync = QualitySettings.vSyncCount > 0;
        }
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            cachedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            if (cachedResolutionIndex >= availableResolutions.Length){
                cachedResolutionIndex = availableResolutions.Length - 1;
            }
        }
        else
        {
            Resolution currentRes = Screen.currentResolution;
            cachedResolutionIndex = -1;
            for (int i = 0; i < availableResolutions.Length; i++)
            {
                if (availableResolutions[i].width == currentRes.width && availableResolutions[i].height == currentRes.height)
                {
                    cachedResolutionIndex = i;
                    break;
                }
            }
            if (cachedResolutionIndex == -1) cachedResolutionIndex = availableResolutions.Length - 1;
        }
        cachedMasterVol = PlayerPrefs.GetFloat("MasterVol", AudioListener.volume * 100f);
        cachedMusicVol = PlayerPrefs.GetFloat("MusicVol", 100f);
        cachedSfxVol = PlayerPrefs.GetFloat("SfxVol", 100f);
    }

    /**
     * Die Werte werden aus dem Cache genommen und gesetzt
     */  
    private void UpdateUIFromCache()
    {
        languageDropdown.value = cachedLanguage;
        tutorialToggle.value = cachedTutorial;
        hudToggle.value = cachedHud;
        displayModeDropdown.index = cachedDisplayModeIndex;
        resolutionDropdown.index = cachedResolutionIndex;
        qualityDropdown.index = cachedQualityIndex;
        vSyncToggle.value = cachedVSync;
        masterSlider.value = cachedMasterVol;
        musicSlider.value = cachedMusicVol;
        sfxSlider.value = cachedSfxVol;
    }

    /**
     * Hier werden nachdem Apply gedrückt werde die Werte gespeichert. Dabei wird geholt, was der Spieler eingestellt hat.
     */ 
    private void ApplySettings()
    {
        cachedLanguage = languageDropdown.value;
        cachedTutorial = tutorialToggle.value;
        cachedHud = hudToggle.value;
        cachedDisplayModeIndex = displayModeDropdown.index;
        cachedResolutionIndex = resolutionDropdown.index;
        cachedQualityIndex = qualityDropdown.index;
        cachedVSync = vSyncToggle.value;
        cachedMasterVol = masterSlider.value;
        cachedMusicVol = musicSlider.value;
        cachedSfxVol = sfxSlider.value;
        PlayerPrefs.SetString("Language", cachedLanguage);
        PlayerPrefs.SetInt("Tutorials", cachedTutorial ? 1 : 0);
        PlayerPrefs.SetInt("ShowHUD", cachedHud ? 1 : 0);
        PlayerPrefs.SetInt("DisplayModeIndex", cachedDisplayModeIndex);
        PlayerPrefs.SetInt("ResolutionIndex", cachedResolutionIndex);
        PlayerPrefs.SetInt("QualityIndex", cachedQualityIndex);
        PlayerPrefs.SetInt("VSync", cachedVSync ? 1 : 0);
        PlayerPrefs.SetFloat("MasterVol", cachedMasterVol);
        PlayerPrefs.SetFloat("MusicVol", cachedMusicVol);
        PlayerPrefs.SetFloat("SfxVol", cachedSfxVol);

        PlayerPrefs.Save();
        FullScreenMode mode = (cachedDisplayModeIndex == 0) ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;

        if (cachedResolutionIndex >= 0 && cachedResolutionIndex < availableResolutions.Length)
        {
            Resolution r = availableResolutions[cachedResolutionIndex];
            Screen.SetResolution(r.width, r.height, mode);
        }
        else
        {
            Screen.fullScreenMode = mode;
        }

        QualitySettings.SetQualityLevel(cachedQualityIndex);
        QualitySettings.vSyncCount = cachedVSync ? 1 : 0;
        AudioListener.volume = cachedMasterVol / 100f;

        settingsPanel.style.display = DisplayStyle.None;
        mainButtons.style.display = DisplayStyle.Flex;
    }

    /**
     * Das Fenster wird geschlossen wenn auf Cancel gedrückt wird
     */ 
    private void CancelSettings()
    {
        settingsPanel.style.display = DisplayStyle.None;
        mainButtons.style.display = DisplayStyle.Flex;
    }

    /**
     * Schließt die Fenster wenn Play gedrückt wird
     */ 
    private void OnPlayClicked()
    {
        PlayUISound("UIWindowOpen");
        mainButtons.style.display = DisplayStyle.None;
        levelSelectPanel.style.display = DisplayStyle.Flex;
    }

    /**
     * Holt die Daten aus dem Cache und updatet die UI vom Cache, wenn die Einstellungen geöffnet werden.
     */ 
    private void OnSettingsClicked()
    {
        LoadSystemSettingsToCache();
        UpdateUIFromCache();
        PlayUISound("UIWindowOpen");
        mainButtons.style.display = DisplayStyle.None;
        settingsPanel.style.display = DisplayStyle.Flex;
    }
    /**
     * Das Spiel wird geschlossen wenn Exit gedrückt wird
     */ 
    private void OnExitClicked()
    {
        Application.Quit();
    }
    /**
     * Das LevelSelect Fenster wird geschlossen wenn auf das X gedrückt wird.
     */ 
    private void OnCloseLevelSelectClicked()
    {
        levelSelectPanel.style.display = DisplayStyle.None;
        mainButtons.style.display = DisplayStyle.Flex;
    }
    /**
     * Für TDOT: Die Szene Prototyp wird beim Level 1 reingeladen
     */ 
    private void OnLevel1Clicked()
    {
        SceneManager.LoadScene("Prototyp");
    }
    /**
     * Hier wird das Messaging Service implementiert, was geplant wird wenn das Spiel weiter ist, um die Einstellungen öffnen zu können
     */ 
    private void toggleSettings()
    {
        if (settingsPanel.style.display == DisplayStyle.Flex)
        {
            CancelSettings();
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "UI")
            {
                OnSettingsClicked();
            }
            else
            {
                SceneManager.LoadScene("UI");
            }
        }
    }
}