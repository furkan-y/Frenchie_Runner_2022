using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour
{
    GameMusicManager gameMusicManager;
    Animator animator;

    private bool dogIsJumping;
    private bool dogIsBowed;
    [SerializeField] float dogJumpingTime = 0.6f;
    [SerializeField] float dogBendOverTime = 0.55f;
    [SerializeField] float dogMovingAnotherLaneTime = 0.45f;

    public bool dogJumped;
    public GameObject legDust;

    public bool RunningOnTheMiddle;
    private bool RunningOnTheRight;
    private bool RunningOnTheLeft;
    private bool MovingToRightOrToLeft;

    private bool isGameStarted;


    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameMusicManager = FindObjectOfType<GameMusicManager>();

        ResetAllTriggers();
        SetTheBooleansAtTheBeginning();
        RunningOnTheMiddle = false;

        legDust.SetActive(false);
        StartCoroutine(countDownEnumerator());
    }

    void Update()
    {
        SubwaySurfersMove();
        JumpAndBendOver();

        ForPCSubwaySurfersMove();
        ForPCJumpAndBendOver();
    }

    private void SubwaySurfersMove()
    {
        if (Input.touchCount > 0)
        {
            Touch finger = Input.GetTouch(0);

            if (finger.deltaPosition.x > 60 && !MovingToRightOrToLeft && !dogIsJumping && !dogIsBowed)
            {
                if (RunningOnTheLeft)
                {
                    StartCoroutine(MovingToRightOrToLeftNow());
                    RunningOnTheLeft = false;
                    RunningOnTheMiddle = true;
                    animator.SetTrigger("LeftToMiddle");
                    animator.SetBool("RunningOnTheLeft", false);
                    animator.SetBool("RunningOnTheMiddle", true);

                }
                else if (RunningOnTheMiddle)
                {
                    StartCoroutine(MovingToRightOrToLeftNow());
                    RunningOnTheMiddle = false;
                    RunningOnTheRight = true;
                    animator.SetTrigger("MiddleToRight");
                    animator.SetBool("RunningOnTheMiddle", false);
                    animator.SetBool("RunningOnTheRight", true);
                }
            }

            if (finger.deltaPosition.x < -60 && !MovingToRightOrToLeft && !dogIsJumping && !dogIsBowed)
            {
                if (RunningOnTheRight)
                {
                    StartCoroutine(MovingToRightOrToLeftNow());
                    RunningOnTheRight = false;
                    RunningOnTheMiddle = true;
                    animator.SetTrigger("RightToMiddle");
                    animator.SetBool("RunningOnTheRight", false);
                    animator.SetBool("RunningOnTheMiddle", true);
                }
                else if (RunningOnTheMiddle)
                {
                    StartCoroutine(MovingToRightOrToLeftNow());
                    RunningOnTheMiddle = false;
                    RunningOnTheLeft = true;
                    animator.SetTrigger("MiddleToLeft");
                    animator.SetBool("RunningOnTheMiddle", false);
                    animator.SetBool("RunningOnTheLeft", true);
                }
            }
        }
    }

    private void JumpAndBendOver()
    {
        if (Input.touchCount > 0)
        {
            Touch finger = Input.GetTouch(0);

            if (finger.deltaPosition.y > 65 && !dogIsJumping && !dogIsBowed && !MovingToRightOrToLeft)
            {
                StartCoroutine(JumpAnimation());
            }

            if (finger.deltaPosition.y < -65 && !dogIsJumping && !dogIsBowed && !MovingToRightOrToLeft)
            {
                StartCoroutine(BendOverAnimation());
            }
        }
    }



    private void ForPCSubwaySurfersMove()
    {
        if (Input.GetKeyDown(KeyCode.D) && !MovingToRightOrToLeft && !dogIsJumping && !dogIsBowed)
        {
            if (RunningOnTheLeft)
            {
                StartCoroutine(MovingToRightOrToLeftNow());
                RunningOnTheLeft = false;
                RunningOnTheMiddle = true;
                animator.SetTrigger("LeftToMiddle");
                animator.SetBool("RunningOnTheLeft", false);
                animator.SetBool("RunningOnTheMiddle", true);
            }
            else if (RunningOnTheMiddle)
            {
                StartCoroutine(MovingToRightOrToLeftNow());
                RunningOnTheMiddle = false;
                RunningOnTheRight = true;
                animator.SetTrigger("MiddleToRight");
                animator.SetBool("RunningOnTheMiddle", false);
                animator.SetBool("RunningOnTheRight", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && !MovingToRightOrToLeft && !dogIsJumping && !dogIsBowed)
        {
            if (RunningOnTheRight)
            {
                StartCoroutine(MovingToRightOrToLeftNow());
                RunningOnTheRight = false;
                RunningOnTheMiddle = true;
                animator.SetTrigger("RightToMiddle");
                animator.SetBool("RunningOnTheRight", false);
                animator.SetBool("RunningOnTheMiddle", true);
            }
            else if (RunningOnTheMiddle)
            {
                StartCoroutine(MovingToRightOrToLeftNow());
                RunningOnTheMiddle = false;
                RunningOnTheLeft = true;
                animator.SetTrigger("MiddleToLeft");
                animator.SetBool("RunningOnTheMiddle", false);
                animator.SetBool("RunningOnTheLeft", true);
            }
        }
    }

    private void ForPCJumpAndBendOver()
    {
        if (Input.GetKeyDown(KeyCode.W) && !dogIsJumping && !dogIsBowed && !MovingToRightOrToLeft)
        {
            StartCoroutine(JumpAnimation());
        }

        if (Input.GetKeyDown(KeyCode.S) && !dogIsBowed && !dogIsJumping && !MovingToRightOrToLeft)
        {
            StartCoroutine(BendOverAnimation());
        }
    }

    IEnumerator MovingToRightOrToLeftNow()
    {
        gameMusicManager.DogMoveToRightLeftSoundFunc();

        MovingToRightOrToLeft = true;
        yield return new WaitForSeconds(dogMovingAnotherLaneTime); // The time between the dog moving right or left, don't decrease or can be bug
        MovingToRightOrToLeft = false;
    }

    IEnumerator JumpAnimation()
    {
        if (!gameMusicManager.effectsAudioSource.isPlaying && isGameStarted) 
        {
            gameMusicManager.DogJumpedSoundFunc();
        }


        dogIsJumping = true;
        legDust.SetActive(false);
        if (RunningOnTheMiddle)
        {
            animator.SetTrigger("JumpOnTheMiddle");
        }
        else if (RunningOnTheRight)
        {
            animator.SetTrigger("JumpOnTheRight");
        }
        else if (RunningOnTheLeft)
        {
            animator.SetTrigger("JumpOnTheLeft");
        }
        yield return new WaitForSeconds(dogJumpingTime);
        dogIsJumping = false;

        if (legDust.activeSelf == false)  {legDust.SetActive(true);}
    }
    IEnumerator BendOverAnimation()
    {
        if (!gameMusicManager.effectsAudioSource.isPlaying && isGameStarted)
        {
            gameMusicManager.DogBendOverSoundFunc();
        }

        dogIsBowed = true;
        if (RunningOnTheMiddle)
        {
            //animator.ResetTrigger("JumpOnTheMiddle");


            animator.SetTrigger("BendOverOnTheMiddle");
        }
        else if (RunningOnTheRight)
        {
            animator.SetTrigger("BendOverOnTheRight");
        }
        else if (RunningOnTheLeft)
        {
            animator.SetTrigger("BendOverOnTheLeft");
        }
        yield return new WaitForSeconds(dogBendOverTime);
        dogIsBowed = false;
    }

    IEnumerator Death()
    {
        legDust.SetActive(false);
        yield return new WaitForSeconds(1f);

    }

    private void SetTheBooleansAtTheBeginning()
    {
      dogIsJumping = false;
      dogIsBowed = false;
      dogJumped = false;
      RunningOnTheMiddle = false;
      RunningOnTheRight = false;
      RunningOnTheLeft = false;
      MovingToRightOrToLeft = false;
    }

    public void GameStarted()
    {
        RunningOnTheMiddle = true;
        animator.SetBool("RunningOnTheMiddle", true);
        legDust.SetActive(true);
    }

    private void ResetAllTriggers()
    {
        foreach (var parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(parameter.name);
            }
        }
    }

    IEnumerator countDownEnumerator()
    {
        isGameStarted = false;
        yield return new WaitForSeconds(3f);
        isGameStarted = true;
    }

    public void DogHitTheObstacle()
    {
        if (RunningOnTheMiddle)
        {
            animator.SetBool("DeathOnTheMiddle", true);
        }
        else if (RunningOnTheRight)
        {
            animator.SetBool("DeathOnTheRight", true);            
        }
        else if (RunningOnTheLeft)
        {
            animator.SetBool("DeathOnTheLeft", true);            
        }

    }

}
