using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeSimpleCS : MonoBehaviour {

    private Transform player; // Drag your player here
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    private float angle;
    private float swipeDistanceX;
    private float swipeDistanceY;

    /*private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    private Vector3 pos4;
    private Vector3 pos5;
    private Vector3 pos6;
    private Vector3 pos7;
    private Vector3 pos8;
    private Vector3 pos9;
    private Vector3 pos10;*/
    public bool isCollided; 
    private Vector3 currentPos;
	private bool isSelected;
	private Collider playerc;



	public void Start()
	{	player = GetComponent<Transform>();	
		playerc = GetComponent<Collider> ();
		PinSockets.pos[0] = player.localPosition;
		Debug.Log(this.transform.localPosition);
		PinSockets.pos[1] = PinSockets.pos[0] + new Vector3(-0.4f, 0.0f, -0.7f);
		PinSockets.pos[2] = PinSockets.pos[0] + new Vector3(0.4f, 0.0f, -0.7f);
		PinSockets.pos[3] = PinSockets.pos[0] + new Vector3(-0.8f, 0.0f, -1.4f);
		PinSockets.pos[4] = PinSockets.pos[0] + new Vector3(0.0f, 0.0f, -1.4f);
		PinSockets.pos[5] = PinSockets.pos[0] + new Vector3(0.8f, 0.0f, -1.4f);
		PinSockets.pos[6] = PinSockets.pos[0] + new Vector3(-1.2f, 0.0f, -2.1f);
		PinSockets.pos[7] = PinSockets.pos[0] + new Vector3(-0.4f, 0.0f, -2.1f);
		PinSockets.pos[8] = PinSockets.pos[0] + new Vector3(0.4f, 0.0f, -2.1f);
		PinSockets.pos[9] = PinSockets.pos[0] + new Vector3(1.2f, 0.0f, -2.1f);
		currentPos = player.localPosition;
        isCollided = false;
		isSelected = false;
	
	}

    void Update()
    {
		Touch[] myTouches = Input.touches;
		if (!isCollided)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				Ray ray = Camera.main.ScreenPointToRay (myTouches[i].position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 15f)) 
				{
					
					if (hit.collider == playerc) 
					{
					 isSelected = true;	
					}
				}
			if(isSelected)
			{
			 	if (myTouches[i].phase == TouchPhase.Began)
					{
						fp = myTouches[i].position;
						lp = myTouches[i].position;
					}
				if (myTouches[i].phase == TouchPhase.Moved) 
					{
						
							lp = myTouches[i].position;
							swipeDistanceX = Mathf.Abs ((lp.x - fp.x));
							swipeDistanceY = Mathf.Abs ((lp.y - fp.y));
						
					}
				if (myTouches[i].phase == TouchPhase.Ended)
					{
						
							angle = Mathf.Atan2 ((lp.x - fp.x), (lp.y - fp.y)) * 57.2957795f;

							Debug.Log ("angle: " + angle);

							if (angle > 85 && angle < 95 && swipeDistanceX > 40) {
								print ("right");
								if (checkRightAndDiagonalUpperRight ())
                           // player.position += new Vector3(0.8f, 0.0f, 0.0f);
								player.Translate (new Vector3 (0.8f, 0.0f, 0.0f));
								//StartCoroutine(smooth_move(new Vector3 (0.8f, 0.0f, 0.0f),1f));
							}
							if ((angle > 95 && angle < 175) && (swipeDistanceY > 40 || swipeDistanceX > 40)) {
								print ("diagonal bottom right");
								if (checkDiagonalBottomLeftAndRight ())
                           // player.position += new Vector3(0.4f, 0.0f, -0.7f);
							     player.Translate (new Vector3 (0.4f, 0.0f, -0.7f));
								//StartCoroutine(smooth_move(new Vector3 (0.4f, 0.0f, -0.7f),1f));
							}
							if ((angle >= -175 && angle <= -100) && (swipeDistanceY > 40 || swipeDistanceX > 40)) {
								print ("diagonal bottom left");
								if (checkDiagonalBottomLeftAndRight ())
                          //  player.position += new Vector3(-0.4f, 0.0f, -0.7f);
								player.Translate (new Vector3 (-0.4f, 0.0f, -0.7f));
								//StartCoroutine(smooth_move(new Vector3 (-0.4f, 0.0f, -0.7f),1f));
							}
							if (angle > -105 && angle < -80 && swipeDistanceX > 40) {
								print ("left");
								if (checkLeftAndDiagonalUpperLeft ())
                           // player.position += new Vector3(-0.8f, 0.0f, 0.0f);
								player.Translate (new Vector3 (-0.8f, 0.0f, 0.0f));
								//StartCoroutine(smooth_move(new Vector3 (-0.8f, 0.0f, 0.0f),1f));
							}
							if ((angle <= -5 && angle >= -80) && (swipeDistanceX > 40 || swipeDistanceY > 40)) {
								print ("diagonal upper left");
								if (checkLeftAndDiagonalUpperLeft ())
                        //    player.position += new Vector3(-0.4f, 0.0f, 0.7f);
								player.Translate (new Vector3 (-0.4f, 0.0f, 0.7f));
								//StartCoroutine(smooth_move(new Vector3 (-0.4f, 0.0f, 0.7f),1f));
							}
							if ((angle > 5 && angle < 85) && (swipeDistanceY > 40 || swipeDistanceX > 40)) {
								print ("diagonal upper right");
								if (checkRightAndDiagonalUpperRight ())
                          //  player.position += new Vector3(0.4f, 0.0f, 0.7f);
								player.Translate (new Vector3 (0.4f, 0.0f, 0.7f));
								//StartCoroutine(smooth_move(new Vector3 (0.4f, 0.0f, 0.7f),1f));
							}
						    currentPos = player.position;
						    isSelected = false;
					}

				}
			}
		}
    }
	 private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bowlingBall")
        {
         
            Debug.Log("Impulse Force" +collision.impulse);
            if(collision.impulse.magnitude > 1)
		{ 
			GameObject enemy = GameObject.Find("Main Camera");
			GameController gamecontrol = enemy.GetComponent<GameController> ();
			gamecontrol.isCollided = true;
			gamecontrol.gameOver.SetActive (true);
			gamecontrol.gameover ();
			gamecontrol.pauseB.SetActive (false);
			isCollided = true;
        }
    }

	}
    bool checkRightAndDiagonalUpperRight()
    {
		if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[0])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[2])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[5])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[9])) < 0.01)
            return false;
        else
            return true;
    }

    bool checkLeftAndDiagonalUpperLeft()
    {
		if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[0])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[1])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[3])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[6])) < 0.01)
            return false;
        else
            return true;
    }

    bool checkDiagonalBottomLeftAndRight()
    {
		if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[6])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[7])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[8])) < 0.01)
            return false;
		else if (Mathf.Abs(Vector3.SqrMagnitude(currentPos - PinSockets.pos[9])) < 0.01)
            return false;
        else
            return true;
    }

/*	IEnumerator smooth_move(Vector3 direction,float speed)
	{
		float startime = Time.time;
		float move = Mathf.Lerp (0, 1, startime *speed);
		player.position += direction * move;
		yield return null;
	


	}*/




}

