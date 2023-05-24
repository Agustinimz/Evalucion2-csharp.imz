using imz.ConexionMedidor.Comunicacion;
using ModeloLectura;
using ModeloLectura.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imz.ConexionMedidor
{

    public class Program
    {
        private static ModeloLecturaDAL mensajesDAL = ModeloLecturaDALArchivos.GetInstancia();

        static bool Menu()
        {
            bool continuar = true;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Selecciona una de las Opciones");
            Console.ResetColor();

            Console.WriteLine("0. Salir");
            Console.WriteLine("1. Ingresar");
            Console.WriteLine("2. Mostrar");
            Console.WriteLine("9. Limpiar");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    Environment.Exit(0);
                    break;
                case "9":
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Opcion no encontrada, intentalo nuevamente........");
                    Console.ResetColor();
                    
                    break;
            }
            return continuar;
        }

        static void Main(string[] args)
        {
            //Iniciar el Servidor Socket en el puerto 3000
            //El puerto tiene que ser configurable App.Config
            //IniciarServidor();

            HbServidor hebra = new HbServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.Start();
            while (Menu()) ;
        }

        static void Ingresar()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Ingresa el numero de tu medidor");
            Console.ResetColor();
            Console.WriteLine("Numero del medidor: ");
            string nromedidor = Console.ReadLine().Trim();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Ingresa la Fecha (formato yyyy-MM-dd HH:mm:ss)");
            Console.ResetColor();
            Console.WriteLine("Fecha: ");
            string fecha = Console.ReadLine().Trim();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Ingresa el valor de tu consumo actual kw/h utilizando coma decimal");
            Console.ResetColor();
            Console.WriteLine("Valor Consumo: ");
            string valorconsumo = Console.ReadLine().Trim();
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
        }

        static void Mostrar()
        {
            List<LecturaMedidor> lecturamedidores = null;
            lock (mensajesDAL)
            {
                lecturamedidores = mensajesDAL.ObtenerMensajes();
            }
            foreach (LecturaMedidor lecturamedidor in lecturamedidores)
            {
                Console.WriteLine(lecturamedidor);
            }

        }
    }
}

