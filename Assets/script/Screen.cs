using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screen : MonoBehaviour
{


    /// <summary>
    /// Función para cargar la escena desde fuera.
    /// </summary>
    /// <param name="NombreEscena"></param>
    public void CargarPantalla(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);         //Método para cargar una escena.
    }

    /// <summary>
    /// Función para que cierre la aplicación.
    /// </summary>
    public void Salir()
    {
        Application.Quit();
    }


}
