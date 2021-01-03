using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject player;
    public Text textVida;
    public Text textPunts;
   
    void Update()
    {
        textVida.text = player.GetComponent<Player>().vida.ToString();
        textPunts.text = player.GetComponent<Player>().punts.ToString();
    }
}
