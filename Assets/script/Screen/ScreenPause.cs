using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenPause : MonoBehaviour
{
    string NombreEscena;
    public void Pausar()
    {
        
    }

   public void ReanudarPartida()
    {

    }

    public void Salir()
    {
        SceneManager.LoadScene(NombreEscena);
    }

    public void Controloes()
    {

    }
}
