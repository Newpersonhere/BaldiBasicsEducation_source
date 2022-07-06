// BullyScript
using UnityEngine;

public class BullyScript : MonoBehaviour
{
	public Transform player;

	public GameControllerScript gc;

	public Renderer bullyRenderer;

	public AILocationSelectorScript wanderer;

	public float waitTime;

	public float guilt;

	public bool spoken;

	private AudioSource audioDevice;

	public AudioClip aud_Denied;

	private void Start()
	{
	}

	private void Update()
	{
		if (waitTime > 1f)
		{
			waitTime -= Time.deltaTime;
		}
		{
		}
		if (active)
		{
			activeTime += Time.deltaTime;
			if (activeTime >= 0f & (base.transform.position - player.position).magnitude >= 0f)
			{
				Reset();
			}
		}
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		Vector3 direction = player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + new Vector3(0f, 4f, 0f), direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & bullyRenderer.isVisible & (base.transform.position - player.position).magnitude <= 20f & active)
		{
			if (!spoken)
			{
				int num = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(aud_Taunts[num]);
				spoken = false;
			}
			guilt = 1f;
		}
	}

	private void Activate()
	{
		wanderer.GetNewTargetHallway();
		base.transform.position = wanderTarget.position + new Vector3(0f, 5f, 0f);
		while ((base.transform.position - player.position).magnitude < 20f)
		{
			wanderer.GetNewTargetHallway();
			base.transform.position = wanderTarget.position + new Vector3(0f, 5f, 0f);
		}
		active = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			if (gc.item[0] == 0 & gc.item[1] == 0 & gc.item[2] == 0)
			{
				audioDevice.PlayOneShot(aud_Denied);
			}
			else
			{
				int num = Mathf.RoundToInt(Random.Range(0f, 2f));
				while (gc.item[num] == 0)
				{
					num = Mathf.RoundToInt(Random.Range(0f, 2f));
				}
				gc.LoseItem(num);
				int num2 = Mathf.RoundToInt(Random.Range(0f, 1f));
				audioDevice.PlayOneShot(aud_Thanks[num2]);
				Reset();
			}
		}
		if (other.transform.name == "Principal of the Thing" & guilt > 0f)
		{
			Reset();
		}
	}

	private void Reset()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 20f, 0f);
		waitTime = Random.Range(60f, 120f);
		active = false;
		activeTime = 0f;
		spoken = false;
	}
}
