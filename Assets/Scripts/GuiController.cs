﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public CellGrid CellGrid;
    public GameObject UnitsParent;
    public Canvas Canvas;

    public GameObject InfoPanel;
    private GameObject _infoPanel;

    private void Start()
    {
        
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

        var stats = new StringBuilder();
        stats.Append(unit.HitPoints.ToString() + "/" + unit.MaxHitPoints.ToString()).AppendLine();
        stats.Append(unit.AttackFactor.ToString()).AppendLine();
        stats.Append(unit.DefenceFactor.ToString()).AppendLine();
        stats.Append(unit.MovementPoints.ToString());

        _infoPanel.transform.Find("NameText").GetComponent<Text>().text = unit.DisplayName;
        _infoPanel.transform.Find("StatsText").GetComponent<Text>().text = stats.ToString();

        _infoPanel.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);
    }

    private void OnUnitDehighlighted(object sender, EventArgs e)
    {
        Destroy(_infoPanel);
    }

    private void OnUnitDestroyed(object sender, EventArgs e)
    {
        Destroy(_infoPanel);
    }

    void Update () {
		if(Input.GetKeyDown(KeyCode.N))
        {
            CellGrid.EndTurn();
        }
	}
}
