using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarEnBaseDatos
{ 
    private static int      NumeroZombiesMatados = inforPartida.Pakineitor.getNumeroZombiesMatados(); //Inicializo los zombies que he matado en la partida.
    private static string   Nombre = "pakineitor";

    public void setNumeroZombiesMatados(int numero)
    {
        NumeroZombiesMatados = numero;
    }

    public static int getNumeroZomnbiesMatados()
    {
        return NumeroZombiesMatados;
    }

    public static string getNombre()
    {
        return Nombre;
    }

    //Método para pasar el nombre en la base de datos. CRUD.

}
