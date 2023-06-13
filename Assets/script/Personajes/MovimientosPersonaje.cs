using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MovimientosPersonaje : MonoBehaviour
{

    Animator               animacion;
    Rigidbody2D            rigidbody2;                                             //--------Vamos a referenciarle el Rigibody para que apunte al aplicado al personaje desde el editor. De esta manera podremos aplicarle las fuerzas necesarias para hacer real un salto.
    
    public float           fuerzaSalto;
    public float           velx;
    public float           movX;
    public float           retroceso             = 400f;
    public float           impactoAZombie        = 900f;
    
    public Transform       refPie;
    public Transform       ContenedorArma;
    public Transform       mirilla;
    public Transform       ReferenciaManoArmada;
    public Transform       Refcabeza;
    public Transform       refOjos;
    public Transform       refCanionPistola;

    RaycastHit2D           impacto;
   

    public GameObject      particulasDisparo;
    public GameObject      particulasSangre;
    public GameObject      particulasSangrePakineitor;
    public GameObject      BonusMunicion;
    public GameObject      MunicionInterfaz;
    public GameObject      BonusBotiquin;
    public GameObject      InterfazZombiesMatados;
    public GameObject      Corazon1;
    public GameObject      Corazon2;
    public GameObject      Corazon3;

    public bool            isSuelo;                                                 //--------Objeto que se va a referenciar al transform del arma.
    bool                   isArmado;                                                //--------Variable para ver si ha cogido o no el arma.
    bool                   isRecargado          = true;                             //--------Si est� recargado el cargador al m�ximo de su capacidad. Por defecto es que s�.
    bool                   isDisparar           = true;                             //--------Si puedo disparar o no.
    bool                   isBonusMunicionCogido        = false;                            
    bool                   isMuerto             = false;
    bool                   isBonusBotiquinCogido = false;

    int                    energiaMaxima         = 4;                                //--------Representa la vida del personaje.
    int                    energiaActual         = 0;                                //--------Representa la vida actual que tiene el personaje en tiempo real.
    int                    cargadorPistola       = 0;                                //--------Representa las balas que tiene el arma.
    int                    municionReserva       = 120;                              //--------Representa las balas que tiene guardadas para recargar.
    int                    capacidadCargador     = 20;                               //--------Representa los disparos que va a poder realizar.
    int                    numeroZombiesMatados  = 0;
    int                    contadorDeMuertes     = 0;
    int                    hermanoContadorDeMuertes = 0; //Variable que va a compararse con contadorDeMuertes.

    public UnityEngine.UI.Image mascaraDa�o;
    public TMPro.TextMeshProUGUI TXT_Vida;
    public TMPro.TextMeshProUGUI TXT_CargadorPistola;                               //--------Definimos la variable de tipo TextMesh para manipularla en la scena.
    public TMPro.TextMeshProUGUI TXT_MunicionReserva;                               //--------Definimos la variable de tipo TextMesh para manipularla en la scena.
    public TMPro.TextMeshProUGUI TXT_ZombiesMatados;

    public UnityEngine.UI.Image BarraVerde;
    public UnityEngine.UI.Image telaNegra;
    float ValorDeseadoPantallaNegra             = 1f;


    //---------------------------------------------------------------------------- INICIO SETs -----------------------------------------------------------------------------------------------------------




    public void setHermanoContadorDeMuertes(int numero)  
    {
        hermanoContadorDeMuertes=numero;
    }


    /// <summary>
    /// M�todo que sumar� uno a la variable contadora de muertes.
    /// </summary>
    void AutoIncrementarContadorMuertes()
    {
        contadorDeMuertes++;
      
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
        energiaActual                      = energiaMaxima--;
        float transparencia                =0f;

        Instantiate(particulasSangrePakineitor, posicion, Quaternion.identity);
       
        if (energiaActual <=0) {
            BarraVerde.fillAmount          = 0.0f;
            animacion.SetTrigger("Muere");
            isMuerto = true;
           AutoIncrementarContadorMuertes();
           ActualizarNumeroVidas();
        }
        else {
            
            
            if(energiaActual == 3)
            {
                transparencia             = 74;
                BarraVerde.fillAmount     = 0.856f;
            }

            if(energiaActual == 2)
            {
                transparencia             = 134f;
                BarraVerde.fillAmount     = 0.355f;
            }

            if (energiaActual == 1)
            {
                transparencia             = 255f;
                BarraVerde.fillAmount     = 0.036f;
            }

            TXT_Vida.text = energiaActual.ToString();

            Debug.Log("Energ�a actual: " + energiaActual);
            mascaraDa�o.color             = new Color(1, 1, 1, transparencia);
           
        }
       
    }

                                                                                                                                    /// <summary>
                                                                                                                                    /// M�todo que pone la transparencia de la pantalla negra opaca.
                                                                                                                                    /// </summary>
  public void FadeOut()
    {
        ValorDeseadoPantallaNegra        = 1;
    }

    /// <summary>
    /// M�todo que pone la pantalla negra transparente.
    /// </summary>
    public void FadeIn()
    {
        ValorDeseadoPantallaNegra        = 0;
        
    }


    private void FixedUpdate()
    {
        float ValorAlpha                 = Mathf.Lerp(telaNegra.color.a, ValorDeseadoPantallaNegra, 0.1f);
        if(energiaActual == 0)
        {
            telaNegra.color              = new Color(0, 0, 0, ValorAlpha);
            if (ValorAlpha > 0.9f && ValorDeseadoPantallaNegra == 1) SceneManager.LoadScene("Nivel1");
        }
    }

                                                                                                                                    /// <summary>
                                                                                                                                    /// M�todo que va a actualizar la cantidad de corazones que aparecen en pantalla.
                                                                                                                                    /// </summary>
    public void ActualizarNumeroVidas()
    {

        if (getContadorDeMuertes() != getHermanoContadorDeMuertes()) //Condici�n que comprueba si el personaje ha muerto una vez o no para actualizar la variable hermanoContadorDeMuertes para saber si quito una vida o no.
        {
            Debug.Log("Entro al if de ActualizarNumeroVidas");
            if (getContadorDeMuertes() == 1) Destroy(Corazon1); //Oculto el objeto o imagen en este caso.
            if (getContadorDeMuertes() == 2) Destroy(Corazon2); //Oculto objeto o imagen en este caso.
            if (getContadorDeMuertes() == 3) Destroy(Corazon3); //Oculto objeto o imagen en este caso. 
            setHermanoContadorDeMuertes(getContadorDeMuertes()); //Actualizo la variable para as� inidcarle al programa si muere o no el personaje comparando las variables de la condici�n.
        }
    }


    /// <summary>
    /// M�todo que gracias a una clase est�tica va a guardar la informaci�n actual de la partida.
    /// </summary>
    public void GuardarPartida()
    {
        inforPartida.Pakineitor.posicion = transform.position;                                      //��� pendiente de hacer los set y get que faltan.
        inforPartida.Pakineitor.setMunicionCargador(this.cargadorPistola);
        inforPartida.Pakineitor.setMunicionReserva(this.municionReserva);
        inforPartida.Pakineitor.setEnergiaActual(this.energiaActual);
        inforPartida.Pakineitor.setContadorMuertes(getContadorDeMuertes());
        inforPartida.Pakineitor.setIsArmado(this.isArmado);
        inforPartida.Pakineitor.IsBonusBotiquinCogido(this.getIsBonusBotiquinCogido());
        inforPartida.Pakineitor.IsBonusMunicionMaximaCogido(this.isBonusMunicionCogido);
        inforPartida.Pakineitor.setCargado(getRecargado());
        inforPartida.Pakineitor.setNumeroZombiesMatados(getNumeroZombiesMatados());
        inforPartida.Pakineitor.mascaraDa�o = this.mascaraDa�o;
        inforPartida.Pakineitor.BarraVerde = this.BarraVerde;
    }

    /// <summary>
    /// M�todo encargado de extraer la informaci�n guardada de la parida.
    /// </summary>
    public void CargarPartida()
    {
        transform.position                  = inforPartida.Pakineitor.posicion;
        contadorDeMuertes                   = inforPartida.Pakineitor.getContadorMuertes();
        cargadorPistola                     = inforPartida.Pakineitor.getMunicionCargador();
        municionReserva                     = inforPartida.Pakineitor.getMunicionReserva();
        energiaActual                       = inforPartida.Pakineitor.getEnergiaActual();
        isArmado                            = inforPartida.Pakineitor.getIsArmado();
        numeroZombiesMatados                = inforPartida.Pakineitor.getNumeroZombiesMatados();
        this.mascaraDa�o.fillAmount         = inforPartida.Pakineitor.mascaraDa�o.fillAmount;
        this.BarraVerde.fillAmount          = inforPartida.Pakineitor.BarraVerde.fillAmount;

        setBonusBotiquinCogido(inforPartida.Pakineitor.getIsBonusBotiquinCogido());
        setIsBonusMunicionCogido(inforPartida.Pakineitor.getIsBonusMunicionMaximaCogido());
        setCargado(inforPartida.Pakineitor.getCargado());

    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



    // Start is called before the first frame update
    void Start()
    { 
        
        energiaActual                     = energiaMaxima;
        animacion                         = GetComponent<Animator>();                                                            //--------Agregamos una referencia al animator
        rigidbody2                        = GetComponent<Rigidbody2D>();                                                         //--------Aqu� referenciamos el componente externo con la variable de tipo RigiBody2d.
        mirilla.gameObject.SetActive(false);
        if(isMuerto == true) telaNegra.color = new Color(0, 0, 0, 1);
        cargadorPistola = capacidadCargador;                                                                                     //--------Inicializo a 120 el cargador.
        


    }

    // Update is called once per frame
    void Update()
    {
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
                mascaraDa�o.color = new Color(1, 1, 1, 0);
                BarraVerde.fillAmount = 1f;
            }
        }

        isMuerto = false;
        TXT_ZombiesMatados.text = numeroZombiesMatados.ToString();
        
    }

   
}
