using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public CellGrid CellGrid;
    public GameObject UnitsParent;
    public Canvas Canvas;

    public HealthBar HealthBar;
    private HealthBar _healthBar;
    public Text DamageAlert;
    public GameObject InfoPanel;
    private GameObject _infoPanel;

    private void Start()
    {
        
        foreach (Transform unit in UnitsParent.transform)
        {
            unit.GetComponent<Unit>().UnitHighlighted += OnUnitHighlighted;
            unit.GetComponent<Unit>().UnitDehighlighted += OnUnitDehighlighted;

            _healthBar = Instantiate(HealthBar);
            _healthBar.ParentUnit = unit.GetComponent<GhUnit>();
            _healthBar.DamageAlert = DamageAlert;
            _healthBar.Canvas = Canvas;
            _healthBar.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);
            _healthBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(unit.transform.position.x, unit.transform.position.y + 0.6f);
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

    void Update () {
		if(Input.GetKeyDown(KeyCode.N))
        {
            CellGrid.EndTurn();
        }
	}
}
