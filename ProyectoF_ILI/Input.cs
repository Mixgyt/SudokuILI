using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoF_ILI
{
    public static class Input
    {
        //Codigo escrito por César Enoc Aparicio Reyes
        //Proposito del codigo recibir las entradas y validar que sean correctas, o si son comandos del usuario
        
        //Esta funcion es la predefinida que busca una casilla automaticamente para ingresarle un valor
        public static void NuevaJugada(int[,]tabla, int dificultad, out int sum, out string command)
        {
            //Variables que retornan valores
            command = "";
            sum = 0;
            //Codigo para ingresar los valores al sudoku
            if(BuscarJugada(tabla, dificultad, out int fila, out int columna))
            {
                //Se inicia la funcion ingresar numero
                int numero = InputNumber(dificultad, fila, columna, out string com);
                //Si la variable com esta vacia entonces no hay comandos ingresados y prosigue con normalidad
                if (com == "")
                {
                    //El validador verifica que el numero ingresado esta correcta y le otorga un puntaje
                    if (Validador.ValidarNumero(numero, fila, columna, tabla))
                    {
                        //Si esta correcto recibe 100 puntos
                        sum = 100;
                    }
                    else
                    {
                        //Si es incorrecto le quitan 50 puntos
                        sum = -50;
                    }
                    //se asigna el numero a la tabla
                    tabla[fila, columna] = numero;
                }
                //Si l variable com no esta vacia entonces se retornara 0 ademas de retornar el comando escrito
                else
                {
                    //Se le asigna 0 a la coordenada de la tabla
                    tabla[fila, columna] = 0;
                    //Se le asigna el comando de retorno
                    command = com;
                }
            }
            else
            {
                //Esta parte se ejecuta cuando ya no se encuentran mas jugadas automaticamente
                Console.WriteLine("No se más encontraron jugadas");
            }
        }

        //Funcion que retorna numero para asignar en filas y columnas
        public static int InputNumber(int dificultad, int fila, int columna, out string command)
        {
            //variables necesarias
            command = "";
            int number;

            //Se multiplica 3 por la dificultad para saber el tamaño de la tabla
            int size = 3 * dificultad;

            //Bucle para ingresar numeros
            while (true)
            {
                // este string guardara los valores ingresados por si acaso son cosiderados un comando
                string input = "";
                //Try para comprobar las si los valores no causan error al convertirse
                try
                {
                    //Se le pide al usuario que ingrese el valor en la respectiva coordenada
                    Console.WriteLine($"Escribe el valor a ingresar en coordenada ({fila+1},{columna+1})");
                    input = Console.ReadLine().Trim();
                    number = Convert.ToInt32(input);
                    //Si el valor es valido el bucle se acaba
                    if (number > 0 && number <= size)
                    {
                        break;
                    }
                    //Si el valor no es valido el bucle continua hasta recibir un valor valido
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Por favor ingrese un numero entero valido, entre 1 a {0}",size);
                        Console.ResetColor();
                    }
                }
                //Si la comversion del valor causo un error entonces se ejecuta catch
                catch
                {
                    //Si el valor en input concuerda con coord o salir entonces son un comando
                    //Comando coord permite al usuario entrar en modo de ingreso de coordenadas
                    if(input == "coord" || input == "Coord" || input == "COORD")
                    {
                        command = "coord";
                        number = 0;
                        break;
                    }
                    //Comando clean que permite borrar todo el avance de la tabla para comenzar a llenarla nuevamente
                    else if(input == "clean" || input == "Clean" || input == "CLEAN")
                    {
                        command = "clean";
                        number = 0;
                        break;
                    }
                    //Comando
                    else if (input == "c?" || input == "C?")
                    {
                        command = "c?";
                        number = 0;
                        break;
                    }
                    //Comando Salir permite al usuario salir a voluntad en medio de la partida
                    else if(input == "salir" || input == "Salir" || input == "SALIR")
                    {
                        command = "salir";
                        number = 0;
                        break;
                    }
                    //Si no es ningun comando entonces es un error y se le cataloga como tal
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Error debes ingresar un valor numerico valido");
                        Console.ResetColor();
                    }
                }
            }
            return number;
        }

        //Funcion que busca jugadas automaticamente retorna true si la encuentra y false si no encuentra
        public static bool BuscarJugada(int[,] tabla, int dificultad, out int fila, out int columna)
        {
            //Variables necesarias
            fila = 0; 
            columna = 0;
            //Variable bool que verifica si encontro jugadas, por defecto true
            bool jugadas = true;
            //Se multiplica 3 por la dificultad para saber el tamaño de la tabla
            int size = 3 * dificultad;

            //Bucle que busca 0 dentro del array para saber donde posicionar la jugada
            for (int a = 0; a < size; a++)
            {
                for (int b = 0; b < size; b++)
                {
                    if (tabla[a, b] == 0)
                    {
                        //Si encontro un creo le asigna la coordenada a fila y columna y jugadas es igual a falso
                        fila = a;
                        columna = b;
                        jugadas = false;
                        return !jugadas;
                    }
                }
                //Si ya encontro un 0 entonces se sale del bucle automaticamente
                if (!jugadas)
                {
                    break;
                }
            }
            //Si encontro 0 entonces jugadas es false, pero se envia distinto a jugadas para recibir valores claros
            return !jugadas;
        }

        //Funcion que permite ingresar las jugadas manualmente
        public static void IngresarJugada(int[,] tabla, int dificultad, int score, out int sum)
        {
            //Objeto para dibujar la tabla
            TableDrawing draw = new TableDrawing();
            draw.Difitult = dificultad;

            //comando por defecto esta vacio
            string command = "";
            //bucle que ejecuta todo lo relaciona a ingresar una jugada
            while (true) 
            {
                //Primero se dibuja la tabla en el modo de ingreso de coordenadas
                draw.Drawing(tabla,score,"Modo de Ingreso de Coordenadas");
                //Espaciado
                Console.WriteLine();
                //Se invoca a la funcion para ingresar coordenadas manualmente
                IngresarCoordenada(dificultad, out int fila, out int columna);
                //Si retorna numeros negativos entonces signfica que el proceso se cancelo
                if(fila == -1 || columna == -1)
                {
                    //puntucacion = 0;
                    sum = 0;
                    //fin del bucle
                    break;
                }

                //Si continua el proceso de la jugada manual
                //Se crea la variable numero que guardara el numero que deseemos escribir en la tabla
                int numero;
                while (true)
                {
                    //Funcion input number para ingresar un numero valido
                    numero = InputNumber(dificultad, fila, columna, out string com);
                    //Si numero es distinto a 0 y comando esta vacio entonces el numero es correcto y finaliza el bucle
                    if (numero != 0 && com == "")
                    {
                        break;
                    }
                    //Si el comando es igual a salir entonces se finaliza el proceso de jugada manual
                    else if(com == "salir")
                    {
                        command = com;
                        break;
                    }
                }
                //Se verifica fuera del anterior bucle para cancelar el bucle principal si el proceso se cancelo
                if(command == "salir")
                {
                    sum = 0;
                    break;
                }

                //Si el proceso sigue correctamente entonces se verifica que la coordenada seleccionada este vacia
                if (tabla[fila, columna] == 0)
                {
                    //Si esta vacia entonces se incia la validacion para saber el puntaje que se asignara
                    if (Validador.ValidarNumero(numero, fila, columna, tabla))
                    {
                        //Mas 100 puntos si el numero es valido
                        sum = 100;
                    }
                    else
                    {
                        //Menos 50 puntos si el numero es invalido
                        sum = -50;
                    }
                    //Se asigna el numero a la tabla y se finaliza el proceso correctamente
                    tabla[fila, columna] = numero;
                    break;
                }
                //Si en un dado caso la casilla seleccionada tiene un numero entonces se reinicia el proceso de seleccion de coordenadas
                else
                {
                    Console.WriteLine("La casilla que estas ingresando ya tiene valores");
                    Console.WriteLine("Debes ingresar una casilla vacia");
                    Console.WriteLine("Presiona Enter para seguir");
                    Console.ReadKey();
                }
            }
        }

        //Funcion que permite ingresar coordenadas manualmente
        public static void IngresarCoordenada(int dificultad, out int fila, out int columna)
        {
            //Se multiplica 3 por la dificultad para saber el tamaño de la tabla
            int size = 3 * dificultad;

            //Ingresar Fila
            while (true)
            {
                //Codigo que permite ingresar un valor a la fila
                Console.WriteLine("Escribe la coordenada de la Fila");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                //El valor valido dependera del tamaño de la tabla
                Console.WriteLine("Valores validos del 1 al {0}", size);
                Console.ResetColor();
                //Se recibe un valor
                string filaStr = Console.ReadLine().Trim();
                //Comrobacion que no genere error
                try
                {
                    fila = Convert.ToInt32(filaStr);
                    //Si la conversion es correcta y el valor es valido el bucle termina y se le asigna un valor fila
                    if(fila > 0 && fila <= size)
                    {
                        fila--;
                        break;
                    }
                    //Si el valor es invalido entonces se muestra un error y se prosigue el bucle hasta recibir un valor valido
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Debe ingresar un valor valido entre 1 a {0}", size);
                        Console.ResetColor();
                    }
                }
                //Si dio error la conversion entonces se trata de saber si lo ingresado es un comando
                catch
                {   //Si el comando es salir entonces se retorna el proceso y retorna una fila negativa
                    if (filaStr == "salir" || filaStr == "Salir" || filaStr == "SALIR")
                    {
                        fila = -1;
                        break;
                    }
                    //Si no es un comando entonces se toma como un error y retorna el proceso en hasta obtener un valor valido
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Error Valor incorrecto");
                        Console.ResetColor();
                    }
                }
            }
            //Si la fila es negativa entonces retorna el bucle por que se da por hecho que el proceso se cancelo
            if(fila == -1)
            {
                columna = -1;
                return;
            }

            //Ingresar columna
            while (true)
            {
                //Codigo para ingresar valor a la columna
                Console.WriteLine("Escribe la coordenada de la Columna");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                //El valor valido dependera del tamaño de la tabla
                Console.WriteLine("Valores validos del 1 al {0}", size);
                Console.ResetColor();
                //Se guarda el valor ingresado
                string columnaStr = Console.ReadLine().Trim();
                //Comprueba que la conversion del valor es correcta
                try
                {
                    columna = Convert.ToInt32(columnaStr);
                    //Si la conversion correcta y el valor es valido entonces se retornara el valor de la columna
                    if (columna > 0 && columna <= size)
                    {
                        columna--;
                        break;
                    }
                    //Si el valor es invalido entonces se retorna nuevamente el bucle en espera de un nuevo valor
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Debe ingresar un valor valido entre 1 a {0}", size);
                        Console.ResetColor();
                    }
                }
                //Si a conversion fallo se va en busqueda que el valor sea un comando
                catch
                {
                    //Si el comando es salir entonces comienza el proceso de salir del modo de coordenada
                    if (columnaStr == "salir" || columnaStr == "Salir" || columnaStr == "SALIR")
                    {
                        columna = -1;
                        break;
                    }
                    //Si no es considerado un comando entonces se retorna como un error en espera de un nuevo valor valido
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Error Valor incorrecto");
                        Console.ResetColor();
                    }
                }
            }
        } 

        //Que hace este codigo ???
        public static void C_()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("        ---Creditos---");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("           Grupo #2");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" Jaqueline Lisseth Ramirez Ramos");
            Console.WriteLine("  Erick Alexis Delgado Sanchez");
            Console.WriteLine("   Cesar Enoc Aparicio Reyes");
            Console.WriteLine(" Kevin Edenilson Portillo Ayala");
            Console.WriteLine(" William Ernesto Romero Aguilar");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("      Diseñador del Juego");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("   Cesar Enoc Aparicio Reyes");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("         Programadores");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" Jaqueline Lisseth Ramirez Ramos");
            Console.WriteLine("  Erick Alexis Delgado Sanchez");
            Console.WriteLine(" Kevin Edenilson Portillo Ayala");
            Console.WriteLine(" William Ernesto Romero Aguilar");
            Console.WriteLine("");
            Console.WriteLine("");
            
            Console.Write(" Con mucha dedicacion y esfuerzo, ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Gracias por Jugar");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" Presiona Enter para continuar...");
            Console.ResetColor();
            Console.ReadKey();

        }
    }
}
