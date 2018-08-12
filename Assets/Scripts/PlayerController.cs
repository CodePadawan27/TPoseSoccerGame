using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Player movement
    [Header("Player movement")]
    public float speed = 2f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    public LayerMask ground;
    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private Vector3 _playerPosition;
    private Quaternion _playerRotation;

    //Player sounds
    [Header("Player sounds")]
    public AudioSource playerAudio;
    public AudioClip sfxFall;
    public AudioClip sfxJump;

    //Player characters
    public List<Transform> playerCharacters = new List<Transform>();

    //Player health
    [Header("Player health")]
    public bool heart1 = true;
    public bool heart2 = true;
    public bool heart3 = true;

    //Player explosion prefab
    public GameObject playerExplosion;

    //Player statistics
    [Header("Player statistics")]
    public int hitsToTheBall;
    public int hitsToTheBear;
    public int falls;

    //Player runUI bar
    public Image runBar;
    private float runMaxPower = 100f;
    private float runPower;
    private bool isRunning = false;
    private float speedMultiplier;

    void Start()
    {
        runPower = runMaxPower;

        playerAudio = GetComponent<AudioSource>();
        _playerPosition = this.transform.position;
        _playerRotation = this.transform.rotation;
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
        PopulateCharactersList();
    }

    //Populates List which contains parent gameobjects children
    void PopulateCharactersList()
    {
        foreach (Transform character in gameObject.transform)
        {
            playerCharacters.Add(character);
        }
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("HorizontalP1");
        _inputs.z = Input.GetAxis("VerticalP1");
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetButtonDown("JumpP1") && _isGrounded)
        {
            playerAudio.PlayOneShot(sfxJump);
            _body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (transform.position.y < -20)
        {
            falls++;
            FallRecovery();
        }

        UpdateRunBar();
    }

    void FixedUpdate()
    {
        float runSpeed = speed * (isRunning ? speedMultiplier : 1);
        _body.MovePosition(_body.position + _inputs * runSpeed * Time.fixedDeltaTime);
    }

    //Updates the running bar in UI when "RunP1" button is pressed. Changes also the players running speed.
    void UpdateRunBar()
    {
        if (Input.GetButton("RunP1") && _isGrounded && runBar.fillAmount >= 0 && runPower >= 0)
        {
            isRunning = true;
            speedMultiplier = 3f;
            runPower -= 2f;
            runBar.fillAmount = runPower / runMaxPower;
        }

        if (!Input.GetButton("RunP1") && (runBar.fillAmount == 0 || runBar.fillAmount < 1))
        {
            isRunning = false;
            runPower += 2f;
            runBar.fillAmount = runPower / runMaxPower;
        }

        if (Input.GetButton("RunP1") && _isGrounded && runBar.fillAmount == 0)
        {
            isRunning = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name.Equals("man_02_default") || other.gameObject.name.Equals("man_02_bdozer")) && other.transform.localScale.y > this.transform.localScale.y)
        {
            print("CRUSH!");
            GameObject intantiatedExplosion = Instantiate(playerExplosion, transform.position, transform.rotation);
            Destroy(intantiatedExplosion, playerExplosion.GetComponent<ParticleSystem>().main.duration);
            FallRecovery();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "TheBall")
        {
            hitsToTheBall++;
        }
        else if (collision.gameObject.name == "Supabear(Clone)")
        {
            hitsToTheBear++;
        }
    }

    //If player falls from the field below Y -20, function d
    void FallRecovery()
    {
        HeartDecrease();
        playerAudio.PlayOneShot(sfxFall);
        _playerPosition[1] = 6;
        transform.Rotate(90, _playerRotation.y, _playerRotation.z);
        transform.position = _playerPosition;
        StartCoroutine(BlinkPlayer());
    }

    //Blinks player
    IEnumerator BlinkPlayer()
    {
        var flashable = playerCharacters.Find(x => x.gameObject.activeInHierarchy);
        var flashableRenderer = flashable.GetComponent<Renderer>();
        var whenWeAreDone = Time.time + 5;
        while (Time.time <= whenWeAreDone)
        {
            flashableRenderer.enabled = false;
            print(flashableRenderer.enabled.ToString());
            yield return new WaitForSecondsRealtime(0.3f);
            flashableRenderer.enabled = true;
            yield return new WaitForSecondsRealtime(0.3f);
            print(flashableRenderer.enabled.ToString());
        }
        flashableRenderer.enabled = true;
    }


    //If player loses health, function decreases one heart from UI
    void HeartDecrease()
    {
        if (heart1 && heart2 && heart3)
        {
            GameObject.Find("P1heart3").GetComponent<Text>().enabled = false;
            heart3 = false;
        }
        else if (heart1 && heart2 && !heart3)
        {
            GameObject.Find("P1heart2").GetComponent<Text>().enabled = false;
            heart2 = false;
        }
        else if (heart1 && !heart2 && !heart3)
        {
            GameObject.Find("P1heart1").GetComponent<Text>().enabled = false;
            heart1 = false;
        }
    }

    //If player picks up heart from the field, function increases hearts in UI by 1
    public void HealthIncrease()
    {
        if (heart1 && heart2 && !heart3)
        {
            GameObject.Find("P1heart3").GetComponent<Text>().enabled = true;
            heart3 = true;
        }

        else if (heart1 && !heart2 && !heart3)
        {
            GameObject.Find("P1heart2").GetComponent<Text>().enabled = true;
            heart2 = true;
        }
    }
}

