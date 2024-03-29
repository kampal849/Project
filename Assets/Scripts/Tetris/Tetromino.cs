﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fall = 0;
    private float fallSpeed;

    public bool allowRotation = true;
    public bool limitRotation = false;

    public string prefabName;


    private float continuousVerticalSpeed = 0.05f;
    private float continuousHorizontalSpeed = 0.1f;
    private float buttonDownWaitMax = 0.2f;

    private float verticalTimer = 0;
    private float horizontalTimer = 0;
    private float buttonDownWaitTimerHorizontal = 0;
    private float buttonDownWaitTimerVertical = 0;

    private bool movedImmediateHorizontal = false;
    private bool movedImmediateVertical = false;



    // Start is called before the first frame update
    void Start()
    {
        fallSpeed = GameObject.Find("GameScript").GetComponent<Game>().fallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game.isPaused)
        {
            CheckUserInput();
        }
    }

    //touch movement   
    private int touchSensitivityHorizontal = 8;
    private int touchSensitivityVertical = 4;
    Vector2 previousUnitPosition = Vector2.zero;
    Vector2 direction = Vector2.zero;

    bool moved = false;

    void CheckUserInput()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                previousUnitPosition = new Vector2(t.position.x, t.position.y);
            }

            else if (t.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = t.deltaPosition;
                direction = touchDeltaPosition.normalized;
                if (Mathf.Abs(t.position.x - previousUnitPosition.x) >= touchSensitivityHorizontal && direction.x < 0 && t.deltaPosition.y > -10 && t.deltaPosition.y < 10)
                {
                    MoveLeft();
                    previousUnitPosition = t.position;
                    moved = true;

                }

                else if (Mathf.Abs(t.position.x - previousUnitPosition.x) >= touchSensitivityHorizontal && direction.x > 0 && t.deltaPosition.y > -10 && t.deltaPosition.y < 10)
                {
                    MoveRight();
                    previousUnitPosition = t.position;
                    moved = true;
                }

                else if (Mathf.Abs(t.position.y - previousUnitPosition.y) >= touchSensitivityVertical && direction.y < 0 && t.deltaPosition.x > -10 && t.deltaPosition.x < 10)
                {
                    MoveDown();
                    previousUnitPosition = t.position;
                    moved = true;

                }


            }

            else if (t.phase == TouchPhase.Ended)
            {
                if (!moved && t.position.x > Screen.width / 4)
                {
                    Rotate();
                }
                moved = false;
            }
        }
    }
    /*
    void CheckUserInput()
    {

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) )
        {
            movedImmediateHorizontal = false;          
            horizontalTimer = 0;            
            buttonDownWaitTimerHorizontal = 0;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            movedImmediateVertical = false;
            verticalTimer = 0;
            buttonDownWaitTimerVertical = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
            
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
        }

        if (Input.GetKey(KeyCode.DownArrow) || Time.time-fall>=fallSpeed)
        {
            
            MoveDown();
        }


    }
    */
    void MoveLeft()
    {
        if (movedImmediateHorizontal)
        {
            if (buttonDownWaitTimerHorizontal < buttonDownWaitMax)
            {
                buttonDownWaitTimerHorizontal += Time.deltaTime;
                return;
            }

            if (horizontalTimer < continuousHorizontalSpeed)
            {
                horizontalTimer += Time.deltaTime;
                return;
            }
        }

        if (!movedImmediateHorizontal)
            movedImmediateHorizontal = true;

        horizontalTimer = 0;

        transform.position += new Vector3(-1, 0, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrip(this);
        }
        else
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    void MoveRight()
    {
        if (movedImmediateHorizontal)
        {

            if (buttonDownWaitTimerHorizontal < buttonDownWaitMax)
            {
                buttonDownWaitTimerHorizontal += Time.deltaTime;
                return;
            }

            if (horizontalTimer < continuousHorizontalSpeed)
            {
                horizontalTimer += Time.deltaTime;
                return;
            }
        }

        if (!movedImmediateHorizontal)
            movedImmediateHorizontal = true;

        horizontalTimer = 0;

        transform.position += new Vector3(1, 0, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrip(this);
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    void MoveDown()
    {
        if (movedImmediateVertical)
        {

            if (buttonDownWaitTimerVertical < buttonDownWaitMax)
            {
                buttonDownWaitTimerVertical += Time.deltaTime;
                return;
            }

            if (verticalTimer < continuousVerticalSpeed)
            {
                verticalTimer += Time.deltaTime;
                return;
            }
        }


        if (!movedImmediateVertical)
            movedImmediateVertical = true;

        verticalTimer = 0;

        transform.position += new Vector3(0, -1, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrip(this);
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);

            FindObjectOfType<Game>().DeleteRow();

            if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
            {
                FindObjectOfType<Game>().GameOver();
            }

            enabled = false;

            FindObjectOfType<Game>().SpawnNextTetromino();

            FindObjectOfType<Game>().UpdateHighScore();
        }

        fall = Time.time;
    }

    void Rotate()
    {
        if (allowRotation)
        {
            if (limitRotation)
            {
                if (transform.rotation.eulerAngles.z > 90)
                {
                    transform.Rotate(0, 0, -90);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }

            else
            {

                transform.Rotate(0, 0, 90);
            }

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrip(this);
            }


            else
            {
                if (limitRotation)
                {

                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }

            }

        }
    }


    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);

            if (FindObjectOfType<Game>().CheckIsInsideGrid(pos) == false)
            {
                return false;
            }

            if(FindObjectOfType<Game>().GetTransformAtGridPosition(pos)!=null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false; 
            }
        }
        return true;
    }
}
