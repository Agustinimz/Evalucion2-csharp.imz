using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloLectura.DAL
{
    public interface ModeloLecturaDAL
    {
        void AgregarMensaje(LecturaMedidor lecturamedidor);
        List<LecturaMedidor> ObtenerMensajes();
    }
}

