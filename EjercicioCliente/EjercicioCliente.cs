using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioCliente
{
    public class EjercicioCliente
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inserte la IP del servidor: \n");
            string servidor = Console.ReadLine().Trim();
            Console.WriteLine("Inserte el puerto del servidor: \n");
            int puerto = Convert.ToInt32(Console.ReadLine().Trim());
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Conectado a Servidor {0} en Puerto: {1}", servidor, puerto);
                string mensaje = clienteSocket.Leer();
                Console.WriteLine(mensaje);
                bool desconectar = false;
                while (!desconectar)
                {
                    mensaje = Console.ReadLine().Trim();
                    if (String.IsNullOrEmpty(mensaje))
                    {
                        desconectar = false;
                    }
                    else
                    {
                        clienteSocket.Escribir(mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            desconectar = true;
                            clienteSocket.Desconectar();
                        }
                        mensaje = clienteSocket.Leer();
                        Console.WriteLine(mensaje);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error de Comunicación.");
            }
            Console.ReadKey();
        }
    }
}
