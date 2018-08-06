using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Svenardo;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    private List<GameObject> gameObjects;
    private int iterator = 0;

    void Start()
    {
        int size = 500;

        Dictionary<string, double> myDictionary = new Dictionary<string, double>
        {
            {"Sphere", 0.5},
            {"Cube", 0.1},
            {"Capsule", 0.2},
            {"Cylinder", 0.2}
        };

        WeightedRandom weighted = new WeightedRandom();
        List<string> values = weighted.RandomList(size, myDictionary);
        gameObjects = new List<GameObject>(size);
        
        for (int i = 0; i < values.Count; i++)
        {
            GameObject go;
            if (values[i].Equals("Sphere"))
            {
                go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (values[i].Equals("Cube"))
            {
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.GetComponent<Renderer>().material.color = Color.blue;
            }
            else if (values[i].Equals("Capsule"))
            {
                go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                go.GetComponent<Renderer>().material.color = Color.cyan;
            }
            else
            {
                go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                go.GetComponent<Renderer>().material.color = Color.green;
            }

            go.AddComponent<Rigidbody>();
            go.SetActive(false);
            Instantiate(go, Vector3.zero, Quaternion.identity);
            gameObjects.Add(go);
        }

        StartCoroutine(InvokeMethod(SpawnGameobject, gameObjects.Count));
    }

    public IEnumerator InvokeMethod(Action method, int invokeCount)
    {
        for (int i = 0; i < invokeCount; i++)
        {
            iterator = i;
            method();
            yield return new WaitForSeconds(.5f);
        }
    }

    private void SpawnGameobject()
    {
        Debug.Log("placing "+iterator);
        gameObjects[iterator].transform.position = new Vector3(Random.Range(-2, 2), Random.Range(15, 25), Random.Range(-5,5));
        gameObjects[iterator].SetActive(true);
    }
}