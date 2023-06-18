using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovimientosPersonaje : MonoBehaviour
{

    Rigidbody2D rigidbody2;                                                                  //--------Vamos a referenciarle el Rigibody para que apunte al aplicado al personaje desde el editor. De esta manera podremos aplicarle las fuerzas necesarias para hacer real un salto.


    public Animator        animacion;
    public float           fuerzaSalto;
    public float           velx;
    public float           movX;
    public float           retroceso             = 400f;
    public float           impactoAZombie        = 900f;
    float                  ValorAlphaTelaNegra   = 1f;
    
    public Transform       refPie;
    public Transform       ContenedorArma;
    public Transform       mirilla;
    public Transform       ReferenciaManoArmada;
    public Transform       Refcabeza;
    public Transform       refOjos;
    public Transform       refCanionPistola;

    RaycastHit2D           impacto;

    public GameObject      Manager;
    public GameObject      particulasDisparo;
    public GameObject      particulasSangre;
    public GameObject      particulasSangrePakineitor;
    public GameObject      BonusMunicion;
    public GameObject      MunicionInterfaz;
    public GameObject      BonusBotiquin;
    public GameObject      InterfazZombiesMatados;
 

    public bool            isSuelo;                                                                     //--------Objeto que se va a referenciar al transform del arma.
    bool                   isArmado;                                                                    //--------Variable para ver si ha cogido o no el arma.
    bool                   isRecargado              = true;                                             //--------Si est� recargado el cargador al m�ximo de su capacidad. Por defecto es que s�.
    bool                   isDisparar               = true;                                             //--------Si puedo disparar o no.
    bool                   isBonusMunicionCogido    = false;                            
    bool                   isBonusBotiquinCogido    = false;




    int                    numeroMaximoVidas        = inforPartida.Pakineitor.getNumeroMaximoVidas();   //--------Variable que se inicializa seg�n la dificultad por m�s o enos vidas.
    int                    energiaActual            = inforPartida.Pakineitor.getEnergiaActual();       //--------Representa la vida actual que tiene el personaje en tiempo real.
    int                    cargadorPistola          = 0;                                                //--------Representa las balas que tiene el arma.
    int                    municionReserva          = 120;                                              //--------Representa las balas que tiene guardadas para recargar.
    int                    capacidadCargador        = 20;                                               //--------Representa los disparos que va a poder realizar.
    int                    numeroZombiesMatados     = 0;
    int                    contadorDeMuertes        = inforPartida.Pakineitor.getNumeroMaximoVidas();
    int                    hermanoContadorDeMuertes = 0;                                                //--------Variable que va a compararse con contadorDeMuertes.
    

  
    public TMPro.TextMeshProUGUI TXT_Vida_Actual; 
    public TMPro.TextMeshProUGUI TXT_Vida_M�xima;
    public TMPro.TextMeshProUGUI TXT_CargadorPistola;                                                   //--------Definimos la variable de tipo TextMesh para manipularla en la scena.
    public TMPro.TextMeshProUGUI TXT_MunicionReserva;                                                   //--------Definimos la variable de tipo TextMesh para manipularla en la scena.
    public TMPro.TextMeshProUGUI TXT_ZombiesMatados;
    
    
    public Image BarraVerde;
    public Image telaNegra;
  
   


    //---------------------------------------------------------------------------- INICIO SETs -----------------------------------------------------------------------------------------------------------



    /// <summary>
    /// M�todo que va a cambiar la variable mencionada pas�ndole por par�metro un int.
    /// </summary>
    /// <param name="numero"></param>
    public void setHermanoContadorDeMuertes(int numero)  
    {
        hermanoContadorDeMuertes=numero;
    }


    /// <summary>
    /// M�todo que sumar� uno a la variable contadora de muertes.
    /// </summary>
    void AutoRestarContadorMuertes()
    {
        contadorDeMuertes--;
      
    }


                                                                                              /// <summary>
                                                                                              /// M�todo que guarda un booleano en el atributo del objeto.
                                                                                              /// </summary>
                                                                                              /// <param name="isCogido"> bool </param>
    public void setBonusBotiquinCogido(bool isCogido)
    {
        isBonusBotiquinCogido=isCogido;
    }

                                                                                              /// <summary>
                                                                                              /// M�todo que guarda un booleano en el atributo del objeto.
                                                                                              /// </summary>
                                                                                              /// <returns></returns>
    public bool IsBonusBotiquinCogido()
    {
        return isBonusBotiquinCogido;
    }

                                                                                              /// <summary>
                                                                                              /// M�todo que se encarga de establecer un int en cargador del arma y en munici�nReserva.
                                                                                              /// </summary>
                                                                                              /// <param name="municionMaxima"></param>
                                                                                              /// <param name="cargadorPistola"></param>
    public void BonusCogido(int municionMaxima, int cargadorPistola)
    {
        this.cargadorPistola = cargadorPistola;
        this.municionReserva = municionMaxima;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isBonusCogido"></param>
    public void setIsBonusMunicionCogido(bool isBonusCogido)
    {
        isBonusMunicionCogido = isBonusCogido;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isSuelo"></param>
    public void setIsSuelo(bool isSuelo)
    {
        this.isSuelo= isSuelo;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="disparar"></param>
    public void setIsDisparar(bool disparar)
    {
        this.isDisparar = disparar;
    }

   

    /// <summary>
    /// 
    /// </summary>
    /// <param name="recargado"></param>
    public void setCargado(bool recargado)
    {
        this.isRecargado = recargado;
    }
    // ---------------------------------------------------------------------------- FIN SETs -----------------------------------------------------------------------------------------------------------


    //**************************************************************************** INICIO GET **********************************************************************************************************

                                                                                            /// <summary>
                                                                                            /// Este m�todo se encarga de extraer y devolver un booleano para gestionar si se ha cogido o no el bonus de la vida.
                                                                                            /// </summary>
                                                                                            /// <returns>isBonusBotiquinCogido</returns>
    public bool getIsBonusBotiquinCogido()
    {
        return isBonusBotiquinCogido;
    }
                                                                                            /// <summary>
                                                                                            /// Este m�todo se encarga de comprobar devolviendo un booleano, si el personaje que maneja el usuario, est� tocando el suelo o no.
                                                                                            /// </summary>
                                                                                            /// <returns>isSuelo</returns>
    public bool getISuelo()
    {
        return this.isSuelo;
    }



                                                                                            /// <summary>
                                                                                            /// M�todo que se encarga de comprobar devolviendo un booleano si se puede disparar o no.
                                                                                            /// </summary>
                                                                                            /// <returns>isDisparar</returns>
    public bool getIsDisparar()
    {
        return this.isDisparar;
    }


                                                                                            /// <summary>
                                                                                            /// M�todo que se encarga de comprobar devolviendo un booleano si se puede recargar o no.
                                                                                            /// </summary>
                                                                                            /// <returns>isRecargado</returns>
    public bool getRecargado()
    {
        return this.isRecargado;
    }

    
    public int getContadorDeMuertes()
    {
        return contadorDeMuertes;
    }


    public int getHermanoContadorDeMuertes()
    {
        return hermanoContadorDeMuertes;
    }


    public int getNumeroZombiesMatados()
    {
        return numeroZombiesMatados;
    }
        

    //**************************************************************************** FIN GET **********************************************************************************************************


    



    /// <summary>
    /// Funci�n que va a ejecutar el salto.
    /// </summary>

    public void Saltar()
    { 
            rigidbody2.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);                                            //--------Si pulsamos saltar le a�adimos una fuerza con un vector2 y as� poder pasarle la fuerza en ambos ejes, y le pasamos el tipo de fuerza que en este caso es impulso.
            animacion.SetTrigger("saltar");                                                                                   //--------Establecemos la acci�n de saltar si presionamos la tecla Jump (barra espaciadora).
        
    }
                                                                                                                              /// <summary>
                                                                                                                              /// Funci�n que va a comprobar la direcci�n en la que andamos jugando con la rotaci�n.
                                                                                                                              /// </summary>
                                                                                                                              /// <param name="movimientox"></param>
    public void Mirada(float movimientox)
    {
        if(movX < 0)                                                                                                           //--------Condici�n para cuando pulse hacia la izquierda que el mu�eco mire a esa direcci�n.
        {
            transform.localScale        = new Vector3(-1,1,1);
        }

        if(movX > 0) {
            transform.localScale        = new Vector3(1, 1, 1);
        }
        
            }
                                                                                                                                /// <summary>
                                                                                                                                /// Funci�n que fijar� la c�mara al personaje.
                                                                                                                                /// </summary>
    public void FijarCamara()
    {
        Camera.main.transform.position  = transform.position + new Vector3(0, 0, -20);                                          //--------Referenciamos la c�mara
    }

                                                                                                                                /// <summary>
                                                                                                                                /// Funci�n que se va a disparar en cuanto el personaje entre en el �rea del collider 2d de la pistola que est� en la calle.
                                                                                                                                /// </summary>
                                                                                                                                /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PistolaCalle"))
        {
            isArmado                            = true;
            Destroy(collision.gameObject);
            ContenedorArma.                     gameObject.SetActive(true);
            MunicionInterfaz.                   gameObject.SetActive(true);
            InterfazZombiesMatados.             gameObject.SetActive(true);
        }

        if (collision.gameObject.CompareTag("BonusMunicion"))
        {
            setIsBonusMunicionCogido(true);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Botiquin"))
        {
           setBonusBotiquinCogido(true);
           Destroy(collision.gameObject); 
        }
       
    }


    public Vector3 PosicionMouse(Transform Mirilla)
    {
       
        Mirilla.position                 = Camera.main.ScreenToWorldPoint(new Vector3(                                           //--------Con este m�todo, extraemos la posici�n del mouse.
                                           Input.mousePosition.x,
                                           Input.mousePosition.y,
                                         - Camera.main.transform.position.z
            ));

        return Mirilla.position;
    }

                                                                                                                                 /// <summary>
                                                                                                                                 /// Funci�n que va a ejecutarse en cuanto coja el arma el personaje.
                                                                                                                                 /// </summary>
                                                                                                                                 /// <param name="mirilla">Par�metro que es objeto de tipo transform</param>
    public void ComprobarVistaArmado(Transform mirilla)
    {
        if (mirilla.transform.position.x < transform.position.x)
        {
            transform.localScale          = new Vector3(-1, 1, 1);
        }

        if (mirilla.transform.position.x > transform.position.x)
        {
            transform.localScale          = new Vector3(1, 1, 1);
        }
    }

                                                                                                                                 /// <summary>
                                                                                                                                 /// M�todo que va a evitar que se sobreescriba el movimiento de la cabeza, ya que lo �ltimo que ejecuta Unity en la pila es la animaci�n y renderizado de las animaciones generadas por nosotros.
                                                                                                                                 /// </summary>

    private void LateUpdate()
    {
        if (isArmado)
        {
            Refcabeza.up                   = refOjos.position - mirilla.position;
        }  
    }


                                                                                                                                   /// <summary>
                                                                                                                                   /// M�todo encargado de efectuar el disparo y los efectos que conlleva la ejecuci�n.
                                                                                                                                   /// </summary>
    public void Disparar()
    {
        Vector3 direccion                  = (mirilla.position - ContenedorArma.position).normalized;
        rigidbody2.AddForce(retroceso * -direccion, ForceMode2D.Impulse);
        impacto                            =  Physics2D.Raycast(ContenedorArma.position, direccion, 1000f, ~(1 << 10));            //--------M�todo al que se le pasa un origen, una posici�n y con normalized convertimos ese vector en una magnitud. Este m�todo ofrecido por Unity simula la ba�la del arma
        Instantiate(particulasDisparo, refCanionPistola.position, Quaternion.identity);
        
        cargadorPistola--;
        TXT_CargadorPistola.text = cargadorPistola.ToString();
        setCargado(false);

        if (impacto.collider != null)
        {
            if (impacto.collider.gameObject.CompareTag("Zombie"))
            {
                ImpactoZombie(direccion);

            }

            if (impacto.collider.gameObject.CompareTag("Cabeza"))
            {
                numeroZombiesMatados++;
                inforPartida.Pakineitor.setNumeroZombiesMatados(numeroZombiesMatados);      //Actualizo la variable en la clase est�tica.
                Instantiate(particulasSangre, impacto.point, Quaternion.identity);
                impacto.transform.GetComponent<Zombies>().ZombieMuere();
            }
        }
        if (cargadorPistola == 0)
        {
            RecargarArma();
            municionReserva = CalcularMunicionReserva();
        }
    }

    /// <summary>
    /// M�todo que va a recargar el arma.
    /// </summary>
    public void RecargarArma()
    {
           municionReserva = CalcularMunicionReserva();
           TXT_MunicionReserva.text = municionReserva.ToString();
           TXT_CargadorPistola.text = capacidadCargador.ToString();
           setCargado(true);
          
    }
                                                                                                                                  /// <summary>
                                                                                                                                  /// M�todo encargado de calcular para actualizar cu�nta munici�n va quedando para poder recargar.
                                                                                                                                  /// </summary>
                                                                                                                                  /// <param name="municionReserva"></param>
                                                                                                                                  /// <param name="municionCargador"></param>
                                                                                                                                  /// <returns>Devuelve la cantidad de balas que le quedan en la reserva.</returns>
                                                                                                                                  /// 

    public int CalcularMunicionReserva()
    {
        int resultado = 0;
        resultado = 20 - cargadorPistola;
        cargadorPistola = capacidadCargador;
        return municionReserva = municionReserva - resultado;                                                                                                                              
    }



                                                                                                                                   /// <summary>
                                                                                                                                   /// Funci�n que ejecutar� la simulaci�n del impacto de la bala al zombie.
                                                                                                                                   /// </summary>
                                                                                                                                   /// <param name="direccion"></param>
    public void ImpactoZombie(Vector3 direccion)
    {
        Instantiate(particulasSangre, impacto.point, Quaternion.identity);
    }

                                                                                                                                   /// <summary>
                                                                                                                                   /// 
                                                                                                                                   /// </summary>
                                                                                                                                   /// <param name="posicion"></param>
    public void RecibirDa�o(Vector2 posicion)
    {
        energiaActual--;
        Instantiate(particulasSangrePakineitor, posicion, Quaternion.identity);
            if(energiaActual >= 0)
            {    
                if (energiaActual == 3) BarraVerde.fillAmount = 0.856f;
                if (energiaActual == 2) BarraVerde.fillAmount = 0.355f;
                if (energiaActual == 1) BarraVerde.fillAmount = 0.036f;

                if(energiaActual <= 0 && numeroMaximoVidas>0) //Si me quedan oportunidades, reaparezco.
                {
                    animacion.SetTrigger("Muere");
                    BarraVerde.fillAmount = 0.0f;
                    numeroMaximoVidas--;  //Restamos 1 al n�mero m�ximo de vidas
                    inforPartida.Pakineitor.setIsMuerto(true);
                    Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAH");

            }


                if(energiaActual == 0 && numeroMaximoVidas == 0) //Se ejecuta el game Over.
                {
                   
                    Sonidos.setMusicaGameOver(true);
                    Manager.GetComponent<ScreenPause>().Musica.GetComponent<AudioSource>().Stop();
                    Manager.GetComponent<ScreenPause>().PantallaMuerte.gameObject.SetActive(true);
                    if (Sonidos.getSonidoGameOver() == true) Manager.GetComponent<ScreenPause>().MusicaGameOver.GetComponent<AudioSource>().Play();
                    Time.timeScale = 0f;
                
                }
            
                inforPartida.Pakineitor.setNumeroMaximoVidas(numeroMaximoVidas);
                inforPartida.Pakineitor.setEnergiaActual(energiaActual);

                TXT_Vida_M�xima.text = inforPartida.Pakineitor.getNumeroMaximoVidas().ToString();
                TXT_Vida_Actual.text = inforPartida.Pakineitor.getEnergiaActual().ToString();
                Debug.Log("Energ�a actual: " + energiaActual);
                
            }
        

        
    }

                                                                                                                                    /// <summary>
                                                                                                                                    /// M�todo que pone la transparencia de la pantalla negra opaca.
                                                                                                                                    /// </summary>
  public void FadeOut()
    {
        ValorAlphaTelaNegra = 1;
    }

    private void FixedUpdate()
    {
        float ValorAlpha                 = Mathf.Lerp(telaNegra.color.a, ValorAlphaTelaNegra, 0.05f);                               //Valor entre valor inicial al final, es decir, va a ir avanazando paso a paso del principio al final.
        telaNegra.color                  = new Color(0, 0, 0, ValorAlpha);
        if (ValorAlpha > 0.9 && ValorAlphaTelaNegra ==1) SceneManager.LoadScene("Nivel1");
        
    }

                                                                                                                                    /// <summary>
                                                                                                                                    /// M�todo que va a actualizar la cantidad de corazones que aparecen en pantalla.
                                                                                                                                    /// </summary>
    public void ActualizarNumeroVidas()
    {

        if (getContadorDeMuertes() != getHermanoContadorDeMuertes()) //Condici�n que comprueba si el personaje ha muerto una vez o no para actualizar la variable hermanoContadorDeMuertes para saber si quito una vida o no.
        {
            Debug.Log("Entro al if de ActualizarNumeroVidas");
            inforPartida.Pakineitor.setNumeroMaximoVidas(numeroMaximoVidas); //Actualizo la variable en a clase est�tca.
            inforPartida.Pakineitor.setEnergiaActual(inforPartida.Pakineitor.getEnergiaActual()); //Reinicio la variable
            Manager.GetComponent<ScreenPause>().ComprobarVidaMaxima(inforPartida.Pakineitor.getNumeroMaximoVidas());
            setHermanoContadorDeMuertes(getContadorDeMuertes()); //Actualizo la variable para as� inidcarle al programa si muere o no el personaje comparando las variables de la condici�n.
        }
    }

    

    /// <summary>
    /// M�todo que gracias a una clase est�tica va a guardar la informaci�n actual de la partida.
    /// </summary>
    public void GuardarPartida()
    {
        

        inforPartida.Pakineitor.setPartidaGuardada(true);
        inforPartida.Pakineitor.setMunicionCargador(this.cargadorPistola); 
        inforPartida.Pakineitor.setMunicionReserva(this.municionReserva);
        inforPartida.Pakineitor.setEnergiaActual(this.energiaActual);
        inforPartida.Pakineitor.setContadorMuertes(getContadorDeMuertes());
        inforPartida.Pakineitor.setIsArmado(this.isArmado);
        inforPartida.Pakineitor.IsBonusBotiquinCogido(this.getIsBonusBotiquinCogido());
        inforPartida.Pakineitor.IsBonusMunicionMaximaCogido(this.isBonusMunicionCogido);
        inforPartida.Pakineitor.setCargado(getRecargado());
        inforPartida.Pakineitor.setNumeroZombiesMatados(getNumeroZombiesMatados());
        inforPartida.Pakineitor.posicion        = transform.position;                                       //��� pendiente de hacer los set y get que faltan.
        inforPartida.Pakineitor.BarraVerde      = BarraVerde;
        
  

    }

    /// <summary>
    /// M�todo encargado de extraer la informaci�n guardada de la partida.
    /// </summary>
    public void CargarPartida()
    {
        transform.position                  = inforPartida.Pakineitor.posicion;                             //--------Guardar la posici�n del personaje.
        contadorDeMuertes                   = inforPartida.Pakineitor.getContadorMuertes();                 //--------Guardar el contador de muertes.
        cargadorPistola                     = inforPartida.Pakineitor.getMunicionCargador();                //--------Guardar el cargador de la pistola.
        municionReserva                     = inforPartida.Pakineitor.getMunicionReserva();                 //--------Guardar el estado de la munici�n m�xima.
        energiaActual                       = inforPartida.Pakineitor.getEnergiaActual();                   //--------Guardar la energ�a actual.
        isArmado                            = inforPartida.Pakineitor.getIsArmado();                        //--------Guardar si est� armado o no.
        numeroZombiesMatados                = inforPartida.Pakineitor.getNumeroZombiesMatados();            //--------Guardar el n�mero de zombies matados.
        BarraVerde                          = inforPartida.Pakineitor.BarraVerde;                           //Guardar el estado de la barra verde.
        contadorDeMuertes                   = inforPartida.Pakineitor.getContadorMuertes();                 //Guardar las veces que has muerto.

    
        setBonusBotiquinCogido(inforPartida.Pakineitor.getIsBonusBotiquinCogido());
        setIsBonusMunicionCogido(inforPartida.Pakineitor.getIsBonusMunicionMaximaCogido());
        setCargado(inforPartida.Pakineitor.getCargado());

    }


    public void ComprobarDificultad()
    {
        if (inforPartida.Pakineitor.getFacil() == true)
        {
            inforPartida.Pakineitor.setNumeroMaximoVidas(4);
            inforPartida.Pakineitor.setEnergiaActual(4);
            this.TXT_Vida_M�xima.text = inforPartida.Pakineitor.getNumeroMaximoVidas().ToString();
            this.TXT_Vida_Actual.text = inforPartida.Pakineitor.getEnergiaActual().ToString();
            
        }
        
        if (inforPartida.Pakineitor.getMedia() == true)
        {
            inforPartida.Pakineitor.setNumeroMaximoVidas(2);
            inforPartida.Pakineitor.setEnergiaActual(2);
            this.TXT_Vida_M�xima.text = inforPartida.Pakineitor.getNumeroMaximoVidas().ToString();
            this.TXT_Vida_Actual.text = inforPartida.Pakineitor.getEnergiaActual().ToString();
        }
    
        
        if(inforPartida.Pakineitor.getDificil()  == true)
        {
            inforPartida.Pakineitor.setNumeroMaximoVidas(1);
            inforPartida.Pakineitor.setEnergiaActual(1);
            this.TXT_Vida_M�xima.text = inforPartida.Pakineitor.getNumeroMaximoVidas().ToString();
            this.TXT_Vida_Actual.text = this.energiaActual.ToString();
            this.velx = 15f;

        }

       
    }

    

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



    // Start is called before the first frame update
    void Start()
    {

        if(Sonidos.getSonidoGameOver()==false) Manager.GetComponent<ScreenPause>().MusicaGameOver.GetComponent<AudioSource>().Stop();
        ComprobarDificultad();                                                                                                   //--------Actualizo la variable de la energ�a actual para que me vuelva a dar 4 toques.
        animacion                         = GetComponent<Animator>();                                                            //--------Agregamos una referencia al animator
        rigidbody2                        = GetComponent<Rigidbody2D>();                                                         //--------Aqu� referenciamos el componente externo con la variable de tipo RigiBody2d.
        mirilla.gameObject.SetActive(false);
        
        cargadorPistola = capacidadCargador;                                                                                     //--------Inicializo a 120 el cargador.
        
        if (Input.GetKeyDown(KeyCode.C)) inforPartida.Pakineitor.setPartidaGuardada(true); //Si guardo manual cambio la variable a true para cuando se recargue la escena que carge la partida.
        if (inforPartida.Pakineitor.getPartidaGuardada() == true) CargarPartida();                                               //--------Comprobamos si hemos guardado una sola vez la partida para cargarla.
        if (Sonidos.getSonidoNivel1() == false) {
            Manager.GetComponent<ScreenPause>().Musica.GetComponent<AudioSource>().Stop();
            Manager.GetComponent<ScreenPause>().bt_desMutear.gameObject.SetActive(true);
            Manager.GetComponent<ScreenPause>().bt_mutear.gameObject.SetActive(false);   
        }

        telaNegra.color = new Color(0,0,0,1);
        ValorAlphaTelaNegra = 0f;

        if(inforPartida.Pakineitor.getIsMuerto() == true)
        {
            inforPartida.Pakineitor.setIsMuerto(false);
            this.energiaActual = inforPartida.Pakineitor.getEnergiaActual(); //Reinicio la variable.
            this.numeroMaximoVidas = inforPartida.Pakineitor.getNumeroMaximoVidas(); //Actualizo la variable.
            this.TXT_Vida_M�xima.text = inforPartida.Pakineitor.getNumeroMaximoVidas().ToString();
            this.TXT_Vida_Actual.text = inforPartida.Pakineitor.getEnergiaActual().ToString();
        }
        
    } 

    // Update is called once per frame
    void Update()
    {
        if (energiaActual <= 0) return;

        //ActualizarNumeroVidas();
        rigidbody2.velocity               = new Vector2(velx * movX, rigidbody2.velocity.y);                                     //--------Referenciamos el componente Rigidbody2d con un vector que contiene una velocidad por un movimiento en el eje x y una coordenadas en el eje y.
        movX                              = Input.GetAxis("Horizontal");                                                         //--------Extraemos el axis horizontal para el movimiento en el eje x con -1 o 1. 
        isSuelo                           = Physics2D.OverlapCircle(refPie.position, 1f, 1 << 8);                                //--------En esta sentencia de c�digo, guardamos un bool comparando que si hay algo en el �rea formada por el radio de refPie, que sea true o false.
        animacion.SetFloat( "MoveX"  ,  Mathf.Abs(movX));                                                                        //--------Establecemos un float en valor absoluto para que cuando e mueva en sentido negativo, no haya errores pero Unity sepa disntinguirlos.
        animacion.SetBool(  "isPiso" ,  isSuelo);

        if (Input.GetKeyDown(KeyCode.G)) GuardarPartida();
        if (Input.GetKeyDown(KeyCode.C)) CargarPartida();

        if (isBonusMunicionCogido == true) municionReserva = 120;                                                                //--------Actualizo el texto de la munici�n de reserva.
                                                                                                                                 //--------Referenciamos par�metro del animator llamado isPisom con el valor booleano guardado en la variable isSuelo.

        if (Input.GetButtonDown("Jump") && isSuelo)                                                                              //--------Si est� en el sueloy pulso saltar:
        {
            Saltar();                                                                                                            //--------Establecemos la acci�n de saltar si presionamos la tecla Jump (barra espaciadora).
        }

        Mirada(movX);
        FijarCamara();

        if (isArmado)
        {
            mirilla.position = PosicionMouse(mirilla);
            mirilla.gameObject.SetActive(true);
            ComprobarVistaArmado(mirilla);
            ReferenciaManoArmada.position  = PosicionMouse(mirilla); 
            Refcabeza.up                   = refOjos.position - mirilla.position;
            LateUpdate();

            if (getIsDisparar() == true)                                                                                        //--------Compruebo si estoy en el men� de pausa para no poder disparar ni recargar.
            {
                if (Input.GetButtonDown("Fire1"))
                {
               
                    if (cargadorPistola >= 1)                                                                                   //--------Compruebo si puedo disparar.
                    {
                        Disparar();
                    }
                    else
                    {
                        //Reproducir sonido sin balas.
                    }
                }

                if (Input.GetKeyDown(KeyCode.R) && getRecargado() == false)                                                     //--------Comrpuebo si puedo recargar.
                {
                    if (municionReserva >= 1)                                                                                   //--------Compruebo que tengo munici�n para recargar.
                    {
                        if (cargadorPistola < 20)                                                                               //--------Y ahora compruebo que no est� lleno la capacidad del cargador.
                        {
                            RecargarArma();
                            municionReserva = CalcularMunicionReserva();

                        }
                    }
                    else
                    {
                        //Reproducir alg�n sonido;
                    }


                }

            }

            if (isBonusMunicionCogido == true)
            {
                TXT_CargadorPistola.text = capacidadCargador.ToString();
                TXT_MunicionReserva.text = 120.ToString();
                setIsBonusMunicionCogido(false);
            }

            if(isBonusBotiquinCogido == true)
            {
                BarraVerde.fillAmount = 1f;
            }
        }

        TXT_ZombiesMatados.text = inforPartida.Pakineitor.getNumeroZombiesMatados().ToString();
        
    }

   

}
