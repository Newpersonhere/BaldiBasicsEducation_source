// CraftersScript
using UnityEngine;
using UnityEngine.AI;

public class CraftersScript : MonoBehaviour
{
	public bool db;

	private float forceShowTime;

	public Transform player;

	public Transform playerCamera;

	public GameObject sprite;

	public GameControllerScript gc;

	private NavMeshAgent agent;

	public Renderer craftersRenderer;

	public SpriteRenderer spriteImage;

	private AudioSource audioDevice;

	private void Start()
	{
		agent = base.GetComponent<NavMeshAgent>();
		audioDevice = base.GetComponent<AudioSource>();
		sprite.SetActive(false);
	}

	private void Update()
	{
		if (forceShowTime > 0f)
		{
			forceShowTime -= Time.deltaTime;
		}
		{
			{
				angry = false;
			}
		}
		else if (anger > 0f)
		{
			anger -= Time.deltaTime;
		}
		if (!angry)
		{
			if (((base.transform.position - agent.destination).magnitude <= 20f & (base.transform.position - player.position).magnitude >= 60f) || forceShowTime > 0f)
			{
				sprite.SetActive(false);
			}
			else
			{
				sprite.SetActive(true);
			}
		}
		else
		{
			agent.speed += 60f * Time.deltaTime;
			if (!audioDevice.isPlaying)
			{
			}
		}
	}

	private void FixedUpdate()
	{
		if (gc.notebooks >= 8)
		{
			Vector3 direction = player.position - base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player" & craftersRenderer.isVisible & sprite.activeSelf)
			{
				gettingAngry = false;
			}
			else
			{
				gettingAngry = false;
			}
		}
	}

	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!angry)
		{
			agent.SetDestination(location);
			if (flee)
			{
				forceShowTime = 0f;
			}
		}
	}

	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" & angry)
		{
			Transform transform = player;
			Vector3 position = player.position;
			transform.position = new Vector3(5f, position.y, 80f);
			NavMeshAgent navMeshAgent = baldiAgent;
			Vector3 position2 = baldi.position;
			navMeshAgent.Warp(new Vector3(5f, position2.y, 125f));
			Transform transform2 = player;
			Vector3 position3 = baldi.position;
			float x = position3.x;
			Vector3 position4 = player.position;
			float y = position4.y;
			Vector3 position5 = baldi.position;
			transform2.LookAt(new Vector3(x, y, position5.z));
			gc.DespawnCrafters();
		}
	}
}
