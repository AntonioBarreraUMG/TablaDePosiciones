using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablaDePosiciones.Datos
{
    class EquipoDTO
    {
        public string nombre { get; set; }
        public int juegos { get; set; }
        public int puntos { get; set; }
        public int partidosGanados { get; set; }
        public int partidosEmpatados { get; set; }
        public int partidosPerdidos { get; set; }
        public int golesFavor { get; set; }
        public int golesEnContra { get; set; }
        public int golesDeVisitante { get; set; }
        public int diferenciaDeGoles { get; set; }

        public EquipoDTO(String nombre, int juegos, int puntos, int partidosGanados, int partidosEmpatados, int partidosPerdidos,
        int golesFavor, int golesEnContra, int golesDeVisitante, int diferenciaDeGoles) {
            this.nombre = nombre;
            this.juegos= juegos;
            this.puntos=puntos;
            this.partidosGanados=partidosGanados;
            this.partidosEmpatados = partidosEmpatados;
            this.partidosPerdidos = partidosPerdidos;
            this.golesFavor= golesFavor;
            this.golesEnContra = golesEnContra;
            this.golesDeVisitante = golesDeVisitante;
            this.diferenciaDeGoles = diferenciaDeGoles;
        }

    }
}
