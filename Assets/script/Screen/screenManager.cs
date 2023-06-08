using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class screenManager : MonoBehaviour
{

    public GameObject  MenuPrincipal;
    public GameObject  PantallaAjustes;
    public GameObject  PantallaValoracion;
    public Button      SonidoOn;
    public Button      SonidoOff;
    private string     inputMensaje;
    string password = "gfnrfccettcsdiwa", correo = "pakineitor123@gmail.com";

    public void setMensaje(string mensaje)
    {
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

    public void ActivarValoracion()
    {
        PantallaValoracion.SetActive(true);
    }

    public void DesactivarValoracion()
    {
        PantallaValoracion.SetActive(false);
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

        Debug.Log(getMensaje());
    }

}

