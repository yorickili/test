using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//add
//属性：当前电量、最大电量、电量阈值
//电量减少：发波（20f）、敌人1（持续消耗10f）、敌人2（立即消耗10f）
//电量增加：血包1（作用于当前血量）、血包2（作用于最大血量）
public class PlayerHealth : MonoBehaviour
{
    public float health = 60f;					//最大血量
    public float maxhealth = 100f;              //血量阀值
    public float nowhealth = 60f;               //当前血量,出生时为最大血量值

    public float repeatDamagePeriod = 2f;       // How frequently the player can be damaged.
    //public AudioClip[] ouchClips;               // Array of clips to play when the player is damaged.
    public float hurtForce = 10f;               // The force with which the player is pushed when hurt.

    public float damageAmount = 10f;            // The amount of damage to take when enemies touch the player 遇到障碍物后掉血量
    public float hurtingtime=5f;                //持续掉血属性：每秒掉多少血

    public float waveCost = 20f;                //每次发波消耗
    //public float CD = 3f;                       //发波CD
    //public float wavetimes = 5f;                //发波次数限制

    private SpriteRenderer healthBar;           // Reference to the sprite renderer of the health bar.
    private float lastHitTime;                  // The time at which the player was last hit.
    private Vector3 healthScale;                // The local scale of the health bar initially (with full health).
    private PlayerControl playerControl;        // Reference to the PlayerControl script.
   // private Animator anim;                      // Reference to the Animator on the player

    private Button waveBtn;
    //private float time;                           //发波冷却剩余时间
    private bool  islastinghurt = false;          //是否持续掉血
    private float temphurting;                    //持续伤害总和变量

    public Sprite sprite;
    public Sprite damageSprite;
    public Sprite deadSprite;


    void Awake()
    {
        // Setting up references.
        playerControl = GetComponent<PlayerControl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
       // anim = GetComponent<Animator>();

        // Getting the intial scale of the healthbar (whilst the player has full health).
        healthScale = healthBar.transform.localScale;

       /* waveBtn = GameObject.Find("WaveBtn").GetComponent<Button>();
        waveBtn.onClick.AddListener(waveClick);  //后面需要改成触摸发波 */

        Debug.Log("nowhealth" + nowhealth);

    }

    private void Update()  
    {
        /*if (time > 0)    //用来实现波的CD冷却 可放置在波的相关方法中
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (nowhealth > waveCost&&wavetimes>0)   //如果剩余的电量能够大于发波消耗量，并且发波次数大于0
                waveBtn.interactable = true;         //可点击发波按钮
        }*/

        if (islastinghurt)  //如果遇到持续伤害
        {
            if (temphurting > 0)  //每次扣10点 
            {
                temphurting -= Time.deltaTime * hurtingtime;  //每秒扣5点电量,一共10点，花2s左右
                if (nowhealth > 0)
                {
                    nowhealth -= Time.deltaTime * 5f;
                }
                UpdateHealthBar();

            }
            else
            {
                //Debug.Log("掉血量temphurting" + temphurting);
                Debug.Log("逐渐掉血结束nowhealth" + nowhealth);
                islastinghurt = false;
            }

        }
           

        if (nowhealth < 20)
        {
           GetComponent<SpriteRenderer>().sprite = damageSprite;
        }
        else if(nowhealth <= 0)
        {
            Death();
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If the colliding gameobject is an Enemy...
        if (col.gameObject.name == "obstacle_ stab")  //立即掉血
        {
            
            // ... and if the time exceeds the time of the last hit plus the time between hits...
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                // ... and if the player still has health...
                if (nowhealth > 0f)
                {
                    // ... take damage and reset the lastHitTime.
                    TakeDamage(col.transform);
                    lastHitTime = Time.time;
                    print("enemy2立即掉血nowhealth"+nowhealth);
                }
            }
        }
        else if (col.gameObject.name == "env_neon_river")  //持续受伤
        {
            print("enemy1逐渐掉血开始");
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                // ... and if the player still has health...
                if (nowhealth > 0f)
                {
                    // ... take damage and reset the lastHitTime.
                    if (islastinghurt)     //如果持续伤害正在进行中
                    {
                        temphurting += damageAmount;
                        lastHitTime = Time.time;    //上次碰撞时间
                    }
                    else
                    {
                        temphurting = damageAmount;   //持续受伤总和
                        islastinghurt = true;        //持续受伤开启
                        lastHitTime = Time.time;    //上次碰撞时间
                                                    //hurtingtime = 2f;
                    }
                   
                }
            }
        }
        else if (col.gameObject.tag == "")  //立即死亡
        {
            Death();
        }
    }

    void TakeDamage(Transform enemy)
    {
        // Make sure the player can't jump.
        playerControl.jump = false;

        // Create a vector that's from the enemy to the player with an upwards boost.
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

        // Add a force to the player in the direction of the vector and multiply by the hurtForce.
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

        // Reduce the player's health by 10.
        nowhealth -= damageAmount;

        // Update what the health bar looks like.
        UpdateHealthBar();

        // Play a random clip of the player getting hurt.
        //int i = Random.Range(0, ouchClips.Length);
        //AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
    }

    void Death()
    {
        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        // Move all sprite parts of the player to the front
        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spr)
        {
            s.sortingLayerName = "UI";
        }

        // ... disable user Player Control script
        GetComponent<PlayerControl>().enabled = false;

        // ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
        GetComponentInChildren<Gun>().enabled = false;

        // ... Trigger the 'Die' animation state
        //anim.SetTrigger("Die");
    }

    public void WaveReduceHealth()
    {
        //waveBtn.interactable = false;
        nowhealth -= waveCost; //Reduce the player's health by 20.
        UpdateHealthBar();  //Update what the health bar looks like.
        //time = CD;
        //wavetimes -= 1;
        //Debug.Log("发波后nowhealth" + nowhealth);
    }

    public void UpdateHealthBar()
    {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        //healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - nowhealth * 0.01f);
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - nowhealth/health);

        // Set the scale of the health bar to be proportional to the player's health.
        // healthBar.transform.localScale = new Vector3(healthScale.x * nowhealth * 0.01f, 1, 1);
        healthBar.transform.localScale = new Vector3(healthScale.x * (nowhealth/health), 1, 1);
    }

}
