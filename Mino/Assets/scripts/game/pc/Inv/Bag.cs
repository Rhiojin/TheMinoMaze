using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Bag : MonoBehaviour {

	//PC_Main PC;
	EventTrigger eventTrigger = null;
	bool open;
	Vector3 normSize; 
	Vector3 largeSize;
	Vector3 normPos;
	Vector3 largePos;

	Rigidbody rigBod;

	void Start () {
		//PC = GameObject.Find ("PC").GetComponent<PC_Main> ();
		rigBod = GetComponent<Rigidbody>();
		eventTrigger = GetComponent<EventTrigger>();
		AddEventTrigger(OnPointerClick, EventTriggerType.PointerUp);
		normSize = transform.localScale;
		normPos = transform.localPosition;
		largeSize = new Vector3 (0.5f, 0.01f, 0.4f);
		largePos = new Vector3 (0.2f, -0.6f, 0.2f);
	}

	// Use listener that uses the BaseEventData passed to the Trigger
	private void AddEventTrigger(UnityAction<BaseEventData> action, EventTriggerType triggerType)
	{
		// Create a nee TriggerEvent and add a listener
		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		trigger.AddListener((eventData) => action(eventData)); // capture and pass the event data to the listener

		// Create and initialise EventTrigger.Entry using the created TriggerEvent
		EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

		// Add the EventTrigger.Entry to delegates list on the EventTrigger
		eventTrigger.triggers.Add(entry);
	}

	void Update(){
		if (open) {
			transform.localScale = Vector3.Lerp (transform.localScale, largeSize, 4 * Time.deltaTime);
			transform.localPosition = Vector3.Lerp (transform.localPosition, largePos, 4 * Time.deltaTime);
		} else {
			transform.localScale = Vector3.Lerp (transform.localScale, normSize, 4 * Time.deltaTime);
			transform.localPosition = Vector3.Lerp (transform.localPosition, normPos, 4 * Time.deltaTime);
		}
	}

	public void dead(){
		transform.SetParent (null);
		open = false;
		rigBod.useGravity = true;
		//set all items in inventory to big and unparent
	}

	public void close(){
		open = false;
	}

	private void OnPointerClick(BaseEventData data){
		if (!open) {
			open = true;
		}
		return;
	}
}
