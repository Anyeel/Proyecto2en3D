using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Spawn : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int amount = 3;

    [SerializeField] float maxX = 5;
    [SerializeField] float maxY = 5;
    [SerializeField] float maxZ = 5;

    [SerializeField] float minX = -5;
    [SerializeField] float minY = -5;
    [SerializeField] float minZ = -5;

    [SerializeField] Gradient colors;

    [SerializeField] Vector3 size;
    [SerializeField] Vector3 speed;

    private GameObject instantiateObject;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        Movement();
    }
    
    void Spawn()
    {
        for (int i = 0; i > amount; i++)
        {
            speed = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));
            size = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));
            instantiateObject = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            instantiateObject.transform.localScale = size;
            //instantiateObject.GetComponent<MeshRenderer>().material.color = colors.Evaluate(); hacer que se reparta con todos los colores
        }

    }
    void Movement()
    {
        instantiateObject.transform.position += speed; //script aparte
    }
    void Teleport()
    {
        if (maxX > instantiateObject.transform.position.x) instantiateObject.transform.position = new Vector3(minX, instantiateObject.transform.position.y, instantiateObject.transform.position.z);
        if (maxY > instantiateObject.transform.position.y) instantiateObject.transform.position = new Vector3(instantiateObject.transform.position.x, minY, instantiateObject.transform.position.z);
        if (maxZ > instantiateObject.transform.position.z) instantiateObject.transform.position = new Vector3(instantiateObject.transform.position.x, instantiateObject.transform.position.y, minZ);

        if (minX < instantiateObject.transform.position.x) instantiateObject.transform.position = new Vector3(maxX, instantiateObject.transform.position.y, instantiateObject.transform.position.z);
        if (minY < instantiateObject.transform.position.y) instantiateObject.transform.position = new Vector3(instantiateObject.transform.position.x, maxY, instantiateObject.transform.position.z);
        if (minZ < instantiateObject.transform.position.z) instantiateObject.transform.position = new Vector3(instantiateObject.transform.position.x, instantiateObject.transform.position.y, maxZ);
    }
}
