using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemic : MonoBehaviour
{
    public enum EstatEnemic
    {
        patrullar, perseguir
    }

    private EstatEnemic estatActual = EstatEnemic.patrullar;

    public int vidaMaxima = 3;
    private int vidaActual;

    public float radiPatrulla = 5f;
    private Vector2 posicioRandom;
    public float marge;

    public float velocitatMoviment = 6;

    public Transform player;
    private Rigidbody2D rb;

    public LayerMask vista;
    public float distanciaDeVisio;

    private void Start()
    {
        posicioRandom = Random.insideUnitCircle * radiPatrulla;
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();

        vidaActual = vidaMaxima;
    }

    private void FixedUpdate()
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
            Vector2 direccio = posicioRandom - (Vector2)transform.position;
            
            rb.MovePosition(new Vector2(rb.position.x + direccio.x * velocitatMoviment * Time.deltaTime, rb.position.y + direccio.y * velocitatMoviment * Time.deltaTime));
        }

        VeigJugador();
    }

    public void Perseguir()
    {
        VeigJugador();

        Vector2 direccio = (Vector2)player.position - (Vector2)transform.position;

        rb.MovePosition(new Vector2(rb.position.x + direccio.x * velocitatMoviment * Time.deltaTime, rb.position.y + direccio.y * velocitatMoviment * Time.deltaTime));
    }

    private void VeigJugador()
    {
        float distancia = (player.position - transform.position).magnitude;

        if (distancia < distanciaDeVisio && !Physics.Linecast(transform.position, player.position, vista.value))
        {
            estatActual = EstatEnemic.perseguir;
        }
        else
        {
            estatActual = EstatEnemic.patrullar;
        }
    }

    public void GetDamage(int dany)
    {
        vidaActual -= dany;

        if(vidaActual <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
