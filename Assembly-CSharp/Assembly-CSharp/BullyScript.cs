// BullyScript
using UnityEngine;

public class BullyScript : MonoBehaviour
{

	public GameControllerScript gc;

	public Renderer bullyRenderer;

	public AILocationSelectorScript wanderer;

	public float guilt;

	public bool spoken;

	private AudioSource audioDevice;

	private void Start()
	{
	}

	private void Update()
	{
		if (waitTime > 0f)
		{
			waitTime -= Time.deltaTime;
		}
		else if (!active)
		{
			Activate();
		}
		if (active)
		{
			activeTime += Time.deltaTime;
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
			guilt = 0f;
		}
	}

	private void Activate()
	{
		while ((base.transform.position - player.position).magnitude < 20f)
		{
			wanderer.GetNewTargetHallway();
			base.transform.position = wanderTarget.position + new Vector3(0f, 5f, 0f);
		}
		active = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		{
			{
			}
			else
			{
				{
				}
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
