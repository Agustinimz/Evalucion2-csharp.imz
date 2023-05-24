using ModeloLectura.DAL;
using ServidorSocket;
using System;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;


namespace imz.ConexionMedidor.Comunicacion
{
    public class HbServidor
    {
        private ModeloLecturaDAL mensajesDAL = ModeloLecturaDALArchivos.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("S: Servidor iniciado en puerto {0}", puerto);
            Console.ResetColor();
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("                              S: Esperando cliente....");
                    Console.ResetColor();
                    Socket cliente = servidor.ObtenerCliente();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("                              S: Cliente recibido");
                    Console.ResetColor();
                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HbCliente clienteThread = new HbCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.ejecutar));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error fatal {0}", puerto);
                Console.ResetColor();
            }
        }
    }
}
