using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class moveBox : MonoBehaviour {

	PC_Main PC;
	EventTrigger eventTrigger = null;

	// Use this for initialization
	void Start () {
		PC = GameObject.Find ("PC").GetComponent<PC_Main> ();
		eventTrigger = GetComponent<EventTrigger>();
		AddEventTrigger(OnPointerClick, EventTriggerType.PointerClick);
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
	
	private void OnPointerClick(BaseEventData data){
			PC.Move (transform.position);
			return;
	}
}
