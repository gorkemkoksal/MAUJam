using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private RectTransform mainMenuTransform;
    [SerializeField] private RectTransform settingsTransform;
    [SerializeField] private RectTransform creditsTransform;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    private Transform currentScene;
    void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener((v) => { PlayerPrefs.SetFloat("MusicVolume", v); print(PlayerPrefs.GetFloat("MusicVolume")); });
        effectsVolumeSlider.onValueChanged.AddListener((ev) => { PlayerPrefs.SetFloat("EffectsVolume", ev); });
    }
    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void OnSettings()
    {
        mainMenuTransform.DOMoveY(mainMenuTransform.position.y + 400f, 0.5f).OnComplete(() => { settingsTransform.DOMoveY(settingsTransform.position.y + 400f, 1f); });
        currentScene = settingsTransform;
    }
    public void OnCredits()
    {
        mainMenuTransform.DOMoveY(mainMenuTransform.position.y + 400f, 0.5f).OnComplete(() => { creditsTransform.DOMoveY(creditsTransform.position.y + 400f, 1f); });
        currentScene = creditsTransform;
    }
    public void OnBack()
    {
        currentScene.DOMoveY(currentScene.position.y - 400f, 0.5f).OnComplete(() => { mainMenuTransform.DOMoveY(mainMenuTransform.position.y - 400f, 1f); });
    }
}
