using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sonidos 
{
    
   static bool SonidoMenuPrincipal  = true;                    //--------Por defecto.
   static bool SonidoNivel1         = true;
   
   static bool MutearSonidos        = false;
   static bool DesmutearSonidos     = false;

    public static void setMutearSonidos(bool yes)
    {
        MutearSonidos = yes;
    }

    public static void setDesmutearSonidos(bool yes)
    {
        DesmutearSonidos=yes;
    }

    public static void setSonidoMenuPrincipal(bool yes)
    {
        SonidoMenuPrincipal = yes;
    }

    public static void setSonidoNivel1(bool yes)
    {
        SonidoNivel1 = yes;
    }

    public static bool getSonidoMenuPrincipal()
    {
        return SonidoMenuPrincipal;
    }


    public static bool getSonidoNivel1()
    {
        return SonidoNivel1;
    }

    public static bool getMutearSonidos()
    {
        return MutearSonidos;
    }

    public static bool getDesmutearSonidos()
    {
        return DesmutearSonidos;
    }



    public static void DesactivarTodosSonidos()
    {
        setSonidoMenuPrincipal(false);
        setSonidoNivel1(false);
        //Resto de sonidos a false.
    }

    public static void ActivarTodosLosSonidos()
    {
        setSonidoMenuPrincipal(true);
        setSonidoNivel1(true);
    }

}
