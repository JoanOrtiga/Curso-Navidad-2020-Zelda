using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInici : MonoBehaviour
{
   public void Jugar()
    {
        SceneManager.LoadScene(1);
    }
    public void SurtirJoc()
    {
        print("EXIT");
        Application.Quit();
    }
}
