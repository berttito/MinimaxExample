/// <summary>
/// Clase tablero, el cual contiene los espacios del tablero de juego y evalua segun el estado del mismo
/// </summary>
public class Board
{
    public string[] spaces; 
    public string activePlayer;
    int contador = 0;

    // Se inicializa el tablero
    public Board()
    {
        spaces = new string[24];
        int i;
        for (i = 0; i < 24; i++)
        {
            spaces[i] = "";
        }
    }

    // Evalua el tablero segun el jugador que juega dando valores a las posiciones
    public int Evaluate(string player)
    {
        // Si es una posicion ganadora - 3 en raya
        if (IsWinningPosition(player))
        {
            if (contador == 3)
                return 300;
            else
                return 10;           
        }
        // si es una posicion ganadora del rival
        if (IsWinningPosition(Opponent(player)))
        {
            if (contador == 3)
                return 300;
            else
                return -30;
        }
        // si el tablero esta lleno
        if (IsBoardFull())
        {
            return -50;
        }
        return 0;
    }

    // comprueba si se hace 3 en raya
    public bool Crea3EnRaya()
    {
        //Horizontal
        if ((spaces[0] == "O") && (spaces[1] == "O") && (spaces[2] == "O"))
        {
            toDelete[0] = 0;
            toDelete[1] = 1;
            toDelete[2] = 2;

            return true;
        }
        if ((spaces[3] == "O") && (spaces[4] == "O") && (spaces[5] == "O"))
        {
            toDelete[0] = 3;
            toDelete[1] = 4;
            toDelete[2] = 5;
            return true;
        }
        if ((spaces[6] == "O") && (spaces[7] == "O") && (spaces[8] == "O"))
        {
            toDelete[0] = 6;
            toDelete[1] = 7;
            toDelete[2] = 8;
            return true;
        }
        if ((spaces[9] == "O") && (spaces[10] == "O") && (spaces[11] == "O"))
        {
            toDelete[0] = 9;
            toDelete[1] = 10;
            toDelete[2] = 11;
            return true;
        }
        if ((spaces[12] == "O") && (spaces[13] == "O") && (spaces[14] == "O"))
        {
            toDelete[0] = 12;
            toDelete[1] = 13;
            toDelete[2] = 14;
            return true;
        }
        if ((spaces[15] == "O") && (spaces[16] == "O") && (spaces[17] == "O"))
        {
            toDelete[0] = 15;
            toDelete[1] = 16;
            toDelete[2] = 17;
            return true;
        }
        if ((spaces[18] == "O") && (spaces[19] == "O") && (spaces[20] == "O"))
        {
            toDelete[0] = 18;
            toDelete[1] = 19;
            toDelete[2] = 20;
            return true;
        }
        if ((spaces[21] == "O") && (spaces[22] == "O") && (spaces[23] == "O"))
        {
            toDelete[0] = 21;
            toDelete[1] = 22;
            toDelete[2] = 23;
            return true;
        }

        //Vertical
        if ((spaces[0] == "O") && (spaces[9] == "O") && (spaces[21] == "O"))
        {
            toDelete[0] = 0;
            toDelete[1] = 9;
            toDelete[2] = 21;
            return true;
        }
        if ((spaces[3] == "O") && (spaces[10] == "O") && (spaces[18] == "O"))
        {
            toDelete[0] = 3;
            toDelete[1] = 10;
            toDelete[2] = 18;
            return true;
        }
        if ((spaces[6] == "O") && (spaces[11] == "O") && (spaces[15] == "O"))
        {
            toDelete[0] = 6;
            toDelete[1] = 11;
            toDelete[2] = 15;
            return true;
        }
        if ((spaces[1] == "O") && (spaces[4] == "O") && (spaces[7] == "O"))
        {
            toDelete[0] = 1;
            toDelete[1] = 4;
            toDelete[2] = 7;
            return true;
        }
        if ((spaces[16] == "O") && (spaces[19] == "O") && (spaces[22] == "O"))
        {
            toDelete[0] = 16;
            toDelete[1] = 19;
            toDelete[2] = 22;
            return true;
        }
        if ((spaces[8] == "O") && (spaces[12] == "O") && (spaces[17] == "O"))
        {
            toDelete[0] = 8;
            toDelete[1] = 12;
            toDelete[2] = 17;
            return true;
        }
        if ((spaces[5] == "O") && (spaces[13] == "O") && (spaces[20] == "O"))
        {
            toDelete[0] = 5;
            toDelete[1] = 13;
            toDelete[2] = 20;
            return true;
        }
        if ((spaces[2] == "O") && (spaces[14] == "O") && (spaces[23] == "O"))
        {
            toDelete[0] = 2;
            toDelete[1] = 14;
            toDelete[2] = 23;
            return true;
        }
        return false;
    }

    // retorna las posiciones del jugador
    public int[] ObtenerPosicionesContrario()
    {
        int count = 0;

        // para cada posicion en el tablero
        for (byte i = 0; i < spaces.Length; i++) 
        {
            // si la posicion es el jugador
            if (spaces[i] == Opponent(activePlayer))
            {
                count++; // incrementas
            }
        }
        
        int[] posiciones = new int[count];
        count = 0;

        // para cada posicion del tablero
        for (byte i = 0; i < spaces.Length; i++)
        {
            // si es la posicion del jugador
            if (spaces[i] == Opponent(activePlayer))
            {
                // guardamos en el array cual es la posicion del jugador en el tablero
                posiciones[count] = i;
                count++;
            }
        }

        return posiciones;
    }

    // devuelve los posibles movimientos de la IA
    public Move[] PossibleMoves()
    {        
        Move[] moves;
        int count = 0;
        int i;
        
        // obtienes las posiciones del contrario
        int[] posicionesContrario = ObtenerPosicionesContrario();

        for (i = 0; i < 24; i++) // para las 24 casillas
        {
            if (IsEmptySpace(i)) // si es un espacio vacio
            {
                count++; // incrementamos una posicion
                if (IsWinningPosition("O")) // si la IA hace
                {
                    count += posicionesContrario.Length - 1;
                }
            }
        }
        
        moves = new Move[count];
        count = 0;

        for (i = 0; i < 24; i++) // para las 24 casillas
        {
            if (IsEmptySpace(i)) // si esta vacia
            {
                if (IsWinningPosition("O")) // si la IA hace linea
                {                    
                    foreach (int j in posicionesContrario) // para cada posicion del jugador
                    {
                        moves[count] = new Move(i, j); // creamos un nuevo movimiento, con la posicion del jugador a eleminar
                        count++;
                    }
                }
                else // si no
                {
                    moves[count] = new Move(i, -1); // cremos un movimiento con -1, indicando que no hay jugador para borrar
                }
                count++;
            }
        }

        return moves;
    }

    // determinamos el oponente
    string Opponent (string player)
    {
        if (player == "X")
        {
            return "O";
        }
        else
        {
            return "X";
        }
    }

    // comprueba si es el final del juego
    public bool IsEndOfGame()
    {
        if (IsWinningPosition("X")) // comprueba si hace linea el jugador
        {
            return true;
        }
        else if (IsWinningPosition("O")) // comprueba si hace linea la IA
        {
            return true;
        }
        else if (IsBoardFull()) // comprueba si el tablero está lleno
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int[] toDelete = new int[3];
    // coomprueba si la IA hace linea, guarda las casillas para borrarlas y suma +1 a la puntuación
    public bool IsWinningPosition(string player) 
    {
        //Horizontal
        if ((spaces[0] == player) && (spaces[1] == player) && (spaces[2] == player))
        {
            toDelete[0] = 0;
            toDelete[1] = 1;
            toDelete[2] = 2;

            contador++;
            return true;
        }
        if ((spaces[3] == player) && (spaces[4] == player) && (spaces[5] == player))
        {
            toDelete[0] = 3;
            toDelete[1] = 4;
            toDelete[2] = 5;
            return true;
        }
        if ((spaces[6] == player) && (spaces[7] == player) && (spaces[8] == player))
        {
            toDelete[0] = 6;
            toDelete[1] = 7;
            toDelete[2] = 8;
            return true;
        }
        if ((spaces[9] == player) && (spaces[10] == player) && (spaces[11] == player))
        {
            toDelete[0] = 9;
            toDelete[1] = 10;
            toDelete[2] = 11;
            return true;
        }
        if ((spaces[12] == player) && (spaces[13] == player) && (spaces[14] == player))
        {
            toDelete[0] = 12;
            toDelete[1] = 13;
            toDelete[2] = 14;
            return true;
        }
        if ((spaces[15] == player) && (spaces[16] == player) && (spaces[17] == player))
        {
            toDelete[0] = 15;
            toDelete[1] = 16;
            toDelete[2] = 17;
            return true;
        }
        if ((spaces[18] == player) && (spaces[19] == player) && (spaces[20] == player))
        {
            toDelete[0] = 18;
            toDelete[1] = 19;
            toDelete[2] = 20;
            return true;
        }
        if ((spaces[21] == player) && (spaces[22] == player) && (spaces[23] == player))
        {
            toDelete[0] = 21;
            toDelete[1] = 22;
            toDelete[2] = 23;
            return true;
        }

        //Vertical
        if ((spaces[0] == player) && (spaces[9] == player) && (spaces[21] == player))
        {
            toDelete[0] = 0;
            toDelete[1] = 9;
            toDelete[2] = 21;
            return true;
        }
        if ((spaces[3] == player) && (spaces[10] == player) && (spaces[18] == player))
        {
            toDelete[0] = 3;
            toDelete[1] = 10;
            toDelete[2] = 18;
            return true;
        }
        if ((spaces[6] == player) && (spaces[11] == player) && (spaces[15] == player))
        {
            toDelete[0] = 6;
            toDelete[1] = 11;
            toDelete[2] = 15;
            return true;
        }
        if ((spaces[1] == player) && (spaces[4] == player) && (spaces[7] == player))
        {
            toDelete[0] = 1;
            toDelete[1] = 4;
            toDelete[2] = 7;
            return true;
        }
        if ((spaces[16] == player) && (spaces[19] == player) && (spaces[22] == player))
        {
            toDelete[0] = 16;
            toDelete[1] = 19;
            toDelete[2] = 22;
            return true;
        }
        if ((spaces[8] == player) && (spaces[12] == player) && (spaces[17] == player))
        {
            toDelete[0] = 8;
            toDelete[1] = 12;
            toDelete[2] = 17;
            return true;
        }
        if ((spaces[5] == player) && (spaces[13] == player) && (spaces[20] == player))
        {
            toDelete[0] = 5;
            toDelete[1] = 13;
            toDelete[2] = 20;
            return true;
        }
        if ((spaces[2] == player) && (spaces[14] == player) && (spaces[23] == player))
        {
            toDelete[0] = 2;
            toDelete[1] = 14;
            toDelete[2] = 23;
            return true;
        }
        return false;
    }

    // Comprueba si el tablero está lleno
    protected bool IsBoardFull()
    {
        int i;
        for (i = 0; i < 24; i++)
        {
            if (spaces[i] == "")
            {
                return false;
            }
        }
        return true;
    }

    // Genera un nuevo tablero a partir del movimiento
    public Board GenerateNewBoardFromMove(Move move)
    {
        Board newBoard = this.DuplicateBoard(); // crea el tablero
        newBoard.Move(move, activePlayer); // mueve la ficha
        newBoard.activePlayer = Opponent(newBoard.activePlayer); // setea el active player
        return newBoard;
    }

    // Duplica el tablero - creando uno nuevo y llenandolo con las casillas de este
    public Board DuplicateBoard ()
    {
        Board newBoard = new Board();
        int i;
        for (i = 0; i < 24; i++)
        {
            newBoard.spaces[i] = this.spaces[i];           
        }
        newBoard.activePlayer = this.activePlayer;
        return newBoard;
    }

    // Comprueba si es un espacio vacio
    public bool IsEmptySpace(int pos)
    {
        if (spaces[pos] == "")
            return true;
        else
            return false;
    }

    // Mueve la ficha a una casilla
    public void Move(Move move, string player)
    {
        // si la casilla esta vacia
        if (IsEmptySpace(move.putFicha))
        {
            spaces[move.putFicha] = player; // la rellena con la ficha 
        }

        if(move.takeficha >= 0) // si la casilla donde tiene que borrar es mayor que -1
        {
            spaces[move.takeficha] = ""; // borra la casilla
        }
    }
}
