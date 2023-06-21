using System;
using System.ComponentModel;

namespace ProyectoF_ILI
{
    class Program
    {

        static void Main(string[] args)
        {
            //Variables necesarias fuera del bucle
            //Objeto para dibujar las tablas
            TableDrawing draw = new TableDrawing();

            //Variable que guarda el puntaje maximo
            int maxscore = 0;
            //Variable que comprueba si quieres seguir jugando o no
            bool seguir = true;
            while (seguir)
            {
                //Variables necesarias dentro del while
                int dificultad;
                int puntuacion = 0;
                int[,] tabla;

                //La presentacion inicial del juego
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("SODOKU GRUPO #2\n");
                Console.ResetColor();
                //Seleccion de dificultad
                Console.WriteLine("Elige la Dificultad que deseas:\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("--> Facil = 1 \n--> Intermedio = 2 \n--> Dificil = 3\n");
                Console.ResetColor();

                //Logica para recibir un numero de entre 1 a 3
                while (true)
                {
                    //Try y Catch para capturar los errres que se puedan generar al convertir string a entero
                    try
                    {
                        //Covierte el string Console.ReadLine en un numero entero
                        int dificult = Convert.ToInt32(Console.ReadLine());
                        //Si el numero se encuentra en los valores validos
                        //se guarda el numero y se finaliza el bucle
                        if (dificult > 0 && dificult <= 3)
                        {
                            draw.Difitult = dificult;
                            dificultad = dificult;
                            break;
                        }
                        //Si no se explica que deben ser valores de entre 1 a 3 y se repite el bucle nuevamente
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Debe ingresar valores enteros entre 1 a 3");
                            Console.ResetColor();
                        }
                    }
                    catch
                    {
                        //Si hubo un error de escritura se muestra y en color rojo el texto
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Error debe de ingresar valores numericos validos");
                        Console.ResetColor();
                    }
                }
                //Este codigo crea una nueva tabla para jugar el sudoku dependiendo la dificultad elegida
                //Accede al table creator que maneja toda la logica de su creacion
                tabla = TableCreator.TableGenerator(dificultad);
                int nl = tabla.GetLength(0);
                int[,] tabclean = new int[nl,nl];
                Array.Copy(tabla, tabclean, tabla.Length);

                //String que convertira el valor entero de dificultad en un string
                string dificultadStr;
                //Este switch recibe el valor entero y le asigna un valor al string dependiendo la dificultad
                switch (dificultad)
                {
                    case 1:
                        dificultadStr = "Facil";
                        break;
                    case 2:
                        dificultadStr = "Intermedio";
                        break;
                    case 3:
                        dificultadStr = "Dificil";
                        break;
                    default:
                        dificultadStr = "Dificil";
                        break;
                }

                //Num de intentos que tomo resolver el sudoku
                int intentos = 0;

                //BUCLE DE JUEGO
                while (true)
                {
                    //Cada vez que se repita de redibujara la tabla
                    draw.Drawing(tabla, puntuacion, "Tabla de Juego Nivel: "+dificultadStr);
                    Console.WriteLine();
                    //Se buscara una nueva casilla para ingresar valores
                    Input.NuevaJugada(tabla, dificultad, out int sum, out string command);
                    if (command == "") 
                    {
                        puntuacion += sum;
                        //Se comprueba si el sudoku ya esta completo
                        if (!SudokuComplete.HasZero(tabla))
                        {
                            //Si esta completo finaliza el bucle de juego
                            break;
                        }
                    }
                    //Este else ingresa en el modo busqueda de coordenadas
                    else if(command == "coord")
                    {
                        Input.IngresarJugada(tabla, dificultad, puntuacion, out int sco);
                        puntuacion += sco;
                        if (!SudokuComplete.HasZero(tabla))
                        {
                            //Si esta completo finaliza el bucle de juego
                            break;
                        }
                    }
                    //Comando Clean para limpiar la tabla por si quiere retroceder al inicio nuevamente
                    else if(command == "clean")
                    {
                        //Copia el array limpio a la tabla para empezar a resolverlo nuevamente
                        Array.Copy(tabclean,tabla,tabclean.Length);
                        continue;
                    }
                    //?
                    else if(command == "c?")
                    {
                        Input.C_();
                        continue;
                    }
                    //Este else ingresa en el modo salir del juego
                    else if (command == "salir")
                    {
                        break;
                    }

                    //Si no se suma un intento más y se repite el bucle nuevamente
                    intentos++;
                }


                //Codigo de Erick Alexis Delgado Sanchez
                draw.Drawing(tabla, puntuacion, "Tabla de Juego Nivel: "+dificultadStr);
                //Busca cuantos erroes cometio el usuario
                int errores = DuplicateNumbers.Errores(tabla);

                Console.WriteLine();
                //Si verifica que no hay numeros duplicados para saber si el sudoku esta correcto
                if (DuplicateNumbers.NumerosDuplicados(tabla))
                {
                    //Se le añaden colores a la presentacion y se muestran resultados
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("**-PERDISTE-**");
                    Console.WriteLine("-Juego terminado-");
                    Console.WriteLine("Dificultad: " + dificultadStr);
                    Console.WriteLine("Puntuacion Final: " + puntuacion);
                    Console.WriteLine("Cantidad de Errores: " + errores);
                    Console.WriteLine("Intentos: " + intentos);
                }
                else if(!SudokuComplete.HasZero(tabla))
                {
                    //Se añaden colores a la presentacion y se muestran los resultados
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("°--GANASTE--°");
                    Console.WriteLine("-Juego terminado-");
                    Console.WriteLine("Dificultad: " + dificultadStr);
                    Console.WriteLine("Puntuacion Final: " + puntuacion);
                    Console.WriteLine("Intentos: " + intentos);
                }
                //Por defecto se pierde
                else
                {
                    //Se le añaden colores a la presentacion y se muestran resultados
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("**-PERDISTE-**");
                    Console.WriteLine("-Juego terminado-");
                    Console.WriteLine("Dificultad: " + dificultadStr);
                    Console.WriteLine("Puntuacion Final: " + puntuacion);
                    Console.WriteLine("Cantidad de Errores: " + errores);
                    Console.WriteLine("Intentos: " + intentos);
                }

                //Este if guarda el puntaje maximo cuando se obtiene un valor mas grande que el anterior
                if(maxscore < puntuacion || (maxscore == 0 && puntuacion < 0))
                {
                    maxscore = puntuacion;
                }

                //Este codigo hace te pregunta si queres volver a jugar o quieres salir
                Console.ResetColor();
                Console.WriteLine("\n¿Deseas volver a Jugar? [S/N] \nSi = S    No = N");
                while (true)
                {
                    //Se ingresa lo escrito en la variable entrada
                    string entrada = Console.ReadLine();
                    //Si es S se vuelve a iniciar la partida
                    if (entrada == "S" || entrada == "s")
                    {
                        Console.Clear();
                        Console.WriteLine("--EL JUEGO COMIENZA DE NUEVO--");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nPuntaje Maximo en esta sesion: " + maxscore);
                        Console.ResetColor();
                        Console.WriteLine("\nPresiona ENTER para continuar");
                        Console.ReadKey();
                        break;
                    }
                    //Si es N se finaliza el juego
                    else if(entrada == "N" || entrada == "n")
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("--Fin de la sesion de juego--");
                        seguir = false;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nPuntaje maximo en la sesion: " + maxscore);
                        Console.ResetColor();
                        Console.WriteLine("\nPresiona ENTER para salir");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        //Si no es S ni N entonces no es valor valido
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Valor no valido.");
                        Console.ResetColor();
                        Console.WriteLine("Si = S    No = N");
                    }
                }
            }
            //Fin de la estructura principal
            

        }
    }
}