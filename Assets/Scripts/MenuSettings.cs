using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
  public AudioMixer audioMixer;
  [SerializeField] Toggle toggleFullScreen;
  [SerializeField] Toggle toggleMediumScreen;

  [SerializeField] Toggle toggleSmallScreen;

  Resolution[] resolutions;
  [SerializeField] GameObject menuPannel;

  public static MenuSettings Instance;

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
  public void SetResolution(int resolutionIndex)
  {
    Resolution resolution = resolutions[resolutionIndex];
    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
  }
  public void SetVolumn(float volumn)
  {
    audioMixer.SetFloat("volumn", volumn);
  }
  public void SetSoundVolumn(float volumn)
  {
    audioMixer.SetFloat("sound", volumn);
  }
  public void SetFullScreen(bool isFullScreen)
  {
    Screen.fullScreen = isFullScreen;
    if (isFullScreen)
    {
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
      toggleFullScreen.enabled = true;
      toggleMediumScreen.enabled = true;
      toggleSmallScreen.enabled = false;
      Screen.SetResolution(960, 540, false);
      toggleFullScreen.isOn = false;
      toggleMediumScreen.isOn = false;
    }
  }
  public void Exit(){
    Application.Quit();
  }
}
