using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState currentState;
    public Transform player;
    public Health playerHealth;

    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float speed = 3f;
    public LayerMask obstacleMask;

    void Start()
    {
        currentState = new IdleState(this);
        currentState.Enter();
    }

    void Update()
    {
        currentState.Execute();
        IEnemyState nextState = currentState.CheckTransitions();
        if (nextState != null)
        {
            currentState = nextState;
            currentState.Enter();
        }
    }

    public bool HasLineOfSight()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.5f;
        Vector3 rayEnd = player.position + Vector3.up * 0.5f;
        Vector3 direction = rayEnd - rayStart;
        float distance = direction.magnitude;

        if (Physics.Raycast(rayStart, direction.normalized, out RaycastHit hit, distance))
        {
            if (hit.transform != player)
                return false;
        }
        return true;
    }
}

public interface IEnemyState
{
    void Enter();
    void Execute();
    IEnemyState CheckTransitions();
}

public class IdleState : IEnemyState
{
    private EnemyStateMachine enemy;

    public IdleState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("in idle state");
    }

    public void Execute()
    {
        enemy.transform.LookAt(enemy.player.position);
    }

    public IEnemyState CheckTransitions()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (distance <= enemy.detectionRange && enemy.HasLineOfSight())
            return new ChaseState(enemy);
        return null;
    }
}

public class ChaseState : IEnemyState
{
    private EnemyStateMachine enemy;

    public ChaseState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("entered chase state");
    }

    public void Execute()
    {
        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position,
            enemy.player.position,
            enemy.speed * Time.deltaTime
        );
        enemy.transform.LookAt(enemy.player.position);
    }

    public IEnemyState CheckTransitions()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (!enemy.HasLineOfSight())
            return new IdleState(enemy);
        if (distance <= enemy.attackRange)
            return new AttackState(enemy);
        if (distance > enemy.detectionRange)
            return new IdleState(enemy);
        return null;
    }
}

public class AttackState : IEnemyState
{
    private EnemyStateMachine enemy;
    private float attackCooldown = 1f;
    private float attackTimer = 0f;

    public AttackState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("entered attack state");
        attackTimer = attackCooldown;
    }

    public void Execute()
    {
        enemy.transform.LookAt(enemy.player.position);
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            if (enemy.playerHealth != null)
                enemy.playerHealth.TakeDamage(10);
            Debug.Log("attacked player");
            attackTimer = attackCooldown;
        }
    }

    public IEnemyState CheckTransitions()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (!enemy.HasLineOfSight())
            return new IdleState(enemy);
        if (distance > enemy.attackRange && distance <= enemy.detectionRange)
            return new ChaseState(enemy);
        if (distance > enemy.detectionRange)
            return new IdleState(enemy);
        return null;
    }
}