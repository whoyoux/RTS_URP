using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public Transform target;

    private NavMeshAgent agent;

    public ParticleSystem clickEffect;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(1) && !GameManager.instance.IsGamePaused())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                RemoveFocus();
                MoveToPoint(hit.point);
                if (clickEffect != null)
                {
                    var go = Instantiate(clickEffect, hit.point + new Vector3(0,0.1f, 0), clickEffect.transform.rotation);
                    StartCoroutine(DestroyParticle(go.gameObject));
                }
            }

        }

        if (Input.GetMouseButtonDown(0) && !GameManager.instance.IsGamePaused())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable == null) return;

                SetFocus(interactable);
            }
        }

        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    private void MoveToPoint(Vector3 target)
    {
        agent.SetDestination(target);
    }

    private void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    private void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    private void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
                focus.OnDefocused();

            focus = newFocus;
            FollowTarget(newFocus);
        }
        
        newFocus.OnFocus(transform);
    }

    private void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();

        focus = null;
        StopFollowingTarget();
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    IEnumerator DestroyParticle(GameObject go)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(go);

    }
}
