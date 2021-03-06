﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBomb : BulletController
{
    private float timeExplose = 3.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        body.SetRotation(Time.deltaTime * 1000f);
    }

    public override void Fired(Vector3 direction, string tag)
    {
        // if (body == null)
        // {
        //     body = GetComponent<Rigidbody2D>();
        // }
        // transform.Translate(direction * 100f);
        // tagShootFrom = tag;
        base.Fired(direction, tag);
        StartCoroutine(ScheduleExplode());
    }

    IEnumerator ScheduleExplode()
    {
        yield return new WaitForSeconds(timeExplose);
        DestroyBullet();
    }
}
