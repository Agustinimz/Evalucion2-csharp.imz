using ModeloLectura;
using ModeloLectura.DAL;
using ServidorSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imz.ConexionMedidor.Comunicacion
{
    public class HbCliente
    {
        private ClienteCom clienteCom;
        private ModeloLecturaDAL mensajesDAL = ModeloLecturaDALArchivos.GetInstancia();

        public HbCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void ejecutar()
        {
            //ahora traemos el codigo que atiende al cliente
            clienteCom.Escribir("Ingrese Numero de Medidor (formato 1234567)");
            string nromedidor = clienteCom.Leer();

            clienteCom.Escribir("Ingrese Fecha (formato yyyy-MM-dd HH:mm:ss)");
            string fecha = clienteCom.Leer();

            clienteCom.Escribir("Ingrese Valor Consumo (kw/h utilizando coma decimal)");
            string valorconsumo = clienteCom.Leer();
            LecturaMedidor lecturamedidor = new LecturaMedidor()
            {
                NroMedidor = Convert.ToInt32(nromedidor),
                Fecha = fecha,
                ValorConsumo = Convert.ToDecimal(valorconsumo)
            };
            lock (mensajesDAL)
            {
                mensajesDAL.AgregarMensaje(lecturamedidor);
            }

            clienteCom.Escribir("chao");
            clienteCom.Desconectar();
        }
    }
}
