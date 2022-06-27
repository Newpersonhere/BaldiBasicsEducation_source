// BaldiScript
using UnityEngine;
using UnityEngine.AI;

public class BaldiScript : MonoBehaviour
{
	public bool db;

	public float baseTime;

	public float speed;

	public float timeToMove;

	public float baldiHappy;

	public float baldiTempHappy;

	public float baldiWait;

	public float baldiSpeedScale;

	private float moveFrames;

	private float currentPriority;

	public bool antiHearing;

	public float antiHearingTime;

	public float happyRate;

	public float happyRateRate;

	public float happyFrequency;

	public float timeToHappy;

	public bool endless;

	public Transform player;

	public Transform wanderTarget;

	public AILocationSelectorScript wanderer;

	private AudioSource baldiAudio;

	public AudioClip slap;

	public AudioClip[] speech = new AudioClip[3];

	public Animator baldiAnimator;

	public float coolDown;

	private Vector3 previous;

	private NavMeshAgent agent;

	private void Start()
	{
		baldiAudio = base.GetComponent<AudioSource>();
		agent = base.GetComponent<NavMeshAgent>();
		timeToMove = baseTime;
		Wander();
	}

	private void Update()
	{
		if (timeToMove > 0f)
		{
			timeToMove -= 1f * Time.deltaTime;
		}
		else
		{
			Move();
		}
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
		if (baldiTempHappy > 0f)
		{
			baldiTempHappy -= 0.02f * Time.deltaTime;
		}
		else
		{
			baldiTempHappy = 0f;
		}
		if (antiHearingTime > 0f)
		{
			antiHearingTime -= Time.deltaTime;
		}
		else
		{
			antiHearing = false;
		}
		if (endless)
		{
			if (timeToHappy > 0f)
			{
				timeToHappy -= 1f * Time.deltaTime;
			}
			else
			{
				timeToHappy = happyFrequency;
				Gethappy(happyRate);
				happyRate += happyRateRate;
			}
		}
	}

	private void FixedUpdate()
	{
		if (moveFrames > 0f)
		{
			moveFrames -= 1f;
			agent.speed = speed;
		}
		else
		{
			agent.speed = 0f;
		}
		Vector3 direction = player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player")
		{
			db = true;
			Target();
		}
		else
		{
			db = false;
		}
	}

	private void Wander()
	{
		wanderer.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		coolDown = 1f;
		currentPriority = 0f;
	}

	public void Target()
	{
		agent.SetDestination(player.position);
		coolDown = 1f;
		currentPriority = 0f;
	}

	private void Move()
	{
		if (base.transform.position == previous & coolDown < 0f)
		{
			Wander();
		}
		moveFrames = 10f;
		timeToMove = baldiWait - baldiTempHappy;
		previous = base.transform.position;
		baldiAudio.PlayOneShot(slap);
		baldiAnimator.SetTrigger("slap");
	}

	public void GetHappy(float value)
	{
		baldiHappy += value;
		if (baldiHappy < 0.5f)
		{
			baldiHappy = 0.5f;
		}
		baldiWait = -3f * baldiHappy / (baldiHappy + 2f / baldiSpeedScale) + 3f;
	}

	public void GetTempHappy(float value)
	{
		baldiTempHappy += value;
	}

	public void Hear(Vector3 soundLocation, float priority)
	{
		if (!antiHearing && priority >= currentPriority)
		{
			agent.SetDestination(soundLocation);
			currentPriority = priority;
		}
	}

	public void ActivateAntiHearing(float t)
	{
		Wander();
		antiHearing = true;
		antiHearingTime = t;
	}
}
