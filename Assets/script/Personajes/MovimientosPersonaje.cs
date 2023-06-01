using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MovimientosPersonaje : MonoBehaviour
{

    Animator               animacion;
    Rigidbody2D            rigidbody2;                                                                                       //--------Vamos a referenciarle el Rigibody para que apunte al aplicado al personaje desde el editor. De esta manera podremos aplicarle las fuerzas necesarias para hacer real un salto.
    
    public float           fuerzaSalto;
    public float           velx;
    public float           movX;
    public float           retroceso      = 400f;
    public float           impactoAZombie = 900f;
    
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
    public bool            isSuelo;                                                                                           //--------Objeto que se va a referenciar al transform del arma.
    bool                   isArmado;                                                                                          //--------Variable para ver si ha cogido o no el arma.
    bool                   isRecargado = true;

    int                    energiaMaxima = 4;                                                                                 //--------Representa la vida del personaje.
    int                    energiaActual = 0;                                                                                 //--------Representa la vida actual que tiene el personaje en tiempo real.
    int                    cargadorPistola= 0;                                                                                //--------Representa las balas que tiene el arma.
    int                    municionReserva = 120;                                                                             //--------Representa las balas que tiene guardadas para recargar.
    int                    capacidadCargador = 20;                                                                            //Representa los disparos que va a poder realizar.

    public UnityEngine.UI.Image mascaraDa�o;
    public TMPro.TextMeshProUGUI TXT_Vida;
    public TMPro.TextMeshProUGUI TXT_CargadorPistola;                                                                         //--------Definimos la variable de tipo TextMesh para manipularla en la scena.
    public TMPro.TextMeshProUGUI TXT_MunicionReserva;                                                                         //--------Definimos la variable de tipo TextMesh para manipularla en la scena.

    public UnityEngine.UI.Image BarraVerde;
    public UnityEngine.UI.Image telaNegra;
    float ValorDeseadoPantallaNegra = 1f;
    bool isMuerto = false;
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
            isArmado                     = true;
            Destroy(collision.gameObject);
            ContenedorArma.gameObject.SetActive(true);
            isArmado                     = true;
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
        isRecargado = false;
        if (impacto.collider != null)
        {
            if (impacto.collider.gameObject.CompareTag("Zombie"))
            {
                ImpactoZombie(direccion);

            }

            if (impacto.collider.gameObject.CompareTag("Cabeza"))
            {
                impacto.transform.GetComponent<Zombies>().ZombieMuere();
           
            }
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
           
           isRecargado = true;
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
       
        if (energiaActual ==0) {
            BarraVerde.fillAmount          = 0.0f;
            animacion.SetTrigger("Muere");
            isMuerto = true;
      
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

   
  
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



    // Start is called before the first frame update
    void Start()
    { 
        
        energiaActual                     = energiaMaxima;
        animacion                         = GetComponent<Animator>();                                                            //--------Agregamos una referencia al animator
        rigidbody2                        = GetComponent<Rigidbody2D>();                                                         //--------Aqu� referenciamos el componente externo con la variable de tipo RigiBody2d.
        mirilla.gameObject.SetActive(false);
        if(isMuerto == true) telaNegra.color = new Color(0, 0, 0, 1);
        cargadorPistola = capacidadCargador;                                        //Inicializo a 120 el cargador.
       


    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2.velocity               = new Vector2(velx * movX, rigidbody2.velocity.y);                                     //--------Referenciamos el componente Rigidbody2d con un vector que contiene una velocidad por un movimiento en el eje x y una coordenadas en el eje y.
        movX                              = Input.GetAxis("Horizontal");                                                         //--------Extraemos el axis horizontal para el movimiento en el eje x con -1 o 1. 
        isSuelo                           = Physics2D.OverlapCircle(refPie.position, 1f, 1 << 8);                                //--------En esta sentencia de c�digo, guardamos un bool comparando que si hay algo en el �rea formada por el radio de refPie, que sea true o false.
        animacion.SetFloat( "MoveX"  ,  Mathf.Abs(movX));                                                                        //--------Establecemos un float en valor absoluto para que cuando e mueva en sentido negativo, no haya errores pero Unity sepa disntinguirlos.
        animacion.SetBool(  "isPiso" ,  isSuelo);
                                                                       //--------Actualizo el texto de la munici�n de reserva.
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

            if (Input.GetButtonDown("Fire1"))
            {
                if (cargadorPistola >= 1)               //Compruebo si puedo disparar.
                {
                    Disparar();
                }
                else
                {
                    //Reproducir sonido sin balas.
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && isRecargado==false)       //Comrpuebo si puedo recargar.
            {
                if (municionReserva >= 1) //Compruebo que tengo munici�n para recargar.
                {
                    if (cargadorPistola >= 0 && cargadorPistola < 20) //Y ahora compruebo que no est� lleno la capacidad del cargador.
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

        isMuerto = false;
    }

   
}
