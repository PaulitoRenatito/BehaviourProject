using Behaviour;
using Behaviour.Nodes;
using Behaviour.Strategies;
using UnityEngine;
using Utilities;

public class Entity : MonoBehaviour
{
    
    [SerializeField] int health = 100;
    [SerializeField] int stamina = 100;
    [SerializeField] int accumulatedDamage = 0;
    
    [SerializeField] string currentAction;

    private BehaviourTree behaviourTree;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Node checkHealth = new Leaf("Health less then 20", new ConditionStrategy(() => health < 20));
        Node recoverHealth = new Leaf("Recover health", new ActionStrategy(() =>
        {
            health += 10;
            stamina -= 5;
            currentAction = "Recover health";
        }));
        
        Node recoverHealthDelay = new Delay("Recover Health Delay", time: 4);
        recoverHealthDelay.AddChild(recoverHealth);
        
        Node healthSequence = new Sequence("HealthSequence", 3);
        healthSequence.AddChild(checkHealth);
        healthSequence.AddChild(recoverHealthDelay);
        
        Node checkStamina = new Leaf("Stamina less then 20", new ConditionStrategy(() => stamina < 20));
        Node recoverStamina = new Leaf("Recover stamina", new ActionStrategy(() =>
        {
            currentAction = "Recover stamina";
            stamina += 5;
        }));
        
        Node recoverStaminaDelay = new Delay("Recover Stamina Delay", time: 1);
        recoverStaminaDelay.AddChild(recoverStamina);
        
        Node staminaSequence = new Sequence("StaminaSequence", 2);
        staminaSequence.AddChild(checkStamina);
        staminaSequence.AddChild(recoverStaminaDelay);
        
        Node attack = new Leaf("Attack", new ActionStrategy(() =>
        {
            currentAction = "Attack";
            accumulatedDamage += 5;
            health -= 10;
            stamina -= 15;
        }), 1);
        Node attackDelay = new Delay("Attack Delay", time: 2);
        attackDelay.AddChild(attack);

        Node prioritySelector = new PrioritySelector("Priority Selector");
        prioritySelector.AddChild(healthSequence);
        prioritySelector.AddChild(staminaSequence);
        prioritySelector.AddChild(attackDelay);

        Node utilFail = new UntilFail("Until Fail");
        utilFail.AddChild(prioritySelector);

        behaviourTree = new BehaviourTree("Root");
        behaviourTree.AddChild(utilFail);
    }
    
    void Update()
    {
        behaviourTree.Process();
    }
}
