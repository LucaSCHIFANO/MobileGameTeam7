using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicklManager : MonoBehaviour
{
    void Update() // check les differentes possibilité de click
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
                        Debug.Log("panel touched");
                        var touchedPanel = touchedCollier.gameObject.GetComponent<Panel>();
                        
                        if (player.state == PlayerMovement.States.SELECTED && touchedPanel.canBeClick)
                        {
                            player.StartCoroutine(player.movement(Grid.Instance.PathFinding(player.xPos, player.yPos, touchedPanel.x, touchedPanel.y)));
                        }
                        else if (!touchedPanel.canBeClick)
                        {
                            Grid.Instance.resetClicked();
                            player.state = PlayerMovement.States.IDLE;
                            UiActionManager.Instance.showButton();
                        }



                    }else if (touchedCollier.gameObject.tag == "ClickableChar")
                    {

                        if (touchedCollier.gameObject.GetComponentInParent<PlayerMovement>())
                        {
                            if (player.state == PlayerMovement.States.IDLE && player.mouvementPoint > 0)
                            {
                                BlueRedGrid.Instance.movementsPossible(player.xPos, player.yPos);
                                BlueRedGrid.Instance.blueRedPath(player.mouvementPoint);
                                //BlueRedGrid.Instance.attackPossible();

                                UiActionManager.Instance.hideButton();

                                player.state = PlayerMovement.States.SELECTED;

                            }else if (player.state == PlayerMovement.States.SELECTED)
                            {
                                Grid.Instance.resetClicked();
                                player.state = PlayerMovement.States.IDLE;
                                UiActionManager.Instance.showButton();
                            }
                        }
                    }
                }

            }
        }
    }
}
