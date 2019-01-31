using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public float startWait;
	public float spawnWait;
	public float waveWait;
	public int hazardCount;
	public Text scoreText;
	public Text restartText, gameOverText;
	private bool restart, gameOver;
	private int score;

	void Start () {
		score = 0;
		restartText.text = gameOverText.text = "";
		restart = gameOver = false;
		StartCoroutine (SpawnWaves ());
	}

	void Update() {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		}
	} 
	
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for(int i = 0;i<hazardCount;i++){
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x,spawnValues.x),spawnValues.y,spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (startWait);
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver) {
				restart = true;
				Debug.Log ("Press 'R' to Restart");
				restartText.text = "Restart";
				break;
			}
		}

	
	}
		
	public void SetGameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	public void AddScore (int newScore){
		score += newScore;
		UpdateScore ();
	}

	void UpdateScore (){
		scoreText.text = "Score: " + score;
	}
}
