using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//add
//属性：当前电量、最大电量、电量阈值
//电量减少：发波（20f）、敌人1（持续消耗10f）、敌人2（立即消耗10f）
//电量增加：血包1（作用于当前血量）、血包2（作用于最大血量）
public class PlayerHealth : MonoBehaviour
{
    public float maxhealth = 100f;              //血量阀值
    public float nowhealth = 100f;               //当前血量,出生时为最大血量值

    //private SpriteRenderer healthBar;           // Reference to the sprite renderer of the health bar.
    //private Vector3 healthScale;                // The local scale of the health bar initially (with full health).
    private PlayerControl playerControl;        // Reference to the PlayerControl script.


    void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
        //healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        //healthScale = healthBar.transform.localScale;

        Debug.Log("nowhealth" + nowhealth);

    }

    private void Update()
    {

    }

    public void ReduceHealth(float delta)
    {
        nowhealth -= delta;
        if (nowhealth < 0) nowhealth = 0f;

        //UpdateHealthBar();
        if (nowhealth <= 0) Death();
    }

    public void IncreaseHealth(float delta)
    {
        nowhealth += delta;
        if (nowhealth > maxhealth) maxhealth = nowhealth;

        //UpdateHealthBar();
    }

    private void Death()
    {
        GetComponent<PlayerControl>().Death();

        //GetComponent<PlayerControl>().enabled = false;
    }

    public void UpdateHealthBar()
    {
        //healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - nowhealth / maxhealth);
        //healthBar.transform.localScale = new Vector3(healthScale.x * (nowhealth / maxhealth), 1, 1);
    }

}
