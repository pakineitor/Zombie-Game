using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class screenManager : MonoBehaviour
{

    public GameObject  MenuPrincipal;
    public GameObject  PantallaAjustes;
    public Button      SonidoOn;
    public Button      SonidoOff;



    /// <summary>
    /// Función para cargar la escena desde fuera.
    /// </summary>
    /// <param name="NombreEscena"></param>
    public void CargarNivel1(string NombreEscena)
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

    /// <summary>
    /// 
    /// </summary>
    public void CargarPantallaAjustes()
    {
        PantallaAjustes.SetActive(true);
   
    }

    /// <summary>
    /// 
    /// </summary>
    public void VolverAlMenu()
    {
        PantallaAjustes.SetActive(false);

    }

    /// <summary>
    /// 
    /// </summary>
    public void ActivarSonido()
    {
        SonidoOff.gameObject.SetActive(true);
        SonidoOn.gameObject.SetActive(false);
    }
    /// <summary>
    /// 
    /// </summary>
    public void DesactivarSonido()
    {
        SonidoOff.gameObject.SetActive(false);
        SonidoOn.gameObject.SetActive(true);
    }

    
}
