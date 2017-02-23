using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public GhUnit ParentUnit;
    private Vector3 offset;
    public Text DamageAlert;
    private Text _damageAlert;
    public Canvas Canvas;

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

    public void UpdateHealthBar(int damage)
    {
        var healthBar = GetComponent<Renderer>();
        //healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - (ParentUnit.HitPoints / ParentUnit.MaxHitPoints));
        healthBar.transform.localScale = new Vector3(transform.localScale.x * ParentUnit.HitPoints / ParentUnit.MaxHitPoints, 1, 1);
        StartCoroutine(ShowDamage(damage));
    }

    IEnumerator ShowDamage(int damage)
    {
        _damageAlert = Instantiate(DamageAlert);
        _damageAlert.transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
        _damageAlert.text = damage.ToString();
        _damageAlert.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);

        float startTime = Time.time;

        while (startTime + 0.25f > Time.time)
        {
            _damageAlert.transform.position = Vector3.MoveTowards(_damageAlert.transform.position, new Vector3(_damageAlert.transform.position.x, _damageAlert.transform.position.y + 0.05f, _damageAlert.transform.position.z), ((startTime + 0.25f) - Time.time));
            yield return 0;
        }
        Destroy(_damageAlert);        
    }
}
