using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
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

                if (touchedCollier != null) {
                    var player = CharacterManager.Instance.currentPlayer.GetComponent<PlayerMovement>();
                    player.StartCoroutine(player.movement(Grid.Instance.PathFinding(player.xPos, player.yPos, touchedCollier.gameObject.GetComponent<Panel>().x, touchedCollier.gameObject.GetComponent<Panel>().y)));
                }

            }
        }
    }
}
