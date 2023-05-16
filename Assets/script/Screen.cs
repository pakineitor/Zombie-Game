using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screen : MonoBehaviour
{


    /// <summary>
    /// Funci�n para cargar la escena desde fuera.
    /// </summary>
    /// <param name="NombreEscena"></param>
    public void CargarPantalla(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);         //M�todo para cargar una escena.
    }

    /// <summary>
    /// Funci�n para que cierre la aplicaci�n.
    /// </summary>
    public void Salir()
    {
        Application.Quit();
    }


}
