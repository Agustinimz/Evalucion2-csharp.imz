using SimuladorMedidorUt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorMedidor
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Conexion al servidor
            int puerto;
            string servidor;


            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Ingrese la ip del servidor");
                Console.ResetColor();
                servidor = Console.ReadLine().Trim();


                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Ingrese el puerto (puerto preferencia: 3000)");
                Console.ResetColor();
                puerto = Convert.ToInt32(Console.ReadLine().Trim());


                // Creamos el socket para la conexion al servidor
                ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

                // Validamos la conexion
                if (clienteSocket.Conectar())
                {
                    //OK, puede conectar
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Enhorabuena!! conexion exitosa...");
                    Console.ResetColor();

                    //aqui esta el protocolo de comunicacion
                    bool seguir = true;
                    string respuesta;
                    string mensaje_server;

                    do
                    {
                        mensaje_server = clienteSocket.Leer();
                        if (mensaje_server == "chao")
                        {
                            seguir = false;
                            clienteSocket.Desconectar();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("                       Te has desconectado del servidor...");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("S: " + mensaje_server);
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Envia una respuesta: ");
                            Console.ResetColor();
                            respuesta = Console.ReadLine().Trim();
                            clienteSocket.Escribir(respuesta);

                        }
                        
                    } while (seguir);
                }
                else
                {
                    Console.WriteLine("Error de conexion, el puerto {0} esta en uso", puerto);
                }
            }
        }
    }
}