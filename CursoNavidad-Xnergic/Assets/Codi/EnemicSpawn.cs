using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemicSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemic;
    public float distanciaSpawn = 3;
    public float tempsEntreEnemics;
    public int vidaSpawn; //Les vegades que el player te que tocar el spawn per elimarlo

    int enemicRandom;
    float temps;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        if (distance < distanciaSpawn)
        {
            temps += Time.deltaTime;
           
            if (temps > tempsEntreEnemics)
            {
                print("SPAWN");
                enemicRandom = Random.Range(0, enemic.Length);
                Instantiate(enemic[enemicRandom], transform.position, transform.rotation);
                temps = 0;
            }
        }

        if (vidaSpawn <= 0)
        {
            Destroy(gameObject);
        }
    }
}
