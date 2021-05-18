using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    private Vector3 touchPosition;

    public Transform leftLimit;
    public Transform rightLimit;
    public Transform bottomLimit;
    public Transform upLimit;

    public void functionStart()
    {
        Vector3 transWoutZ = new Vector3(transform.position.x, transform.position.y, 0);
        var grid = Grid.Instance.gridArray;
        int height = Grid.Instance.height / 4;
        int width = Grid.Instance.width / 4;

        leftLimit.transform.position = grid[width, height * 3].transform.position;
        rightLimit.transform.position = grid[width * 3, height].transform.position;
        upLimit.transform.position = grid[width, height].transform.position;
        bottomLimit.transform.position = grid[width * 3, height * 3].transform.position;
    }


    void Update()
    {
        moveCam();
    }

    public void moveCam()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        if(Camera.main.transform.position.x < leftLimit.position.x)
        {
            transform.position = new Vector3(leftLimit.position.x, transform.position.y, -100);
        }

        if (Camera.main.transform.position.x > rightLimit.position.x)
        {
            transform.position = new Vector3(rightLimit.position.x, transform.position.y, -100);
        }

        if (Camera.main.transform.position.y < bottomLimit.position.y)
        {
            transform.position = new Vector3(transform.position.x, bottomLimit.position.y, -100);
        }

        if (Camera.main.transform.position.y > upLimit.position.y)
        {
            transform.position = new Vector3(transform.position.x, upLimit.position.y, -100);
        }
    }
}
