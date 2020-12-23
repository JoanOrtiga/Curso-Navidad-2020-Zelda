using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIABLES
    public float velocitat = 4f;
    public Animator anim;


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
       
        //ANIMACIÓ
        anim.SetFloat("movX", moviment.x);
        anim.SetFloat("movY", moviment.y);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }


        //ATAC
        if (Input.GetButtonDown("Fire1"))
        {    
            anim.SetBool("isAtac", true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isAtac", false);
        }

    }
}
