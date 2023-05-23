using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    private static GameObject m_canvas;

    private GameObject m_overlay;

    private void Awake()
    {
        // Create a new, ad-hoc canvas that is not destroyed after loading the new scene
        // to more easily handle the fading code.
        m_canvas = new GameObject("TransitionCanvas");
        var canvas = m_canvas.AddComponent<Canvas>();
        canvas.sortingOrder = 10;
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        DontDestroyOnLoad(m_canvas);
    }

    public static void LoadScene(string scene){
        LoadScene(scene, 0.5f, Color.black);
    }

    public static void LoadScene(string scene, float duration, Color color)
    {
        var fade = new GameObject("Transition");
        fade.AddComponent<Transition>();
        fade.GetComponent<Transition>().StartFade(scene, duration, color);
        fade.transform.SetParent(m_canvas.transform, false);
        fade.transform.SetAsLastSibling();
    }

    private void StartFade(string scene, float duration, Color fadeColor)
    {
        StartCoroutine(RunFade(scene, duration, fadeColor));
    }

    // This coroutine performs the core work of fading out of the current scene
    // and into the new scene.
    private IEnumerator RunFade(string scene, float duration, Color fadeColor)
    {
        var bgTex = new Texture2D(1, 1);
        bgTex.SetPixel(0, 0, fadeColor);
        bgTex.Apply();

        m_overlay = new GameObject("ImageOverlay");
        var image = m_overlay.AddComponent<Image>();
        var rect = new Rect(0, 0, bgTex.width, bgTex.height);
        var sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);
        //image.material.mainTexture = bgTex;
        image.sprite = sprite;
        image.color = fadeColor;
        image.canvasRenderer.SetAlpha(0.0f);

        m_overlay.transform.localScale = new Vector3(1, 1, 1);
        m_overlay.GetComponent<RectTransform>().sizeDelta = m_canvas.GetComponent<RectTransform>().sizeDelta;
        m_overlay.transform.SetParent(m_canvas.transform, false);
        m_overlay.transform.SetAsFirstSibling();

        var time = 0.0f;
        var halfDuration = duration / 2.0f;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            image.canvasRenderer.SetAlpha(Mathf.InverseLerp(0, 1, time / halfDuration));
            yield return new WaitForEndOfFrame();
        }

        image.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForEndOfFrame();

        //SceneManager.LoadScene(scene);
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        
        time = 0.0f;
        while (time < halfDuration)
        {
            time += Time.deltaTime;
            image.canvasRenderer.SetAlpha(Mathf.InverseLerp(1, 0, time / halfDuration));
            yield return new WaitForEndOfFrame();
        }

        image.canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForEndOfFrame();

        Destroy(m_canvas);
    }
}
