using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecollirObjectes : MonoBehaviour
{
    public int punts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().punts += punts;
            Destroy(gameObject);
        }
    }
}
