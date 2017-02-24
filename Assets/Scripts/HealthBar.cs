using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private Image HealthAmount;
    public GhUnit ParentUnit;
    public Text DamageAlert;
    private Text _damageAlert;
    public Canvas Canvas;

    private void Start()
    {
        HealthAmount = transform.GetComponentInChildren<Image>();
        ParentUnit.HealthBar = this;
    }

    private void Update()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(ParentUnit.transform.position.x, ParentUnit.transform.position.y + 0.6f);
    }

    public void UpdateHealthBar(int damage)
    {
        HealthAmount.fillAmount = ParentUnit.HitPoints / ParentUnit.MaxHitPoints;
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
