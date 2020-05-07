using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Eagle : Enemy
{
    public AIPath aiPath;

    protected override void Start()
    {
        base.Start();
    }

    public void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
