using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorSocket
{
    public class ServerSocket
    {
        //Asignamos variables para la conexion

        private int puerto;
        private Socket servidor;

        public ServerSocket(int puerto)
        {
            this.puerto = puerto;
        }

        //Iniciamos la conexion
        //En caso de ser TRUE = conexion exitosa, de lo contrario FALSE = errror de conexion

        public bool Iniciar()
        {
            try
            {
                this.servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));
                this.servidor.Listen(10);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Cerrar()
        {
            try
            {
                this.servidor.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public Socket ObtenerCliente()
        {
            return this.servidor.Accept();
        }
    }
}
