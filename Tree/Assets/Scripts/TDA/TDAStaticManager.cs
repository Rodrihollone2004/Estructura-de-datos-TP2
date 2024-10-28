using TMPro;
using UnityEngine;

public class TDAStaticManager : MonoBehaviour
{
    [Header("Static Sets Numbers")]
    [SerializeField] private int[] firstArray = { 2, 1, 8, 0, 5, 9, 4, 3 };
    [SerializeField] private int[] secondArray = { 2, 1, 8, 0, 5, 9, 4, 3 };

    [Header("Set Max Sizes")]
    [SerializeField] private int firstMaxSize;
    [SerializeField] private int secondMaxSize;

    [Header("Activate functions")]
    [SerializeField] private bool isUnion;
    [SerializeField] private bool isIntersection;
    [SerializeField] private bool isDifference;

    [Header("Texts")]
    [SerializeField] private TMP_Text firstStaticComplete;
    [SerializeField] private TMP_Text secondStaticComplete;
    [SerializeField] private TMP_Text unionText;
    [SerializeField] private TMP_Text intersectionText;
    [SerializeField] private TMP_Text differenceText;
    [SerializeField] private TMP_Text afterRemoveFirst;
    [SerializeField] private TMP_Text afterRemoveSecond;
    [SerializeField] private TMP_Text sizeFirst;
    [SerializeField] private TMP_Text sizeSecond;

    StaticTDASet firstStaticSet;
    StaticTDASet secondStaticSet;

    private void Awake()
    {
        firstStaticSet = new StaticTDASet(firstMaxSize);
        secondStaticSet = new StaticTDASet(secondMaxSize);
    }

    void Start()
    {
        for (int i = 0; i < firstArray.Length; i++)
        {
            firstStaticSet.Add(firstArray[i]);
        }
        for (int i = 0; i < secondArray.Length; i++)
        {
            secondStaticSet.Add(secondArray[i]);
        }

        CheckFunctions();

        firstStaticComplete.text = $"First Complete: {firstStaticSet.Show()}";
        secondStaticComplete.text = $"Second Complete: {secondStaticSet.Show()}";

        firstStaticSet.Remove(2);
        secondStaticSet.Remove(9);

        afterRemoveFirst.text = $"First After Remove: {firstStaticSet.Show()}";
        afterRemoveSecond.text = $"Second After Remove: {secondStaticSet.Show()}";

        sizeFirst.text = $"Size First:  {firstStaticSet.Cardinality()}";
        sizeSecond.text = $"Size Second:  {secondStaticSet.Cardinality()}";
    }

    private void CheckFunctions()
    {
        if (isUnion)
        {
            TDA unionSet = firstStaticSet.Union(secondStaticSet);
            unionText.text = $"Union Set: {unionSet.Show()}";
        }
        if (isIntersection)
        {
            TDA intersectionSet = firstStaticSet.Intersection(secondStaticSet);
            intersectionText.text = $"Intersection Set: {intersectionSet.Show()}";
        }
        if (isDifference)
        {
            TDA differenceSet = firstStaticSet.Difference(secondStaticSet);
            differenceText.text = $"Difference Set: {differenceSet.Show()}";
        }
    }
}
