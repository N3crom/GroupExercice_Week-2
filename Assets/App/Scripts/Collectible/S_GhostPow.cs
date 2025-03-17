using System;
using UnityEngine;

public class S_GhostPow : MonoBehaviour
{
	
	[Header("Settings")]
	[SerializeField, TagName] private string tagPlayer;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(tagPlayer))
		{
			other.GetComponent<IPowerUp>()?.EnablePowerUp();
			Destroy(gameObject);
		}
	}
}