using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class inforPartida
{
    public static class Pakineitor
    {
        public static        Image mascaraDaño;
        public static        Image BarraVerde;
        public static        Vector2 posicion;
               static int    energiaActual                      = 4; //Default
               static int    municionCargador;
               static int    municionReserva;
               static int    contadorMuertes;
               static int    numeroZombiesMatados;
               static int    numeroMaximoVidas                  = 4; //Default.

               static bool   isMuerto = false;
               static bool   Armado;
               static bool   BonusBotiquinCogido;
               static bool   BonusMunicionMaximaCogido;
               static bool   isCargado;
               static bool   isPartidaGuardada                  = false;
               static bool   facil                              = true; //Default.
               static bool   media                              = false;
               static bool   dificil                            = false;
               static string Nombre = "pakineitor";

               static GameObject game;


        public static void setIsMuerto(bool ismuerto)
        {
            isMuerto = ismuerto;
        }

        public static void setGame(GameObject Game)
        {
            game = Game;
        }


        public static void setNombre(string nombre)
        {
            Nombre = nombre;
        }

        public static void setNumeroMaximoVidas(int n)
        {
            numeroMaximoVidas = n;
        }

        public static void setPartidaGuardada(bool yes)
        {
            isPartidaGuardada = yes;
        }


        public static void setNumeroZombiesMatados(int n)
        {
            numeroZombiesMatados = n;
        }

        public static void setCargado(bool yes)
        {
            isCargado = yes;
        }


        public static void setIsArmado(bool yes)
        {
            Armado = yes;
        }

        public static void IsBonusBotiquinCogido(bool yes)
        {
            BonusBotiquinCogido = yes;
        }

        public static void IsBonusMunicionMaximaCogido(bool yes)
        {
            BonusMunicionMaximaCogido = yes;
        }


        public static bool getIsArmado()
        {
            return Armado;
        }

        public static bool getIsBonusBotiquinCogido()
        {
            return BonusBotiquinCogido;
        }

        public static bool getIsBonusMunicionMaximaCogido()
        {
            return BonusMunicionMaximaCogido;
        }


        public static void setContadorMuertes(int n)
        {
            contadorMuertes = n;
        }

        public static int getContadorMuertes() {  
            return contadorMuertes; 
        }

        public static void setEnergiaActual(int n)
        {
            energiaActual = n;
        }

        public static void setMunicionCargador(int n)
        {
            municionCargador = n;
        }

        public static void setMunicionReserva(int n)
        {
            municionReserva = n;
        }

        public static int getMunicionCargador() {
            return  municionCargador;
        }

        public static int getMunicionReserva()
        {
            return municionReserva;
        }

        public static int getEnergiaActual()
        {
            return energiaActual;
        }

        public static bool getCargado()
        {
            return isCargado;
        }
        public static int getNumeroZombiesMatados()
        {
            return numeroZombiesMatados;
        }


        public static bool getPartidaGuardada()
        {
            return isPartidaGuardada;
        }

        public static int getNumeroMaximoVidas()
        {
            return numeroMaximoVidas;
        }

        public static void setFacil(bool dificultad)
        {
            facil = dificultad;
        }

        public static void setMedia(bool dificultad)
        {
            media = dificultad;
        }

        public static void setDificil(bool dificultad)
        {
            dificil = dificultad;
        }

        public static bool getFacil()
        {
            return facil;
        }

        public static bool getMedia()
        {
            return media;
        }

        public static bool getDificil()
        {
            return dificil;
        }

        public static string getNombre()
        {
            return Nombre;
        }

        public static GameObject getGameObject()
        {
            return game;
        }

        public static bool getIsMuerto()
        {
            return isMuerto;
        }



    }


   
}
