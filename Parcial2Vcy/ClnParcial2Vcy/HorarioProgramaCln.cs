using CadParcial2Vcy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnParcial2Vcy
{
    public class HorarioProgramaCln
    {
        public static int crear(HorarioPrograma horarioPrograma)
        {
            using (var context = new Parcial2VcyEntities())
            {
                var existe = context.HorarioPrograma.Where(x => x.idCanal == horarioPrograma.idCanal).FirstOrDefault();
                if (existe != null) throw new Exception("El canal ya existe");

                context.HorarioPrograma.Add(horarioPrograma);
                context.SaveChanges();
                return horarioPrograma.id;
            }
        }
        public static int actualizar(HorarioPrograma horarioPrograma)
        {
            using (var context = new Parcial2VcyEntities())
            {
                var existe = context.HorarioPrograma.Find(horarioPrograma.id);
                if (existe == null) throw new Exception($"El Canal con id {horarioPrograma.id} no existe");

                existe.id = horarioPrograma.id;
                existe.idCanal = horarioPrograma.id;
                existe.titulo = horarioPrograma.titulo;
                existe.horaInicio = horarioPrograma.horaInicio;
                existe.horaFin = horarioPrograma.horaInicio;
                existe.diaSemana = horarioPrograma.diaSemana;
                existe.estado = horarioPrograma.estado;

                return context.SaveChanges();

            }
        }
        public static int eliminar(int id)
        {
            using (var context = new Parcial2VcyEntities())
            {
                var existe = context.HorarioPrograma.Find(id);
                if (existe == null) throw new Exception($"El Canal con id {id} se Elimino");

                existe.estado = -1;

                return context.SaveChanges();

            }
        }
        public static HorarioPrograma obtenerUno(int id)
        {
            using (var context = new Parcial2VcyEntities())
            {
                var HorarioPrograma = context.HorarioPrograma.Find(id);
                if (HorarioPrograma == null) throw new Exception($"El Canal con id {id} no existe");
                return HorarioPrograma;
            }
        }
        public static List<HorarioPrograma> listar()
        {
            using (var context = new Parcial2VcyEntities())
            {
                return context.HorarioPrograma.Where(x => x.estado == 1).OrderBy(x => x.titulo).ToList();
            }
        }
        public static List<paHorarioProgramaListar_Result> listarPa(string parametro)
        {
            using (var context = new Parcial2VcyEntities())
            {
                return context.paHorarioProgramaListar(parametro.Trim()).ToList();
            }
        }
    }
}
