using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class inforPartida
{
    public static class Pakineitor
    {
        public static        Image mascaraDa�o;
        public static        Image BarraVerde;
        public static        Vector2 posicion;
               static int    energiaActual;
               static int    municionCargador;
               static int    municionReserva;
               static int    contadorMuertes;
               static int    numeroZombiesMatados;

               static bool   Armado;
               static bool   BonusBotiquinCogido;
               static bool   BonusMunicionMaximaCogido;
               static bool   isCargado;



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

    }
}