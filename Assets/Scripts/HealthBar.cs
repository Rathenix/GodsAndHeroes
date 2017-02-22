using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public GhUnit ParentUnit;
    private Vector3 offset;

    private void Start()
    {
        ParentUnit = GetComponentInParent<GhUnit>();
        offset = new Vector3(-0.3f, 0.5f, 0);
        transform.position = ParentUnit.transform.position + offset;
    }

    private void Update()
    {
        transform.position = ParentUnit.transform.position + offset;
    }

    public void UpdateHealthBar()
    {
        var healthBar = GetComponent<Renderer>();
        //healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - ParentUnit.HitPoints / ParentUnit.MaxHitPoints);
        healthBar.transform.localScale = new Vector3(transform.localScale.x * ParentUnit.HitPoints / ParentUnit.MaxHitPoints, 0.15f, 0.15f);
    }
}
