using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhUnit : Unit
{
    public int MaxHitPoints;
    public string DisplayName;
    public Color LeadingColor;
    private HealthBar HealthBar;

    public override void Initialize()
    {
        base.Initialize();
        transform.position += new Vector3(0, 0, -1);
        HealthBar = GetComponentInChildren<HealthBar>();
        //GetComponent<Renderer>().material.color = LeadingColor;
    }

    public void OnUnitHighlighted(object sender, EventArgs e)
    {
        var unit = sender as GhUnit;
        unit.GetComponent<Renderer>().material.color = LeadingColor + Color.green;
    }

    public override void MarkAsSelected()
    {
        GetComponent<Renderer>().material.color = LeadingColor + Color.green;
    }

    public override void MarkAsAttacking(Unit other)
    {
        //GetComponent<Renderer>().material.color = LeadingColor;
        StartCoroutine(AttackMotion(other));
    }

    public override void MarkAsDefending(Unit other)
    {
        //GetComponent<Renderer>().material.color = LeadingColor;
    }

    protected override void Defend(Unit other, int damage)
    {
        var oldHP = HitPoints;
        base.Defend(other, damage);
        var dmgDelta = oldHP - HitPoints;
        HealthBar.UpdateHealthBar(dmgDelta);
    }
 
    public override void MarkAsDestroyed()
    {
        Destroy(this);
    }

    public override void MarkAsFinished()
    {
        GetComponent<Renderer>().material.color = LeadingColor + Color.black;
    }

    public override void MarkAsFriendly()
    {
        //GetComponent<Renderer>().material.color = LeadingColor;
    }

    public override void MarkAsReachableEnemy()
    {
        //GetComponent<Renderer>().material.color = LeadingColor;
    }

    public override void UnMark()
    {
        //GetComponent<Renderer>().material.color = LeadingColor;
    }

    IEnumerator AttackMotion(Unit other)
    {
        var heading = other.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        float startTime = Time.time;

        while (startTime + 0.25f > Time.time)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + (direction / 3f), ((startTime + 0.25f) - Time.time));
            yield return 0;
        }
        startTime = Time.time;
        while (startTime + 0.25f > Time.time)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - (direction / 3f), ((startTime + 0.25f) - Time.time));
            yield return 0;
        }
        transform.position = Cell.transform.position + new Vector3(0, 0, -0.1f);
    }

    public override bool IsUnitAttackable(Unit other, Cell sourceCell)
    {
        if (sourceCell.GetDistance(other.Cell) == AttackRange)
            return true;

        return false;
    }
}