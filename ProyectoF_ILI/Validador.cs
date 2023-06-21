using System;

//Codigo escrito por César Enoc Aparicio Reyes
//Esto es una bifurcacion de validadores de tablas encontrados en la web
namespace ProyectoF_ILI
{
    //Clase Validador que verifica valores validos segun su fila y columna
    //Esta clase verifica los valores aun antes de ingresarlos
    public class Validador
    {
        //Verifica si el valor ingresado es valido segun su respectiva Fila

        //Sus valores de entrada el numero a verificar, la fila y su columna,
        //ademas de la tabla que en la que verificara los valores

        //Retornara false si encuentra numeros repetidos si no retornara true
        public static bool ValidoEnFila(int numero, int nFila, int nColumna, int[,]tabla)
        {
            int size = tabla.GetLength(0);
            //Bucle que la recorre las columnas segun su respectiva fila
            for(int columna = 0; columna < size; columna++)
            {
                if (tabla[nFila,columna] == numero && nColumna != columna)
                {
                   // Codigo utilizado en el debug del proyecto
                   // Console.WriteLine("Fila No Valido");

                    //Retorna false si se econtro un numero repetido en la fila
                    return false;
                }
            }
            //Si no se econtro un numero repetido se retorna true
            return true;
        }

        //Verifica si el valor ingresado es valido segun su respectiva Columna

        //Sus valores de entrada el numero a verificar, la fila y su columna,
        //ademas de la tabla que en la que verificara los valores

        //Retornara false si encuentra numeros repetidos en la columna si no retorna true
        static public bool ValidoEnColumna(int numero, int nFila, int nColumna, int[,] tabla)
        {
            int size = tabla.GetLength(0);
            //Bucle que recorre las respectivas filas de una columna
            for (int fila = 0; fila < size; fila++)
            {
                if (tabla[fila, nColumna] == numero && nFila != fila)
                {
                    //Codigo utilizado en el debug del proyecto
                    //Console.WriteLine("Columna No Valido");
                    
                    //Econtro numeros repetidos retorna false
                    return false;
                }
            }
            //No econtro numeros repetidos retorna true
            return true;
        }

        //Verifica si el valor ingresado es valido segun su respectiva Region de 3x3 o 2x3

        //Sus valores de entrada el numero a verificar, la fila y su columna,
        //ademas de la tabla que en la que verificara los valores

        //Retorna false si encuentra numeros repetidos en la region, si no es true
        public static bool ValidoEnRegion(int numero, int nFila, int nColumna, int[,] tabla)
        {
            //Variables necesarias para calcular las regiones
            int size = tabla.GetLength(0);
            int nfilas = 3;
            int nFilaRegion = (nFila % 3);

            //Segun el tamaño de tabla las regiones cambian por lo cual
            //Si la tabla es de 6x6 las regiones son de 2x3 por lo contrario la region es de 3x3
            if (size == 6)
            {
                nFilaRegion = (nFila % 2);
                nfilas = 2;
            }  
            int nColumnaRegion = (nColumna%3);

            //Bucle que verificara la fila y columna dentro de la region
            for (int fila = 0; fila < nfilas; fila++)
            {
                //Si la fila es distinta al numero de fila de region entonces fila se procede comprobar la columna
                if (fila != nFilaRegion)
                {
                    //Bucle para comprobar columnas
                    for (int columna = 0; columna < 3; columna++)
                    {
                        //Si la columna es distinta al numero de columna de region entonces
                        //se procede a comprbar cada espacio de 3x3 (o 2x3) de la region
                        if (columna != nColumnaRegion)
                        {
                            //Se obtinene los numeros pequeños de las filas y las tablas de 3x3 o 2x3
                            int filaTabla = nFila - nFilaRegion + fila;
                            int columnaTabla = nColumna - nColumnaRegion + columna;

                            //Si se ecuentra el numero dentro del la casilla entonces se retornara falso
                            if (tabla[filaTabla, columnaTabla] == numero)
                            {
                                //Codigo utilizado en el debug del proyecto
                                //Console.WriteLine("Region No Valido");
                                return false;
                            }
                        }
                    }
                }
            }
            //Si no se encontro ningun numero repetido en la region entonces retorna true
            return true;
        }

        //Funcion que verifica todas las posibles repeticiones que puede tener un numero en la tabla segun columna fila y region
        public static bool ValidarNumero(int numero, int nFila, int nColumna, int[,] tabla)
        {
            int size = tabla.GetLength(0);
            //Se comprueba que no hayan numeros repetidos en la fila
            if(!ValidoEnFila(numero,nFila, nColumna, tabla))
            {
                //Si los retornara falso
                return false;
            }
            //Se comprueba que no hayan numeros repetidos en la columna
            if(!ValidoEnColumna(numero, nFila, nColumna,tabla))
            {
                //Si los hay retornara falso
                return false;
            }
            //Se verifica que la tabla mide mas de 3 para verificar su region
            if (size > 3)
            {
                //Si lo es entonces verifica su region
                //Se comprueba que no hayan numeros repetidos en las regiones
                if (!ValidoEnRegion(numero, nFila, nColumna, tabla))
                {
                    //Si los hay retornara falso
                    return false;
                }
            }
            //Si ninguno retorno falso entonces es que no hay numeros repetidos
            //Por lo cual retornara true
            return true;
        }
    }
}
