using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[RequireComponent(typeof(TrackableBehaviour), typeof(FixedJoint))]
public class ImageTargetHandler : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    [SerializeField] private GameObject playground;
    private FixedJoint joint;

    void Start()
    {
        joint = GetComponent<FixedJoint>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // target is found
            joint.connectedBody = playground.GetComponent<Rigidbody>();
        }
        else
        {
            // TODO place playground somewhere else
            // target is lost
            joint.connectedBody = null;
        }
    }
}