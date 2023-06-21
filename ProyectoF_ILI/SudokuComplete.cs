using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoF_ILI
{
    //Codigo escrito por William Ernesto Romero Aguilar
    public static class SudokuComplete
    {
        public static bool IsSudokuComplete(int[,] sudoku)
        {
            int size = sudoku.GetLength(0);

            // Verificar filas
            for (int row = 0; row < size; row++)
            {
                bool[] found = new bool[size + 1];
                for (int col = 0; col < size; col++)
                {
                    int num = sudoku[row, col];
                    if (num < 1 || num > size || found[num])
                        return false;
                    found[num] = true;
                }
            }

            // Verificar columnas
            for (int col = 0; col < size; col++)
            {
                bool[] found = new bool[size + 1];
                for (int row = 0; row < size; row++)
                {
                    int num = sudoku[row, col];
                    if (num < 1 || num > size || found[num])
                        return false;
                    found[num] = true;
                }
            }

            // Verificar subcuadros
            int subSize = (int)Math.Sqrt(size);
            for (int startRow = 0; startRow < size; startRow += subSize)
            {
                for (int startCol = 0; startCol < size; startCol += subSize)
                {
                    bool[] found = new bool[size + 1];
                    for (int row = startRow; row < startRow + subSize; row++)
                    {
                        for (int col = startCol; col < startCol + subSize; col++)
                        {
                            int num = sudoku[row, col];
                            if (num < 1 || num > size || found[num])
                                return false;
                                
                            found[num] = true;
                        }
                    }
                }
            }

            return true; // Sudoku completo
        }

        // Método que verifica si hay algún cero en el array
        public static bool HasZero(int[,] sudoku)
        {
            int size = sudoku.GetLength(0);

            // Recorrer el array en busca de un cero
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    int num = sudoku[row, col];
                    if (num == 0) // Si se encuentra un cero, se retorna true
                        return true;
                }
            }

            return false; // Si no se encuentra ningún cero, se retorna false
        }
    }
}
