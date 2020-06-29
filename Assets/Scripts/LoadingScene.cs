using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingBar;
    public GameObject loadingDot;
    public GameObject logo;
    public Text percent;
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("HomeScene");
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / .9f);
            percent.text = (progress * 100f).ToString(CultureInfo.CurrentCulture);
            loadingDot.transform.Rotate(0f, 0f, Time.deltaTime * 20f);
            if (asyncOperation.progress == 0.9f)
            {
                loadingBar.SetActive(false);
                logo.SetActive(true);
                yield return new WaitForSeconds(3.0f);
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        logo.SetActive(false);
        loadingBar.SetActive(true);
        StartCoroutine(LoadAsyncScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
