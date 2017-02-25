using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public CellGrid CellGrid;
    public GameObject UnitsParent;

    public GameObject InfoPanel;
    private GameObject _infoPanel;
    public GameObject TurnPanel;
    private GameObject _turnPanel;

    private void Start()
    {
        CellGrid.GameStarted += OnGameStarted;
        CellGrid.TurnEnded += OnTurnEnded;
        CellGrid.GameEnded += OnGameEnded;
        foreach (Transform unit in UnitsParent.transform)
        {
            unit.GetComponent<Unit>().UnitHighlighted += OnUnitHighlighted;
            unit.GetComponent<Unit>().UnitDehighlighted += OnUnitDehighlighted;
            unit.GetComponent<Unit>().UnitDestroyed += OnUnitDestroyed;
        }
    }

    private void OnUnitHighlighted(object sender, EventArgs e)
    {
        var unit = sender as GhUnit;
        _infoPanel = Instantiate(InfoPanel);
        var scroll = _infoPanel.transform.Find("InfoScroll");

        var stats = new StringBuilder();
        stats.Append(unit.HitPoints.ToString() + "/" + unit.MaxHitPoints.ToString()).AppendLine();
        stats.Append(unit.AttackFactor.ToString()).AppendLine();
        stats.Append(unit.DefenceFactor.ToString()).AppendLine();
        stats.Append(unit.MovementPoints.ToString());

        scroll.transform.Find("NameText").GetComponent<Text>().text = unit.DisplayName;
        scroll.transform.Find("StatsText").GetComponent<Text>().text = stats.ToString();

        _infoPanel.transform.SetParent(transform, false);
    }

    private void OnUnitDehighlighted(object sender, EventArgs e)
    {
        Destroy(_infoPanel);
    }

    private void OnUnitDestroyed(object sender, EventArgs e)
    {
        Destroy(_infoPanel);
    }

    private void OnGameStarted(object sender, EventArgs e)
    {
        StartCoroutine(ShowTurnPanel(false));
    }

    private void OnTurnEnded(object sender, EventArgs e)
    {
        StartCoroutine(ShowTurnPanel(false));
    }

    private void OnGameEnded(object sender, EventArgs e)
    {
        StartCoroutine(ShowTurnPanel(true));
    }

    IEnumerator ShowTurnPanel(bool gameOver)
    {
        if (_turnPanel != null) Destroy(_turnPanel);
        _turnPanel = Instantiate(TurnPanel);
        if (gameOver)
        {
            _turnPanel.GetComponentInChildren<Text>().text = "Game Over";
        }
        else
        {
            var playerName = CellGrid.CurrentPlayer.PlayerNumber == 0 ? "Player" : "Computer";
            _turnPanel.GetComponentInChildren<Text>().text = playerName + "'s\nTurn";
        }
        _turnPanel.transform.SetParent(transform, false);
        yield return new WaitForSeconds(1f);
        if (!gameOver) Destroy(_turnPanel);
    }

    void Update () {
		if(Input.GetKeyDown(KeyCode.N))
        {
            CellGrid.EndTurn();
        }
	}
}
