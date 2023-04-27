using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	
	[Tooltip("idPlayer: 0-gato vermelho; 1-cachorro azul; 2-????; 3-???")]
	[HideInInspector]
	public int idPlayer;
	[HideInInspector]
	public bool collisionTarget = false;

	// Create public variables for player speed, and for the Text UI game objects
	public float speed = 3f;
    private float movementX, movementY;
	public float rotationAngle = 10f; // Ângulo de inclinação do cubo
	private Rigidbody rb;

	public bool isFalling = false; // Indica se o jogador está caindo
    public float fallTime = 0f; // Contador de tempo de queda
	private bool isTilt = false;

	void Start ()
	{
		rb = this.GetComponent<Rigidbody>();
		rb.freezeRotation = true;  //para o player não ficar rolando
		rb.useGravity = false;
		transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z);
		StartCoroutine(IsStarting());
	}
	
	void FixedUpdate ()
	{
		//movimentação
		Vector3 movement = new Vector3 (movementX, 0f, movementY).normalized; //InputSystem
        rb.MovePosition(transform.position + movement * Time.deltaTime * speed);
        //inclinação
		if(!isTilt){
			TiltRotation(movementX, -movementY);
		}

		IsFalling();
	}
	
	public void OnMove(Vector2 moveInput){
		movementX = moveInput.x;
		movementY = moveInput.y;
    }

	/* PLAYER INPUT -> SEND MESSAGES	
	public void OnMove(InputValue value)
    {
		Debug.Log(playerInput.currentActionMap.name);
		Debug.Log($"Input value: {value.Get<Vector2>()}");
        Vector2 v = value.Get<Vector2>();
		movementX = v.x;
		movementY = v.y;
    }
	*/

	/* PLAYER INPUT -> INVOKE UNITY EVENTS
	public void OnMove(InputAction.CallbackContext context)
    {
		Vector2 moveInput = context.ReadValue<Vector2>();
		Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y).normalized; //InputSystem
        rb.MovePosition(transform.position + movement * Time.deltaTime * speed);

        Quaternion tiltRotation = Quaternion.Euler(-moveInput.y * rotationAngle, 0f, moveInput.x * rotationAngle);
        rb.MoveRotation(tiltRotation);
	}*/
	
	public void OnTilt(Vector2 value){
		isTilt = value == Vector2.zero ? false : true;
		TiltRotation(-value.x, value.y);
	}

	private void TiltRotation(float x, float y){
		Quaternion tiltRotation = Quaternion.Euler(y * rotationAngle, 0f, x * rotationAngle);
        rb.MoveRotation(tiltRotation);
	}

	public void IsFalling() {
        // Verifica se o jogador está caindo
        if (transform.position.y < 0f && !isFalling) {
            isFalling = true;
            fallTime = 0f;
        }
        if (isFalling) {
			if(!PlayerManager.instance.loseLevel){
				PlayerManager.instance.LoseLevel();
			}
            transform.Rotate(new Vector3(1,1,1), 150f * Time.deltaTime); // Rotaciona o jogador
            fallTime += Time.deltaTime;
            if (fallTime > 1f){ Destroy(gameObject); }// Se passaram mais de x segundos, destrói o jogador
        }
    }
	//OnTriggerEnter //OnTriggerStay //OnTriggerExit //OnCollisionEnter //OnCollisionExit
	void OnTriggerStay(Collider collision) 
	{
		float distance = Vector3.Distance(transform.position, collision.gameObject.transform.position);

		if (idPlayer == 0 && collision.gameObject.CompareTag ("TargetRed") && distance < 0.55f)
		{
			//Debug.Log("GATO encostou no VERMELHO ---- "+collision.gameObject.name);
			collisionTarget = true;
		}
		if (idPlayer == 1 && collision.gameObject.CompareTag ("TargetBlue") && distance < 0.55f)
		{
			//Debug.Log("CAHORRO encostou no AZUL ---- "+collision.gameObject.name);
			collisionTarget = true;
		}

		PlayerManager.instance.WinLevel();
	}

	void OnTriggerExit(Collider collision){
		float distance = Vector3.Distance(transform.position, collision.gameObject.transform.position);

		if (idPlayer == 0 && collision.gameObject.CompareTag ("TargetRed"))
		{
			//Debug.Log("GATO desencostou do VERMELHO ---- "+collision.gameObject.name);
			collisionTarget = false;
		}
		if (idPlayer == 1 && collision.gameObject.CompareTag ("TargetBlue"))
		{
			//Debug.Log("CAHORRO desencostou do AZUL ---- "+collision.gameObject.name);
			collisionTarget = false;
		}

		PlayerManager.instance.winLevel = false;
	}

	IEnumerator IsStarting()
    {
        yield return new WaitForSeconds(2);
		rb.useGravity = true;
    }
}