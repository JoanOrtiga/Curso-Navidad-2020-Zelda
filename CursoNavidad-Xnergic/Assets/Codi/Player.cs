using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIABLES

    //MOVIMENT
    public float velocitat = 4f;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moviment;

    //STATS JOC
    public int vida;
    public int punts;

    //ATAC
    private bool atacant;


    // Start es crida una vegada al principi
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }



    private void FixedUpdate()
    {
        Vector2 direction = Vector2.ClampMagnitude(moviment, 1f);

        rb.MovePosition(new Vector2(rb.position.x + direction.x * velocitat * Time.deltaTime, rb.position.y + direction.y * velocitat * Time.deltaTime));
    }

    // Update es crida per cada frame
    void Update()
    {
        //MOVIMENT
        moviment = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //ANIMACIÓ
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetFloat("MovX", moviment.x);
            anim.SetFloat("MovY", moviment.y);

            anim.SetBool("Moventse", true);
        }
        else
        {
            anim.SetBool("Moventse", false);
        }

        //ATAC
        if (Input.GetButton("Fire1"))
        {
            atacant = true;
            anim.SetBool("Atacan", true);
        }
        else
        {
            atacant = false;
            anim.SetBool("Atacan", false);
        }

        if(vida <= 0)
        {
            print("GAME OVER");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (atacant == true)
        {
            if (collision.CompareTag("Enemic"))
            {
                
                collision.GetComponent<Enemic>().vidaActual -= 1;
            }

            if (collision.CompareTag("Destructible"))
            {
                collision.GetComponent<EnemicSpawn>().vidaSpawn -= 1;

            }
        }
        
    }

    public void RebreDany(int dany)
    {
        vida -= dany;
    }
}
