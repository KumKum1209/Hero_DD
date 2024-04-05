using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;
    [SerializeField] float hp=1;
    [SerializeField] float maxHp=1;

    [SerializeField] private Transform target;

    void FixedUpdate()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 5f);
        transform.position = target.position + offset;
        transform.rotation = Quaternion.identity;
    }
    public void OnInit(float maxHp, Transform target)
    {
        this.target = target;
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1f;
    }
    public void SetNewHP(float hp)
    {
        this.hp = hp;
    }
}
