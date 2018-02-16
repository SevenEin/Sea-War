using System;

// Проблемы с определением победителя

class Ship // SHip - корабль, класс в котором создается объект корабля
{
    int lenght = 0;

    public Ship(int a) // конструктор корабля, вписываешь количество клеток(максимум 4)
    {
        if (a > 0 )
        {
            lenght = a;
        }
        else
        {
            lenght = 0;
        }

    }

    private void zapros(out int a, out int b) // функция запрашивания начальных координатов;
    {
        Console.WriteLine("Введите координаты для корабля длинной: " + (lenght) + " единицу:");
        int c = -1, d = -1;
        // запрос ввода в поле координатов
        Point1:
        Console.Write("Горизонталь: ");
        try // попытка реализовать исключения
        {
            c = Convert.ToInt32(Console.ReadLine());
        }
        catch(SystemException exc)
        {
            Console.WriteLine("Проверка: ");
            Console.WriteLine("Ввели неверное значени!");
            goto Point1;
        }
        if (c < 0 || c > 10)
        {
            Console.WriteLine("Неверный координат!");
            goto Point1;
        }
        else
            a = c - 1;

        Point2:
        Console.Write("Вертикаль: ");
        try
        {
            d = Convert.ToInt32(Console.ReadLine());
        }
        catch(SystemException)
        {
            Console.WriteLine("Ввели неверное значение!");
            goto Point2;
        }
        if (c < 0 || d > 10)
        {
            Console.WriteLine("Неверный координат!");
            goto Point2;
        }
        else
            b = d - 1;
    }

    private bool doit(int a, int b, char[,] Tboard) // для ввода одинарного корабля
    {
        if (Tboard[a, b] != '_')
        {
            Console.WriteLine("Координат не верен или уже занят! Попробуйте снова: ");
            return true;
        }
        else
        {
            Tboard[a, b] = '+';
            return false;
        }

    }

    private bool checkwhat(int vect, int a, int b, char[,] Tboard) // проверка доски на занятость клетки
    {

        if (vect == 1)
        {
            if ((b + lenght) <= 10)
            {
                for (int temp = b; temp < (b + lenght); temp++)
                {
                    if (Tboard[a, temp] != '_')
                    {
                        Console.WriteLine("Занято!, Точка: " + temp + "; " + b);
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Корабль не может выходить за пределы доски!");
                return false;
            }
        }
        else
        {
            if ((a + lenght) <= 10)
                for (int temp = a; temp < (a + lenght); temp++)
                {
                    if (Tboard[temp, b] != '_')
                    {
                        Console.WriteLine("Занято!, Точка: " + temp + "; " + b);
                        return false;
                    }
                }
            else
            {
                Console.WriteLine("Корабль не может выходить за пределы доски!");
                return false;
            }
        }
        return true;
    }

    private void real_do(int vect, int a, int b, char[,] Tboard)
    {
        if (vect == 1)
        {
            for (int temp = b; temp < (b + lenght); temp++)
            {
                Tboard[a,temp] = '+';
            }
        }
        else
        {
            for (int temp = a; temp < (a + lenght); temp++)
            {
                Tboard[temp, b] = '+';
            }
        }
    }  // чистое заполнение

    private bool doitmoreone(int a, int b, char[,] Tboard)
    {
        int temp = 0;
        Console.WriteLine("Направление вашего корабля: 1 горизонталь, 2 вертикаль: ");
        temp = Convert.ToInt32(Console.ReadLine());
        switch (temp)
        {
            case 1:
                if (checkwhat(temp, a, b, Tboard))
                {
                    real_do(temp, a, b, Tboard);
                }
                else
                {
                    Console.WriteLine("омг");
                    return false;
                }              
                break;
            case 2:
                if (checkwhat(temp, a, b, Tboard))
                {
                    real_do(temp, a, b, Tboard);
                }
                else
                {
                    Console.WriteLine("омг");
                    return false;
                }
                break;
            default:
                Console.WriteLine("Ошибка выбора.");
                return false;
        }
        return true;
    }  // заполнение корабля nго количества ячеек, набор функций

    public void include_in_board(char[,] Tboard) // заполнение доски игрока этим объектом if - корабль 1, else - более 1, 
        //выполняет функцию doitmoreone()->checkwhat(проверка координатов) и real_do(заполнение координатов)
    {
        Start:
        int a, b;
        if (lenght == 1) // если корабль размером с 1 единицу
        {
            Start1:
            zapros(out a, out b);
            if (doit(a, b, Tboard))
                goto Start1; 
        }
        else
        {
        Start2:
            zapros(out a, out b);
            if (Tboard[a, b] != '_')
            {
                Console.WriteLine("Координат не верен или уже занят! Попробуйте снова: ");
                goto Start2;
            }
            else 
            {
                bool wht = doitmoreone(a, b, Tboard);
               if (wht == false)
                {
                    Console.WriteLine("Попытайтесь снова!");
                    goto Start;
                }
            }
        }
    }
}

class Board_player
{
    public string name_player = "no name";
    char[,] board = new char[10, 10];
    char[,] opponent = new char[10, 10];
    char[,] watch_opponent = new char[10, 10];

    public Board_player() // конструктор игровой доски игроков
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
                board[i, j] = '_';
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
                watch_opponent[i, j] = '#';
        }
    }

    public void out_board_player() // отображает поле игрока
    {
        Console.WriteLine("\n");
        Console.Write("    1  2  3  4  5  6  7  8  9  10");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < 10; j++)
            {
                if (j < 1)
                {
                    if (i < 9)
                        Console.Write(" {0} ", i + 1);
                    else
                        Console.Write(10 + " ");
                }
                Console.Write(" " + board[i, j] + " ");
            }
        }
        Console.WriteLine();
    }

    public void out_board_opponent() // отображает поле противника(то что отгадал) 
        {
        Console.WriteLine("\n");
            Console.Write("    1  2  3  4  5  6  7  8  9  10");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < 10; j++)
                {
                    if (j < 1)
                    {
                        if (i < 9)
                            Console.Write(" {0} ", i + 1);
                        else
                            Console.Write(10 + " ");
                    }
                    Console.Write(" " + watch_opponent[i, j] + " ");
                }
            }
        Console.WriteLine();

        }


    public void is_player()
    {
        Console.Write("Введите ваше имя: ");
        name_player = Convert.ToString(Console.ReadLine());
    } // имя игрока вводится

    private Ship[] real_create() // создает 10 кораблей(4 - 1яч, 3 - 2яч, 2 - 3яч. 1 - 4яч) 
    {
        Ship[] arr_Ship = new Ship[10];
        int count = 0;
        for(int a = 0; a < 5; a++)
        {
            int temp = 5 - a;
            for(int b = 0; b < a; b++, count++)
            {
                Console.WriteLine("Temp: " + temp);
                arr_Ship[count] = new Ship(temp);
            }

            
            Console.WriteLine("Созданно объектов: "+ count);
        }
        return arr_Ship;
    }

    private void innicialized(Ship[] array) // вписывание кораблей в доску
    {
        for (int a = 0; a < 10; a++)
        {
            Console.WriteLine("\nЗаполняет " + name_player);
            out_board_player();
            array[a].include_in_board(board);
                Console.Clear();
        }
    }

    public void create_ships()
    {
        Console.WriteLine();
       Ship[] all_Ships = real_create();
       innicialized(all_Ships);
    } // создание всех кораблей на поле

    public char[,] getFullBoard()
    {
        return board;
    }

    public char[,] getOpp()
    {
        return opponent;
    }

    public char[,] getWatch()
    {
        return watch_opponent;
    }

    public void inOpponent(char[,] ground)
    {
        for (int a = 0; a < 10; a++ )
        {
            for(int b = 0; b < 10; b++)
            {
                char temp = ground[a, b];
                opponent[a, b] = temp; 
            }
        }
    }
}

class Game
{
    Board_player one;
    Board_player two;
    public Game(Board_player First, Board_player Second)
    {
        one = First;
        two = Second;
    }

    private bool checkBoard(char[,]board) // проверка наличия врага на поле
    {
        for (int a = 0; a < 10; a++)
        {
            for (int b = 0; b < 10; b++)
            {
                if (board[a, b] == '+')
                    return true;
            }
        }
        return false;
    }

    private void choise(char[,] whatdontsee, char[,] whatcharr)     // запрос ввода в поле координатов и изминение
    {
        PS:
        Console.WriteLine("Введите координаты для желаемой точки атаки: ");
        int c, d, a ,b;
        Point1:
        Console.Write("Горизонталь: ");
        try
        {
            c = Convert.ToInt32(Console.ReadLine());
        }
        catch(SystemException)
        {
            Console.WriteLine("Неверное значение!");
            goto Point1;
        }
        if (c < 0 || c > 10)
        {
            Console.WriteLine("Неверный координат!");
            goto Point1;
        }
        else
        {
             a = c - 1;
        }

        Point2:
        Console.Write("Вертикаль: ");
        try
        {
            d = Convert.ToInt32(Console.ReadLine());
        }
        catch (SystemException)
        {
            Console.WriteLine("Неверное значение!");
            goto Point2;
        }
        if (c < 0 || d > 10)
        {
            Console.WriteLine("Неверный координат!");
            goto Point2;
        }
        else
        {
             b = d - 1;
        }

        if(whatdontsee[a,b] == '_')
        {
            whatcharr[a, b] = 'N';
        }
        else if(whatcharr[a, b] == 'N' || whatcharr[a, b] == 'D')
        {
            Console.WriteLine("Координат уже был!");
            goto PS;
        }
        else
        {
            whatcharr[a, b] = 'D';
            whatdontsee[a, b] = '_';
        }
    }

    public bool whogo() // алгоритм игры
    {
        bool bac = checkBoard(one.getOpp());
        if(bac)
        {
            Console.Clear();
            Console.WriteLine("Очередь " + one.name_player);
            one.out_board_opponent();
            choise(one.getOpp(), one.getWatch());
        }
        else
        {
            Console.WriteLine("Выиграл игрок " + two.name_player);
            return false;
        }

        bool bac2 = checkBoard(two.getOpp());
        if (bac2)
        {
            Console.Clear();
            Console.WriteLine("Очередь " + two.name_player);
            two.out_board_opponent();
            choise(two.getOpp(), two.getWatch());
        }
        else
        {
            
            Console.WriteLine("Выиграл игрок " + two.name_player);
            return false;
        }
        return true;
    }
}

class SeaWarGame
{
    static void Main()
    {
        Board_player First = new Board_player();
        Board_player Second = new Board_player();
        Console.WriteLine("Добро пожаловать в игру!");
        Console.Write("Игрок #1 ");
        First.is_player();
        Console.Write("Игрок #2 ");
        Second.is_player();
        First.create_ships();
        Second.create_ships();
        First.inOpponent(Second.getFullBoard());
        Second.inOpponent(Second.getFullBoard());
        Game StartGame = new Game(First, Second);
        bool end= true;
        while(end)
        {
            end = StartGame.whogo();
        }










        Console.ReadKey();
    }
}