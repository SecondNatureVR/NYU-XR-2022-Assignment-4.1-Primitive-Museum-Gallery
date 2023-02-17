using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LookAtHead : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform coffee;
    [SerializeField] [Range(0,100)] public float weightScale = 1;
    private LookAtConstraint constraint;

    private ConstraintSource playerSource;
    private ConstraintSource coffeeSource;
    private List<ConstraintSource> sourcesEnter;
    private List<ConstraintSource> sourcesExit;

    private void Start()
    {
        constraint = GetComponent<LookAtConstraint>();
        playerSource = new ConstraintSource();
        coffeeSource = new ConstraintSource();
        playerSource.sourceTransform = player;
        coffeeSource.sourceTransform = coffee;
        coffeeSource.weight = 1;
        sourcesEnter = new List<ConstraintSource> { playerSource, coffeeSource };
        sourcesExit = new List<ConstraintSource> { coffeeSource };
        constraint.SetSources(sourcesEnter);
    }

    void Update()
    {
        if (constraint.sourceCount > 1)
        {
            playerSource.weight = weightScale / Mathf.Pow(Vector3.Distance(player.position, transform.position), 3);
            constraint.SetSource(0, playerSource);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            constraint.SetSources(sourcesEnter);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            constraint.SetSources(sourcesExit);
        }
    }
}
