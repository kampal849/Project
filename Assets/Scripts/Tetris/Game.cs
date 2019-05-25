using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    public int scoreOneLine = 40;
    public int scoreTwoLine = 100;
    public int scoreThreeLine = 300;
    public int scoreFourLine = 1200;

    public int currentLevel = 0;
    private int numLinesCleared = 0;

    public float fallSpeed = 1.0f;

    public static bool isPaused = false;

    public Text hud_score;
    public Text hud_level;
    public Text hud_lines;

    private int numberOfRowsThisTurn = 0;

    private int currentScore = 0;

    private GameObject previewTetromino;
    private GameObject nextTetromino;

    private bool gameStarted = false;
    private int startingHighScore;

    private Vector2 previewTetrominoPosition = new Vector2(12.5f, 6f);

    public List<GameObject> tetrominoList;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNextTetromino();

        startingHighScore = Vault.GetScore(GameType.Tetris);
    }

    void Update()
    {
        UpdateScore();

        UpdateUI();

        UpdateLevel();

        UpdateSpeed();

        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (Time.timeScale == 1)
                PauseGame();
            else
                ResumeGame();
            
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    void UpdateLevel()
    {
        currentLevel = numLinesCleared / 10;
    }

    void UpdateSpeed()
    {
        fallSpeed = 1.0f - ((float)currentLevel * 0.1f);
    }

    public void UpdateUI()
    {
        hud_score.text = currentScore.ToString();
        hud_level.text = currentLevel.ToString();
        hud_lines.text = numLinesCleared.ToString();
    }

    public void UpdateScore()
    {
        if (numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                ClearedOneLine();
            }
            else if(numberOfRowsThisTurn==2)
            {
                ClearedTwoLines();
            }
            else if (numberOfRowsThisTurn == 3)
            {
                ClearedThreeLines();
            }
            else if (numberOfRowsThisTurn == 4)
            {
                ClearedFourLines();
            }

            numberOfRowsThisTurn = 0;

            FindObjectOfType<Game>().UpdateHighScore();
        }
    }

    public void UpdateHighScore()
    {
        if (currentScore > startingHighScore)
        {
            Vault.SetScore(GameType.Tetris, currentScore);
        }
    }

    public void ClearedOneLine()
    {
        currentScore += scoreOneLine;
        numLinesCleared++;
    }

    public void ClearedTwoLines()
    {
        currentScore += scoreTwoLine;
        numLinesCleared += 2;

    }

    public void ClearedThreeLines()
    {
        currentScore += scoreThreeLine;
        numLinesCleared += 3;
    }

    public void ClearedFourLines()
    {
        currentScore += scoreFourLine;
        numLinesCleared += 4;
    }

    public bool CheckIsAboveGrid(Tetromino tetromino)
    {
        for (int x=0;x<gridWidth;++x)
        {
            foreach(Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);

                if (pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }


    public bool IsFullRowAt(int y)
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        numberOfRowsThisTurn++;

        return true;
    
    }


    public void DeleteMinoAt(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }



    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }         
                
    }

    public void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for(int y = 0; y < gridHeight; ++y)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveAllRowsDown(y + 1);
                --y;
            }
        }

    }

    public void UpdateGrip(Tetromino tetromino)
    {
        for (int y=0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                if (grid[x,y] != null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach(Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position);

            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }

    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public void SpawnNextTetromino()
    {
        if (!gameStarted)
        {
            gameStarted = true;

            nextTetromino = Instantiate(GetRandomTetromino(), new Vector2(5.0f, 20.0f), Quaternion.identity);
            previewTetromino = Instantiate(GetRandomTetromino(), previewTetrominoPosition, Quaternion.identity);
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }
        else
        {
            previewTetromino.transform.localPosition = new Vector2(5.0f, 20.0f);
            nextTetromino = previewTetromino;
            nextTetromino.GetComponent<Tetromino>().enabled = true;

            previewTetromino = Instantiate(GetRandomTetromino(), previewTetrominoPosition, Quaternion.identity);
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }

       
    }

    public bool CheckIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round (Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    GameObject GetRandomTetromino()
    {
        return tetrominoList[Random.Range(0, tetrominoList.Count)];
    }

    public void GameOver()
    {
        Vault.AddTotalScore(currentScore);

        SceneManager.LoadScene("Tetris_GameOver");
    }
}
