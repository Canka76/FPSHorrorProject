using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentAnimator : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private Transform[] Tasks; // Task locations
    private bool isPerformingTask = false;

    private void Start()
    {
        // Initialize NavMeshAgent and Animator
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
        }
    }

    private void Update()
    {
        // Update walking animation based on agent movement
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void PerformTask(int taskIndex)
    {
        if (isPerformingTask)
            return;

        if (taskIndex < 0 || taskIndex >= Tasks.Length)
        {
            Debug.LogError("Invalid task index!");
            return;
        }

        StartCoroutine(TaskRoutine(taskIndex));
    }

    private IEnumerator TaskRoutine(int taskIndex)
    {
        isPerformingTask = true;

        // Move to the task location
        agent.SetDestination(Tasks[taskIndex].position);

        // Wait until the agent reaches the destination
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        // Start task animation
        animator.SetBool("IsPerformingTask", true);
        animator.SetInteger("TaskIndex", taskIndex);

        Debug.Log("Performing Task: " + taskIndex);

        // Simulate task duration (e.g., 3 seconds)
        yield return new WaitForSeconds(3f);

        // End task animation
        animator.SetBool("IsPerformingTask", false);

        Debug.Log("Task Completed!");

        isPerformingTask = false;
    }
}