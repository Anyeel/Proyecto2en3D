using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] GameObject collectiblePrefab;
    [SerializeField] float animationDuration = 2f;
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] float yOffset = 0.5f;
    [SerializeField] Vector3 maxHighOffset = new Vector3(0, 3f, 0);
    [SerializeField] GameObject[] spawnPositions;

    private GameObject currentCollectible;
    private Vector3 startHigh;
    private Vector3 maxHigh;

    void Start()
    {
        GameEvents.CollectibleEarned.AddListener(Spawn);
        Spawn(); 
        StartCoroutine(Movement()); 
    }

    IEnumerator Movement()
    {
        while (true)
        {

            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                float curveValue = speedCurve.Evaluate(elapsedTime / animationDuration);
                currentCollectible.transform.position = Vector3.Lerp(startHigh, maxHigh, curveValue);
                yield return null;
            }

            elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                float curveValue = speedCurve.Evaluate(elapsedTime / animationDuration);
                currentCollectible.transform.position = Vector3.Lerp(maxHigh, startHigh, curveValue);
                yield return null;
            }
        }
    }

    void Spawn()
    {
        int randomPosition = Random.Range(0, spawnPositions.Length);
        Vector3 spawnPosition = spawnPositions[randomPosition].transform.position + yOffset * Vector3.up;

        currentCollectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);

        startHigh = currentCollectible.transform.position;
        maxHigh = startHigh + maxHighOffset;
    }
}

