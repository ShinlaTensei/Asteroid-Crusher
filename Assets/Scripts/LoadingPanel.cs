using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    public GameObject loadingDot;
    private bool isLoading;

    void OnEnable()
    {
        isLoading = true;
    }
    void Update()
    {
        if (isLoading)
        {
            loadingDot.transform.Rotate(0, 0, 150f * Time.deltaTime);
        }
    }

    void OnDisable()
    {
        isLoading = false;
    }
}
