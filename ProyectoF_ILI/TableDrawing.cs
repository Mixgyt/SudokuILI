using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoF_ILI
{
    //Codigo escrito por Jaqueline Lisseth Ramirez Ramos
    public class TableDrawing
    {
        //Variable que guarda la dificultad elegida desde el principio para su posterior uso
        public int Difitult {get; set;}

        //Funcion que dibuja la tabla segun su dificultad seleccionada
        public void Drawing(int[,] tabla, int score, string mode)
        {
            //El switch evalua cual es la dificultad y conforme a ello dibuja la tabla
            switch(Difitult)
            {
                case 1:
                    Facil(tabla,score, mode); break;
                case 2:
                    Intermedio(tabla, score, mode); break;
                case 3:
                    Dificil(tabla,score, mode); break;
                //Si el numero de dificultad no es valido se lanza un error
                default:
                    Console.WriteLine("Error Inesperado"); break;
            }
        }

        //Crea una matriz de 2 dimensiones para la dificultad facil
        public void Facil(int[,] tabla, int score, string mode)
        {
             
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                //Muestra la primera linea horizontal de la tabla
                Console.WriteLine("    1   2   3");
                Console.WriteLine("  #############");
                //Recorre la matriz de 2 dimensiones en las filas
                for (int fila = 0; fila < 3; fila++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    //Muestra la primera linea vertical
                    Console.Write(fila+1);
                    Console.Write(" # ");
                    //Recorre la matriz de 2 dimenciones en las columnas
                    for (int columna = 0; columna < 3; columna++)
                    {

                        //Le da color blanco a los números dentro de la tabla 
                        Console.ForegroundColor = ConsoleColor.White;
                        //Rellena la tabla con los valores del array "tabla"
                        if (tabla[fila, columna] != 0)
                        {
                            //Se ingresa su respectivo valor si es diferente a 0
                            Console.Write(tabla[fila, columna]); 
                        }
                        else
                        {
                            Console.Write(" ");//Se ingresa un espacio si el valor es 0
                        }
                        //Agrega espacio entre los números
                        Console.Write(" ");
                        //Aplica color a las columnas
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;

                        //Si la columna es 0 y 1
                        if (columna == 0 || columna == 1)
                        {
                            //Genera una columna de barra vertical
                            Console.Write("| ");
                        }
                        //si no es 0 ni 1, y es 2
                        else if (columna == 2)
                        {
                            //Genera una columna de asterisco
                            Console.Write("# ");
                        }
                    }
                    Console.Write("       ");
                    //Codigo que muesta la puntucacion obtenida
                    if (fila == 1)
                    {
                        Console.Write("Puntuacion: " + score);
                    }
                    //Codigo que sugiere los comandos
                    else if (fila == 2)
                    {
                        Console.Write("Comandos Utiles: 'coord' y 'salir'");
                    }
                    //Codigo que muestra el modo actual de la tabla
                    else if(fila == 0)
                    {
                        Console.Write(mode);
                    }
                    // Genera un espacio 
                    Console.WriteLine();
                    //Aplica color a las filas
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;

                    //se realiza el mismo procedimiento con las filas 
                    if (fila == 0 || fila == 1)
                    {
                        Console.WriteLine("  #---+---+---#");
                    }
                    else if (fila == 2)
                    { //Imprime ultima linea horizontal de la tabla 
                        Console.WriteLine("  #############");
                    }
                }
            }
            Console.ResetColor();
        }

        public void Intermedio(int[,] tabla, int score, string mode)
        {
            //Tabla para el nivel2: Intermedio

            /*Se realiza el mismo procedimiento para la tabla n°2,la diferencia es que aumenta 
             el numero de filas y columnas de la tabla,la tabla sera de 6 filas y 6 columnas */
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("    1   2   3   4   5   6");
                Console.WriteLine("  #########################");

                for (int fila = 0; fila < 6; fila++)
                {
                    Console.Write(fila + 1);
                    Console.Write(" # ");
                    for (int columna = 0; columna < 6; columna++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (tabla[fila, columna] != 0)
                        {
                            //Se ingresa su respectivo valor si es diferente a 0
                            Console.Write(tabla[fila, columna]);
                        }
                        else
                        {
                            Console.Write(" ");//Se ingresa un espacio si el valor es 0
                        }
                        Console.Write(" ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        if (columna == 0 || columna == 1 || columna == 3 || columna == 4)
                        {
                            Console.Write("| ");
                        }
                        else if (columna == 2 || columna == 5)
                        {
                            Console.Write("# ");
                        }
                    }
                    Console.Write("       ");
                    //Codigo que muesta la puntucacion obtenida
                    if (fila == 2)
                    {
                        Console.Write("Puntuacion: " + score);
                    }
                    //Codigo que sugiere los comandos
                    else if(fila == 3) 
                    {
                        Console.Write("Comandos Utiles: 'coord' y 'salir'");
                    }
                    //Codigo que muestra el modo actual de la tabla
                    else if (fila == 1)
                    {
                        Console.Write(mode);
                    }
                    
                    Console.WriteLine();
                    if (fila == 0 || fila == 2 || fila == 4)
                    {
                        Console.WriteLine("  #---+---+---#---+---+---#");
                    }
                    else if (fila == 1 || fila == 3 || fila == 5)
                    {
                        Console.WriteLine("  #########################");
                    }
                }
            }
            Console.ResetColor();
        }

        public void Dificil(int[,] tabla, int score, string mode)
        {
            // Tabla para el nivel3: Avanzado

            /*Utilizando el mismo procedimiento se realiza una tercera tabla 
             a la cual se le da un tamaño de 9 filas y 9 columnas */
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("    1   2   3   4   5   6   7   8   9");
            Console.WriteLine("  #####################################");

                for (int fila = 0; fila < 9; fila++)
                {
                    Console.Write(fila+1);
                    Console.Write(" # ");
                    for (int columna = 0; columna < 9; columna++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        //Rellena la tabla con los valores del array "tabla"
                        if (tabla[fila, columna] != 0)
                        {
                            //Se ingresa su respectivo valor si es diferente a 0
                            Console.Write(tabla[fila, columna]);
                        }
                        else
                        {
                            Console.Write(" ");//Se ingresa un espacio si el valor es 0
                        }
                        Console.Write(" ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        if (columna == 0 || columna == 1 || columna == 3 || columna == 4 || columna == 6 || columna == 7)
                        {
                            Console.Write("| ");
                        }
                        else if (columna == 2 || columna == 5 || columna == 8)
                        {
                            Console.Write("# ");
                        }
                    }
                Console.Write("       ");
                //Codigo que muesta la puntucacion obtenida
                if (fila == 4)
                {
                    Console.Write("Puntuacion: " + score);
                }
                //Codigo que sugiere los comandos
                else if (fila == 5)
                {
                    Console.Write("Comandos Utiles: 'coord' y 'salir'");
                }
                //Codigo que muestra el modo actual de la tabla
                else if (fila == 3)
                {
                    Console.Write(mode);
                }
                Console.WriteLine();

                    if (fila == 0 || fila == 1 || fila == 3 || fila == 4 || fila == 6 || fila == 7)
                    {
                        Console.WriteLine("  #---+---+---#---+---+---#---+---+---#");
                    }
                    else if (fila == 2 || fila == 5 || fila == 8)
                    {
                        Console.WriteLine("  #####################################");
                    }
                }
            Console.ResetColor();
        }
    }
}
