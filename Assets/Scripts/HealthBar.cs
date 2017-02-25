using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public GhUnit ParentUnit;
    public Text DamageAlert;
    private Text _damageAlert;
    private Canvas Canvas;

    private void Start()
    {
        Canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {

    }

    public void UpdateHealthBar(int damage)
    {
        float fill = (float)ParentUnit.HitPoints / (float)ParentUnit.MaxHitPoints;
        GetComponent<Image>().fillAmount = fill;
        StartCoroutine(ShowDamage(damage));
    }

    IEnumerator ShowDamage(int damage)
    {
        _damageAlert = Instantiate(DamageAlert);
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
