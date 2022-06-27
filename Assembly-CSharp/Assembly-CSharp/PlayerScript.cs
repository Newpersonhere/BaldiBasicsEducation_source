// PlayerScript
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public GameControllerScript gc;

	public BaldiScript baldi;

	public DoorScript door;

	public PlaytimeScript playtime;

	public bool ;

	public bool ;

	public bool ;

	public bool ;

	public float sweepingFailsave;

	private Quaternion playerRotation;

	public Vector3 Position;

	public float mouseSensitivity;

	public float ;

	public float ;

	public float ;

	public float ;

	public float ;

	public float guilt;

	public float initGuilt;

	private float moveX;

	private float moveZ;

	private float playerSpeed;

	public float stamina;

	public Rigidbody rb;

	public NavMeshAgent ;

	public NavMeshAgent ;

	public Transform ;

	public Slider ;

	public float db;

	public string guiltType;

	public GameObject ;

	private void Start()
	{
		rb = base.GetComponent<Rigidbody>();
		stamina = maxStamina;
		playerRotation = base.transform.rotation;
		mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	private void Update()
	{
		MouseMove();
		StaminaCheck();
		GuiltCheck();
		if (rb.velocity.magnitude > 5f)
		{
			gc.UnLockMouse();
		}
		if  (base.transform.position - Position).magnitude >= 1f)
		{
			DeactivateJumpRope();
		}
		if (sweepingFailsave > 0f)
		{
			sweepingFailsave -= Time.deltaTime;
		}
		else
		{
			sweeping = false;
			hugging = false;
		}
	}

	private void FixedUpdate()
	{
		PlayerMove();
	}

	private void MouseMove()
    {
        //ref Quaternion val = ref playerRotation;
        playerRotation.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0f);
	}

	private void PlayerMove()
	{
		base.transform.rotation = playerRotation;
		Vector3 a = new Vector3(0f, 0f, 0f);
		Vector3 b = new Vector3(0f, 0f, 0f);
		db = Input.GetAxisRaw("Forward");
		if (stamina > 0f)
		{
			if (Input.GetAxisRaw("Run") > 0f)
			{
				playerSpeed = runSpeed;
				if (rb.velocity.magnitude > 0.1f)
				{
					ResetGuilt("running", 0.1f);
				}
			}
			else
			{
				playerSpeed = walkSpeed;
			}
		}
		else
		{
			playerSpeed = walkSpeed;
		}
		if (Input.GetAxis("Forward") > 0f)
		{
			a = base.transform.forward;
		}
		else if (Input.GetAxis("Forward") < 0f)
		{
			a = base.transform.forward * -1f;
		}
		if (Input.GetAxis("Strafe") > 0f)
		{
			b = base.transform.right;
		}
		else if (Input.GetAxis("Strafe") < 0f)
		{
			b = base.transform.right * -1f;
		}
		if 
		{
			rb.velocity = (a + b).normalized * playerSpeed;
		}
		else if 
		{
			rb.velocity = .velocity + (a + b).normalized * playerSpeed * 0.3f;
		}
		else if 
		{
			Rigidbody rigidbody = rb;
			Vector3 a2 = .velocity * 1.2f;
			Vector3 position = firstPriz;
			Vector3 forward = firstPrizeTransform.forward;
			float x = (float)Mathf.RoundToInt(forward.x);
			Vector3 forward2 = firstPrizeTransform.forward;
			rigidbody.velocity = a2 + (position + new Vector3(x, 0f, (float)Mathf.RoundToInt(forward2.z)) * 3f - base.transform.position);
		}
		else
		{
			rb.velocity = new Vector3(0f, 0f, 0f);
		}
	}

	private void StaminaCheck()
	{
		if (rb.velocity.magnitude > 0.1f)
		{
			if (Input.GetAxisRaw("Run") > 0f & stamina > 0f)
			{
				stamina -= staminaRate * Time.deltaTime;
			}
			if (stamina < 0f & stamina > -5f)
			{
				stamina = -5f;
			}
		}
		else if (stamina < maxStamina)
		{
			stamina += staminaRate * Time.deltaTime;
		}
		staminaBar.value = stamina / maxStamina * 100f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.name == "Baldi" & !gc.debugMode)
		{
			gameOver = false;
		}
		else if (other.transform.name ==  
		{
			();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == )
		{
			sweeping = false;
			sweepingFailsave = 1f;
		}
		else if (other.transform.name == "1st Prize" & firstPrize.velocity.magnitude > 5f)
		{
			hugging = false;
			sweepingFailsave = 1f;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == )
		{
			ResetGuilt("escape", door.lockTime);
		}
		else if (other.transform.name == )
		{
			sweeping = false;
		}
		else if (other.transform.name == )
		{
			hugging = false;
		}
	}

	public void ResetGuilt(string type, float amount)
	{
		if (amount >= guilt)
		{
			guilt = amount;
			guiltType = type;
		}
	}

	private void GuiltCheck()
	{
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
	}

	public void UnActivateJumpRope()
	{
		jumpRopeScreen.SetActive(false);
		jumpRope = false;
		Position = base.transform.position;
	}

	public void DeactivateJumpRope()
	{
		jumpRopeScreen.SetActive(false);
		jumpRope = false;
	}
}
