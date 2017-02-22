using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiController : MonoBehaviour {

    public CellGrid CellGrid;

	void Update () {
		if(Input.GetKeyDown(KeyCode.N))
        {
            CellGrid.EndTurn();
        }
	}
}
