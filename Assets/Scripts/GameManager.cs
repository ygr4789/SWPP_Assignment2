using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject coins;
    public GameObject gate;

    private CollectCoin[] coinScripts;

    private bool stage1Clear = false;

    void Start()
    {
        coinScripts = coins.GetComponentsInChildren<CollectCoin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stage1Clear)
        {
            bool collectedAllCoins = true;
            foreach (CollectCoin coin in coinScripts)
            {
                if (!coin.collected) collectedAllCoins = false;
            }
            if (collectedAllCoins)
            {
                stage1Clear = true;
                StartCoroutine(MoveOverSeconds(gate, new Vector3(0.0f, 5.0f, 0.0f), 3.0f));
            }
        }
    }

    IEnumerator MoveOverSeconds(GameObject obj, Vector3 offset, float seconds)
    {
        float elapsedTime = 0;
        Vector3 initialPos = obj.transform.position;
        while (elapsedTime < seconds)
        {
            obj.transform.position += offset * (Time.deltaTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        obj.transform.position = initialPos + offset;
    }
}
