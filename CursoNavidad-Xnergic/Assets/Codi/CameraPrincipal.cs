using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrincipal : MonoBehaviour
{
    [Range(0, 1)] public float deathZone = 0.7f;
    [Range(0, 1)] public float smoothFactor = 0.1f;

    public Transform target;
    public bool isHalf = true;

    private float deathZoneHeight;
    private float deathZoneWidth;

    private float halfSizeTarget;

    private SpriteRenderer spr;


    void Start()
    {
        deathZoneHeight = Camera.main.orthographicSize * deathZone;
        deathZoneWidth = Camera.main.aspect * deathZoneHeight;

        spr = target.gameObject.GetComponent<SpriteRenderer>();

        halfSizeTarget = spr.bounds.size.x * 0.5f;
    }

    void FixedUpdate()
    {
        float deltaX, deltaY;

        deltaX = deltaY = 0;

        if (!IsInDeadZone(ref deltaX, ref deltaY))
        {
            Vector3 newPos = transform.position + new Vector3(deltaX, deltaY, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, smoothFactor);
        }
    }

    private bool IsInDeadZone(ref float deltaX, ref float deltaY)
    {
        Bounds cameraBounds;

        if (isHalf)
        {
            cameraBounds = new Bounds(transform.position, new Vector3(2f * deathZoneWidth + halfSizeTarget, 2f * deathZoneHeight + halfSizeTarget, 100f));

        }
        else
            cameraBounds = new Bounds(transform.position, new Vector3(2f * deathZoneWidth, 2f * deathZoneHeight, 100f));


        if (!cameraBounds.Contains(target.position))
        {
            Vector3 cp = cameraBounds.ClosestPoint(target.position);

            deltaX = target.position.x - cp.x;

            deltaY = target.position.y - cp.y;

            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        deathZoneHeight = Camera.main.orthographicSize * deathZone;
        deathZoneWidth = Camera.main.aspect * deathZoneHeight;

        Gizmos.color = Color.cyan;

        Gizmos.DrawWireCube(transform.position, new Vector3(2 * deathZoneWidth, 2 * deathZoneHeight, 5));
    }
}
