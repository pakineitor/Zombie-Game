using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zombies : MonoBehaviour
{
    Rigidbody2D         rb2d;                                                                                                                    //---------Declaramos un objeto de tipo Rigibody2d para referenciarlo.
    float               limiteCaminarDerecho;                                                                                                    //---------Variable para establecer un recorrido autom�tico del zombie hacia la derecha.
    float               limiteCaminarIzquierdo;                                                                                                  //---------Variable para establecer un recorrido autom�tico del zombie hacia la izquierda.
    public float        velocidadDelZombie                                  = 20f;                                                               //---------Variable p�blica que permitir� ser camiada desde el editor de Unity.
    int                 direccion                                           = 1;
    public float        umbralVelocidad;
    Vector3             escalaDefault;
    public float        magnitudVueloCabeza                                 = 300f;
    float               zonaActiva                                          = 20f;
    float               zonaPersecuccion                                    = 10f;
    float               distanciaAtaque                                     = 3f;
    float               distanciaConPakineitor;
    public GameObject   prefabMuerto;
    public Transform    pakineitor;
    Animator            anim;
    enum                tipoComportamientoZombie { pasivo, persecucion, ataque}
    tipoComportamientoZombie EstadoZombie                                   = tipoComportamientoZombie.pasivo;

    

    public void ZombieMuere()
    {
       
       Destroy(gameObject);
    }

                                                                                                                                                   /// <summary>
                                                                                                                                                   /// M�todo que va acomprobar todo el rato el estado del zombie para saber si ataca, persigue o est� pasivo, es decir, a su bola.
                                                                                                                                                   /// </summary>
                                                                                                                                                   /// <param name="EstadoActualDelZombie"> Le pasamos una variable de tipo enum que contiene 3 estados. Es un vector.</param>
    void EstadoActualZombie(tipoComportamientoZombie EstadoActualDelZombie)
    {
        distanciaConPakineitor                                              = Mathf.Abs(pakineitor.position.x - transform.position.x);

        switch (EstadoActualDelZombie)
        {
            case tipoComportamientoZombie.pasivo:

                //Camina.
                rb2d.velocity                                               = new Vector2(velocidadDelZombie * direccion, rb2d.velocity.y);        //---------Aqu� lo que hacemos es asignar al objeto  una velocidad en el eje x e y. 
                
                //Gira seg�n los l�mites de su recorrido.
                if (transform.position.x < limiteCaminarIzquierdo) direccion= 1;                                                                   //---------Condici�n que comprueba la posici�n del zombie con respecto a sus l�mites para saber hacia d�nde tiene que mirar cuando se desplace
                if (transform.position.x > limiteCaminarDerecho) direccion  = -1;
                anim.speed                                                  = 1f;

                //Entrar en la zona de persecucci�n
                if (distanciaConPakineitor < zonaActiva) EstadoZombie       = tipoComportamientoZombie.persecucion;
            break;

            case tipoComportamientoZombie.persecucion:

                
                //corre
                rb2d.velocity = new Vector2(velocidadDelZombie * 1.5f * direccion, rb2d.velocity.y);                                                //---------Aqu� lo que hacemos es asignar al objeto  una velocidad en el eje x e y. 
                //gira seg�n est� el personaje.
                if (pakineitor.position.x > transform.position.x) direccion = 1;                                                                    //---------Condici�n que comprueba la posici�n del zombie con respecto a sus l�mites para saber hacia d�nde tiene que mirar cuando se desplace
                if (pakineitor.position.x < transform.position.x) direccion = -1;

                //Velocidad de la animaci�n:
                anim.speed                                                  = 1.8f;

                //Sale y vuelve a la zona pasiva.
                if (distanciaConPakineitor > zonaPersecuccion) EstadoZombie = tipoComportamientoZombie.pasivo;

                //Entre a la zona de ataque:
                if (distanciaConPakineitor < distanciaAtaque) EstadoZombie  = tipoComportamientoZombie.ataque;
                
                break;

            case tipoComportamientoZombie.ataque:
                
                anim.SetTrigger("Atacar");
                //corre
                rb2d.velocity = new Vector2(velocidadDelZombie * direccion, rb2d.velocity.y);                                                       //---------Aqu� lo que hacemos es asignar al objeto  una velocidad en el eje x e y. 
                                                                                                                                                    //---------gira seg�n est� el personaje.
                if (pakineitor.position.x > transform.position.x) direccion = 1;                                                                    //---------Condici�n que comprueba la posici�n del zombie con respecto a sus l�mites para saber hacia d�nde tiene que mirar cuando se desplace
                if (pakineitor.position.x < transform.position.x) direccion = -1;

                //Velocidad de la animaci�n:
                anim.speed                                                  = 1f;

                //Vuelve a la zona de persecuci�n:
                if (distanciaConPakineitor > distanciaAtaque) EstadoZombie  = tipoComportamientoZombie.persecucion;
                
                break;
        }
        transform.localScale = new Vector3(escalaDefault.x * direccion, escalaDefault.y, escalaDefault.z);
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        anim                                = transform.GetComponent<Animator>();
        rb2d                                = GetComponent<Rigidbody2D>();                                                                          //---------En este caso lo que hacemos es inicializar el objeto.
        limiteCaminarDerecho                = transform.position.x + GetComponent<CircleCollider2D>().radius;                                       //---------En esta sentencia lo que estamos dici�ndole a unity es, oye, el l�mite derecho es la posici�n del zombie + la longitud del radio.
        limiteCaminarIzquierdo              = transform.position.x - GetComponent<CircleCollider2D>().radius;
        Destroy(GetComponent<CircleCollider2D>());
        escalaDefault                       = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.magnitude < umbralVelocidad)
            EstadoActualZombie(EstadoZombie); 
          
    }
}
