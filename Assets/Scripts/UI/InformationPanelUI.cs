using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanelUI : MonoBehaviour
{

    protected BuildingSO buildingInfo;
    private List<GameObject> producibles = new List<GameObject>();
    public TextMeshProUGUI buildSizeText;
    public TextMeshProUGUI buildNameText;
    public GameObject produciblesParent;
    public static InformationPanelUI instance;
    public Image buildImage;
    private Building selectedBuilding;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetBuildInfo(BuildingSO buildingSO,Building building)
    {
        buildingInfo = buildingSO;
        selectedBuilding = building;

        for(int i = 0; i < produciblesParent.transform.childCount; i++)
        {
            Destroy(produciblesParent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < buildingSO.Producibles.Count; i++)
        {
            
          
                producibles.Add(buildingSO.Producibles[i]);
                Button produciblesButton = Instantiate(producibles[i], produciblesParent.transform).GetComponent<Button>();
                int x = i;
                produciblesButton.onClick.AddListener(()=>OnButtonClick(x));
           
        }
        buildSizeText.text = ("Build Size : " + buildingInfo.BuildSize);
        buildNameText.text = buildingInfo.name;
        buildImage.sprite = buildingSO.BuildSprite;

    }

    private void OnButtonClick(int produciblesButtonId)
    {
        if (selectedBuilding.TryGetComponent(out IProducible p))
        {
            p.Produce(produciblesButtonId);
        }
        
    }



}
