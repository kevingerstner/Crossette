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

    [Header("Crossette Combat")]
    [SerializeField] public float attack_dmg = 15f;
    [SerializeField] public float attack_dur = 0.2f;
    [SerializeField] public float block_downtime = 0.25f;

    private Hitbox hitbox;
    private Collider2D m_pushbox;
    private Animator            animator;
    public Rigidbody2D         m_body2d;
    private Fortitude           m_fortitude;
    private bool                m_stunned = false;
    private bool                m_killed = false;

    public delegate void OnPlayerDeath(PlayerNum n);
    public event OnPlayerDeath onPlayerDeath;

    public MovementStateMachine movementSM;
    public CrossetteStateMachine crossetteSM;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        m_pushbox = transform.Find("Pushbox").GetComponent<Collider2D>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_fortitude = GetComponent<Fortitude>();
        hitbox = GetComponentInChildren<Hitbox>();
        movementSM = GetComponent<MovementStateMachine>();
        m_fortitude.onFortitudeChange += CheckDeath;
        //m_fortitude.onLoseFortitude += GetHit;
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
        animator.SetTrigger("Death");
        m_pushbox.enabled = false;
    }

    private void Recover()
    {
        m_stunned = false;
    }

    public string getResponderTag()
    {
        return this.tag;
    }
}
