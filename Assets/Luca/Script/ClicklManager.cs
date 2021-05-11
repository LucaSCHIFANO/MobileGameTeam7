using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicklManager : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollier = Physics2D.OverlapPoint(touchPosition);
                var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();

                if (touchedCollier != null) {
                    if (touchedCollier.gameObject.tag == "Panel")
                    {
                        var touchedPanel = touchedCollier.gameObject.GetComponent<Panel>();
                        
                        if (player.state == PlayerMovement.States.SELECTED && touchedPanel.canBeClick)
                        {
                            player.StartCoroutine(player.movement(Grid.Instance.PathFinding(player.xPos, player.yPos, touchedPanel.x, touchedPanel.y)));
                        }
                        else if (!touchedPanel.canBeClick)
                        {
                            Grid.Instance.resetClicked();
                            player.state = PlayerMovement.States.IDLE;
                        }

                    }else if (touchedCollier.gameObject.tag == "Characters")
                    {
                        if (touchedCollier.gameObject.GetComponent<PlayerMovement>())
                        {
                            if (player.state == PlayerMovement.States.IDLE)
                            {
                                BlueRedGrid.Instance.movementsPossible(player.xPos, player.yPos);
                                BlueRedGrid.Instance.blueRedPath(player.maxMouvementPoint);
                                //BlueRedGrid.Instance.attackPossible();

                                player.state = PlayerMovement.States.SELECTED;
                            }
                        }
                    }
                }

            }
        }
    }
}
