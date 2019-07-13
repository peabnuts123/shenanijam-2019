using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Use this gameobject's transform.y position to set the sorting 
///     order of a target SpriteRenderer.
///     The user-defined sort order of the target SpriteRenderer should
///     be honoured for objects that are at the same Y coordinate.
/// </summary>
public class YCoordinateAsSortOrder : MonoBehaviour
{
    private static readonly float FACTOR = 100F;

    [SerializeField]
    [NotNull]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    [NotNull]
    public Transform transformMarker;

    // Private state
    private int originalSortOrder;
    private Transform originalTransformMarker;

    void Start()
    {
        this.originalSortOrder = this.spriteRenderer.sortingOrder;
        this.originalTransformMarker = this.transformMarker;
    }

    void Update()
    {
        this.spriteRenderer.sortingOrder = (int)(FACTOR * -this.transformMarker.position.y + this.originalSortOrder);
    }

    public void OverrideTransformMarker(Transform transformMarkerOverride)
    {
        this.transformMarker = transformMarkerOverride;
    }

    public void ClearTransformMarkerOverride()
    {
        this.transformMarker = this.originalTransformMarker;
    }
}
