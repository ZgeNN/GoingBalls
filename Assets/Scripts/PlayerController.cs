using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	#region Self Variables

	#region Serialized Variables

	[SerializeField] private Text text;
	[SerializeField] public float speed;
	public Camera Playercamera;

	#endregion

	#region Private Variables
	private Rigidbody _rb;
	private int _count;
	private Vector2 _inputValues;
	private bool _isReadyToMove, _isReadyToPlay = true;
	private Vector3 _initialPostion, _checkpointPosition;

    #endregion
    #endregion
    private void Awake()
	{
		GetReferences();
	}

	private void GetReferences()
	{
		_rb = GetComponent<Rigidbody>();
	}
    private void Start()
    {
		SetInitialPosition();
    }

	private void SetInitialPosition()
    {
		_initialPostion = transform.localPosition;
    }
	public void ReEnableMovement()
    {
		_isReadyToPlay = true;
    }
    private Vector2 GetInputData()
	{
		return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}
	private void Move()
	{

		Vector3 movement = new Vector3(_inputValues.x, 0, _inputValues.y);
		movement = Playercamera.transform.TransformDirection(movement);
		movement.y = 0;
		movement.Normalize();
		_rb.AddForce(movement * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
	}

	void FixedUpdate()
	{
		if (!_isReadyToPlay) return;
		if(_isReadyToMove) Move();
	}

    private void Update()
    {
		if (!_isReadyToPlay) return;
		_inputValues = GetInputData();
		_isReadyToMove = Input.anyKey;
    }

	private void Stop()
    {
		//_rb.AddForce(Vector3.zero);
		//_rb.AddTorque(Vector3.zero);
		_rb.velocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;
	}

	private void MovePlayerToCheckpointPosition()
    {
		transform.localPosition = _checkpointPosition;
		ReEnableMovement();
    }
	
	private void UpdateCheckPoint(Vector3 transformLocalPosition)
    {
		_checkpointPosition = transformLocalPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeadZone"))
        {
			_isReadyToMove = false;
			_isReadyToPlay = false;
			Stop();
			MovePlayerToCheckpointPosition();
			return;
        }

        if (other.CompareTag("CheckPoint"))
        {
			UpdateCheckPoint(transform.localPosition);
			Debug.Log("Checkpoint alýndý");
		}
        if (other.CompareTag("Finish"))
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		
		if (!other.CompareTag("Collectable")) return;
		Destroy(other.gameObject);
		_count++;
		text.text = _count.ToString();
	}


    private void OnCollisionEnter(Collision collision)
	{
		
		
		
		if (collision.collider.CompareTag("Bridge"))
		{
			Rigidbody blockRB = collision.collider.GetComponent<Rigidbody>();
			blockRB.isKinematic = false;
			blockRB.useGravity = true;
		}
	}

}


