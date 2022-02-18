using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TablaDePosiciones.Datos
{

    class ImplAccesoDatosDAO : InterfazAccesoDatos
    {



        public void escribirDatos(string[,] matriz)
        {
            File.WriteAllText(_path, SerializarJsonFile(matriz));
        }

        public string[,] leerDatos()
        {
            
            var leido = obtenerMatrizDeJason;
            return desserializarJsonFile(leido);
        }

        private static string _path = @"../archivo.json";
        public static string SerializarJsonFile(string[,] matriz)
        {
            string matrizJson = JsonConvert.SerializeObject(matriz, Formatting.Indented);
           return matrizJson;
        }

        public static String obtenerMatrizDeJason()
        {
            string MatrizDeJason;
            /*el reader es para leer un archivo de disco*/ using (var reader = new StreamReader(_path))
            {
                MatrizDeJason = reader.ReadToEnd();
            }
            return MatrizDeJason;
        }

        public static void desserializarJsonFile(string MatrizDeJason)
        {
            Console.WriteLine(MatrizDeJason);
            var leido = JsonConvert.DeserializeObject<List<>>(MatrizDeJason) ;
        }
    }
}
