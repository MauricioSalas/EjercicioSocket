using EjercicioSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioServerSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en puerto: {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                Console.WriteLine("Servidor iniciado!");
                while (true)
                {
                    Console.WriteLine("Esperando cliente...");
                    Socket socketCliente = servidor.ObtenerCliente();
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    bool desconectar = false;
                    cliente.Escribir("Chat iniciado.");
                    while (!desconectar)
                    {
                        string respuesta = cliente.Leer();
                        Console.WriteLine("Cliente: {0}", respuesta);
                        if (String.IsNullOrEmpty(respuesta))
                        {
                            desconectar = false;
                        }
                        else
                        {
                            if (respuesta.ToLower() == "chao")
                            {
                                desconectar = true;
                            }
                            else
                            {
                                respuesta = Console.ReadLine().Trim();
                                if (respuesta.ToLower() == "chao")
                                {
                                    cliente.Escribir("Servidor Desconectado.");
                                    desconectar = true;
                                }
                                else
                                {
                                    cliente.Escribir("Servidor: " + respuesta);
                                }
                            }
                        }
                    }
                    cliente.Desconectar();
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} está en uso.", puerto);
            }
        }
    }
}
