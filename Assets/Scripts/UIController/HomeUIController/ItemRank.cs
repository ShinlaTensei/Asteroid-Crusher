
using UnityEngine;
using UnityEngine.UI;

public class ItemRank : MonoBehaviour
{
    [SerializeField] private Image rankSprite;

    [SerializeField] private Text rankText;

    [SerializeField] private Text nameText;

    [SerializeField] private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Sprite sprite, string rank, string name, string score)
    {
        rankSprite.sprite = sprite;
        rankText.text = rank;
        nameText.text = name;
        scoreText.text = score;
    }
}
