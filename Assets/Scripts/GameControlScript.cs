using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour {

    public static int score;
    public int scoreNeededToProgress;

    private Scene currentScene;
    private string sceneName;

    // PigHead Stuff and Reset Stuff
    public Rigidbody2D projectile;
    public float resetSpeed = 0.025f;

    private float resetSpeedSqr;
    private SpringJoint2D spring;

    // Use this for initialization
    void Start () {

        resetSpeedSqr = resetSpeed * resetSpeed;
        spring = projectile.GetComponent<SpringJoint2D>();

        score = 0;
        Debug.Log("Score is: " + score);

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if(sceneName == "Level1")
        {
            scoreNeededToProgress = 2;
        }
        if (sceneName == "Level2")
        {
            scoreNeededToProgress = 3;
        }
        if (sceneName == "Level3")
        {
            scoreNeededToProgress = 4;
        }

        Debug.Log("ScoreNeededToProgress: " + scoreNeededToProgress);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("User pressed Reset!");
            Reset();
        }

        if (spring == null && projectile.velocity.sqrMagnitude < resetSpeedSqr)
        {
            Debug.Log("Failed...Invoking Reset");
            Invoke("Reset", 3);
        }

        if (sceneName == "Level1")
        {
            if(score == scoreNeededToProgress)
            {
                Debug.Log("Score reached " + score + " Time to load level 2!");
                Invoke("LoadLevel2", 2);
            }
        }
        if (sceneName == "Level2")
        {
            if (score == scoreNeededToProgress)
            {
                Debug.Log("Score reached " + score + " Time to load level 3!");
                Invoke("LoadLevel3", 2);
            }
        }
        if (sceneName == "Level3")
        {
            if (score == scoreNeededToProgress)
            {
                Debug.Log("Score reached " + score + " Time to load level Game Won Screen!");
                Invoke("LoadGameWon", 2);
            }
        }
    }

    void LoadLevel1()
    {
        Debug.Log(".....Loading Level 1.....");
        SceneManager.LoadScene("Level1");
    }
    void LoadLevel2()
    {
        Debug.Log(".....Loading Level 2.....");
        SceneManager.LoadScene("Level2");
    }
    void LoadLevel3()
    {
        Debug.Log(".....Loading Level 3.....");
        SceneManager.LoadScene("Level3");
    }
    void LoadGameWon()
    {
        Debug.Log(".....Loading Game Won.....");
        SceneManager.LoadScene("GameWon");
    }

    void Reset()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        Debug.Log("ResetFunction - Loading " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        string name = obj.gameObject.name;

        if (name == "PigHead")
        {
            Debug.Log("PigHead has left the building... invoking reset.");
            Invoke("Reset", 3);
        }
    }

}
