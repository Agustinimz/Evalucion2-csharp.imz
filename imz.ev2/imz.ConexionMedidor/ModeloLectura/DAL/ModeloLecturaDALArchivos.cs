using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloLectura.DAL
{
    public class ModeloLecturaDALArchivos : ModeloLecturaDAL
    {
        //implementar Singleton:
        //1. Contructor tiene que ser private
        private ModeloLecturaDALArchivos() { }

        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static ModeloLecturaDALArchivos instancia;
        //3. tener un metodo getIntance, que de vuelva una referencia al atributo
        public static ModeloLecturaDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new ModeloLecturaDALArchivos();
            }
            return instancia;
        }
        //como vamos a hacer para que 2 hebras no accedan a la vez a este archivo?????


        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";
        public void AgregarMensaje(LecturaMedidor lecturamedidor)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(archivo, true))
                {
                    write.WriteLine(lecturamedidor.NroMedidor + "|" + lecturamedidor.Fecha + "|" + lecturamedidor.ValorConsumo);
                    write.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<LecturaMedidor> ObtenerMensajes()
        {
            List<LecturaMedidor> lista = new List<LecturaMedidor>();
            try
            {
                using (StreamReader read = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = read.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            LecturaMedidor lecturamedidor = new LecturaMedidor()
                            {
                                NroMedidor = Convert.ToInt32(arr[0]),
                                Fecha = arr[1],
                                ValorConsumo = Convert.ToDecimal(arr[2])
                            };
                            lista.Add(lecturamedidor);
                        }

                    } while (texto != null);
                }

            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }


    }
}
