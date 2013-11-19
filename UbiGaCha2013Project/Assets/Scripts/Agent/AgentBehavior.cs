using UnityEngine;
using System.Collections;

public class AgentBehavior : MonoBehaviour {

	public float   f_speed, coeff_reduc, floor = 0, velocity_y = 50, gravity = -2, f_speed_jump = 15.0f;
    public float xMax = 0.0f, xMin = 0.0f;
    GameObject player;
    public bool isRanged = false, isFreezed = false, jump = false;
    int dir = 0;
   
    
	// Use this for initialization
	void Start () {
        f_speed = Random.Range(0.001f, 0.08f);
        coeff_reduc = Random.Range(0.001f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {

        player = GameObject.FindGameObjectWithTag("Player");


        if(!jump && !isFreezed)
        {
            if (player.GetComponent<PlayerControl>().floor == this.floor)
            {
                if (this.transform.position.x < player.transform.position.x)
                {
                    this.transform.Translate(Vector3.right * Time.deltaTime * f_speed);
                }
                else
                {
                    this.transform.Translate(Vector3.left * Time.deltaTime * f_speed);
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

            if (this.transform.position.y <= 5.0f + (floor * 9.0f))
            {

                this.transform.position = new Vector3(this.transform.position.x, 5.0f + (floor * 9.0f), this.transform.position.z);
                jump = false;
                velocity_y = 50;
                f_speed_jump = 15.0f;
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
        yield return new WaitForSeconds(2);
        isFreezed = false;

    }
}

