using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float sprintValueMultiplier = 1.75f;
    [SerializeField] private float defaultGravity = 3f;
    [SerializeField] private float gravityOnLadder = 2f;
    [SerializeField] private float itemsAboveHeadOffset = 0.5f;
    
    private Rigidbody2D _rb;
    
    private float _horizontalMove;
    private float _verticalMove;

    private bool _onLadder;
    private bool _jump;
    private float _speedModifier = 1f;

    private bool _isHandEmpty;
    private Transform _objectInHand;
    
    private bool _nearCannon;
    private Cannon _closeCannon;
    private bool _nearFoodSource;
    private FoodsManager _foodSource;
    private bool _nearFood;
    private Food _closeFood;
    private bool _foodPicked;
    private bool _nearCannonball;
    private Cannonball _closeCannonball;
    private bool _cannonballPicked;

    private float _dropForceX = 20f;
    private float _dropForceY = 20f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = defaultGravity;
        _isHandEmpty = true;
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * _speedModifier;
        _verticalMove = Input.GetAxisRaw("Ascend") * moveSpeed * Convert.ToInt32(_onLadder);

        // Picking food, only the player's not already having an item in her hands
        if (Input.GetButtonDown("Fire2") && _nearFood && _isHandEmpty)
        {
            PickHandyObject(_closeFood);
        }
        // Dropping food
        else if (Input.GetButtonDown("Fire2") && _foodPicked)
        {
            DropHandyObject();
        }
        // Taking a food from the stash, only the player's not already having an item in her hands
        else if (Input.GetButtonDown("Fire2") && _nearFoodSource && _isHandEmpty)
        {
            GrabFood();
        }

        if (Input.GetButtonDown("Fire2") && _nearCannonball && _isHandEmpty)
        {
            PickHandyObject(_closeCannonball);
            _closeCannonball.PickedUpByPlayer();
        }
        // Loading cannonball to the cannon
        else if (Input.GetButtonDown("Fire2") && _cannonballPicked && _nearCannon && !_closeCannon.Loaded)
        {
            _closeCannon.Load(_objectInHand.GetComponent<Cannonball>());
            _objectInHand = null;
            _isHandEmpty = true;
            _cannonballPicked = false;
        }
        // Dropping cannonball
        else if (Input.GetButtonDown("Fire2") && _cannonballPicked)
        {
            Cannonball cannonball = _objectInHand.GetComponent<Cannonball>();
            DropHandyObject();
            cannonball.DroppedByPlayer();
        }
        else if (Input.GetButtonUp("Fire1") && _nearCannon) // Player can only shoot the cannon when she is near it
        {
            _closeCannon.Shoot();
        }

        // Jump
        if (Input.GetButtonDown("Jump") && !_onLadder && !_nearCannon)
        {
            _jump = true;
        }
        
        // Checking for sprinting
        if (Input.GetButtonDown("Sprint"))
        {
            _speedModifier = sprintValueMultiplier;
        }
        // Stop sprinting
        else if (Input.GetButtonUp("Sprint")){
            _speedModifier = 1f;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontalMove, _verticalMove, _jump);
        _jump = false;
        
        // Move the object in hand as well
        if (!_isHandEmpty)
        {
            _objectInHand.position = transform.position + Vector3.up * itemsAboveHeadOffset;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _onLadder = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("LadderTop"));
            _rb.gravityScale = gravityOnLadder;
        }
        
        if (other.gameObject.CompareTag("Cannon"))
        {
            _nearCannon = true;
            _closeCannon = other.gameObject.GetComponent<Cannon>();
        }

        if (other.gameObject.CompareTag("Food"))
        {
            _nearFood = true;
            _closeFood = other.gameObject.GetComponent<Food>();
        }
        
        if (other.gameObject.CompareTag("Cannonball"))
        {
            _nearCannonball = true;
            _closeCannonball = other.gameObject.GetComponent<Cannonball>();
        }

        if (other.gameObject.CompareTag("FoodSource"))
        {
            _nearFoodSource = true;
            _foodSource = other.gameObject.GetComponent<FoodsManager>();
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            _nearFood = true;
            _closeFood = other.gameObject.GetComponent<Food>();
        }
        
        if (other.gameObject.CompareTag("Cannonball"))
        {
            _nearCannonball = true;
            _closeCannonball = other.gameObject.GetComponent<Cannonball>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            _onLadder = false;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("LadderTop"), _onLadder);
            _rb.gravityScale = defaultGravity;
        }
        
        if (other.gameObject.CompareTag("Cannon"))
        {
            _nearCannon = false;
            _closeCannon = null;
        }
        
        if (other.gameObject.CompareTag("Food"))
        {
            _nearFood = false;
            _closeFood = null;
        }
        
        if (other.gameObject.CompareTag("Cannonball"))
        {
            _nearCannonball = false;
            _closeCannonball = null;
        }
        
        if (other.gameObject.CompareTag("FoodSource"))
        {
            _nearFoodSource = false;
            _foodSource = null;
        }
    }

    private void PickHandyObject(HandyObject obj)
    {
        obj.PickUp();
        SpriteRenderer objSR = obj.GetComponent<SpriteRenderer>();
        objSR.sortingLayerName = "Character";
        objSR.sortingOrder = 1;
        if (obj.GetType() == typeof(Food))
        {
            _foodPicked = true;
        }
        else if (obj.GetType() == typeof(Cannonball))
        {
            _cannonballPicked = true;
        }
        _objectInHand = obj.transform;
        _isHandEmpty = false;
    }

    private void DropHandyObject()
    {
        HandyObject ho = _objectInHand.GetComponent<HandyObject>();
        // add some force when dropping the object corresponding to the player's direction
        bool facingRight = GetComponent<CharacterController2D>().FacingRight;
        Vector3 force = facingRight ? new Vector3(_dropForceX, _dropForceY, 0) : new Vector3(-_dropForceX, _dropForceY, 0);
        ho.Drop(force);
        
        // resetting the sorting layer of the food
        SpriteRenderer objSR = ho.GetComponent<SpriteRenderer>();
        if (ho.GetType() == typeof(Food))
        {
            objSR.sortingLayerName = "Castle";
            objSR.sortingOrder = 12;
            _foodPicked = false;
        }
        else if (ho.GetType() == typeof(Cannonball))
        {
            objSR.sortingLayerName = "Castle";
            objSR.sortingOrder = 17;
            _cannonballPicked = false;
        }
        
        _objectInHand = null;
        _isHandEmpty = true;
    }


    private void GrabFood()
    {
        _closeFood = _foodSource.SpawnAFood();
        if (_closeFood != null)
        {
            PickHandyObject(_closeFood);
        }
        else
        {
            Debug.Log("You can only have so many foods at a time!");
        }
    }
}
