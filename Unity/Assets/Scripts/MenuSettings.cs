using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
  public AudioMixer audioMixer;
  [SerializeField] AudioMixerGroup mixerGroup;
  [SerializeField] Toggle toggleFullScreen;
  [SerializeField] Toggle toggleMediumScreen;
  [SerializeField] Slider sliderSound;
  [SerializeField] Slider sliderSfx;


  [SerializeField] Toggle toggleSmallScreen;
  [SerializeField] GameObject menuPannel;

  [Header("Sound")]
  [SerializeField] AudioClip pickUpItem;
  [SerializeField] float volumePick = 0.3F;
  [SerializeField] AudioClip audioSell;
  [SerializeField] float volumeSell = 0.2F;
  [SerializeField] AudioClip audioBuy;
  [SerializeField] float volumeBuy = 0.3F;

  [SerializeField] AudioClip audioFail;
  [SerializeField] float volumeFail = 0.4F;
  [SerializeField] AudioClip impactOnGroundAudio;
  [SerializeField] float volumeImpactGround = 0.3F;
  [SerializeField] AudioClip audioComplete;
  [SerializeField] float volumeComplete = 0.3F;

  const string MIXER_MUSIC = "MusicVolume";
  const string MIXER_SFX = "SFXVolume";

  public static MenuSettings Instance;
  public System.Guid userId;
  public int money { get; set; }

  public Vector3 playVector { get; set; }
  public string sceneName { get; set; }
  public bool firstStart {get; set;}  = true;
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
  void Start()
  {
    ToggleMenu();
    Screen.fullScreen = true;
    toggleFullScreen.enabled = false;
    SetSoundVolume(PlayerPrefs.GetFloat("MIXER_SFX", 60));
    SetVolume(PlayerPrefs.GetFloat("MIXER_MUSIC", 5));
    if (sliderSfx != null && sliderSound != null)
    {
      sliderSfx.value = PlayerPrefs.GetFloat("MIXER_SFX", 60);
      sliderSound.value = PlayerPrefs.GetFloat("MIXER_MUSIC", 5);
    }
    SetScreen();
  }

  private void SetScreen()
  {
    switch (PlayerPrefs.GetString("screen", "full"))
    {
      case "medium":
        SetMediumScreen(true);
        toggleMediumScreen.isOn = true;
        break;
      case "small":
        SetSmallScreen(true);
        toggleSmallScreen.isOn = true;
        break;
      default:
        SetFullScreen(true);
        toggleFullScreen.isOn = true;
        break;
    }
  }

  public void ToggleMenu()
  {
    if (menuPannel != null)
    {
      if (!menuPannel.activeSelf)
      {
        menuPannel.SetActive(true);
      }
      else
      {
        menuPannel.SetActive(false);
      }
    }
  }
  public bool GetStatusMenuSetting()
  {
    return menuPannel.activeSelf;
  }

  public void SetVolume(float volume)
  {
    PlayerPrefs.SetFloat("MIXER_MUSIC", volume);
    audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 30);
  }
  public void SetSoundVolume(float volume)
  {
    PlayerPrefs.SetFloat("MIXER_SFX", volume);
    audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 30);
  }
  public void SetFullScreen(bool isFullScreen)
  {
    Screen.fullScreen = isFullScreen;
    if (isFullScreen)
    {
      PlayerPrefs.SetString("screen", "full");
      toggleFullScreen.enabled = false;
      toggleMediumScreen.enabled = true;
      toggleSmallScreen.enabled = true;
      toggleMediumScreen.isOn = false;
      toggleSmallScreen.isOn = false;
    }
  }

  public void SetMediumScreen(bool isMediumScreen)
  {
    if (isMediumScreen)
    {
      PlayerPrefs.SetString("screen", "medium");
      toggleFullScreen.enabled = true;
      toggleMediumScreen.enabled = false;
      toggleSmallScreen.enabled = true;
      Screen.SetResolution(1280, 720, false);
      toggleFullScreen.isOn = false;
      toggleSmallScreen.isOn = false;
    }
  }

  public void SetSmallScreen(bool isSmallScreen)
  {
    if (isSmallScreen)
    {
      PlayerPrefs.SetString("screen", "small");
      toggleFullScreen.enabled = true;
      toggleMediumScreen.enabled = true;
      toggleSmallScreen.enabled = false;
      Screen.SetResolution(960, 540, false);
      toggleFullScreen.isOn = false;
      toggleMediumScreen.isOn = false;
    }
  }
  public void Exit()
  {
    Application.Quit();
  }
  public void SaveGame()
  {
    if (SceneManager.GetActiveScene().name != "Sence Start")
    {
      GameManager.instance.SaveDataAndPerformActions(null);
    }
  }
  public void SoundPickItem()
  {
    PlayAudioCustom(pickUpItem, volumePick);
  }
  public void SoundBuyItem()
  {
    PlayAudioCustom(audioBuy, volumeBuy);
  }
  public void SoundFail()
  {
    PlayAudioCustom(audioFail, volumeFail);
  }
  public void SoundSellItem()
  {
    PlayAudioCustom(audioSell, volumeSell);
  }
  public void SoundImpactGround()
  {
    PlayAudioCustom(impactOnGroundAudio, volumeImpactGround);
  }
  public void SoundComplete(Vector3Int posistion)
  {
    GameObject tempGO = new GameObject("TempAudio");
    tempGO.transform.position = posistion;
    AudioSource audioSource = tempGO.AddComponent<AudioSource>();
    audioSource.outputAudioMixerGroup = mixerGroup;
    audioSource.PlayOneShot(audioComplete, volumeComplete);
    Destroy(tempGO, audioComplete.length);
  }
  public void PlayAudioCustom(AudioClip clip, float volume)
  {
    GameObject tempGO = new GameObject("TempAudio");
    tempGO.transform.position = Camera.main.transform.position;
    AudioSource audioSource = tempGO.AddComponent<AudioSource>();
    audioSource.outputAudioMixerGroup = mixerGroup;
    audioSource.PlayOneShot(clip, volume);
    Destroy(tempGO, clip.length);
  }
}
