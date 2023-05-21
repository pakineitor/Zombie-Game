using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientosPersonaje : MonoBehaviour
{

    Animator               animacion;
    Rigidbody2D            rigidbody2;                                                                                       //--------Vamos a referenciarle el Rigibody para que apunte al aplicado al personaje desde el editor. De esta manera podremos aplicarle las fuerzas necesarias para hacer real un salto.
    
    public float           fuerzaSalto;
    public float           velx;
    float                  movX;
    
    public Transform       refPie;
    public Transform       ContenedorArma;
    public Transform       mirilla;
    
    public bool            isSuelo;                                                                                          //--------Objeto que se va a referenciar al transform del arma.
    bool                   isArmado;                                                                                                      //Variable para ver si ha cogido o no el arma.
    

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
            isArmado                    = true;
            Destroy(collision.gameObject);
            ContenedorArma.gameObject.SetActive(true);
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

    // Start is called before the first frame update
    void Start()
    {
        animacion                         = GetComponent<Animator>();                                                             //--------Agregamos una referencia al animator
        rigidbody2                        = GetComponent<Rigidbody2D>();                                                          //--------Aquí referenciamos el componente externo con la variable de tipo RigiBody2d.
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
        mirilla.position                  = PosicionMouse(mirilla);


    }

   
}
