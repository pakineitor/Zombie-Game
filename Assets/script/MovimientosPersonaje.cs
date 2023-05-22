using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientosPersonaje : MonoBehaviour
{

    Animator               animacion;
    Rigidbody2D            rigidbody2;                                                                                       //--------Vamos a referenciarle el Rigibody para que apunte al aplicado al personaje desde el editor. De esta manera podremos aplicarle las fuerzas necesarias para hacer real un salto.
    
    public float           fuerzaSalto;
    public float           velx;
    public float           movX;
    public float           retroceso      = 300f;
    public float           impactoAZombie = 300f;

    public Transform       refPie;
    public Transform       ContenedorArma;
    public Transform       mirilla;
    public Transform       ReferenciaManoArmada;
    public Transform       Refcabeza;
    public Transform       refOjos;
    public Transform       refCanionPistola;

    RaycastHit2D           impacto;
    public GameObject      particulasDisparo;
    public bool            isSuelo;                                                                                           //--------Objeto que se va a referenciar al transform del arma.
    bool                   isArmado;                                                                                          //Variable para ver si ha cogido o no el arma.

                                                                                                                              /// <summary>
                                                                                                                              /// Función que va a ejecutar el salto.
                                                                                                                              /// </summary>
    public void Saltar()
    { 
            rigidbody2.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);                                            //--------Si pulsamos saltar le añadimos una fuerza con un vector2 y así poder pasarle la fuerza en ambos ejes, y le pasamos el tipo de fuerza que en este caso es impulso.
            animacion.SetTrigger("saltar");                                                                                   //--------Establecemos la acción de saltar si presionamos la tecla Jump (barra espaciadora).
        
    }
                                                                                                                              /// <summary>
                                                                                                                              /// Función que va a comprobar la dirección en la que andamos jugando con la rotación.
                                                                                                                              /// </summary>
                                                                                                                              /// <param name="movimientox"></param>
    public void Mirada(float movimientox)
    {
        if(movX < 0)                                                                                                          //--------Condición para cuando pulse hacia la izquierda que el muñeco mire a esa dirección.
        {
            transform.localScale        = new Vector3(-1,1,1);
        }

        if(movX > 0) {
            transform.localScale        = new Vector3(1, 1, 1);
        }
        
            }
                                                                                                                                /// <summary>
                                                                                                                                /// Función que fijará la cámara al personaje.
                                                                                                                                /// </summary>
    public void FijarCamara()
    {
        Camera.main.transform.position  = transform.position + new Vector3(0, 0, -20);                                          //--------Referenciamos la cámara
    }

                                                                                                                                /// <summary>
                                                                                                                                /// Función que se va a disparar en cuanto el personaje entre en el área del collider 2d de la pistola que está en la calle.
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
       
        Mirilla.position                 = Camera.main.ScreenToWorldPoint(new Vector3(                                           //--------Con este método, extraemos la posición del mouse.
                                           Input.mousePosition.x,
                                           Input.mousePosition.y,
                                         - Camera.main.transform.position.z
            ));

        return Mirilla.position;
    }

                                                                                                                                 /// <summary>
                                                                                                                                 /// Función que va a ejecutarse en cuanto coja el arma el personaje.
                                                                                                                                 /// </summary>
                                                                                                                                 /// <param name="mirilla">Parámetro que es objeto de tipo transform</param>
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
                                                                                                                                 /// Método que va a evitar que se sobreescriba el movimiento de la cabeza, ya que lo último que ejecuta Unity en la pila es la animación y renderizado de las animaciones generadas por nosotros.
                                                                                                                                 /// </summary>

    private void LateUpdate()
    {
        if (isArmado)
        {
            Refcabeza.up                   = refOjos.position - mirilla.position;
            //ContenedorArma.up = ContenedorArma.position - mirilla.position;
        }
    }



    public void Disparar()
    {
        Vector3 direccion = (mirilla.position - ContenedorArma.position).normalized;
        impacto                             =  Physics2D.Raycast(ContenedorArma.position, direccion, 1000f, ~(1 << 10));         //--------Método al que se le pasa un origen, una posición y con normalized convertimos ese vector en una magnitud. Este método ofrecido por Unity simula la bañla del arma.

        if (impacto.collider != null)
        {
            if (impacto.collider.gameObject.CompareTag("Zombie"))
            {
                ImpactoZombie(direccion);
            }
        }

        Instantiate (particulasDisparo, refCanionPistola.position, Quaternion.identity);

    }

                                                                                                                                 /// <summary>
                                                                                                                                 /// Función que ejecutará la simulación del impacto de la bala al zombie.
                                                                                                                                 /// </summary>
                                                                                                                                 /// <param name="direccion"></param>
    public void ImpactoZombie(Vector3 direccion)
    {
        impacto.rigidbody.AddForce(impactoAZombie * direccion, ForceMode2D.Impulse);
    }


    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        animacion                         = GetComponent<Animator>();                                                            //--------Agregamos una referencia al animator
        rigidbody2                        = GetComponent<Rigidbody2D>();                                                         //--------Aquí referenciamos el componente externo con la variable de tipo RigiBody2d.
        mirilla.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2.velocity               = new Vector2(velx * movX, rigidbody2.velocity.y);                                     //--------Referenciamos el componente Rigidbody2d con un vector que contiene una velocidad por un movimiento en el eje x y una coordenadas en el eje y.
        movX                              = Input.GetAxis("Horizontal");                                                         //--------Extraemos el axis horizontal para el movimiento en el eje x con -1 o 1. 
        isSuelo                           = Physics2D.OverlapCircle(refPie.position, 1f, 1 << 8);                                //--------En esta sentencia de código, guardamos un bool comparando que si hay algo en el área formada por el radio de refPie, que sea true o false.
        animacion.SetFloat("MoveX", Mathf.Abs(movX));                                                                            //--------Establecemos un float en valor absoluto para que cuando e mueva en sentido negativo, no haya errores pero Unity sepa disntinguirlos.
        
        animacion.SetBool("isPiso", isSuelo);                                                                                    //--------Referenciamos parámetro del animator llamado isPisom con el valor booleano guardado en la variable isSuelo.

        if (Input.GetButtonDown("Jump") && isSuelo)                                                                              //--------Si está en el sueloy pulso saltar:
        {
            Saltar();                                                                                                            //--------Establecemos la acción de saltar si presionamos la tecla Jump (barra espaciadora).
        }

        Mirada(movX);
        FijarCamara();

        if (isArmado)
        {
            mirilla.position = PosicionMouse(mirilla);
            mirilla.gameObject.SetActive(true);
            ComprobarVistaArmado(mirilla);
            ReferenciaManoArmada.position   = PosicionMouse(mirilla); 
            Refcabeza.up                    = refOjos.position - mirilla.position;
            LateUpdate();
           
            if (Input.GetButtonDown("Fire1"))
            {
                Disparar();
            }
        }

       
    }

   
}
