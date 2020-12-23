using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemic : MonoBehaviour
{
    public enum EstatEnemic
    {
        patrullar, perseguir
    }

    private EstatEnemic estatActual;

    public float radiPatrulla = 3f;
    private Vector2 posicioRandom;
    private float marge;

    public float velocitatMoviment = 6;

    public Transform player;

    public LayerMask vista;
    public float angleDeVisio;

    private Vector2 forward;
    private void Start()
    {
        posicioRandom = Random.insideUnitCircle * radiPatrulla;
        player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        switch (estatActual)
        {
            case EstatEnemic.patrullar:
                Patrullar();
                break;

            case EstatEnemic.perseguir:
                Perseguir();
                break;
        }
    }

    public void Patrullar()
    {
        if ((posicioRandom - (Vector2)transform.position).magnitude < marge)
        {
            posicioRandom = Random.insideUnitCircle * radiPatrulla;
        }
        else
        {
            Vector2.MoveTowards(transform.position, posicioRandom, velocitatMoviment * Time.deltaTime);
        }

        VeigJugador();
    }

    public void Perseguir()
    {
        VeigJugador();

        Vector2.MoveTowards(transform.position, player.position, velocitatMoviment * Time.deltaTime);
    }

    private void VeigJugador()
    {
        Vector2 direction = player.position - transform.position;

        bool isOnCone = Vector2.Angle(forward, direction.normalized) < angleDeVisio;

        if (isOnCone && !Physics.Linecast(transform.position, player.position, vista.value))
        {
            estatActual = EstatEnemic.perseguir;
        }
        else
        {
            estatActual = EstatEnemic.patrullar;
        }
    }
}
