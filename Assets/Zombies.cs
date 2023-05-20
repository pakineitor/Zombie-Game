using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zombies : MonoBehaviour
{
    Rigidbody2D rb2d;                                                                                             //---------Declaramos un objeto de tipo Rigibody2d para referenciarlo.
    float limiteCaminarDerecho;                                                                                   //---------Variable para establecer un recorrido automático del zombie hacia la derecha.
    float limiteCaminarIzquierdo;                                                                                 //---------Variable para establecer un recorrido automático del zombie hacia la izquierda.
    public float velocidadDelZombie=20f;                                                                          //---------Variable pública que permitirá ser camiada desde el editor de Unity.
    int direccion = 1;



    // Start is called before the first frame update
    void Start()
    {
        rb2d                        = GetComponent<Rigidbody2D>();                                                 //---------En este caso lo que hacemos es inicializar el objeto.
        limiteCaminarDerecho        = transform.position.x + GetComponent<CircleCollider2D>().radius;              //---------En esta sentencia lo que estamos diciéndole a unity es, oye, el límite derecho es la posición del zombie + la longitud del radio.
        limiteCaminarIzquierdo      = transform.position.x - GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity               = new Vector2(velocidadDelZombie * direccion, rb2d.velocity.y);                //---------Aquí lo que hacemos es asignar al objeto  una velocidad en el eje x e y. 

        if (transform.position.x < limiteCaminarIzquierdo)                                                         //---------Condición que comprueba la posición del zombie con respecto a sus límites para saber hacia dónde tiene que mirar cuando se desplace.
            direccion               = 1;
        
        if (transform.position.x > limiteCaminarDerecho)
            direccion               = -1;
        
                                                   
        transform.localScale        = new Vector3(2.07714f * direccion, 2.07714f, 2.07714f);
        
       

    }
}
