using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    public bool isRising = false;
    public bool isFalling = false;

    private int activeZombieIndex = 0;
    private Vector2 startPosition;

    public int riseSpeed = 1;
    public int scoreThreshold = 5;

    private int zombiesSmashed;

    private int livesRemaining;
    private bool gameOver;

    public Image life01;
    public Image life02;
    public Image life03;
    public Text scoreText;

    public Button gameOverButton;

    void Start()
    {
        gameOver = false;
        zombiesSmashed = 0;
        scoreText.text = zombiesSmashed.ToString();
        livesRemaining = 3;
        pickNewZombie();
    }


    void Update()
    {
        if (!gameOver)
        {
            if (isRising)
            {
                if (zombies[activeZombieIndex].transform.position.y - startPosition.y >= 3f)
                {
                    //Then we need to start bringing it down. 
                    isRising = false;
                    isFalling = true;

                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
                }
            }

            else if (isFalling)
            {
                //all the logic while the zombie is going down goes in here. 
                if (zombies[activeZombieIndex].transform.position.y - startPosition.y <= 0f)
                {
                    //stop making it fall. 
                    isFalling = false;
                    isRising = false;
                    livesRemaining--;
                    UpdateLifeUI();
                }
                else
                {
                    zombies[activeZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
                }
            }

            else
            {
                //anything else happens in here. 
                zombies[activeZombieIndex].transform.position = startPosition;
                pickNewZombie();
            } 
        }
    }

    private void UpdateLifeUI()
    {
        //hitEffect.GetComponent<Animator>().SetTrigger("GetHit");
       // Camera.main.GetComponent<Animator>().SetTrigger("shake");

        if (livesRemaining == 3)
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(true);
            life03.gameObject.SetActive(true);
        }
        if (livesRemaining == 2)
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(true);
            life03.gameObject.SetActive(false);
        }
        if (livesRemaining == 1)
        {
            life01.gameObject.SetActive(true);
            life02.gameObject.SetActive(false);
            life03.gameObject.SetActive(false);
        }
        if (livesRemaining == 0)
        {
            //Game over. 
            life01.gameObject.SetActive(false);
            life02.gameObject.SetActive(false);
            life03.gameObject.SetActive(false);
            gameOver = true;
            gameOverButton.gameObject.SetActive(true);
        }

    }

    private void pickNewZombie()
    {
        isRising = true;
        isFalling = false;
        activeZombieIndex = UnityEngine.Random.Range(0, zombies.Length); // This is going to generate a number between 0 and 6. 
        startPosition = zombies[activeZombieIndex].transform.position;

    }


    public void KillEnemy()
    {
        zombiesSmashed++;
        IncreaseSpawnSpeed();
        scoreText.text = zombiesSmashed.ToString();
        //write code for killing enemy. 
        zombies[activeZombieIndex].transform.position = startPosition;
        pickNewZombie();
       
    }

    private void IncreaseSpawnSpeed()
    {
        if (zombiesSmashed >= scoreThreshold)
        {
            riseSpeed++; //riseSpeed = riseSpeed + 1;
            scoreThreshold *= 2; //scoreThreshold = scoreThreshold * 2;
        }
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
