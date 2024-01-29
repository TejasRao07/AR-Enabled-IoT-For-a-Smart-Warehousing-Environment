

using UnityEngine;
using UnityEngine.Events;
using Vuforia;

[System.Serializable]
public class TrackEvents
{
	#region PUBLIC_EVENTS

	public UnityEvent onInitialized;
	public UnityEvent onAppear;
	public UnityEvent isAppearing;
	public UnityEvent onDisappear;

	#endregion PUBLIC_EVENTS
}


///     A custom handler that implements the ITrackableEventHandler interface.

public class CustomTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
	#region PUBLIC_EVENTS

	public TrackEvents trackEvents;

	#endregion PUBLIC_EVENTS

	#region PRIVATE_MEMBER_VARIABLES

	protected TrackableBehaviour mTrackableBehaviour;

	#endregion PRIVATE_MEMBER_VARIABLES

	#region UNTIY_MONOBEHAVIOUR_METHODS

	protected virtual void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);

		// onInitialized custom events
		if (trackEvents.onInitialized != null)
			trackEvents.onInitialized.Invoke();
	}

	protected virtual void Update()
	{
		// isAppearing custom events
		if (trackEvents.isAppearing != null)
			trackEvents.isAppearing.Invoke();
	}

	#endregion UNTIY_MONOBEHAVIOUR_METHODS

	#region PUBLIC_METHODS



	///     Implementation of the ITrackableEventHandler function called when the
	///     tracking state changes.

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
			OnTrackingFound();
		}
		else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
				 newStatus == TrackableBehaviour.Status.NOT_FOUND)
		{
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
			OnTrackingLost();
		}
		else
		{
			// For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
			// Vuforia is starting, but tracking has not been lost or found yet
			// Call OnTrackingLost() to hide the augmentations
			OnTrackingLost();
		}
	}

	#endregion PUBLIC_METHODS

	#region PRIVATE_METHODS

	protected virtual void OnTrackingFound()
	{
		var rendererComponents = GetComponentsInChildren<Renderer>(true);
		var colliderComponents = GetComponentsInChildren<Collider>(true);
		var canvasComponents = GetComponentsInChildren<Canvas>(true);

		// Enable rendering:
		foreach (var component in rendererComponents)
			component.enabled = true;

		// Enable colliders:
		foreach (var component in colliderComponents)
			component.enabled = true;

		// Enable canvas:
		foreach (var component in canvasComponents)
			component.enabled = true;

		// onAppear custom events
		if (trackEvents.onAppear != null)
			trackEvents.onAppear.Invoke();
	}

	protected virtual void OnTrackingLost()
	{
		var rendererComponents = GetComponentsInChildren<Renderer>(true);
		var colliderComponents = GetComponentsInChildren<Collider>(true);
		var canvasComponents = GetComponentsInChildren<Canvas>(true);

		// Disable rendering:
		foreach (var component in rendererComponents)
			component.enabled = false;

		// Disable colliders:
		foreach (var component in colliderComponents)
			component.enabled = false;

		// Disable canvas:
		foreach (var component in canvasComponents)
			component.enabled = false;

		// onDisappear custom events
		if (trackEvents.onDisappear != null)
			trackEvents.onDisappear.Invoke();
	}

	#endregion PRIVATE_METHODS
}