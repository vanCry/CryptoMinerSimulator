using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollShop : MonoBehaviour
{
    private List<GameObject> Slides;

    [Header("Controllers")]
    private int panelCount;

    [Range(0, 500)]
    public int panelOffset;

    [Range(0f, 20f)]
    public float snapSpeed;

    [Range(0f, 10f)]
    public float scaleOffset;

    [Range(1f, 20f)]
    public float scaleSpeed;

    [Range(0.1f, 2f)]
    public float scaleFactor;

    [Header("Other Objects")]
    public ScrollRect scrollRect;

    [SerializeField] private RectTransform[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanID;

    [SerializeField]
    private bool isScrolling;

    private void Start()
    {
        contentRect = GetComponent<RectTransform>();
        panelCount = instPans.Length;

        pansPos = new Vector2[panelCount];
        pansScale = new Vector2[panelCount];
        for (int i = 0; i < panelCount; i++)
        {
            if (i == 0) continue;
            instPans[i].localPosition = new Vector2(instPans[i - 1].localPosition.x + instPans[i].GetComponent<RectTransform>().sizeDelta.x + panelOffset,
                instPans[i].localPosition.y);
            pansPos[i] = -instPans[i].localPosition;
        }
    }

    private void FixedUpdate()
    {
        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panelCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
            float scale = Mathf.Clamp(1 / (distance / panelOffset) * scaleOffset, 0.5f, 1.5f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].localScale.x, scale + scaleFactor, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].localScale.y, scale + scaleFactor, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].localScale = pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}