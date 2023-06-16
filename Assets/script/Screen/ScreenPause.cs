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
    public void Pausar()
    {
        if (Sonidos.getSonidoNivel1() == true) Musica.GetComponent<AudioSource>().Stop();

        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        PausaMenu.SetActive(true);
        Time.timeScale = 0;
        Pakineitor.GetComponent<MovimientosPersonaje>().setIsDisparar(false);
    }

   public void ReanudarPartida()
    {
        if(Sonidos.getSonidoNivel1()==true) Musica.GetComponent<AudioSource>().Play();

        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
       
        Time.timeScale = 1;
        PausaMenu.SetActive(false);
        Pakineitor.GetComponent<MovimientosPersonaje>().setIsDisparar(true);
    }

    /// <summary>
    /// Método que cargará la escena de salir.
    /// </summary>
    public void Salir(string NombreEscena)
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        
        SceneManager.LoadScene(NombreEscena);
        Time.timeScale = 1;
        PausaMenu.SetActive(false);

    }

    public void Controloes()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        PantallaControles.SetActive(true);
   
    }

    public void VolverMenuPausa()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        PantallaControles.SetActive(false);
    }

    public void MutearSonido()
    {
        
        Sonidos.setMutearSonidos(true); //Silenciamos todos los sonidos tanto efectos como música.
        Sonidos.setSonidoMenuPrincipal(false); //Paramos la música del menú principal.
        Sonidos.setSonidoNivel1(false); //Paramos la música del nivel.

        //Desactivas el sonido del nivel.
        bt_mutear.SetActive(false);
        bt_desMutear.SetActive(true);

       if(Sonidos.getMutearSonidos()        == false) Click.GetComponent<AudioSource>().Play();
       if(Sonidos.getSonidoNivel1()         == false) Musica.GetComponent<AudioSource>().Stop();
      
    }

    public void DesmutearSonido()
    {
        //Activar sonido
        bt_mutear.SetActive(true);
        bt_desMutear.SetActive(false);

        Sonidos.setMutearSonidos(false); //Silenciamos todos los sonidos tanto efectos como música.
        Sonidos.setSonidoMenuPrincipal(true); //Paramos la música del menú principal.
        Sonidos.setSonidoNivel1(true); //Paramos la música del nivel.

        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        if (Sonidos.getSonidoNivel1() == true) Musica.GetComponent<AudioSource>().Play();
    }
}
