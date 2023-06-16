using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class screenManager : MonoBehaviour
{

    public GameObject  MenuPrincipal;
    public GameObject  PantallaAjustes;
    public GameObject  PantallaValoracion;
    public GameObject  Musica;
    public GameObject  Click;
 

    public Button      SonidoOn;
    public Button      SonidoOff;
    public Button      facil;
    public Button      media;
    public Button      dificil;
   

    private string     inputMensaje;
    //string password = "gfnrfccettcsdiwa", correo = "pakineitor123@gmail.com";

    public void setMensaje(string mensaje)
    {
        Click.GetComponent<AudioSource>().Play();
        inputMensaje = mensaje;
    }


    public string getMensaje()
    {
        return inputMensaje;
    }

    /// <summary>
    /// Función para cargar la escena desde fuera.
    /// </summary>
    /// <param name="NombreEscena"></param>
    public void CargarNivel1(string NombreEscena)
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(NombreEscena);         //Método para cargar una escena.
    }

    /// <summary>
    /// Función para que cierre la aplicación.
    /// </summary>
    public void Salir()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    /// <summary>
    /// 
    /// </summary>
    public void CargarPantallaAjustes()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        PantallaAjustes.SetActive(true);
   
    }

    /// <summary>
    /// 
    /// </summary>
    public void VolverAlMenu()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        PantallaAjustes.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ActivarSonido()
    {
        SonidoOff.gameObject.SetActive(true);
        SonidoOn.gameObject.SetActive(false);

        Sonidos.setSonidoNivel1(true);
        Sonidos.setSonidoMenuPrincipal(true);

        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        Musica.GetComponent<AudioSource>().Play();
    }
    /// <summary>
    /// 
    /// </summary>
    public void DesactivarSonido()
    {
        SonidoOff.gameObject.SetActive(false);
        SonidoOn.gameObject.SetActive(true);

        Sonidos.setSonidoNivel1(false);
        Sonidos.setSonidoMenuPrincipal(false);

        if (Sonidos.getMutearSonidos() == false) Musica.GetComponent<AudioSource>().Stop();
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();


    }

    public void ActivarValoracion()
    {
        PantallaValoracion.SetActive(true);
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
    }

    public void DesactivarValoracion()
    {
        PantallaValoracion.SetActive(false);
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
    }

    //Pone 4 toques. Es la que hay por defecto.
    public void Facil()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        inforPartida.Pakineitor.setFacil(true);
        inforPartida.Pakineitor.setMedia(false);
        inforPartida.Pakineitor.setDificil(false);
    }
    //Si te dan 2 toques mueres.
    public void Media()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        inforPartida.Pakineitor.setFacil(false);
        inforPartida.Pakineitor.setMedia(true);
        inforPartida.Pakineitor.setDificil(false);
    }
    //1 toque y mueres.
    //Te mueves más lento.
    public void Dificil()
    {
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
        inforPartida.Pakineitor.setFacil(false);
        inforPartida.Pakineitor.setMedia(false);
        inforPartida.Pakineitor.setDificil(true);
    }

    
    


    public void EnviarValoracion()
    {
        /*string senderEmail = correo;
        string senderPassword = password;
        string recipientEmail = correo;
        string subject = "Test Email";
        string body = getMensaje();

        MailMessage mail = new MailMessage(senderEmail, recipientEmail, subject, body);
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

        try
        {
            smtpClient.Send(mail);
            Debug.Log(getMensaje());
        }
        catch (SmtpException e)
        {
            Debug.Log("Error sending email: " + e.Message);
        }*/
        
        Debug.Log("El mensaje: " + getMensaje() + "se ha enviado con éxitoa nuestros técnicos, Gracias por confiar en nosotros");

    }

    private void Start()
    {
        if(Sonidos.getMutearSonidos()==true) Click.GetComponent<AudioSource>().Stop();
        if (Sonidos.getMutearSonidos() == false) Click.GetComponent<AudioSource>().Play();
    }
}

