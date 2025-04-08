using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] GameObject collectiblePrefab;
    [SerializeField] float animationDuration = 2f;
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] float speed = 5f;
    [SerializeField] Vector3 maxHighOffset = new Vector3(0, 3f, 0);
    [SerializeField] Vector3 maxPosition = new Vector3(49, 0.5f, 49);
    [SerializeField] Vector3 minPosition = new Vector3(-49, 0.5f, -49);

    private GameObject currentCollectible; // Referencia al objeto actual
    private Vector3 startHigh;
    private Vector3 maxHigh;

    void Start()
    {
        GameEvents.CollectibleEarned.AddListener(Spawn);
        Spawn(); // Crear el primer objeto
        StartCoroutine(Movement()); // Iniciar el movimiento
    }

    IEnumerator Movement()
    {
        while (true)
        {
            if (currentCollectible == null) yield break; // Salir si no hay objeto

            float elapsedTime = 0f;

            // Movimiento hacia arriba
            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime * speed;
                float curveValue = speedCurve.Evaluate(elapsedTime / animationDuration);
                currentCollectible.transform.position = Vector3.Lerp(startHigh, maxHigh, curveValue);
                yield return null;
            }

            elapsedTime = 0f;

            // Movimiento hacia abajo
            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime * speed;
                float curveValue = speedCurve.Evaluate(elapsedTime / animationDuration);
                currentCollectible.transform.position = Vector3.Lerp(maxHigh, startHigh, curveValue);
                yield return null;
            }
        }
    }

    void Spawn()
    {
        // Generar una posición aleatoria dentro del rango
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );

        currentCollectible = Instantiate(collectiblePrefab, randomPosition, Quaternion.identity);

        // Actualizar las posiciones de movimiento
        startHigh = currentCollectible.transform.position;
        maxHigh = startHigh + maxHighOffset;
    }
}

