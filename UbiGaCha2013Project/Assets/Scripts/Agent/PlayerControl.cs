using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour {

    // Movement Animation handler
    class PlayerMovementAnimator {

        enum State {
            LEFT,
            RIGHT,
            IDLE
        }

        public Animator Animator { get; private set; }
        private Vector3 _oldPosition;
        private State _oldState;

        public PlayerMovementAnimator(Animator animator, Vector3 actualPosition) {
            _oldPosition = actualPosition;
            _oldState = State.IDLE;

            Animator = animator;
        }

        public void Update(Vector3 actualPosition) {
            Vector3 movement;
            State newState;

            movement = actualPosition - _oldPosition;
            if (movement.x <= -0.001) {
                newState = State.LEFT;
            } else if (movement.x >= 0.001) {
                newState = State.RIGHT;
            } else {
                newState = State.IDLE;
            }
            if (newState != _oldState) {
                switch (newState) {
                    case State.IDLE: Animator.SetTrigger("Idle"); break;
                    case State.LEFT: Animator.SetTrigger("WalkLeft"); break;
                    case State.RIGHT: Animator.SetTrigger("WalkRight"); break;
                }
                _oldState = newState;
            }
            _oldPosition = actualPosition;
        }
    }

    // PlayerControl class

    public float velocity = 12.0f, velocity_max = 12.0f;
    public int level = 5;
    public float range_detection = 0.5f, bell_range = 1.5f;
    public int floor = 0;

    public float startX = 0.0f, startY = 0.0f;
    private Vector3 direction = Vector3.zero;
    public bool onFloor = true;
    private bool onStairs = false;
    private GameObject[] floors;
    private GameObject world;

    private bool freeze = false;
    public bool levelClear = false;

    private PlayerMovementAnimator _movementAnimator;
    private GameObject _carillon;
    private int _rank;
    private MusicPlayer _musicPlayer;
  
	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(this.startX, this.startY, 0.0f);
        this.world = GameObject.FindGameObjectWithTag("World");
        _movementAnimator = new PlayerMovementAnimator(this.GetComponentInChildren<Animator>(), this.transform.position);
        _musicPlayer = GameObject.Find("Music").GetComponent<MusicPlayer>();
	}
	
	// Update is called once per frame
	void Update () {

        UpdateSpeed();

        if (this.freeze == false) {
            if (onFloor == true) {
                this.transform.Translate(Input.GetAxis("Horizontal") * -Vector3.left * velocity * Time.deltaTime);
            }

            if (onStairs == true) {
                direction.Normalize();
                this.transform.Translate(Input.GetAxis("Vertical") * direction * velocity * Time.deltaTime);
            }

            if (_carillon != null && Input.GetKeyDown("e")) {
                this.RingCarillon();
            }

            if (Input.GetButtonDown("Fire1"))
                UseBellSkill();

            _movementAnimator.Update(this.transform.position);
        }
	}

    void RingCarillon() {
        PlayerAnimationEventHandler playerAnimationEventHandler;

        playerAnimationEventHandler = this.GetComponentInChildren<PlayerAnimationEventHandler>();
        this.freeze = true;
        _movementAnimator.Animator.SetTrigger("HitCarillon");
        playerAnimationEventHandler.CarillonHitHandler = this.OnCarillonHit;
        playerAnimationEventHandler.HammerDroppedEventHandler = this.OnHammerDropped;
    }

    void OnCarillonHit() {
        _carillon.GetComponent<BellBehaviour>().Ring();
    }

    void OnHammerDropped() {
        this.freeze = false;
        // check we hit the right bell
        if (_carillon.GetComponent<BellBehaviour>().Rank == _rank) {
            // rank up
            _rank += 1;
            _musicPlayer.Next();
            // TODO: increase difficulty
        } else {
            _rank = 0;
            _musicPlayer.Rewind();
            // TODO: reset difficulty
        }
    }

    void UpdateSpeed()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

        for(int i=0; i < enemies.Length; i++)
        {
            if (enemies[i] != null && IsInRange(enemies[i]) && !enemies[i].GetComponent<AgentBehavior>().isRanged && enemies[i].GetComponent<AgentBehavior>().floor == this.floor)
            {
                enemies[i].GetComponent<AgentBehavior>().setIsRange(true);
                this.velocity -= this.velocity * enemies[i].GetComponent<AgentBehavior>().coeff_reduc;
                if (this.velocity < 0)
                    this.velocity = 0;
            }
            else if (!IsInRange(enemies[i]) && enemies[i].GetComponent<AgentBehavior>().isRanged)
            {
                enemies[i].GetComponent<AgentBehavior>().setIsRange(false);
                this.velocity += this.velocity_max * enemies[i].GetComponent<AgentBehavior>().coeff_reduc;
                if (this.velocity > this.velocity_max)
                    this.velocity = this.velocity_max;
            }
        }
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Stairs")
        {
            onStairs = true;
        }
        if (other.tag == "Floor")
        {
            onFloor = true;
            floor = other.GetComponent<FloorClass>().id;
            Debug.Log("OnTriggerEnter Floor id " + floor);
        }
        if (other.tag == "Bell") {
            _carillon = other.gameObject;
            Debug.Log("Enter Bell");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Stairs")
        {
            onStairs = false;

        }
        if (other.tag == "Floor")
        {
            Debug.Log("OnTriggerExit Floor id " + floor);
            onFloor = false;
            floor = -1;
        }
        if (other.tag == "Bell") {
            Debug.Log("Exit bell");
            _carillon = null;
        }
    }

    bool IsInRange(GameObject enemy)
    {
        if (Math.Abs(enemy.transform.position.x - this.transform.position.x) <= range_detection)
            return true;
        else
            return false;
    }

    bool IsInBellSkillRange(GameObject enemy)
    {
        if (Math.Abs(enemy.transform.position.x - this.transform.position.x) <= bell_range)
            return true;
        else
            return false;
    }

    void UseBellSkill()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

        for (int i = 0; i< enemies.Length; i++)
        {
            if (IsInBellSkillRange(enemies[i]) && enemies[i].GetComponent<AgentBehavior>().floor == this.floor && !enemies[i].GetComponent<AgentBehavior>().jump)
            {
                float coeff = 15 - Math.Abs(enemies[i].transform.position.x - this.transform.position.x);
                enemies[i].GetComponent<AgentBehavior>().f_speed_jump += (1.5f * coeff)/10.0f;
                enemies[i].GetComponent<AgentBehavior>().setJump();
                enemies[i].GetComponent<AgentBehavior>().anim.SetTrigger("Kick");
            }
        }
    }

    void SetFreeze(bool activation)
    {
        freeze = activation;
    }

    void SetLastBell(bool b)
    {
        if (b)
        {
            levelClear = true;
            GameObject[] bells = GameObject.FindGameObjectsWithTag("Bell");
            foreach (GameObject bell in bells)
            {
                bell.SendMessage("SetLevelClear");
            }
        }
    }


    void SetRank(int i)
    {
        if (level == i)
        {
            level++;
        }
        else
        {
            level = 0;
        }
    }
}

