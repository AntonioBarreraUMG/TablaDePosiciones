using System;

namespace TablaDePosiciones
{
    class Program
    {
        public static void Main()
        {
            bool salir=false;
            while (!salir) {
                Console.WriteLine("1. Agregar Partido: ");
                Console.WriteLine("2. Desplegar Tabla: ");
                Console.WriteLine("3. Salir: ");
                Console.WriteLine("Elige una de las opciones");
                int opcion = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-----------------------\n");
                switch (opcion)
                {
                    case 1:
                        break;

                    case 2:
                        break;
                    case 3:
                        Console.WriteLine("Saliste de la aplicación");
                        salir = true;
                        break;

                }
            }
            
        }
    }
}