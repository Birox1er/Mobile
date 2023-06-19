using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBand : MonoBehaviour
{
    public RectTransform targetImage;
    public RectTransform startPosition;
    public RectTransform endPosition;
    public float slideDuration = 1f;

    private Coroutine slideCoroutine;

    private void Start()
    {
        // Save the original position of the image as the start position
        startPosition.anchoredPosition = targetImage.anchoredPosition;
    }

    public void SlideImage()
    {
        if (slideCoroutine != null)
        {
            StopCoroutine(slideCoroutine);
        }

        slideCoroutine = StartCoroutine(SlideCoroutine(endPosition.anchoredPosition));
    }

    public void ReturnImage()
    {
        if (slideCoroutine != null)
        {
            StopCoroutine(slideCoroutine);
        }

        slideCoroutine = StartCoroutine(SlideCoroutine(startPosition.anchoredPosition));
    }

    private IEnumerator SlideCoroutine(Vector2 targetPosition)
    {
        float startTime = Time.time;
        Vector2 startPosition = targetImage.anchoredPosition;

        while (Time.time - startTime < slideDuration)
        {
            float t = (Time.time - startTime) / slideDuration;
            targetImage.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        targetImage.anchoredPosition = targetPosition;
        slideCoroutine = null;
    }
}