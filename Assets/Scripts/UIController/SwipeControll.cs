using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SwipeControll : MonoBehaviour
{
    private RectTransform rectTransform;
    private int crrPageIndex = 0;
    private HorizontalLayoutGroup layoutGroup;

    private List<RectTransform> childList = new List<RectTransform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSwipe()
    {
        rectTransform = GetComponent<RectTransform>();
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        foreach (RectTransform child in rectTransform)
        {
            childList.Add(child);
        }
    }

    private void SwipeToIndex(int index)
    {
        RectTransform child = childList[index];
        float posX = child.rect.width + layoutGroup.spacing;
        rectTransform.anchoredPosition = new Vector2(-1 * index * posX, rectTransform.anchoredPosition.y);
    }

    public void ClickPrevious()
    {
        crrPageIndex = Mathf.Clamp(--crrPageIndex, 0, childList.Count - 1);
        SwipeToIndex(crrPageIndex);
    }

    public void ClickNext()
    {
        crrPageIndex = Mathf.Clamp(++crrPageIndex, 0, childList.Count - 1);
        SwipeToIndex(crrPageIndex);
    }
}
