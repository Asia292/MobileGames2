using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    /* public void switchButton()
     {
         SceneManager.LoadScene("test", LoadSceneMode.Single);
     }*/

    /*public static void MergeScene(Scene sourceScene, Scene destinationScene)
    {
       /* sourceScene = SceneManager.GetActiveScene();
        destinationScene = SceneManager.GetSceneByName("test");*/
    //}

    public string scene;    // The scene to move to
    public string exit;     // The exit pos
    public int y;           // Y rotation of player on exit
    GameObject player;      
    GameObject objects;     // Objs to move
    //GameObject canv;
    //GameObject eventS;
    //GameObject buttons;
    GameObject exitPos;     // Place to load player into
    //Vector3 og;
    Quaternion rotation;    // Rotation of the player
    

    private void Start()
    {
        //// ASSIGNS VARIABLES ////
        player = GameObject.FindWithTag("Player");
        //canv = GameObject.Find("GeneralCanvas");
        //eventS = GameObject.Find("EventSystem");
        //buttons = GameObject.Find("Buttons");
        objects = GameObject.Find("ObjectsToMove");

        rotation = Quaternion.Euler(0, y, 0);
    }

    //// CHANGES SCENE WHEN PLAYER COLLIDES WITH OBJ ////
    private void OnCollisionEnter(Collision collision)
    {
        //og = new Vector3 (0.0f,1.0f,0.0f);
        //Debug.Log("colliding");
        //Debug.Log(scene);

        if (collision.gameObject.name == "Ethan")
        {
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    //// COROUTINE TO CHANGE SCENE ////
    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        exitPos = GameObject.Find(exit);    // Assign exit pos

        //// MOVE PLAYER TO SCENE, SET POS AND ROTATION ////
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(scene));
        player.transform.position = exitPos.transform.position;
        player.transform.rotation = rotation;
        //SceneManager.MoveGameObjectToScene(canv, SceneManager.GetSceneByName(scene));
        //SceneManager.MoveGameObjectToScene(eventS, SceneManager.GetSceneByName(scene));
        //SceneManager.MoveGameObjectToScene(buttons, SceneManager.GetSceneByName(scene));
        SceneManager.MoveGameObjectToScene(objects, SceneManager.GetSceneByName(scene));    // Move all objs to scene
        SceneManager.UnloadSceneAsync(currentScene);    // Unload prev scene
    }
}
