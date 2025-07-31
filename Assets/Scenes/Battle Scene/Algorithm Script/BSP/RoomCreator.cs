using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{

    public int gridWidth;
    public int gridLength;
    public int roomWidthMin;
    public int roomLengthMin;
    public int maxIteration;


    void Start()
    {
        createRoom();
    }

    void createRoom()
    {
        RoomGenerator generator = new RoomGenerator(gridWidth, gridLength);
        var listOfRooms = generator.CalculateRooms(maxIteration, roomWidthMin, roomLengthMin);
    }

    void Update()
    {
        
    }
}
