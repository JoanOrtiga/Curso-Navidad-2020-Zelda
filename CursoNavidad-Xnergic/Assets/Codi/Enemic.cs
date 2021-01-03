using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemic : MonoBehaviour
{
    public enum EstatEnemic
    {
        patrullar, perseguir, atacar
    }

    private EstatEnemic estatActual = EstatEnemic.patrullar;

    public int vidaMaxima = 3;
    [HideInInspector]
    public int vidaActual;

    public float radiPatrulla = 5f;
    private Vector2 posicioRandom;
    public float marge;
    public float tempsMaximPatrulla = 2.5f;
    private float temporitzadorPatrulla;
    private Animator anim;

    public float velocitatMoviment = 6;

    public Transform player;
    private Rigidbody2D rb;


    public float distanciaDeVisio;

    public float distanciaAtac = 0.5f;
    public int malAtac = 1;
    public float atacCooldown = 1f;
    public float cooldownAtacTimer;

    Vector2 posicioInicial;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();

        posicioInicial = transform.position;
        posicioRandom = posicioInicial + Random.insideUnitCircle * radiPatrulla;
        vidaActual = vidaMaxima;
    }

    private void Update()
    {
        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
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

            case EstatEnemic.atacar:
                Atacar();
                break;
        }

        VeigJugador();
    }

    public void Patrullar()
    {
        if ((posicioRandom - (Vector2)transform.position).magnitude < marge)
        {
            posicioRandom = posicioInicial + Random.insideUnitCircle * radiPatrulla;
        }
        else
        {
            Vector2 direccio = posicioRandom - (Vector2)transform.position;

            rb.MovePosition(new Vector2(rb.position.x + direccio.x * velocitatMoviment * Time.deltaTime, rb.position.y + direccio.y * velocitatMoviment * Time.deltaTime));

            temporitzadorPatrulla -= Time.deltaTime;

            anim.SetBool("Moventse", true);
            anim.SetBool("Atacan", false);
            anim.SetFloat("MovX", direccio.normalized.x);
            anim.SetFloat("MovY", direccio.normalized.y);

            if (temporitzadorPatrulla <= 0)
            {
                posicioRandom = posicioInicial + Random.insideUnitCircle * radiPatrulla;
                temporitzadorPatrulla = tempsMaximPatrulla;
            }
        }     
    }

    public void Perseguir()
    {
        Vector2 direccio = (Vector2)player.position - (Vector2)transform.position;

        rb.MovePosition(new Vector2(rb.position.x + direccio.x * velocitatMoviment * Time.deltaTime, rb.position.y + direccio.y * velocitatMoviment * Time.deltaTime));

        anim.SetBool("Moventse", true);
        anim.SetBool("Atacan", false);
        anim.SetFloat("MovX", direccio.normalized.x);
        anim.SetFloat("MovY", direccio.normalized.y);

        
    }

    private void Atacar()
    {
        if (cooldownAtacTimer < 0)
        {
            player.GetComponent<Player>().RebreDany(malAtac);
            anim.SetBool("Moventse", false);
            anim.SetBool("Atacan", true);
            cooldownAtacTimer = atacCooldown;
        }
        cooldownAtacTimer -= Time.deltaTime;
        
    }

    private void VeigJugador()
    {
        float distancia = (player.position - transform.position).magnitude;

        
        if ((player.position - transform.position).magnitude < distanciaAtac)
        {    
            estatActual = EstatEnemic.atacar;
        }
        else if (distancia < distanciaDeVisio && !Physics.Linecast(transform.position, player.position))
        {          
            estatActual = EstatEnemic.perseguir;
        }
        else
        {
            estatActual = EstatEnemic.patrullar;
        }
    }

    public void RebreDany(int dany)
    {
        vidaActual -= dany;

        if (vidaActual <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
