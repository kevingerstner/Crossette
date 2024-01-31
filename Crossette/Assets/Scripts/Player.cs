using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public enum PlayerNum {P1, P2};
public class Player : MonoBehaviour, IHitboxResponder
{
    [Header("Basic")]
    [SerializeField] PlayerNum  playerNum;

    [Header("Movement")]
    [SerializeField] public float      m_speed = 4.0f;
    [SerializeField] public float      m_jumpForce = 7.5f;
    [SerializeField] public float      m_rollForce = 6.0f;
    [SerializeField] public bool       m_noBlood = false;
    [SerializeField] public GameObject m_slideDust;
    [SerializeField] float attack_dmg = 15f;
    [SerializeField] float attack_dur = 0.2f;

    private Hitbox hitbox;
    private Collider2D m_pushbox;
    public Animator            m_animator;
    public Rigidbody2D         m_body2d;
    private Fortitude           m_fortitude;
    private Sensor_HeroKnight   m_groundSensor;
    public Sensor_HeroKnight   m_wallSensorR1;
    public Sensor_HeroKnight   m_wallSensorR2;
    public Sensor_HeroKnight   m_wallSensorL1;
    public Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_attacking = false;
    private bool                m_stunned = false;
    private bool                m_killed = false;
    private bool                m_moving = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_currentAttack = 0;
    private Vector3             m_movement = Vector3.zero;
    private float               m_timeSinceAttack = 0.0f;

    public delegate void OnPlayerDeath(PlayerNum n);
    public event OnPlayerDeath onPlayerDeath;

    private MovementStateMachine movementSM;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_pushbox = transform.Find("Pushbox").GetComponent<Collider2D>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_fortitude = GetComponent<Fortitude>();
        hitbox = GetComponentInChildren<Hitbox>();
        movementSM = GetComponent<MovementStateMachine>();
        m_fortitude.onFortitudeChange += CheckDeath;
        m_fortitude.onLoseFortitude += GetHit;
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        if(!m_grounded)
        {
            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);
        }

        // -- Handle Animations --
        //Wall Slide
        m_animator.SetBool("WallSlide", (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State()));

        // Block
        if (Input.GetMouseButtonDown(1))
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(movementSM.m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
    }

    private void OnJump()
    {
        if (!m_grounded || m_killed || m_stunned) return;

        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }

    private void OnStrongAttack()
    {
        print("STRONG ATTACK");
    }

    private void OnLightAttack()
    {
        if (m_killed || m_stunned) return;

        if(!m_attacking && m_timeSinceAttack > 0.25f)
        {
            m_attacking = true;
            m_currentAttack++;

            hitbox.setResponder(this);
            hitbox.startCheckingCollision();

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
            Invoke(nameof(WaitForAttackToFinish), attack_dur);
        }
    }

    public void WaitForAttackToFinish()
    {
        hitbox.stopCheckingCollision();
        m_attacking = false;
    }

    public void collisionedWith(Collider2D collider)
    {
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        if (!hurtbox) Debug.LogError("No Hurtbox Attached");

        hurtbox?.GetHitBy(attack_dmg);
        hitbox.stopCheckingCollision();
    }

    private void CheckDeath(float amt)
    {
        if (!m_fortitude.isDead) return;

        if (!m_killed)
        {
            m_killed = true;
        }
        else return; // if already been killed don't die again

        onPlayerDeath?.Invoke(playerNum);
        m_animator.SetTrigger("Death");
        m_pushbox.enabled = false;
    }

    private void GetHit()
    {
        if (m_killed) return;

        m_animator.SetTrigger("Hurt");
        m_stunned = true;
        DisableMovement();
        Invoke(nameof(Recover), 0.3f);
    }

    private void DisableMovement()
    {
        m_movement.x = 0;
        m_body2d.velocity = Vector3.zero;
        m_animator.SetInteger("AnimState", 0);
        m_moving = false;
    }

    private void Recover()
    {
        m_stunned = false;
    }

    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
    }

    public string getResponderTag()
    {
        return this.tag;
    }
}
