using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIABLES
    public float velocitat = 4f;
    private Animator anim;
    private Rigidbody2D rb;

    public int vida;
    public int punts;

    private Vector2 moviment;


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
        if (Input.GetButtonDown("Fire1"))
        {    
            anim.SetBool("Atacan", true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Atacan", false);
        }

    }
}
