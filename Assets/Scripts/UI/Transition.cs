using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Transition : MonoBehaviour
{
    public static Transition Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private TextMeshProUGUI transitionText;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float textDisplayTime = 2f;

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

    private void Start()
    {
        if (transitionText != null)
        {
            transitionText.text = "";
            transitionText.gameObject.SetActive(false);
        }

        StartCoroutine(FadeIn());
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScenes(sceneName, null));
    }

    public void LoadSceneWithMessage(string sceneName, string message)
    {
        StartCoroutine(FadeAndSwitchScenes(sceneName, message));
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName, string message)
    {
        yield return StartCoroutine(FadeOut());

        if (!string.IsNullOrEmpty(message) && transitionText != null)
        {
            transitionText.text = message;
            transitionText.gameObject.SetActive(true);
            yield return new WaitForSeconds(textDisplayTime);
        }

        SceneManager.LoadScene(sceneName);

        yield return null;

        if (transitionText != null)
        {
            transitionText.gameObject.SetActive(false);
            transitionText.text = "";
        }

        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = 1f - (time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = time / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }
}