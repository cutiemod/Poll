using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WebSC : MonoBehaviour
{

    #region Variables declaration
    public float timeOutFadeDuration = 1;

    public string preloadSceneName = "Preload";
    public string resetSceneName = "menuIdle";
    public string[] timeoutSceneName;
    public int[] timeOutSeconds;

    private Animator animator;
    private string levelNameToLoad;

    public WebGM GM;

    private Coroutine resetCoroutine;

    private bool killAudio;

    AsyncOperation asyncLoad;
    #endregion

    private void Awake()
    {
        WebSC[] scs = FindObjectsOfType<WebSC>();
        if (scs.Length > 0)
        {
            foreach (WebSC sc in scs)
            {
                if (sc != this)
                {
                    Destroy(sc.gameObject);
                }
            }
        }
    }

    private void Start()
    {
        //DontDestroyOnLoad(this);

        animator = GetComponent<Animator>();
        //if (GameObject.Find(gameMasterName))
        //{
        //    GM = GameObject.Find(gameMasterName).GetComponent<GameMaster>();
        //    //GM.GetNewSceneChangerObject();
        //}

        if (SceneManager.GetActiveScene().name == preloadSceneName)
        {
            FadeToScene(resetSceneName, 1);
        }

        //GM.FindNewCamera();
    }

    public void FadeToScene(string levelName, float speedMultiplier, bool killAudio = false)
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine); //so we don't reset if we're already in a transition animation
        }

        //asyncLoad = SceneManager.LoadSceneAsync(levelName);
        //asyncLoad.allowSceneActivation = false;
        //StartCoroutine(LoadYourAsyncScene(levelName));
        if (levelName != "")
        {
            levelNameToLoad = levelName;
        }
        else
        {
            levelNameToLoad = resetSceneName;
        }

        animator.SetTrigger("FadeOut");
        animator.SetFloat("speedMultiplier", 1 / speedMultiplier);
    }

    IEnumerator LoadScene(string level)
    {
        yield return null;

        //GM.EnablePanels(false);

        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        //var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        asyncLoad = SceneManager.LoadSceneAsync(level);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            //Debug.Log("Loading progress: " + (progress * 100) + "%");
            if (asyncLoad.progress == 0.9f)
            {
                //Debug.Log("Loading progress: " + (progress * 100) + "%");
                //Debug.Log("Loading complete!");
                asyncLoad.allowSceneActivation = true;
                //TimeoutSetup();
                //animator.SetTrigger("FadeIn");
                //yield break;
            }
            yield return null;
        }

        if (killAudio)
        {
            AudioController ac = FindObjectOfType<AudioController>() ?? null;
            if(ac != null)
            {
                Destroy(ac.gameObject);
            }
        }

        //asyncLoad.allowSceneActivation = true;
        TimeoutSetup();
        GM.FindNewCamera();
        animator.SetTrigger("FadeIn");
        yield break;
    }

    public void OnFadeOutComplete()
    {
        StartCoroutine(LoadScene(levelNameToLoad));
        //if (asyncLoad != null)
        //{
        //    asyncLoad.allowSceneActivation = true;
        //}
    }

    private void TimeoutSetup()
    {
        int i = System.Array.IndexOf(timeoutSceneName, SceneManager.GetActiveScene().name);

        bool flag = false;

        //if (FindObjectOfType<DemoController>() != null)
        //{
        //    flag = FindObjectOfType<DemoController>().active;
        //}

        if (i > -1 && !flag)
        {
            resetCoroutine = StartCoroutine(TimeoutReset(timeOutSeconds[i]));
        }
        else if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }
    }

    internal IEnumerator TimeoutReset(int time)
    {
        float timePassed = 0;

        while (timePassed < time)
        {
            if (Input.anyKey)
            {
                timePassed = 0;
            }

            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Timeout!");
        FadeToScene(resetSceneName, timeOutFadeDuration);
        yield break;
    }

    internal void RestartGame()
    {
        GM.SetText("");
        GetComponent<Animator>().SetTrigger("fadeOut");
    }

    internal void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
