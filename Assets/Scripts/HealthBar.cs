using UnityEngine;

/// <summary>
/// Displays a configurable health bar for any object with a Damageable as a parent
/// </summary>
public class HealthBar : MonoBehaviour
{
    public GameObject objWithHP;
    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    IObservableHP damageable;

    private void Awake()
    {
        damageable = objWithHP.GetComponent<IObservableHP>();
        if (damageable == null)
        {
            Destroy(this.gameObject);
        }
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        // get the damageable parent we're attached to
    }

    private void Start()
    {
        UpdateParams(damageable.CurrentHP, damageable.MaxHP);
        damageable.HpChange += UpdateParams;
        meshRenderer.enabled = true;
    }

    private void UpdateParams(uint currentHP, uint maxHP)
    {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", (float)currentHP / (float)maxHP);
        meshRenderer.SetPropertyBlock(matBlock);
    }

}