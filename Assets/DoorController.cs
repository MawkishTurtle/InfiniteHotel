using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public enum RoomState { Free, Occupied}
    public enum DoorState { Open, Closed}

    public DoorState doorState = DoorState.Closed;
    public RoomState roomState;

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnEnable()
    {
        int r = Random.Range(0, 3);
        if(r == 2)
        {
            ChangeRoomState(RoomState.Occupied);
        }
        else
        {
            ChangeRoomState(RoomState.Free);
        }
    }

    public void ChangeDoorState(DoorState newDoorState)
    {
        switch (newDoorState)
        {
            case DoorState.Closed:
                doorState = DoorState.Closed;
                break;
            case DoorState.Open:
                doorState = DoorState.Open;
                break;
        }
        ChangeState();
    }

    public void ChangeRoomState(RoomState newRoomState)
    {
        switch (newRoomState)
        {
            case RoomState.Free:
                roomState = RoomState.Free;
                break;
            case RoomState.Occupied:
                roomState = RoomState.Occupied;
                break;
        }
        ChangeState();
    }

    private void ChangeState()
    {
        if(roomState == RoomState.Free && doorState == DoorState.Closed)
        {
            
        }
        else if(roomState == RoomState.Occupied && doorState == DoorState.Closed)
        {
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
            foreach(SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.gameObject.SetActive(true);
            }
        }
        else if(roomState == RoomState.Free && doorState == DoorState.Open)
        {
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = Color.green;
            }
            gameController.pointsCounter.points += 50;
        }
        else if (roomState == RoomState.Occupied && doorState == DoorState.Open)
        {
            StartCoroutine(gameController.SpeedUpForTime(gameController.cameraSpeed, 1.0f));
            gameController.pointsCounter.points -= 100;
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = Color.red;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(collision.name == "MainCharacter")
            {
                if (doorState == DoorController.DoorState.Closed)
                {
                    ChangeDoorState(DoorController.DoorState.Open);
                    GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
