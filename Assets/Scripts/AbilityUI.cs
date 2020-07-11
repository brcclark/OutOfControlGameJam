using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour {
	public Color AbilityAvailableColor;
	public Color AbilityCoolDownColor;

	public Text DashAbility;
	public Text BarkAbility;
	// Start is called before the first frame update
	void Start() {
		FindObjectOfType<PlayerController>().OnDashUsed += SetDashUnavailable;
		FindObjectOfType<PlayerController>().OnDashReady += SetDashAvailable;
	}

	// Update is called once per frame
	void Update() {

	}

	public void SetDashAvailable() {
		DashAbility.color = AbilityAvailableColor;
	}
	public void SetDashUnavailable() {
		DashAbility.color = AbilityCoolDownColor;
	}

	public void SetBarkAvailable() {
		BarkAbility.color = AbilityAvailableColor;
	}
	public void SetBarkUnavailable() {
		BarkAbility.color = AbilityCoolDownColor;
	}
}
