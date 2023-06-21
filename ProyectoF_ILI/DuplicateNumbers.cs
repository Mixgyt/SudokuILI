using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoF_ILI
{
    //Codigo realizado por Kevin Edenilson Portillo Ayala
    public static class DuplicateNumbers
    {
        //Funcion que retorna true si hay numeros duplicados si no returna false
        public static bool NumerosDuplicados(int[,] sudokuBoard)
        {
            int size = sudokuBoard.GetLength(0);

            // Encontrar números repetidos en las filas
            List<int> rowDuplicates = FindRowDuplicates(sudokuBoard);

            // Encontrar números repetidos en las columnas
            List<int> columnDuplicates = FindColumnDuplicates(sudokuBoard);

            List<int> boxDuplicates;
            // Encontrar números repetidos en los cuadros de 3x3
            if (size != 9 && size != 3) 
            {
                boxDuplicates = FindBoxDuplicates2x3(sudokuBoard);
            }
            else if(size == 3)
            {
                boxDuplicates = null;
            }
            else
            {
               boxDuplicates = FindBoxDuplicates(sudokuBoard);
            }

            //Si el tamaño es mayor a 3 entocnes se realiza este proceso de encontrar numeros repetidos
            if (size > 3)
            {
                if (rowDuplicates.Count == 0 && columnDuplicates.Count == 0 && boxDuplicates.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            //Si el tamaño es menor o igual a 3 entonces se realiza este proceso de encontrar numeros repetidos
            else
            {
                if (rowDuplicates.Count == 0 && columnDuplicates.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        //Codigo que retorna el numero de errores en la tabla
        public static int Errores(int[,] sudokuBoard)
        {
            int size = sudokuBoard.GetLength(0);
            int inRow = FindRowDuplicates(sudokuBoard).Count;
            int inColumn = FindColumnDuplicates(sudokuBoard).Count;
            int inBox;
            if (size > 3)
            {
                if (size == 6)
                {
                    inBox = FindBoxDuplicates2x3(sudokuBoard).Count;
                }
                else
                {
                    inBox = FindBoxDuplicates(sudokuBoard).Count;
                }
                return inRow + inColumn + inBox;
            }
            else
            {
                return inRow + inColumn;
            }

            
        }


    //Busca numeros repetidos en las filas
    static List<int> FindRowDuplicates(int[,] sudokuBoard)
    {
            int size = sudokuBoard.GetLength(0);
            List<int> duplicates = new List<int>();

            for (int row = 0; row < size; row++)
            {
                HashSet<int> seen = new HashSet<int>();

                for (int col = 0; col < size; col++)
                {
                    int number = sudokuBoard[row, col];

                    if (number != 0 && !seen.Add(number))
                    {
                        if (!duplicates.Contains(number))
                        {
                            //Codigo usado en el debug
                            //Console.WriteLine($" fila Error ({row},{col}), numero {number}");
                            duplicates.Add(number);
                        }
                    }
                }
            }

            return duplicates;
    }
        //Busca numeros repetidos en las columnas
        static List<int> FindColumnDuplicates(int[,] sudokuBoard)
        {
            int size = sudokuBoard.GetLength(0);
            List<int> duplicates = new List<int>();

            for (int col = 0; col < size; col++)
            {
                HashSet<int> seen = new HashSet<int>();

                for (int row = 0; row < size; row++)
                {
                    int number = sudokuBoard[row, col];

                    if (number != 0 && !seen.Add(number))
                    {
                        if (!duplicates.Contains(number))
                        {
                            //Codigo usado en el debug
                            //Console.WriteLine($" columna Error ({row},{col}), numero {number}");
                            duplicates.Add(number);
                        }
                    }
                }
            }

            return duplicates;
        }

        // Función para encontrar números repetidos en los cuadros de 3x3
        static List<int> FindBoxDuplicates(int[,] sudokuBoard)
        {
            List<int> duplicates = new List<int>();

            for (int boxRow = 0; boxRow < 3; boxRow++)
            {
                for (int boxCol = 0; boxCol < 3; boxCol++)
                {
                    HashSet<int> seen = new HashSet<int>();

                    // Iterar sobre las celdas de cada cuadro de 3x3
                    for (int row = boxRow * 3; row < boxRow * 3 + 3; row++)
                    {
                        for (int col = boxCol * 3; col < boxCol * 3 + 3; col++)
                        {
                            int number = sudokuBoard[row, col];

                            if (number != 0 && !seen.Add(number))
                            {
                                if (!duplicates.Contains(number))
                                {
                                    duplicates.Add(number);
                                }
                            }
                        }
                    }
                }
            }

            return duplicates;
        }

        // Función para encontrar números repetidos en las regiones de 2x3
        static List<int> FindBoxDuplicates2x3(int[,] sudokuBoard)
        {
            List<int> duplicates = new List<int>();

            for (int boxRow = 0; boxRow < sudokuBoard.GetLength(0) / 2; boxRow++)
            {
                for (int boxCol = 0; boxCol < sudokuBoard.GetLength(1) / 3; boxCol++)
                {
                    HashSet<int> seen = new HashSet<int>();

                    for (int row = boxRow * 2; row < boxRow * 2 + 2; row++)
                    {
                        for (int col = boxCol * 3; col < boxCol * 3 + 3; col++)
                        {
                            int number = sudokuBoard[row, col];

                            if (number != 0 && !seen.Add(number))
                            {
                                if (!duplicates.Contains(number))
                                {
                                    //Codigo usado en el debug
                                    //Console.WriteLine($" caja Error ({row},{col}), numero {number}");
                                    duplicates.Add(number);
                                }
                            }
                        }
                    }
                }
            }

            return duplicates;
        }
    }
}

