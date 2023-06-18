using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenPause : MonoBehaviour
{
    string NombreEscena;
    public GameObject PausaMenu;
    public GameObject PantallaControles;
    public GameObject Pakineitor;
    public GameObject bt_mutear;
    public GameObject bt_desMutear;
    public GameObject Click;
    public GameObject Musica;
    public GameObject PantallaMuerte;
    public GameObject MusicaGameOver;

    /// <summary>
    /// M�todo que va a congelar/ pausar la partida.
    /// </summary>
    public void Pausar()
    {
        if (Sonidos.getSonidoNivel1() == true) Musica.GetComponent<AudioSource>().Stop();

        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        PausaMenu.SetActive(true);
        Time.timeScale = 0;
        Pakineitor.GetComponent<MovimientosPersonaje>().setIsDisparar(false);
    }

    /// <summary>
    /// M�todo que volver� a reanudar la partida si est� pausada.
    /// </summary>
   public void ReanudarPartida()
    {
        if(Sonidos.getSonidoNivel1()==true) Musica.GetComponent<AudioSource>().Play();

        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
       
        Time.timeScale = 1;
        PausaMenu.SetActive(false);
        Pakineitor.GetComponent<MovimientosPersonaje>().setIsDisparar(true);
    }

    /// <summary>
    /// M�todo que cargar� la escena de salir.
    /// </summary>
    public void Salir(string NombreEscena)
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        Sonidos.setMusicaGameOver(false);
        
        SceneManager.LoadScene(NombreEscena);
        Time.timeScale = 1;
        PausaMenu.SetActive(false);

        

    }


    /// <summary>
    /// M�todo que se va a encargar de mostrar la pantalla controles.
    /// </summary>
    public void Controloes()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        PantallaControles.SetActive(true);
   
    }

    /// <summary>
    /// M�todo que va a volver al men� de pausa desde la pantalla controles.
    /// </summary>
    public void VolverMenuPausa()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        PantallaControles.SetActive(false);
    }


    /// <summary>
    /// M�todo que va a encargarse de gestionar todos los sonidos del juego y m�sica.
    /// </summary>
    public void MutearSonido()
    {
        
        Sonidos.setMutearSonidos(true); //Silenciamos todos los sonidos tanto efectos como m�sica.
        Sonidos.setSonidoMenuPrincipal(false); //Paramos la m�sica del men� principal.
        Sonidos.setSonidoNivel1(false); //Paramos la m�sica del nivel.

        //Desactivas el sonido del nivel.
        bt_mutear.SetActive(false);
        bt_desMutear.SetActive(true);

       if(Sonidos.getMutearSonidos()        == false) Click.GetComponent<AudioSource>().Play();
       if(Sonidos.getSonidoNivel1()         == false) Musica.GetComponent<AudioSource>().Stop();
      
    }

    /// <summary>
    /// M�todo que va a encargarse de gestionar todos los sonidos del juego y m�sica.
    /// </summary>
    public void DesmutearSonido()
    {
        //Activar sonido
        bt_mutear.SetActive(true);
        bt_desMutear.SetActive(false);

        Sonidos.setMutearSonidos(false); //Silenciamos todos los sonidos tanto efectos como m�sica.
        Sonidos.setSonidoMenuPrincipal(true); //Paramos la m�sica del men� principal.
        Sonidos.setSonidoNivel1(true); //Paramos la m�sica del nivel.

        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        if (Sonidos.getSonidoNivel1() == true) Musica.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// M�todo que va a permitir empezar una nueva partida desde el game over.
    /// </summary>
    /// <param name="nombreEscena"></param>
    public void NuevaPartida(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
        Sonidos.setMusicaGameOver(false);
        Time.timeScale = 1f;
    }

    public void ComprobarVidaMaxima(int vidaMaxima)
    {
        if(vidaMaxima == 0)
        {
            PantallaMuerte.gameObject.SetActive(true);
            Time.timeScale = 0f;
            MutearSonido();
            Sonidos.setMusicaGameOver(false);
        }
    }

}
