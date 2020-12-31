using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemicSpawn : MonoBehaviour
{
    public GameObject[] enemic;
    public float tempsEntreEnemics;
    public int vidaSpawn; //Les vegades que el player te que tocar el spawn per elimarlo

    int enemicRandom;
    float temps;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            temps += temps * Time.deltaTime;

            if (temps > tempsEntreEnemics)
            {
                enemicRandom = Random.Range(0, enemic.Length);
                Instantiate(enemic[enemicRandom], transform.position, transform.rotation);
                temps = 0;
            }
        }
    }
}
