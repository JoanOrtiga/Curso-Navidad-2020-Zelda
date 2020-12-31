using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIABLES
    public float velocitat = 4f;
    public Animator anim;
    public Rigidbody2D rb;

    public int vida;
    public int punts;


    // Start es crida una vegada al principi
    void Start()
    {
        
    }


    // Update es crida per cada frame
    void Update()
    {
        //MOVIMENT
        Vector3 moviment = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        //MoveTowards(Lloc de inici, On volem anar, la velocitat)
        this.transform.position = Vector3.MoveTowards(transform.position,
                                                      transform.position + moviment,
                                                      velocitat * Time.deltaTime);

        //rb.velocity = new Vector2(moviment.x, moviment.y);

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
