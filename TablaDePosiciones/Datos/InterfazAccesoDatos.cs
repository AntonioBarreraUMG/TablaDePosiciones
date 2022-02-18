using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablaDePosiciones.Datos
{
    interface InterfazAccesoDatos
    {
        public string[,] leerDatos();
        public void escribirDatos(string[,] matriz);
        
    }
}
