using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject objecteSpawn;

    private void OnDestroy()
    {
        Instantiate(objecteSpawn, transform.position, transform.rotation);
    }
}
