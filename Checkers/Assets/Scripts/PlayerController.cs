using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Piece redPiece;
    public Piece blackPiece;
    public Text firstPlayer;
    public Text secondPlayer;

    public Text player;

    private int[] blackX = new int[12] { 0, 0, 1, 2, 2, 3, 4, 4, 5, 6, 6, 7 };
    private int[] blackY = new int[12] { 7, 5, 6, 7, 5, 6, 7, 5, 6, 7, 5, 6 };
    private int[] redX = new int[12] { 0, 1, 1, 2, 3, 3, 4, 5, 5, 6, 7, 7 };
    private int[] redY = new int[12] { 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0 };
    private int i = 0;

    private List<Piece> redPieces = new List<Piece>();
    private List<Piece> blackPieces = new List<Piece>(); //consider finding a different way of stroing multiple info underthe same key

    private int numRed = 12;
    private int numBlack = 12;

    private string pieceName;

    private bool isPlayerTurnRed = false;
    private bool isPlayerTurnBlack = false;

    [SerializeField] private EndTurnButton endTurn;

    // Start is called before the first frame update
    // Black always starts in checkers
    void Start()
    {
        SetText();
        spawnETB();
        setupPieces();
        setPlayerTurnBlack();
    }

    void SetText()
    {
        firstPlayer.text = PlayerPrefs.GetString("playerOne", "No Name");
        secondPlayer.text = PlayerPrefs.GetString("playerTwo", "No Name");
        PlayerPrefs.SetString("playerTurn", "Black");
    }


    void setupPieces()
    {
        for (i = 0; i < 12; i++)
        {
            var newPieceRed = Instantiate(redPiece, new Vector3(redX[i] * 2, redY[i] * 2, -1), Quaternion.identity);
            newPieceRed.name = $"Red Piece {i}";
            newPieceRed.setParent(this);
            redPieces.Add(newPieceRed);

            var newPieceBlack = Instantiate(blackPiece, new Vector3(blackX[i] * 2, blackY[i] * 2, -1), Quaternion.identity);
            newPieceBlack.name = $"Black Piece {i}";
            newPieceBlack.setParent(this);
            blackPieces.Add(newPieceBlack);
        }
    }

    public void setPlayerTurnRed()
    {
        for (i = 0; i < 12; i++)
        {
            blackPieces[i].disableInteractions();
            redPieces[i].enableInteractions();
        }
         player.text = "Red";

        isPlayerTurnRed = true;
        isPlayerTurnBlack = false;
    }

    public void setPlayerTurnBlack()
    {
        for (i = 0; i < 12; i++)
        {
            redPieces[i].disableInteractions();
            blackPieces[i].enableInteractions();
        }
        player.text = "Black";

        isPlayerTurnRed = false;
        isPlayerTurnBlack = true;
    }

    public bool isRedTurn()
    {
        return isPlayerTurnRed;
    }

    public bool isBlackTurn()
    {
        return isPlayerTurnBlack;
    }

    public void setPieceName(string newName)
    {
        pieceName = newName;
    }

    public void removeAllMoveTiles()
    {
        for (i = 0; i < 12; i++)
        {
            redPieces[i].destroyTiles();
        }
        for (i = 0; i < 12; i++)
        {
            blackPieces[i].destroyTiles();
        }
    }

    public int checkIfTileOccupied(Vector3 checkThis)
    {
        for (i = 0; i < 12; i++)
        {
            if (redPieces[i].transform.position == checkThis)
            {
                return 1;
            }
        }
        for (i = 0; i < 12; i++)
        {
            if (blackPieces[i].transform.position == checkThis)
            {
                return 1;
            }
        }

        return 0;
    }
    public int checkIfTileHasEnemyPiece(Vector3 checkThis, int teamColor)
    {//black = 0, red = 1
        if (teamColor == 0)
        {
            for (i = 0; i < 12; i++)
            {
                if (redPieces[i].transform.position == checkThis)
                {
                    return 1;
                }
            }
        }
        else
        {
            for (i = 0; i < 12; i++)
            {
                if (blackPieces[i].transform.position == checkThis)
                {
                    return 1;
                }
            }
        }
        return 0;
    }

    public void killAPiece(Vector3 posToKill, int teamColor)
    {
        if (teamColor == 0)
        {
            for (i = 0; i < 12; i++)
            {
                if (redPieces[i].transform.position == posToKill)
                {
                    //kill red piece
                    numRed = numRed - 1;
                    redPieces[i].moveMe(new Vector3(0, 20, -1), false);
                }
            }
        }
        else
        {
            for (i = 0; i < 12; i++)
            {
                if (blackPieces[i].transform.position == posToKill)
                {
                    //kill black piece
                    numBlack = numBlack - 1;
                    blackPieces[i].moveMe(new Vector3(0, 20, -1), false);
                }
            }
        }
    }

    private void spawnETB(){
        var newETB = Instantiate(endTurn, new Vector3(20, 2, -1), Quaternion.identity);
        newETB.setParent(this);
    }

    public void resetAllFirstMoves()
    {
        for (i = 0; i < 12; i++)
        {
            blackPieces[i].resetFirstMove();
            redPieces[i].resetFirstMove();
        }
    }

    public void setallButOneImmovable(Vector3 moveablePos)
    {
        for (i = 0; i < 12; i++)
        {
            if(redPieces[i].transform.position != moveablePos)
            {
                redPieces[i].disableInteractions();
            }
            if (blackPieces[i].transform.position != moveablePos)
            {
                blackPieces[i].disableInteractions();
            }
        }
    }
}
