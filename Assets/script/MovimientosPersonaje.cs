using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientosPersonaje : MonoBehaviour
{

    Animator animacion;
    Rigidbody2D rigidbody2;                                                                 //--------Vamos a referenciarle el Rigibody para que apunte al aplicado al personaje desde el editor. De esta manera podremos aplicarle las fuerzas necesarias para hacer real un salto.
    public float fuerzaSalto;
    public bool isSuelo;
    public Transform refPie;
    float movX;
    public float velx;
    /// <summary>
    /// Función que va a ejecutar el salto.
    /// </summary>
    public void Saltar()
    { 
            rigidbody2.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);           //--------Si pulsamos saltar le añadimos una fuerza con un vector2 y así poder pasarle la fuerza en ambos ejes, y le pasamos el tipo de fuerza que en este caso es impulso.
            animacion.SetTrigger("saltar");                                                  //--------Establecemos la acción de saltar si presionamos la tecla Jump (barra espaciadora).
        
    }

    

    // Start is called before the first frame update
    void Start()
    {
        animacion   = GetComponent<Animator>();                                             //--------Agregamos una referencia al animator
        rigidbody2  = GetComponent<Rigidbody2D>();                                          //--------Aquí referenciamos el componente externo con la variable de tipo RigiBody2d.
    }

    // Update is called once per frame
    void Update()
    {

        rigidbody2.velocity = new Vector2(velx * movX, rigidbody2.velocity.y);
        movX = Input.GetAxis("Horizontal"); 
        animacion.SetFloat("MoveX", Mathf.Abs(movX));
        isSuelo = Physics2D.OverlapCircle(refPie.position, 1f, 1 << 8);
        animacion.SetBool("isPiso", isSuelo);

        if (Input.GetButtonDown("Jump") && isSuelo)                  //--------Si está en el sueloy pulso saltar:
        {
            Saltar();                                                //--------Establecemos la acción de saltar si presionamos la tecla Jump (barra espaciadora).
        }

    }

   
}
