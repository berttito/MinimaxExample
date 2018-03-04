using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/// <summary>
/// Clase AI, determina el juego de la IA según el algoritmo NegamaxAB
/// </summary>
public class AI : MonoBehaviour
{
    public byte MAX_DEPTH = 15;

    public Text[] buttonList;
    public GameController gameController;

    private int INFINITE = 10000;
    private int MINUS_INFINITE = -10000;

    private Board board;
    private string activePlayer;
    public int contadorO;


    public void SetButtonList(Text[] bl)
    {
        buttonList = bl;
    }

    public void SetGameController(GameController gc)
    {
        gameController = gc;
    }   

    // metodo play de la IA - ejecuta el algorimo en base al tablero dado
    public void Play(string actPlayer)
    {
        ScoringMove move; // movimiento que va a hacer la IA
        activePlayer = actPlayer;
        ObserveBoard(); // observa el tablero 
                        
        // ejecuta el algoritmo segun el tablero dado
        move = NegamaxAB(board, 0, MINUS_INFINITE, INFINITE);
        
        Move(move); // mueve la IA
        
        ObserveBoard(); // actualizamos el board por si ha habido un 3 en raya

        int count = 0;

        // contamos los espacios donde hay fichas del jugador
        for (int i = 0; i < board.spaces.Length; i++) 
        {
            if (board.spaces[i] == "X")
            {
                count++;
            }
        }

        int[] aEliminar = new int[count]; // creamos un array de la dimension de esos espacios

        count = 0;

        if(board.IsWinningPosition("O")) // si la IA hace 3 en raya 
        {
            for (int i = 0; i < board.spaces.Length; i++) // para cada espacio
            {
                if (board.spaces[i] == "X") // si hay una ficha del jugador
                {
                    aEliminar[count] = i; // guarda en el array de fichas a eliminar la posicion de la ficha
                    count++;
                }
            }

            move.move.takeficha = aEliminar[0]; // setea la ficha a eliminar        
                        
            MoverEspecial(move); // ejecuta el movimiento para la eliminación
        }

        gameController.EndTurn(); // pasa turno
    }

    // metodo que recorre los espacios del tablero y actualiza el board principal
    void ObserveBoard()
    {
        board = new Board();
        byte numberOfSpaces = (byte)buttonList.Length;
        for (byte i = 0; i < numberOfSpaces; i++)
        {
            Text spaceText = buttonList[i];
            board.spaces[i] = spaceText.text;
        }
        board.activePlayer = this.activePlayer;
    }

    // segun el movimiento - borra la ficha del rival, quita las 3 que hay hecho linea y suma un punto
    void MoverEspecial(ScoringMove scoringMove)
    {
        contadorO++;
        buttonList[scoringMove.move.takeficha].text = "";

        buttonList[board.toDelete[0]].text = "";
        buttonList[board.toDelete[1]].text = "";
        buttonList[board.toDelete[2]].text = "";
    }

    // pone una ficha en una posicion segun el move dado
    void Move(ScoringMove scoringMove)
    {
        buttonList[scoringMove.move.putFicha].text = activePlayer;       
    }  
    
    ScoringMove Minimax(Board board, string activePlayer, byte depth)
    {
        // Devuelve el score del tablero y la jugada con la que se llega a él.
        Move bestMove = null;
        int bestScore = 0;
        ScoringMove scoringMove; // score, movimiento
        Board newBoard;
        // Comprobar si hemos terminado de hacer recursión
        if (board.IsEndOfGame() || depth == MAX_DEPTH)
        {
            scoringMove = new ScoringMove(board.Evaluate(activePlayer), new Move(0, -1));
        }
        else
        {
            if (board.activePlayer == activePlayer) bestScore = MINUS_INFINITE;
            else bestScore = INFINITE;
            
            Move[] possibleMoves;
            possibleMoves = board.PossibleMoves();

            foreach (Move move in possibleMoves)
            {
                newBoard = board.GenerateNewBoardFromMove(move);

                // Recursividad
                scoringMove = Minimax(newBoard, activePlayer, (byte)(depth + 1));

                // Actualizar mejor score
                if (board.activePlayer == activePlayer)
                {
                    if (scoringMove.score > bestScore)
                    {
                        bestScore = scoringMove.score;
                        bestMove = move;
                    }
                }
                else
                {
                    if (scoringMove.score < bestScore)
                    {
                        bestScore = scoringMove.score;
                        bestMove = move;
                    }
                }
            }
            scoringMove = new ScoringMove(bestScore, bestMove);
            
        }
        return scoringMove;
    }

    ScoringMove NegamaxAB(Board board, byte depth, int alfa, int beta)
    {
        // Devuelve el score del tablero y la jugada con la que se llega a él.
        Move bestMove = null;       
        int bestScore = 0;

        ScoringMove scoringMove; // score, movimiento
        Board newBoard;
        // Comprobar si hemos terminado de hacer recursión
        if (board.IsEndOfGame() || depth == MAX_DEPTH)
        {
            if (depth % 2 == 0)
            {
                scoringMove = new ScoringMove(board.Evaluate(activePlayer), new Move(0, -1));
            }
            else
            {
                scoringMove = new ScoringMove(-board.Evaluate(activePlayer), new Move(0, -1));
            }
        }
        else
        {
            bestScore = MINUS_INFINITE;

            Move[] possibleMoves;
            possibleMoves = board.PossibleMoves();

            foreach (Move move in possibleMoves)
            {
                newBoard = board.GenerateNewBoardFromMove(move);

                // Recursividad
                scoringMove = NegamaxAB(newBoard, (byte)(depth + 1), -beta, -Math.Max(alfa, bestScore));

                int invertedScore = -scoringMove.score;

                // Actualizar mejor score
                if (invertedScore > bestScore)
                {
                    bestScore = invertedScore;
                    bestMove = move;
                }
                if (bestScore >= beta)
                {
                    scoringMove = new ScoringMove(bestScore, bestMove);
                    return scoringMove;
                }
            }
            scoringMove = new ScoringMove(bestScore, bestMove);
        }
        return scoringMove;
    }

}