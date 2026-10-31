using CadParcial2Vcy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnParcial2Vcy
{
    public class CanalCln
    {
        public static List<Canal> listar()
        {
            using (var context = new Parcial2VcyEntities())
            {
                return context.Canal
                    .Where(x => x.estado == 1)
                    .OrderBy(x => x.nombre).ToList();
            }
        }
    }
}