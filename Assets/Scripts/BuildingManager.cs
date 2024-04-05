using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cursorSprite;

    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;

    [SerializeField] private TowerInfo selectedTower;
    [SerializeField] private LayerMask cantPlaceLayer;

    public List<GameObject> towers;

    private bool isTowerSelected;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isTowerSelected == true)
        {
            Vector2 screenPos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            Collider2D colliders = Physics2D.OverlapCircle(worldPos, 1f, cantPlaceLayer);
            
            if (colliders == null)
            {
                cursorSprite.color = validColor;
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTower();
                }
            }
            else
            {
                Debug.Log(colliders.name);
                cursorSprite.color = invalidColor;
            }
        }
    }

    public void SelectTower(TowerInfo tower)
    {
        selectedTower = tower;
        if (OrderManager.instance.ownedTowers[selectedTower] > 0)
        {
            Cursor.visible = false;

            cursorSprite.gameObject.SetActive(true);
            cursorSprite.sprite = tower.towerSprite;
            isTowerSelected = true;
        }
    }

    void PlaceTower()
    {
        isTowerSelected = false;
        Cursor.visible = true;
        OrderManager.instance.ownedTowers[selectedTower] -= 1;

        GameObject newTower = Instantiate(selectedTower.towerPrefab, cursorSprite.gameObject.transform.position, Quaternion.identity);
        towers.Add(newTower);

        cursorSprite.gameObject.SetActive(false);


    }
}
