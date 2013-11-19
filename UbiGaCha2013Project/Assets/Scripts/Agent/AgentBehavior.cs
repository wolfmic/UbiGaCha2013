using UnityEngine;
using System.Collections;
using System;

public class AgentBehavior : MonoBehaviour
{

    public float f_speed, coeff_reduc, floor = 0, velocity_y = 0.50f, gravity = -0.02f, f_speed_jump = 0.15f, tSecondes = 2;
    public float xMax = 0.0f, xMin = 0.0f, yGround = 0.0f;
    public float minSpeedRange = 0.0f, maxSpeedRange = 0.0f;
    public float minSlowRange = 0.0f, maxSlowRange = 0.0f;
    public int coeffStair = 1000; // 1/coeffStair;
    GameObject player, stair;
    GameObject[] world;
    public bool isRanged = false, isFreezed = false, jump = false, onStairs = false, monte = false, onFloor = false;
    int dir = 0;
    private Vector3 direction = Vector3.zero;
    private float velocity_y_start, f_speed_jump_start;


    // Use this for initialization
    void Start()
    {
        f_speed = UnityEngine.Random.Range(minSpeedRange,maxSpeedRange);
        coeff_reduc = UnityEngine.Random.Range(minSlowRange, maxSlowRange);
        velocity_y_start = velocity_y;
        f_speed_jump_start = f_speed_jump;
    }

    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        if (this.onStairs == true)
        {
            this.world = GameObject.FindGameObjectsWithTag("Floor");
            if (stair.GetComponent<StairsToScript>().floorUp.GetInstanceID() == this.world[(int)floor].GetInstanceID())
            {
                this.transform.Translate(-1 * direction * f_speed / 126 * Time.deltaTime);
                if (this.renderer.bounds.min.y < stair.GetComponent<StairsToScript>().floorDown.renderer.bounds.max.y)
                {
                    this.onStairs = false;
                    for (int i = 0; i < this.world.Length; i++)
                    {
                        if (stair.GetComponent<StairsToScript>().floorDown.GetInstanceID() == this.world[i].GetInstanceID())
                            this.floor = i;
                    }
                    this.xMin = this.world[(int)floor].renderer.bounds.min.x;
                    this.xMax = this.world[(int)floor].renderer.bounds.max.x;
                }
            }
            else
            {
                this.transform.Translate(1 * direction * f_speed / 126 * Time.deltaTime);
                if (this.renderer.bounds.min.y > stair.GetComponent<StairsToScript>().floorUp.renderer.bounds.max.y)
                {
                    this.onStairs = false;
                    for (int i = 0; i < this.world.Length; i++)
                    {
                        if (stair.GetComponent<StairsToScript>().floorUp.GetInstanceID() == this.world[i].GetInstanceID())
                            this.floor = i;
                    }
                    this.xMin = this.world[(int)floor].renderer.bounds.min.x;
                    this.xMax = this.world[(int)floor].renderer.bounds.max.x;
                }
            }

        }
        else if (!jump && !isFreezed)
        {
           
            if (player.GetComponent<PlayerControl>().floor == this.floor)
            {
                if (this.transform.position.x < player.transform.position.x)
                {
                    this.transform.Translate(Vector3.right * Time.deltaTime * f_speed);
                    if (this.transform.position.x > xMax)
                        dir = 1;
                }
                else
                {
                    this.transform.Translate(Vector3.left * Time.deltaTime * f_speed);
                    if (this.transform.position.x < xMin)
                        dir = 0;
                }
            }
            else
            {
                if (dir == 0)
                {
                    this.transform.Translate(Vector3.right * Time.deltaTime * f_speed);
                    if (this.transform.position.x > xMax)
                        dir = 1;
                }
                else
                {
                    this.transform.Translate(Vector3.left * Time.deltaTime * f_speed);
                    if (this.transform.position.x < xMin)
                        dir = 0;
                }
            }
        }
        else if (jump && player.GetComponent<PlayerControl>().floor == this.floor)
        {
            if (this.transform.position.x < player.transform.position.x)
            {
                this.transform.Translate(Vector3.left * Time.deltaTime * f_speed_jump);
            }
            else
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * f_speed_jump);
            }


            this.transform.Translate(Vector3.up * Time.deltaTime * velocity_y);
            velocity_y += gravity;

            if (this.transform.position.y <= this.yGround)
            {

                this.transform.position = new Vector3(this.transform.position.x, this.yGround, this.transform.position.z);
                jump = false;
                velocity_y = velocity_y_start;
                f_speed_jump = f_speed_jump_start;
                StartCoroutine(freeze());
            }
        }


    }

    public void setIsRange()
    {
        this.isRanged = !this.isRanged;
    }

    public void setIsFreeze()
    {
        this.isFreezed = !this.isFreezed;
    }

    public void setJump()
    {
        this.jump = !this.jump;
    }

    IEnumerator freeze()
    {
        isFreezed = true;
        yield return new WaitForSeconds(tSecondes);
        isFreezed = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Stairs")
        {
            int rand = UnityEngine.Random.Range(1, coeffStair);
            stair = other.gameObject;

            if (rand  == 5)
            {
                this.onStairs = true;
            }

        }

     
    }

    
    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }

    
}