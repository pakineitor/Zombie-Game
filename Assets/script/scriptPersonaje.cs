using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPersonaje : MonoBehaviour
{
    Animator         animacion;
    Rigidbody2D      caida;                                                                                           //----------------------Variable que va a guardar es efecto que le da la masa y gravedad al personaje en este caso.
    public float     fuerzaSalto;                                                                                     //----------------------Es pública para que pueda ser usada por cualquiera.
    public bool      isSuelo;
    public Transform refPie;
           float movimientoEjeX;                                                                                      //----------------------Será o menos 1 o 1 y ya vamos a multiplicarlo por x número para darle velocidad.
    public float VELOCIDADX = 10f;
    public bool CamaraFija;
    

    /// <summary>
    /// Movimiento que se va a ejecutar para saber a dónde mirar.
    /// </summary>
    /// <param name="movimientoEnEjeX"></param>
    void Horientacion(float movimientoEnEjeX)
    {
        if (movimientoEjeX < 0)
        {
            transform.localScale       = new Vector3(-0.8644f, 0.7461f, 1);
        }

        if (movimientoEjeX > 0)
        {
            transform.localScale       = new Vector3(0.8644f, 0.7461f, 1);
        }
    }




    /// <summary>
    /// Función para fijar la cámra con e personaje.
    /// </summary>
    void FijarCamara()
    {
        Camera.main.transform.position = transform.position + new Vector3(0,0,-20);
    }

    /// <summary>
    /// Método que se asegura que se llame 50 veces por segundo.
    /// </summary>
    private void FixUpdate()
    {
        Vector3 posicionActual         = Camera.main.transform.position;                                               //----------------------Devuelve la posición del personaje a la cámara.
        Vector3 posicionALaQueVoy      = transform.position + new Vector3(0,0,-20);
                                                                                                                       //----------------------Camera.main.transform.position = posicionActual + (posicionALaQueVoy - posicionActual) * 0.5f;
        Camera.main.transform.position = Vector3.Lerp(posicionActual, posicionALaQueVoy, 0.05f);                       //----------------------Te hace el cálculo de la sentencia a terior, de manera automática.
    }


    //----------------------Start is called before the first frame update
    void Start()
    {
        animacion                      = GetComponent    <Animator>();                                                //----------------------Referencia a la animator.
        caida                          = GetComponent    <Rigidbody2D>();
       

    }

    // Update is called once per frame
    void Update()
    {
        movimientoEjeX                 = Input.GetAxis("Horizontal");
        animacion.                       SetFloat("VelocidadEnX", Mathf.Abs(movimientoEjeX));
        isSuelo                        = Physics2D.OverlapCircle(refPie.position, 1f, 1<<8);                          //----------------------Función que devuelve true o false en base a la posición del radio del pie con respecto al suelo.
        caida.velocity                 = new Vector2(VELOCIDADX * movimientoEjeX, caida.velocity.y);
        animacion.                       SetBool("isSuelo", isSuelo);                                                 //----------------------Pasamos el valor y la variable de isSuelo al animator para que los referencie.

        if (Input.GetButtonDown("Jump") && isSuelo)                                                                   //----------------------Si el pie está rozando el suelo y se ha pulsado saltar "espacio":
        {                         
            caida.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
        }

        Horientacion(movimientoEjeX);


        if (CamaraFija == true)
        {
            FijarCamara();
        }
        else
        {
            FixUpdate();

        }
       
        


    }
}
