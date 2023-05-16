using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientosPersonaje : MonoBehaviour
{

    Animator animacion;                    
    
    public void Saltar()
    {
        if (Input.GetButtonDown("Jump")){
            animacion.SetTrigger("saltar");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animacion = GetComponent<Animator>();                                   //--------Agregamos una referencia al animator
    }

    // Update is called once per frame
    void Update()
    {
        Saltar();
    }
}
