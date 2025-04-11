using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Animation : MonoBehaviour
{
    [SerializeField] float animationDuration = 2f;
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] CanvasGroup pointsText;
    [SerializeField] float screenSeconds = 2f;
    Coroutine coroutine = null;

    void Start()
    {
        GameEvents.CollectibleEarned.AddListener(PointsChanged);
        GameEvents.ObstacleHit.AddListener(PointsChanged);
    }

    void Update()
    {
        
    }

    void PointsChanged()
    {
        if (coroutine == null) coroutine = StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float evaluatedCurve = speedCurve.Evaluate(elapsedTime / animationDuration);
            pointsText.alpha = Mathf.Lerp(0, 1, evaluatedCurve);
            yield return null;
        }

        yield return new WaitForSeconds(screenSeconds);
        elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float evaluatedCurve = speedCurve.Evaluate(elapsedTime / animationDuration);
            pointsText.alpha = Mathf.Lerp(1, 0, evaluatedCurve);
            yield return null;
        }

        coroutine = null;
    }
}
