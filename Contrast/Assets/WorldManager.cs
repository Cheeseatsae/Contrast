using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public static bool Paused = false;

    public float fadeDelay;
    public float fadeSpeed;
    
    public Canvas pauseCanvas;
    private CanvasGroup pauseFade;
    public CanvasGroup fade;

    private void Awake()
    {
        Paused = false;
        
        pauseFade = pauseCanvas.GetComponent<CanvasGroup>();
        pauseFade.alpha = 0;
        pauseCanvas.enabled = false;
        
        fade.alpha = 1;

        StartCoroutine(Fade(fade, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private IEnumerator Fade(CanvasGroup c, float FadeTo, float speedMod)
    {
        while (c.alpha != FadeTo)
        {
            yield return new WaitForSecondsRealtime(fadeDelay);

            c.alpha = Mathf.Lerp(c.alpha, FadeTo, fadeSpeed * speedMod);

            if (Mathf.Abs(c.alpha - FadeTo) < 0.03f) c.alpha = FadeTo;
        }
    }
    
    private void Pause()
    {
        if (!Paused)
        {
            pauseCanvas.enabled = true;
            pauseFade.alpha = 0;
            StopAllCoroutines();
            StartCoroutine(Fade(pauseFade, 0.75f, 2));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(UnpauseCoroutine(pauseFade, 0, 2));
        }
        
        Paused = !Paused;
        
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        } 
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        
    }

    public void Restart()
    {
        Time.timeScale = 1;

        Button[] b = pauseCanvas.gameObject.GetComponentsInChildren<Button>();
        foreach (Button B in b)
        {
            B.gameObject.SetActive(false);
        }
        
        StartCoroutine(RestartCoroutine(fade, 1, 1));
    }
    
    private IEnumerator UnpauseCoroutine(CanvasGroup c, float FadeTo, float speedMod)
    {
        yield return Fade(c, FadeTo, speedMod);

        pauseCanvas.enabled = false;
    }
    
    private IEnumerator RestartCoroutine(CanvasGroup c, float FadeTo, float speedMod)
    {
        yield return Fade(c, FadeTo, speedMod);

        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        
        Button[] b = pauseCanvas.gameObject.GetComponentsInChildren<Button>();
        foreach (Button B in b)
        {
            B.gameObject.SetActive(false);
        }
        
        StartCoroutine(QuitCoroutine(fade, 1));
    }
    
    private IEnumerator QuitCoroutine(CanvasGroup c, float FadeTo)
    {
        yield return Fade(c, FadeTo, 3);

        Application.Quit();
    }
}
