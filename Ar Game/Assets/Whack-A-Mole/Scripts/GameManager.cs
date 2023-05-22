using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Niantic.ARDKExamples;
using TMPro;

public class GameManager : MonoBehaviour {

	enum State{
		START,
		PLAY,
		GAMEOVER,
	}

	public static float time;
	public float timeLimit = 30;
	const float waitTime = 2;

	Animator anim;
	MoleManager moleManager;
	Text remainingTIme;
	AudioSource audio;

	State state;
	float timer;

	public GameObject NewCanvasThings;

	public GameObject PauseButton;
	public TextMeshProUGUI showScoreAtGameEnd;

	public TextMeshProUGUI Highest;

	void Start () 
	{
		Application.targetFrameRate = 60;

		this.state = State.START;
		this.timer = 0;
		this.anim = GameObject.Find ("Canvas").GetComponent<Animator> ();
		this.moleManager = GameObject.Find ("GameManager").GetComponent<MoleManager> ();
		this.remainingTIme = GameObject.Find ("RemainingTime").GetComponent<Text>();
		this.audio = GetComponent<AudioSource> ();

		NewCanvasThings.SetActive(false);
		PauseButton.SetActive(false);
		GameModePanel.SetActive(false);
		gameoverPanel.SetActive(false);

		Highest.text = "Your Highest: " + PlayerPrefs.GetInt("Highest").ToString();
	}

	public GameObject clickToPlay;
	public GameObject WelcomeScreen;

	public GameObject GameModePanel;
	private bool gamemodeSelected = false;

	public GameObject gameoverPanel;

	public void SetTimelimitedMode()
	{
		PlayerPrefs.SetInt("Mode", 1);
		gamemodeSelected = true;
		GameModePanel.SetActive(false);
		NewCanvasThings.SetActive(true);
		clickToPlay.SetActive(true);
	}


	public void SetTimelessMode()
    {
        PlayerPrefs.SetInt("Mode", 2);
		gamemodeSelected = true;
		GameModePanel.SetActive(false);
		NewCanvasThings.SetActive(true);
		clickToPlay.SetActive(true);
	}


	void Update () 
	{
		if (this.state == State.START) 
		{
			if (PlayerPrefs.GetInt("gameStart") ==  1 && gamemodeSelected == false)
            {
				Time.timeScale = 1f;

				WelcomeScreen.SetActive(false);
				

				// ASKING FOR Mode

				GameModePanel.SetActive(true);

				
			}
			else if (PlayerPrefs.GetInt("gameStart") == 1 && gamemodeSelected == true)
            {
				NewCanvasThings.SetActive(true);
				moleManager.GetMoles();
				clickToPlay.SetActive(true);


				if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    this.state = State.PLAY;

                    // hide start label
                    this.anim.SetTrigger("StartTrigger");

                    // start to generate moles
                    this.moleManager.StartGenerate();

                    this.audio.Play();
                }
            }



		}
		else if (this.state == State.PLAY) 
		{
			WelcomeScreen.SetActive(false);
			NewCanvasThings.SetActive(true);
			PauseButton.SetActive(true);
			clickToPlay.SetActive(false);
			GameModePanel.SetActive(false);

			//
			// TIME LIMITED MODE

			if (PlayerPrefs.GetInt("Mode") == 1)
            {
				this.timer += Time.deltaTime;
				time = this.timer / timeLimit;

				if (this.timer > timeLimit)
				{
					this.state = State.GAMEOVER;

					// show gameover label
					this.anim.SetTrigger("GameOverTrigger");

					// stop to generate moles
					this.moleManager.StopGenerate();

					this.timer = 0;

					// stop audio
					this.audio.loop = false;
				}

				this.remainingTIme.text = "Time: " + ((int)(timeLimit - timer)).ToString("D2");
			}
			else if (PlayerPrefs.GetInt("Mode") == 2)
            {
				this.remainingTIme.text = "";
			}
		}
		else if (this.state == State.GAMEOVER) 
		{
			this.timer += Time.deltaTime;

			if (this.timer > waitTime) 
			{
				//SceneManager.LoadScene ( SceneManager.GetActiveScene().name );
				
				GameModePanel.SetActive(false);
				gameoverPanel.SetActive(true);

				if (ScoreManager.score > PlayerPrefs.GetInt("Highest"))
                {
					showScoreAtGameEnd.text = "You have set your New Highest Score " + ScoreManager.score.ToString() + " in 60 Seconds.";
				}
				else
                {
					showScoreAtGameEnd.text = "You Scored " + ScoreManager.score.ToString() + " in 60 Seconds.";
				}

				PlayerPrefs.SetInt("Highest", ScoreManager.score);


			
			}

			this.remainingTIme.text = "";
		}
	}
}
