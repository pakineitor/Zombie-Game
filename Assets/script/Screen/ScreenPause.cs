using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenPause : MonoBehaviour
{
    string NombreEscena;
    public GameObject PausaMenu;
    public GameObject PantallaControles;
    public GameObject Pakineitor;

    public void Pausar()
    {
       
        PausaMenu.SetActive(true);
        Time.timeScale = 0;
        Pakineitor.GetComponent<MovimientosPersonaje>().setIsDisparar(false);
    }

   public void ReanudarPartida()
    {
        Time.timeScale = 1;
        PausaMenu.SetActive(false);
        Pakineitor.GetComponent<MovimientosPersonaje>().setIsDisparar(true);
    }

    /// <summary>
    /// Método que cargará la escena de salir.
    /// </summary>
    public void Salir(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);
        Time.timeScale = 1;
        PausaMenu.SetActive(false);

    }

    public void Controloes()
    {
        PantallaControles.SetActive(true);
   
    }

    public void VolverMenuPausa()
    {
        PantallaControles.SetActive(false);
    }
}
