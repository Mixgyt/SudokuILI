using System;
using System.Linq;

namespace ProyectoF_ILI
{
    //Codigo escrito por César Enoc Aparicio Reyes
    //Es una bifurcacion de tablas de aleatoriedad econtradas en la web

    static public class TableCreator
    {
        // En esta clase se genera una tabla del sudoku para resolver
        public static int[,] TableGenerator(int dificultad)
        {
            int[,] regionesAzar;
            int[,] tablaRelleno;

            //Este bucle comprobara que las tablas que se generen por medio de las semillas sean solucionables
            while (true)
            {
                /* La manera que se uso para generar la tabla fue por medio de obtener un regiones 
                 * predefinidas por una semilla que se obtienen por medio del e metodo GeneradorDeRegiones
                 * repectivo de su dificultad */
                if(dificultad == 1)
                {
                    //Si la dificultad es facil entonces genera regiones para tablas de 3x3
                    regionesAzar = GeneradorFacil();
                }
                else if (dificultad == 2)
                {
                    //Si la dificultad es intermedia entonces genera regiones para tablas de 6x6
                    regionesAzar = GenradorIntermedio();
                }
                else if(dificultad == 3)
                {
                    //Si la dificultad es dificil entonces genera regiones para tablas de 9x9
                    regionesAzar = GeneradorDeRegiones();
                }
                else
                {
                    //Si ningun valor coincide entonces se generara con una semilla para tablas de 9x9
                    regionesAzar = GeneradorDeRegiones();
                }

                // Ahora se utiliza un metodo resolver el sudoku y crear una tabla solucionada
                //Asi sabemos de antemano que la tabla es solucionable
                tablaRelleno = Solucionador(regionesAzar);
                
                //Si la tabla tiene una solcion se cierra el bucle
                if(tablaRelleno != null)
                {
                    break;
                }
                //Si la tabla no tenia solucion se generara una nueva semilla para encontrarle solucion
            }

            //TablaFinal vaciara la tabla anteriormente solucionada para que el usuario la solucione
            int[,] tablaFinal = TablaFinal(tablaRelleno,dificultad);

            //Se retorna la tabla final con sus espacios para mostrar al usuario
            return tablaFinal;
        }

        // Tabla 9x9 que genera regiones aleatorias en base a semillas predefinidas
        static int[,] GeneradorDeRegiones()
        {
            //Array de 9x9 en el que se generaran las regiones
            int[,] mtabla = new int[9,9];

            //Semillas predefinidas para tablas de 9x9
            int[,] Semillas = new int[6, 3]
            {
                {0,30,60},
                {0,33,57},
                {3,27,60},
                {3,33,54},
                {6,30,54},
                {6,27,57}
            };

            //Con random se obtiene un numero aleatorio para invocar una semilla
            Random rand = new Random();
            int BloquesRegionSe = rand.Next(6);

            //Se escriben los numeros de muestra que se deben escribir en las regiones
            int[] numerosMuestra = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Bucle que buscara las 3 regiones predefinidas 
            for(int region = 0; region < 3; region++)
            {
                //Se mezclan los numeros de muetra para que esten en desorden
                int[] numerosAzar = numerosMuestra.OrderBy(x => rand.Next()).ToArray();

                //El indice del azar seria el orden que se pediran los numeros al array anterior
                int indiceAzar = 0;

                //Variables que buscan las repectivas regiones que se debe usar segun la semilla obtenida
                int primeraCeldaRegion = Semillas[BloquesRegionSe, region];
                int filaRegion = primeraCeldaRegion / 9;
                int columnaRegion = primeraCeldaRegion % 9;

                //Bucle que comprueba las regiones  de la semilla para llenar
                //Busca la fila de la region
                for(int fila = 0; fila < 3; fila++)
                {
                    //Busca la columna de la region
                    for(int columna = 0; columna < 3; columna++)
                    {
                        //Luego se calcula el numero de fila de la region en base a valores obtenidos anterior mente
                        int filaTabla = filaRegion - (filaRegion % 3) + fila;
                        //Asi mismo con el numero de columna
                        int columnaTabla = columnaRegion - (columnaRegion % 3) + columna;

                        //Se le asigna una valor a la casilla de la region el valor es escogido aleatoriamente 
                        //del array numerosAzar
                        mtabla[filaTabla, columnaTabla] = numerosAzar[indiceAzar];
                        //Se le suma al indice para la siguiente vez obtenga otro numero
                        indiceAzar++;
                    }
                }
            }
            //Retorna la tabla al tener ya sus regiones generadas
            return mtabla;
        }

        //Tabla de 6x6 que genera regiones aleatorias en base a semillas
        static int[,] GenradorIntermedio()
        {
            //tabla de 6x6
            int[,] mtabla = new int[6, 6];
            //Semillas predefinidas
            int[,] Semillas = new int[2, 2]
            {
                {0,15},
                {5,12}
            };

            //Con ramdon se obtiene un numero aleatorio con el que se obtendra una semilla
            Random rand = new Random();
            int BloquesRegionSe = rand.Next(2);

            //Numeros predefinidos que deben ir en una region
            int[] numerosMuestra = { 1, 2, 3, 4, 5, 6 };

            //Bucle que busca las regiones de la semilla
            for (int region = 0; region < 2; region++)
            {
                //Los numeros de muestra se desordenan
                int[] numerosAzar = numerosMuestra.OrderBy(x => rand.Next()).ToArray();

                //Este indice se utiliza para asi obtener numeros al azar del numerosAzar
                int indiceAzar = 0;

                //Variables que obtienen valores de as regiones predefinidas de la semilla
                int primeraCeldaRegion = Semillas[BloquesRegionSe, region];
                int filaRegion = primeraCeldaRegion / 6;
                int columnaRegion = primeraCeldaRegion % 6;
                //Estos valores se obtienen para su posterior uso

                //Bucle que busca las filas de la region a generar
                for (int fila = 0; fila < 2; fila++)
                {
                    //Bucle que busca las columnas de la region a generar
                    for (int columna = 0; columna < 3; columna++)
                    {
                        //Se obtienen los valores especificos de las celdas de las regiones
                        //en base a los valores obtenidos anterior mente
                        int filaTabla = filaRegion - (filaRegion % 2) + fila;
                        int columnaTabla = columnaRegion - (columnaRegion % 3) + columna;

                        //Se le asigna un numero aleatorio a la celda de la region 
                        mtabla[filaTabla, columnaTabla] = numerosAzar[indiceAzar];
                        //Se le suma al indice para obtener un numero distinto para la siguiente celda
                        indiceAzar++;
                    }
                }
            }
            //retorna la tabla al tener sus regiones generadas
            return mtabla;
        }

        //Tabla de 3x3 que es generada en base a semillas predefinidas

        //La fucion de este generador es distintos a los anteriores debido 
        //a que las semillas de este no son completamente aletorias
        //Sus semillas ya estan prehechas y luego se eligen aleatoriamente
        static int[,] GeneradorFacil()
        {
            //Las invo un Random
            Random rand = new Random();
            //Se crea la tabla que guardara los valores
            int[,] mtabla;
            //Este indice es el que escogera la semilla que se generara
            int indiceAzar = rand.Next(10);

            //Semillas prehechas para el modo facil
            switch (indiceAzar)
            {
                //Se selecciona una dependiendo el valor aleatorio del indice
                case 1:
                    mtabla = new int[3, 3] { { 2, 0, 0 }, { 0, 0, 1 }, { 0, 0, 0 } };
                break;
                case 2:
                    mtabla = new int[3, 3] { { 3, 0, 0 }, { 0, 0, 0 }, { 0, 2, 0 } };
                break;
                case 3:
                    mtabla = new int[3, 3] { { 0, 0, 3 }, { 2, 0, 0 }, { 0, 0, 0 } };
                break;
                case 4:
                    mtabla = new int[3, 3] { { 1, 0, 0 }, { 0, 0, 0 }, { 0, 0, 3 } };
                break;
                case 5:
                    mtabla = new int[3, 3] { { 0, 0, 1 }, { 0, 0, 0 }, { 2, 0, 0 } };
                break;
                case 6:
                    mtabla = new int[3, 3] { { 1, 0, 0 }, { 0, 0, 0 }, { 0, 3, 0 } };
                break;
                case 7:
                    mtabla = new int[3, 3] { { 0, 0, 1 }, { 2, 0, 0 }, { 0, 0, 0 } };
                break;
                case 8:
                    mtabla = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 2 }, { 1, 0, 0 } };
                break;
                case 9:
                    mtabla = new int[3, 3] { { 0, 2, 0 }, { 0, 0, 0 }, { 1, 0, 0 } };
                break;
                case 10:
                    mtabla = new int[3, 3] { { 2, 0, 0 }, { 0, 0, 0 }, { 0, 0, 1 } };
                break;
                    //semilla predeterminada por si falla el indice
                default:
                    mtabla = new int[3, 3] { { 2, 0, 0 }, { 0, 0, 1 }, { 0, 0, 0 } };
                break;
            }
            //Se retorna la tabla al tener la semilla seleccionada
            return mtabla;
        }

        // Este metodo es un algoritmo que soluciona tablas de sudoku
        public static int[,] Solucionador(int[,] tabla)
        {
            int size = tabla.GetLength(0);
            //Variables que capturaran la fila y columna donde se encuentre un 0
            int fila = -1;
            int columna = -1;
            bool resuelto = true;

            //Este bucle busca 0 dentro del array "tabla"
            //Recorre las filas
            for (int a = 0; a < size; a++)
            {
                //Recorre las columnas
                for (int b = 0; b < size; b++)
                {
                    if (tabla[a, b] == 0)
                    {
                        fila = a;
                        columna = b;
                        //Codigo usado en el debug del proyecto
                        //Console.WriteLine($"0 Encontrado en ({fila},{columna})");
                        resuelto = false;
                        break;
                    }
                }
                if (!resuelto)
                {
                    break;
                }
            }

            //Si el bool resuelto es true entonces se retorna la tabla
            if (resuelto)
            {
                //Codigo usado en el debug del proyecto
                //Console.WriteLine("No se han encontrado mas 0");
                return tabla;
            }

            //Solucionador Progresivo de la tabla
            for (int numero = 1; numero <= size; numero++)
            {
                //Codigo usado en el debug del proyecto
                //Console.WriteLine("Entrada");
                if (Validador.ValidarNumero(numero, fila, columna, tabla))
                {
                    tabla[fila, columna] = numero;
                    if (Solucionador(tabla) != null)
                    {
                        //Codigo usado en el debug del proyecto
                        //Console.WriteLine($"Numero Escrito en la fila {fila} y columna {columna} es = {numero}");
                        return tabla;
                    }
                    else
                    {
                        /*Si se prueban 9 numeros y no funcionan borra
                           el numero anterior y se vuelve a comprobar */
                        //Console.WriteLine("Error Vuelta a 0");
                        tabla[fila, columna] = 0;
                    }
                }
            }

            //Retorna null si no se encontro solucion a la tabla
            return null;
        }

        //Esta tabla genera espacios aleatoriamente en las tablas ya solucionadas
        static int[,] TablaFinal(int[,] tabla, int dificultad)
        {
            //Funcion random utilizada para generar los espacios
            Random rand = new Random();

            //Dependiendo la dificultad asi sera el numero de espacios generados en la tabla
            if(dificultad == 1)
            {
                //Para la dificultad facil se generarn  7 espacios vacios
                for(int espacios = 7; espacios > 0; espacios--)
                {
                    //Se escoge una fila aleatoria
                    int fila = rand.Next(0, 3);
                    //Se escoge una columna aleatoria
                    int columna = rand.Next(0, 3);

                    //Comprueba si no genera error al encontrar la fila y la columna
                    try
                    {
                        //Si el la celda de la fila y columna aleatoria es diferente a 0,
                        //entonces celda cambia su valor a 0  
                        if (tabla[fila, columna] != 0)
                        {
                            tabla[fila, columna] = 0;
                        }
                        //Si la celda ya era 0 entonces
                        //se le suma a los espacios para realizar
                        //otro intento de vaciar una celda
                        else
                        {
                            espacios++;
                        }
                    }
                    //Si dio error al encontrar la celda
                    catch
                    {
                        //Se le cuma a los espacios para realizar otro intento de encontrar una celda
                        espacios++;
                        //Se continua con otro repeticion del bucle
                        continue;
                    }
                }
            }
            //Para la dificultad intermedia se generarn  15 espacios vacios
            else if (dificultad == 2)
            {
                for (int espacios = 15; espacios > 0; espacios--)
                {
                    //Se escoge una fila aleatoria
                    int fila = rand.Next(0, 6);
                    //Se escoge una columna aleatoria
                    int columna = rand.Next(0, 6);

                    //Comprueba si no genera error al encontrar la fila y la columna
                    try
                    {
                        //Si el la celda de la fila y columna aleatoria es diferente a 0,
                        //entonces celda cambia su valor a 0  
                        if (tabla[fila, columna] != 0)
                        {
                            tabla[fila, columna] = 0;
                        }
                        //Si la celda ya era 0 entonces
                        //se le suma a los espacios para realizar
                        //otro intento de vaciar una celda
                        else
                        {
                            espacios++;
                        }
                    }
                    //Si dio error al encontrar la celda
                    catch
                    {
                        //Se le cuma a los espacios para realizar otro intento de encontrar una celda
                        espacios++;
                        //Se continua con otro repeticion del bucle
                        continue;
                    }
                }
            }
            //Para la dificultad dificil se generarn  45 espacios vacios
            else if (dificultad == 3)
            {
                for (int espacios = 45; espacios > 0; espacios--)
                {
                    //Se escoge una fila aleatoria
                    int fila = rand.Next(0, 9);
                    //Se escoge una columna aleatoria
                    int columna = rand.Next(0, 9);

                    //Comprueba si no genera error al encontrar la fila y la columna
                    try
                    {
                        //Si el la celda de la fila y columna aleatoria es diferente a 0,
                        //entonces celda cambia su valor a 0
                        if (tabla[fila, columna] != 0)
                        {
                            tabla[fila, columna] = 0;
                        }
                        //Si la celda ya era 0 entonces
                        //se le suma a los espacios para realizar
                        //otro intento de vaciar una celda
                        else
                        {
                            espacios++;
                        }
                    }
                    //Si dio error al encontrar la celda
                    catch
                    {
                        //Se le cuma a los espacios para realizar otro intento de encontrar una celda
                        espacios++;
                        //Se continua con otro repeticion del bucle
                        continue;
                    }
                }
            }
            //Si la dificultad evaluada no coincide entonces se generan 20 espacios vacios por defecto
            else
            {
                for (int espacios = 20; espacios > 0; espacios--)
                {
                    //Se escoge una fila aleatoria
                    int fila = rand.Next(0, 9);
                    //Se escoge una columna aleatoria
                    int columna = rand.Next(0, 9);


                    //Comprueba si no genera error al encontrar la fila y la columna
                    try
                    {
                        //Si el la celda de la fila y columna aleatoria es diferente a 0,
                        //entonces celda cambia su valor a 0
                        if (tabla[fila, columna] != 0)
                        {
                            tabla[fila, columna] = 0;
                        }
                        //Si la celda ya era 0 entonces
                        //se le suma a los espacios para realizar
                        //otro intento de vaciar una celda
                        else
                        {
                            espacios++;
                        }
                    }
                    //Si dio error al encontrar la celda
                    catch
                    {
                        //Se le cuma a los espacios para realizar otro intento de encontrar una celda
                        espacios++;
                        //Se continua con otro repeticion del bucle
                        continue;
                    }
                }
            }

            //Al evaluar cual sea de las dificultades y generado sus espacios vacios
            //Entoces se retorna la tabla con sus respectivos espacios para solucionar
            return tabla;
        }

    }
}
