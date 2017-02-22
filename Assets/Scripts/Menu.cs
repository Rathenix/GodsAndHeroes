using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public GameObject Pointer;
    public GameObject NewGame;
    public GameObject LoadGame;
    public GameObject Options;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var newGameLoc = NewGame.gameObject.transform.position;
        var loadGameLoc = LoadGame.gameObject.transform.position;
        var optionsLoc = Options.gameObject.transform.position;

    }
}
