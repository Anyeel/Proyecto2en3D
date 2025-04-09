using System.Collections;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float minSpeed = 2f;
    [SerializeField] float maxYOffset = 5f; 
    [SerializeField] float minYOffset = -5f; 
    [SerializeField] GameObject[] obstacles;
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] float animationDuration = 2f;

    void Start()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            StartCoroutine(MoveObstacle(obstacles[i]));
        }
    }

    IEnumerator MoveObstacle(GameObject obstacle)
    {
        Vector3 startPosition = obstacle.transform.position;
        Vector3 targetPosition = GetRandomTargetPosition(startPosition);
        float speed = Random.Range(minSpeed, maxSpeed);
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime * speed;
            float curveValue = speedCurve.Evaluate(elapsedTime / animationDuration);
            obstacle.transform.position = Vector3.Lerp(startPosition, targetPosition, curveValue);
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime * speed;
            float curveValue = speedCurve.Evaluate(elapsedTime / animationDuration);
            obstacle.transform.position = Vector3.Lerp(targetPosition, startPosition, curveValue);
            yield return null;
        }

        StartCoroutine(MoveObstacle(obstacle));
    }

    Vector3 GetRandomTargetPosition(Vector3 startPosition)
    {
        float randomYOffset = Random.Range(minYOffset, maxYOffset);
        return new Vector3(startPosition.x, startPosition.y + randomYOffset, startPosition.z);
    }
}
