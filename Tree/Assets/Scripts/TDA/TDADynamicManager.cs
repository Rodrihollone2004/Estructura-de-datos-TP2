using TMPro;
using UnityEngine;

public class TDADynamicManager : MonoBehaviour
{
    [Header("Dynamic Sets Numbers")]
    [SerializeField] private int[] firstArray = { 2, 1, 8, 0, 5, 9, 4, 3 };
    [SerializeField] private int[] secondArray = { 2, 1, 8, 0, 5, 9, 4, 3 };

    [Header("Activate functions")]
    [SerializeField] private bool isUnion;
    [SerializeField] private bool isIntersection;
    [SerializeField] private bool isDifference;

    [Header("Texts")]
    [SerializeField] private TMP_Text firstDynamicComplete;
    [SerializeField] private TMP_Text secondDynamicComplete;
    [SerializeField] private TMP_Text unionText;
    [SerializeField] private TMP_Text intersectionText;
    [SerializeField] private TMP_Text differenceText;
    [SerializeField] private TMP_Text afterRemoveFirst;
    [SerializeField] private TMP_Text afterRemoveSecond;
    [SerializeField] private TMP_Text sizeFirst;
    [SerializeField] private TMP_Text sizeSecond;


    DynamicTDASet firstDynamicSet;
    DynamicTDASet secondDynamicSet;

    private void Awake()
    {
        firstDynamicSet = new DynamicTDASet();
        secondDynamicSet = new DynamicTDASet();
    }

    private void Start()
    {
        for (int i = 0; i < firstArray.Length; i++)
        {
            firstDynamicSet.Add(firstArray[i]);
        }
        for (int i = 0; i < secondArray.Length; i++)
        {
            secondDynamicSet.Add(secondArray[i]);
        }

        CheckFunctions();

        firstDynamicComplete.text = $"First Complete: {firstDynamicSet.Show()}";
        secondDynamicComplete.text = $"Second Complete: {secondDynamicSet.Show()}";

        firstDynamicSet.Remove(4);
        secondDynamicSet.Remove(8);

        afterRemoveFirst.text = $"First After Remove: {firstDynamicSet.Show()}";
        afterRemoveSecond.text = $"Second After Remove: {secondDynamicSet.Show()}";

        sizeFirst.text = $"Size First:  {firstDynamicSet.Cardinality()}";
        sizeSecond.text = $"Size Second:  {secondDynamicSet.Cardinality()}";
    }

    private void CheckFunctions()
    {
        if (isUnion)
        {
            TDA unionSet = firstDynamicSet.Union(secondDynamicSet);
            unionText.text = $"Union Set: {unionSet.Show()}";
        }

        if (isIntersection)
        {
            TDA intersectionSet = firstDynamicSet.Intersection(secondDynamicSet);
            intersectionText.text = $"Intersection Set: {intersectionSet.Show()}";
        }

        if (isDifference)
        {
            TDA differenceSet = firstDynamicSet.Difference(secondDynamicSet);
            differenceText.text = $"Difference Set: {differenceSet.Show()}";
        }
    }
}
