using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zombies : MonoBehaviour
{
    Rigidbody2D     rb2d;                                                                                             //---------Declaramos un objeto de tipo Rigibody2d para referenciarlo.
    float           limiteCaminarDerecho;                                                                                   //---------Variable para establecer un recorrido autom�tico del zombie hacia la derecha.
    float           limiteCaminarIzquierdo;                                                                                 //---------Variable para establecer un recorrido autom�tico del zombie hacia la izquierda.
    public float    velocidadDelZombie = 20f;                                                                          //---------Variable p�blica que permitir� ser camiada desde el editor de Unity.
    int             direccion          = 1;
    public float    umbralVelocidad;
    Vector3 escalaDefault;



    public void ZombieMuere()
    {
        Destroy(gameObject); 
    }



    // Start is called before the first frame update
    void Start()
    {
        rb2d                        = GetComponent<Rigidbody2D>();                                                 //---------En este caso lo que hacemos es inicializar el objeto.
        limiteCaminarDerecho        = transform.position.x + GetComponent<CircleCollider2D>().radius;              //---------En esta sentencia lo que estamos dici�ndole a unity es, oye, el l�mite derecho es la posici�n del zombie + la longitud del radio.
        limiteCaminarIzquierdo      = transform.position.x - GetComponent<CircleCollider2D>().radius;
        Destroy(GetComponent<CircleCollider2D>());
        escalaDefault               = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.magnitude < umbralVelocidad)
        {
            rb2d.velocity = new Vector2(velocidadDelZombie * direccion, rb2d.velocity.y);                //---------Aqu� lo que hacemos es asignar al objeto  una velocidad en el eje x e y. 

            if (transform.position.x < limiteCaminarIzquierdo)                                                         //---------Condici�n que comprueba la posici�n del zombie con respecto a sus l�mites para saber hacia d�nde tiene que mirar cuando se desplace.
                direccion = 1;

            if (transform.position.x > limiteCaminarDerecho)
                direccion = -1;


            transform.localScale = new Vector3(escalaDefault.x * direccion, escalaDefault.y, escalaDefault.z);

        }

    }
}
