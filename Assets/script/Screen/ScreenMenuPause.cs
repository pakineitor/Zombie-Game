using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenMenuPause : MonoBehaviour
{
    public GameObject pauseMenu;

    /// <summary>
    /// Método que va a llamar al menú de pause.
    /// </summary>
    public void PulsarBotonPause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void CargarPantalla(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Start()
    {
        pauseMenu.SetActive(false);
    }

}
