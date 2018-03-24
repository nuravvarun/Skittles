using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Light lt;
	public GameObject pin;
    public GameObject ball;
    public Vector3 spawnValues;
    public int ballCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
	public Color color0 = Color.red;
	public Color color1 = Color.blue;
	public float duration = 10;
	public float smoothness = 0.02f;
	public Transform[] views;
	public Transform currentView; // Use this for initialization 
	public GameObject mainmenu;
	public Text texta;
	public static bool GameIsPaused=false;
	public GameObject pausemenu;
	public GameObject pauseB;
	public Animator anim;
	public bool isCollided=false;
	public GameObject gameOver;
//Private Variables
	private bool isRunning=false;
	private Coroutine currentCoroutine=null;
	private float transitionSpeed;

	//score part
	private int currentScore = 0;
	public Text scoreUIObject;
	private float scoreTimer;
	public Text highScoreObject;
    	public static int waveCount;   //to keep track of levels

	void Start()
	{	
	}
	public void Wrapper()     				//GAMEPLAY
	{	
		//Restart Scoring!
			currentScore = 0;
			scoreUIObject.text = "";
		//score restarted


		if (GameObject.Find("PlayerPin")==null) 
		{
		Instantiate (pin, new Vector3 (0, 1, 0), Quaternion.identity);   //Pin Spawn On Start

		}
				
		transitionSpeed = 1;
		mainmenu.SetActive (false);
		currentView = views [1];

		if (isRunning == false && currentCoroutine==null) 
		{
			currentCoroutine = StartCoroutine (SpawnWaves());     //Ball Spawn Start
		}
	}
	public void pauseOpen()					//PAUSEMENU
	{	
		pausemenu.SetActive (true);
		Time.timeScale = 0f;	
	}
	public void pauseClose()				//PAUSECLOSED
	{
		pausemenu.SetActive (false);
		Time.timeScale = 1f;

	}
	public void exit()						//GAMEEXIT
	{
		Application.Quit ();
	}
	public void exitmenu()	
	//EXIT TO MAINMENU
	{   highScoreObject.enabled=false;
		scoreUIObject.enabled = false;
		Destroy (GameObject.FindWithTag ("bowlingBall"));
		Destroy (GameObject.FindWithTag ("Pin"));
		if (currentCoroutine != null)
		{
			StopCoroutine (currentCoroutine);
			currentCoroutine = null;
			isRunning = false;

		}
		gameOver.SetActive (false);
		pauseB.SetActive (false);
		transitionSpeed = 2;
		currentView = views [0];
		pausemenu.SetActive (false);
		mainmenu.SetActive (true);
		Time.timeScale = 1f;
		StopCoroutine (SpawnWaves());
		StartCoroutine (colorChange ());

	}
    IEnumerator SpawnWaves()				//BALL SPAWN
	{
        //isRunning = true;  shifted 12 lines below for score purposes          
		yield return new WaitForSeconds (3);
		highScoreObject.enabled=true;
		scoreUIObject.enabled = true;
		anim.Play (0);
        float progress = 0;				
		float increment = smoothness / duration;
		while (progress < 1)
		{
			lt.color = Color.Lerp (color0, color1, progress);		//COLORCHANGE ON START RED TO BLUE
			progress += increment;
			yield return new WaitForSeconds (smoothness);
		}
		yield return new WaitForSeconds (startWait);
        pauseB.SetActive(true);
		isRunning = true;            

        while (!isCollided) 
		{
			int ballCount = Mathf.CeilToInt(getLevel () / 2.0f);
			for (int  i = 0; i < ballCount; i++) {
	            Vector3 spawnPosition = new Vector3 (Random.Range (-1.3f, 1.3f), spawnValues.y, spawnValues.z);//Spawning Position
				Quaternion spawnRotation = Quaternion.identity;

				GameObject g = (GameObject)Instantiate (ball, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (0.2f);
			}
			waveCount += 1;
			yield return new WaitForSeconds (spawnWait);
		}
    }
	public void gameover()
	{
		
		StopCoroutine (currentCoroutine);
		currentCoroutine = null;
		isRunning = false;
		isCollided = false;
		StartCoroutine (colorChange ());

		// PlayerPrefs Score
		int currentGameScoreLocal = currentScore;
		int swapeUsingVariable = 0;

		currentScore = 0; //Game Over Reset score for next play!

		if (PlayerPrefs.GetInt ("Score1",0) < currentGameScoreLocal) {
			//Swap values
			swapeUsingVariable = PlayerPrefs.GetInt("Score1",0);
			PlayerPrefs.SetInt ("Score1", currentGameScoreLocal);
			currentGameScoreLocal = swapeUsingVariable;
		}
		if (PlayerPrefs.GetInt ("Score2",0) < currentGameScoreLocal) {
			//Swap values
			swapeUsingVariable = PlayerPrefs.GetInt("Score2",0);
			PlayerPrefs.SetInt ("Score2", currentGameScoreLocal);
			currentGameScoreLocal = swapeUsingVariable;
		}
		if (PlayerPrefs.GetInt ("Score3",0) < currentGameScoreLocal) {
			//Swap values
			swapeUsingVariable = PlayerPrefs.GetInt("Score3",0);
			PlayerPrefs.SetInt ("Score3", currentGameScoreLocal);
			currentGameScoreLocal = swapeUsingVariable;
		}

		//Update highscores
		highScoreObject.text = "Top Rank\n#1 : "+PlayerPrefs.GetInt("Score1",0)+ "\n#2 : "+PlayerPrefs.GetInt("Score2",0)+ "\n#3 : "+PlayerPrefs.GetInt("Score3",0);
		PlayerPrefs.Save (); //Save Score to disk . Writing disk can cause a small hiccup; not recommended during gameplay
	}
	IEnumerator colorChange()									//COLORCHANGE ON START BLUE TO RED
	{
		float progress = 0;
		float increment = smoothness / 1.0f;
		while (progress < 1)
		{

			lt.color = Color.Lerp (color1, color0, progress);
			progress += increment;
			yield return new WaitForSeconds (smoothness);
		}
	}
	public void restartGame()											//RESTART GAME
	{	Destroy (GameObject.FindWithTag ("bowlingBall"));
		Destroy (GameObject.FindWithTag ("Pin"));
		currentScore = 0; //resetscore
		scoreUIObject.text = "Score : 0";
		waveCount = 0;							// reset waveCount for level purposes

		gameOver.SetActive(false);
		pausemenu.SetActive (false);
		isCollided = false;
		if (currentCoroutine != null) 
		{
			StopCoroutine (currentCoroutine);

			Instantiate (pin, new Vector3 (0, 1, 0), Quaternion.identity);
			currentCoroutine = StartCoroutine (SpawnWaves ());

		} else if (currentCoroutine==null) 								//RESTART ON GAMEOVER
		{
			Destroy (GameObject.FindWithTag ("Pin"));
			Instantiate (pin, new Vector3 (0, 1, 0), Quaternion.identity);
			currentCoroutine = StartCoroutine (SpawnWaves ());
			
		}


		Time.timeScale = 1f;
	}

	void LateUpdate ()													//VIEW CHANGE
	{ //Lerp position

		transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed); 

		Vector3 currentAngle = new Vector3 ( Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed), 
			Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
			Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

		transform.eulerAngles = currentAngle; 

		//Update Score
		if (!isCollided && isRunning) {
			scoreTimer += Time.deltaTime;
			if (scoreTimer >= 1f) {
				scoreUIObject.text = "Score : " + (++currentScore).ToString ();
				scoreTimer = 0; 								// update variable for next time
			}
		}
	} 

	public static int getLevel() {				//get current game level, level goes up at every 5th wave
		if (waveCount > 30)
			return 6;
		return (waveCount / 5) + 1;		
	}
}
