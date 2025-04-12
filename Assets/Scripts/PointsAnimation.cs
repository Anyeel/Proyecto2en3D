using System.Collections;
using UnityEngine;

public class PointsAnimation : MonoBehaviour 
{
    [SerializeField] float animationDuration = 0.5f;
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] CanvasGroup pointsText;
    [SerializeField] float displayDuration = 2f;


    private Coroutine animationCoroutine = null;

    void Start()
    {
        GameEvents.CollectibleEarned.AddListener(PointsChanged);
        GameEvents.ObstacleHit.AddListener(PointsChanged);
    }

    void PointsChanged()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(PointsVisibility());
    }

    IEnumerator PointsVisibility()
    {
        float elapsedTime = 0f;
        float startAlpha = pointsText.alpha;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float curveValue = speedCurve.Evaluate(Mathf.Clamp01(elapsedTime / animationDuration));
            pointsText.alpha = Mathf.Lerp(startAlpha, 1f, curveValue);
            yield return null;
        }
        pointsText.alpha = 1f;

        yield return new WaitForSeconds(displayDuration);

        elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);
            float curveValue = speedCurve.Evaluate(Mathf.Clamp01(elapsedTime / animationDuration));
            pointsText.alpha = Mathf.Lerp(1f, 0f, curveValue);
            yield return null;
        }
        pointsText.alpha = 0f;

        animationCoroutine = null;
    }
}