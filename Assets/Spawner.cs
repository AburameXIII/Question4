using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int NumObstacles;
    public float Dimension;
    public GameObject AsteroidPrefab;

    void Start()
    {
        for (int i = 0; i < NumObstacles; i++)
        {
            Instantiate(AsteroidPrefab, RandomPosition(), Quaternion.identity);
        }
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-Dimension, Dimension),
                            Random.Range(-Dimension, Dimension),
                            Random.Range(-Dimension, Dimension));
    }
}

