﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablaDePosiciones.Datos;

namespace TablaDePosiciones.Negocios
{
    class Tabla
    {


        private const int NOMBRE = 0;
        private const int JUEGOS = 1;
        private const int PUNTOS = 2;
        private const int PARTIDOS_GANADOS = 3;
        private const int PARTIDOS_EMPATADOS = 4;
        private const int PARTIDOS_PERDIDOS = 5;
        private const int GOLES_A_FAVOR = 6;
        private const int GOLES_EN_CONTRA = 7;
        private const int GOLES_DE_VISITANTE = 8;
        private const int DIFERENCIA_DE_GOLES = 9;
        private InterfazAccesoDatos accesodatos = new ImplAccesoDatosDAO();

       // private string[,] matrizEquiposos = new string[2,10] { { "jojo", "1", "4", "1", "0", "0", "4", "2", "4", "2" }, { "jiji", "2", "4", "2", "1", "1", "5", "3", "5", "3" } };
        public string[,] obtenerResultadosEquipo()
        {   
            Console.Write("Nombre equipo local:       ");
            string nombre = Console.ReadLine();
            Console.Write("Goles equipo local:        ");
            string goles = Convert.ToString(Console.ReadLine());
            Console.Write("Nombre equipo visitante:   ");
            string nombre2 = Console.ReadLine();
            Console.Write("Goles quipo visitante:     ");
            string goles2 = Convert.ToString(Console.ReadLine());
            return new string[2,3] { { nombre, goles, goles2 },{ nombre2, goles2, goles } };
        }


        private int equipoExiste(string nombre)
        {
            string[,] matrizEquipos = accesodatos.leerDatos();
            int resultado;

            if (matrizEquipos!=null) { 
                for (int i = 0; i < matrizEquipos.GetLength(0); i++) { 
            
                if (nombre == matrizEquipos[i, NOMBRE])
                {
                    return i;
                }
            }
            }

            return -1;
        }

        private string[] calcularDatosEquipoNuevo(string[] equipoNuevo, string condicion)
        {
            string[] datosCalculados = new string[10];
            datosCalculados[NOMBRE] = equipoNuevo[0];
            datosCalculados[JUEGOS] = "1";
            datosCalculados[GOLES_A_FAVOR] = equipoNuevo[1];
            datosCalculados[GOLES_EN_CONTRA] = equipoNuevo[2];
            datosCalculados[DIFERENCIA_DE_GOLES] = Convert.ToString(Convert.ToInt32(equipoNuevo[1]) - Convert.ToInt32(equipoNuevo[2]));
            if (condicion == "v")
            {
                datosCalculados[GOLES_DE_VISITANTE] = equipoNuevo[1];
            }
            if (Convert.ToInt32(equipoNuevo[1]) > Convert.ToInt32(equipoNuevo[2]))
            {
                datosCalculados[PUNTOS] = "3";
                datosCalculados[PARTIDOS_GANADOS] = "1";
            }
            else if (Convert.ToInt32(equipoNuevo[1]) < Convert.ToInt32(equipoNuevo[2]))
            {
                datosCalculados[PUNTOS] = "0";
                datosCalculados[PARTIDOS_PERDIDOS] = "1";
            }
            else
            {
                datosCalculados[PUNTOS] = "1";
                datosCalculados[PARTIDOS_EMPATADOS] = "1";
            }
            return datosCalculados;
        }

        public void agregarDatosALaTabla(string[] equipoNuevo, string condicion)
        {
            string[] datosCalculados = calcularDatosEquipoNuevo(equipoNuevo, condicion);
            string[] equipo;
            string[,] matrizEquipos = accesodatos.leerDatos();
            if (matrizEquipos != null)
            {
                if (equipoExiste(equipoNuevo[NOMBRE]) < 0)
                {
                    equipo = new string[] { equipoNuevo[NOMBRE], "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                    equipo = sumaDatosEquipos(equipo, datosCalculados);
                    matrizEquipos = agregarArregloALaMatriz(equipo);
                }
                else
                {
                    equipo = new string[matrizEquipos.GetLength(1)];
                    for (int i = 0; i < equipo.Length; i++)
                    {
                        equipo[i] = matrizEquipos[equipoExiste(equipoNuevo[NOMBRE]), i];
                    }
                    equipo = sumaDatosEquipos(equipo, datosCalculados);
                    for (int i = 0; i < equipo.Length; i++)
                    {
                        matrizEquipos[equipoExiste(equipoNuevo[NOMBRE]), i] = equipo[i];
                    }
                }
                for (int i = 0; i < matrizEquipos.GetLength(0); i++)
                {
                    for (int j = 0; j < matrizEquipos.GetLength(1); j++)
                    {
                        Console.WriteLine(matrizEquipos[i, j]);
                    }
                }
            }
           accesodatos.escribirDatos(matrizEquipos);
            
        }

        private string[] sumaDatosEquipos(string[] datosActuales, string[] datosNuevos) {
            for(int i=1; i < datosActuales.Length; i++)
            {
                datosActuales[i] = Convert.ToString(Convert.ToInt32(datosActuales[i]) + Convert.ToInt32(datosNuevos[i]));
            }
            return datosActuales;
        }

        private string[,] agregarArregloALaMatriz(string[] arreglo)
        {
            string[,] matrizEquipos = accesodatos.leerDatos();
            if (matrizEquipos != null)
            {
                string[,] nuevaMatrizEquipos = new string[matrizEquipos.GetLength(0) + 1, matrizEquipos.GetLength(1)];
                for (int i = 0; i < matrizEquipos.GetLength(0); i++)
                {
                    for (int j = 0; j < nuevaMatrizEquipos.GetLength(1); i++)
                    {
                        nuevaMatrizEquipos[i, j] = matrizEquipos[i, j];
                    }
                }
                for (int i = 0; i < nuevaMatrizEquipos.GetLength(1); i++)
                {
                    nuevaMatrizEquipos[nuevaMatrizEquipos.GetLength(0) - 1, i] = arreglo[i];
                }
                return nuevaMatrizEquipos;
            }
            return matrizEquipos;
        }

        private string[,] ordenarBurbujaDescendente(string[,] matrizEquipos)
        {
            if (matrizEquipos != null)
            {
                for (int x = 0; x < matrizEquipos.GetLength(0); x++)
                {

                    for (int indiceActual = 0; indiceActual < matrizEquipos.GetLength(0) - 1; indiceActual++)
                    {
                        int indiceSiguienteElemento = indiceActual + 1;
                        if (Convert.ToInt32(matrizEquipos[indiceActual, PUNTOS]) < Convert.ToInt32(matrizEquipos[indiceSiguienteElemento, PUNTOS]))
                        {
                            matrizEquipos = IntercambioFilasMatriz(matrizEquipos, indiceActual, indiceSiguienteElemento);
                        }
                        else if (Convert.ToInt32(matrizEquipos[indiceActual, PUNTOS]) == Convert.ToInt32(matrizEquipos[indiceSiguienteElemento, PUNTOS]))
                        {
                            if (Convert.ToInt32(matrizEquipos[indiceActual, DIFERENCIA_DE_GOLES]) < Convert.ToInt32(matrizEquipos[indiceSiguienteElemento, DIFERENCIA_DE_GOLES]))
                            {
                                matrizEquipos = IntercambioFilasMatriz(matrizEquipos, indiceActual, indiceSiguienteElemento);
                            }
                            else if (Convert.ToInt32(matrizEquipos[indiceActual, DIFERENCIA_DE_GOLES]) == Convert.ToInt32(matrizEquipos[indiceSiguienteElemento, DIFERENCIA_DE_GOLES]))
                            {
                                if (Convert.ToInt32(matrizEquipos[indiceActual, GOLES_A_FAVOR]) < Convert.ToInt32(matrizEquipos[indiceSiguienteElemento, GOLES_A_FAVOR]))
                                {
                                    matrizEquipos = IntercambioFilasMatriz(matrizEquipos, indiceActual, indiceSiguienteElemento);

                                }
                                else if (Convert.ToInt32(matrizEquipos[indiceActual, GOLES_A_FAVOR]) == Convert.ToInt32(matrizEquipos[indiceSiguienteElemento, GOLES_A_FAVOR]))
                                {
                                    if (Convert.ToInt32(matrizEquipos[indiceActual, GOLES_DE_VISITANTE]) < Convert.ToInt32(matrizEquipos[indiceSiguienteElemento, GOLES_DE_VISITANTE]))
                                    {
                                        matrizEquipos = IntercambioFilasMatriz(matrizEquipos, indiceActual, indiceSiguienteElemento);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return matrizEquipos;
            

        }

        public void imprimirMatrizDeEquipos()
        {
            string[,] matrizEquipos = accesodatos.leerDatos();
            if (matrizEquipos != null)
            {
                string[,] matriz = ordenarBurbujaDescendente(matrizEquipos);
                Console.WriteLine("Nombre   J     P    PG    PE    PP    GF    GC    GV    DF");
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz.GetLength(1); j++)
                    {

                        Console.Write(matriz[i, j] + "     ");
                    }
                    Console.WriteLine();
                }
            }
        }

        private string[] ObtenerVectorDeMatriz(string[,] matri, int fila)
        {
            string[] vectorFila=null;
            if (matri != null)
            {
                vectorFila = new string[matri.GetLength(1)];
                for (int i = 0; i < matri.GetLength(1); i++)
                {
                    vectorFila[i] = matri[fila, i];

                }
            }
            return vectorFila;

        }

        private string[,] IntercambioFilasMatriz(string[,] matrizz, int filaActual, int filaSiguiente)
        {
            string[,] nuevaMatriz = matrizz;
            string[] vectorActual = ObtenerVectorDeMatriz(matrizz, filaActual);
            string[] vectorSiguiente = ObtenerVectorDeMatriz(matrizz, filaSiguiente);
            if (matrizz != null)
            {
                for (int i = 0; i < nuevaMatriz.GetLength(1); i++)
                {
                    nuevaMatriz[filaActual, i] = vectorSiguiente[i];

                    nuevaMatriz[filaSiguiente, i] = vectorActual[i];
                }
            }

            return nuevaMatriz;
        }

    }
}
